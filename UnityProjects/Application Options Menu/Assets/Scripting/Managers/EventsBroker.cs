﻿using System.Collections.Generic;

public class EventsBroker : Singleton<EventsBroker>
{
    private readonly List<IGameStateChange> _gameStateChangeSubscribers = new List<IGameStateChange>();
    private readonly List<IMainMenuFadeComplete> _mainMenuFadeCompleteSubscribers = new List<IMainMenuFadeComplete>();
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
    

    #region Monobehaviour Functions

    private void Start()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
        UIManager.OnMainMenuFadeComplete += OnMainMenuFadeComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        GameManager.OnGameStateChange -= OnGameStateChange;
        UIManager.OnMainMenuFadeComplete -= OnMainMenuFadeComplete;
    }

    #endregion
}