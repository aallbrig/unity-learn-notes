using UnityEngine;

public class UIManager : Singleton<UIManager>, IGameStateChange, IBattleLost
{
    public delegate void MainMenuFadeComplete(bool isFadeIn);
    public static event MainMenuFadeComplete OnMainMenuFadeComplete;

    [SerializeField] private GameObject dummyCamera;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    private bool _listenForPause;

    private void onMainMenuFadeStart(bool isFadeIn)
    {
        // if fading out, turn on UI camera
        if (!isFadeIn)
        {
            dummyCamera.gameObject.SetActive(false);
        }
        else
        {
            dummyCamera.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(true);
        }
    }

    private void onMainMenuFadeComplete(bool isFadeIn)
    {
        // Bubble event out to event broker
        OnMainMenuFadeComplete?.Invoke(isFadeIn);

        if (isFadeIn) dummyCamera.gameObject.SetActive(true);
        else mainMenu.gameObject.SetActive(false);
    }

    #region Monobehaviour Functions

    private void Start()
    {
        MainMenu.OnMainMenuFadeStart += onMainMenuFadeStart;
        MainMenu.OnMainMenuFadeComplete += onMainMenuFadeComplete;
        EventsBroker.Instance.SubscribeToGameStateChange(this);
        EventsBroker.Instance.SubscribeToBattleLost(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MainMenu.OnMainMenuFadeStart -= onMainMenuFadeStart;
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
        // Enable/disable pause menu based on if in certain game states
        _listenForPause = currentState == GameManager.GameState.Running || currentState == GameManager.GameState.Paused;
        
        // Main menu and pause menu are set to active based on
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.Paused);
    }

    public void NotifyBattleLost()
    {
        onMainMenuFadeStart(true);
    }
}
