using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public Events.EventGameState OnGameStateChanged;
    // Keep track of game state
    public enum GameState
    {
        Pregame,
        Running,
        Paused
    }

    private GameState _gameState = GameState.Pregame;

    public GameState CurrentGameState => _gameState;

    // Keep track of the current level
    private string _currentLevelName = string.Empty;
     
    // Generate other persistent systems
    public GameObject[] systemPrefabs;
    private readonly List<GameObject> _instancedSystemPrefabs = new List<GameObject>();
    private readonly List<AsyncOperation> _loadOperations = new List<AsyncOperation>();

    public void StartGame() => LoadLevel("Main");

    public void RestartGame()
    {
        UpdateState(GameState.Pregame);
    }

    public void QuitGame() => Application.Quit();

    public void TogglePauseGame() =>
        UpdateState(_gameState == GameState.Running ? GameState.Paused : GameState.Running);

    public void LoadLevel(string levelName)
    {
        var asyncOp = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (asyncOp == null)
        {
           Debug.LogError("[GameManager] Unable to load level " + levelName);
           return;
        }
        
        _loadOperations.Add(asyncOp);

        asyncOp.completed += OnLoadOperationComplete;
        _currentLevelName = levelName;
    }
    
    public void UnloadLevel(string levelName)
    {
        var asyncOp = SceneManager.UnloadSceneAsync(levelName);
        if (asyncOp == null)
        {
           Debug.LogError("[GameManager] Unable to unload level " + levelName);
           return;
        }

        _loadOperations.Add(asyncOp);

        asyncOp.completed += OnUnloadOperationComplete;
        _currentLevelName = string.Empty;
    }

    private void UpdateState(GameState state)
    {
        var previousGameState = _gameState;
        _gameState = state;

        switch (_gameState)
        {
            case GameState.Pregame:
                Time.timeScale = 1.0f;
                break;
            case GameState.Running:
                Time.timeScale = 1.0f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            default:
                break;
        }
        
        OnGameStateChanged?.Invoke(previousGameState, _gameState);
    }

    private void OnLoadOperationComplete(AsyncOperation asyncOp)
    {
        Debug.Log("Load Complete");
        if (!_loadOperations.Contains(asyncOp)) return;

        _loadOperations.Remove(asyncOp);
        if (_loadOperations.Count == 0) UpdateState(GameState.Running);
    }
    
    private void OnUnloadOperationComplete(AsyncOperation asyncOp)
    {
        Debug.Log("Unload Complete");
        if (_loadOperations.Contains(asyncOp)) _loadOperations.Remove(asyncOp);
    }

    private void InstantiateSystemPrefabs()
    {
        foreach (var sysPrefab in systemPrefabs)
        {
            _instancedSystemPrefabs.Add(Instantiate(sysPrefab));
        }
    }

    private void HandleMainMenuFadeOut(bool isFadeOut)
    {
        if (!isFadeOut)
        {
            UnloadLevel(_currentLevelName);
        }
    }

    private void Start()
    {
        // This game object should never be destroyed
        DontDestroyOnLoad(gameObject);
        InstantiateSystemPrefabs();
        
        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeOut);
    }

    private void Update()
    {
        if (_gameState == GameState.Pregame) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _instancedSystemPrefabs.ForEach(Destroy);
        _instancedSystemPrefabs.Clear();
    }
}
