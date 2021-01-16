using UnityEngine;

public class UIManager : Singleton<UIManager>, IGameStateChange
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
            mainMenu.gameObject.SetActive(false);
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
    }

    #region Monobehaviour Functions

    private void Start()
    {
        MainMenu.OnMainMenuFadeStart += onMainMenuFadeStart;
        MainMenu.OnMainMenuFadeComplete += onMainMenuFadeComplete;
        EventsBroker.Instance.SubscribeToGameStateChange(this);
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
        _listenForPause = currentState == GameManager.GameState.Running || currentState == GameManager.GameState.Paused;
        pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.Paused);
    }
}
