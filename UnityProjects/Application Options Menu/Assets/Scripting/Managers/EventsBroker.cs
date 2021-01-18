using System.Collections.Generic;

public class EventsBroker : Singleton<EventsBroker>
{
    private readonly List<IGameStateChange> _gameStateChangeSubscribers = new List<IGameStateChange>();
    private readonly List<IMainMenuFadeComplete> _mainMenuFadeCompleteSubscribers = new List<IMainMenuFadeComplete>();
    private readonly List<IRandomBattleTriggered> _randomBattleTriggeredSubscribers = new List<IRandomBattleTriggered>();
    // Pub/sub broker
    // List of subjects (that implement INotify)
    // Subjects can register themselves
    // Subjects can unregister themselves
    // Publishers can notify broker of

    public void SubscribeToGameStateChange(IGameStateChange subscriber) =>
        _gameStateChangeSubscribers.Add(subscriber);

    public void UnsubscribeToGameStateChange(IGameStateChange subscriber) =>
        _gameStateChangeSubscribers.Remove(subscriber);
    private void OnGameStateChange(GameManager.GameState prevState, GameManager.GameState newState) =>
        _gameStateChangeSubscribers.ForEach(sub => sub.Notify(prevState, newState));

    public void SubscribeToMainMenuFadeComplete(IMainMenuFadeComplete subscriber) =>
        _mainMenuFadeCompleteSubscribers.Add(subscriber);
    public void UnsubscribeToMainMenuFadeComplete(IMainMenuFadeComplete subscriber) =>
        _mainMenuFadeCompleteSubscribers.Remove(subscriber);
    private void OnMainMenuFadeComplete(bool isFadeIn) =>
        _mainMenuFadeCompleteSubscribers.ForEach(sub => sub.Notify(isFadeIn));

    public void SubscribeToRandomBattleTriggered(IRandomBattleTriggered subscriber) =>
        _randomBattleTriggeredSubscribers.Add(subscriber);
    public void UnsubscribeToRandomBattleTriggered(IRandomBattleTriggered subscriber) =>
        _randomBattleTriggeredSubscribers.Remove(subscriber);
    private void OnRandomBattleTriggered() =>
        _randomBattleTriggeredSubscribers.ForEach(sub => sub.Notify());

    #region Monobehaviour Functions

    private void Start()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
        UIManager.OnMainMenuFadeComplete += OnMainMenuFadeComplete;
        RandomBattleGenerator.OnRandomBattleTriggered += OnRandomBattleTriggered;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        GameManager.OnGameStateChange -= OnGameStateChange;
        UIManager.OnMainMenuFadeComplete -= OnMainMenuFadeComplete;
        RandomBattleGenerator.OnRandomBattleTriggered -= OnRandomBattleTriggered;
    }

    #endregion
}