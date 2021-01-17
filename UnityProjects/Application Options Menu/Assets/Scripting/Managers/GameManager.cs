using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>, IMainMenuFadeComplete, IRandomBattleTriggered
{
    public delegate void GameStateChange(GameState prevState, GameState newState);
    public static event GameStateChange OnGameStateChange;

    [SerializeField] private List<GameObject> gameSystemsToLoad;

    private GameState _currentGameState;
    private string _currentLevelName;
    private readonly List<AsyncOperation> _loadOperations = new List<AsyncOperation>();


    public enum GameState
    {
        Pregame, Loading, Running, Paused, Battle
    }

    public void StartGame() => LoadLevel("Main", GameState.Running);
    public void RestartGame() => UpdateGameState(GameState.Pregame);
    public void QuitGame() => Application.Quit();
    public void TogglePauseGame() =>
        UpdateGameState(_currentGameState == GameState.Running ? GameState.Paused : GameState.Running);

    private void LoadLevel(string levelName, GameState desiredGameState)
    {
        var asyncOp = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (asyncOp == null)
        {
            Debug.LogError("[GameManager] Unable to load level: " + levelName);
            return;
        }
        
        _loadOperations.Add(asyncOp);
        asyncOp.completed += OnLoadOperationComplete;
        asyncOp.completed += (AsyncOperation op) =>
        {
            if (_loadOperations.Count == 0) UpdateGameState(desiredGameState);
        };
        _currentLevelName = levelName;
    }

    private void OnLoadOperationComplete(AsyncOperation asyncOp)
    {
        if (!_loadOperations.Contains(asyncOp)) return;

        _loadOperations.Remove(asyncOp);
    }

    private void UnloadLevel(string levelName)
    {
        var asyncOp = SceneManager.UnloadSceneAsync(levelName);
        if (asyncOp == null)
        {
            Debug.LogError("[GameManager] Unable to unload level: " + levelName);
            return;
        }
        _loadOperations.Add(asyncOp);
        asyncOp.completed += OnUnloadOperationComplete;
        _currentLevelName = string.Empty;
    }

    private void OnUnloadOperationComplete(AsyncOperation asyncOp)
    {
        if (!_loadOperations.Contains(asyncOp)) return;

        _loadOperations.Remove(asyncOp);
    }

    private void UpdateGameState(GameState newGameState)
    {
        var previousGameState = _currentGameState;
        _currentGameState = newGameState;

        switch (_currentGameState)
        {
            case GameState.Loading:
            case GameState.Pregame:
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.Running:
                Time.timeScale = 1;
                break;
            default:
                break;
        }
        
        OnGameStateChange?.Invoke(previousGameState, _currentGameState);
    }

    #region Monobehaviour Functions
    private void Start()
    {
        // Game manager should never be destroyed unless application ends
        DontDestroyOnLoad(gameObject);

        // Instantiate system prefabs
        gameSystemsToLoad.ForEach(go => Instantiate(go));

        // Register for events
        EventsBroker.Instance.SubscribeToMainMenuFadeComplete(this);
        EventsBroker.Instance.SubscribeToRandomBattleTriggered(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Delete instantiated system prefabs (triggers their cleanup behaviour)
        gameSystemsToLoad.ForEach(Destroy);
        gameSystemsToLoad.Clear();
    }
    #endregion

    #region Subscriptions

    public void Notify(bool isFadeIn)
    {
        if (!isFadeIn) return;
        
        // Unload the level if fading in, which means the main menu is done loading in
        if (_currentLevelName != null) UnloadLevel(_currentLevelName);
    }

    public void Notify()
    {
        // Random battle triggered
        UnloadLevel("Main");
        LoadLevel("Battle", GameState.Battle);
    }
    #endregion

}
