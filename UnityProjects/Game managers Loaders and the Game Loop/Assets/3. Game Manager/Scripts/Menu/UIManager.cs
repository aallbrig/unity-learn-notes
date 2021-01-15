using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Events.EventFadeComplete OnMainMenuFadeComplete;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private Camera _dummyCamera;

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

    private void HandleGameStateChange(GameManager.GameState prevState, GameManager.GameState currentState) {
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.Paused);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Pregame) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
        _mainMenu.onMainMenuFadeComplete.AddListener(OnMainMenuFadeComplete.Invoke);
    }
}
