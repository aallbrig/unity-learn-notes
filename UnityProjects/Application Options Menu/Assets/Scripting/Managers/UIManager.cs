using UnityEngine;

public class UIManager : Singleton<UIManager>, IGameStateChange
{
    public delegate void MainMenuFadeComplete(bool isFadeIn);
    public static event MainMenuFadeComplete OnMainMenuFadeComplete;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    private bool _listenForPause;

    private void onMainMenuFadeComplete(bool isFadeIn)
    {
        OnMainMenuFadeComplete?.Invoke(isFadeIn);
    }

    #region Monobehaviour Functions

    private void Start()
    {
        MainMenu.OnMainMenuFadeComplete += onMainMenuFadeComplete;
        EventsBroker.Instance.SubscribeToGameStateChange(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MainMenu.OnMainMenuFadeComplete -= onMainMenuFadeComplete;
        EventsBroker.Instance.UnsubscribeToGameStateChange(this);
    }

    private void Update()
    {
        if (!_listenForPause) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.TogglePauseGame();
        }
    }

    #endregion

    public void Notify(GameManager.GameState prevState, GameManager.GameState currentState)
    {
        _listenForPause = currentState == GameManager.GameState.Running;
    }
}
