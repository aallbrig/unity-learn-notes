using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Keep track of the current level
    private string _currentLevelName = string.Empty;
     
    // Keep track of game state
    // Generate other persistent systems
    public GameObject[] systemPrefabs;
    private readonly List<GameObject> _instancedSystemPrefabs = new List<GameObject>();
    private readonly List<AsyncOperation> _loadOperations = new List<AsyncOperation>();
    
    // Load level
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
    
    // Unload level
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

    private void OnLoadOperationComplete(AsyncOperation asyncOp)
    {
        if (_loadOperations.Contains(asyncOp)) _loadOperations.Remove(asyncOp);
        Debug.Log("Load Complete");
    }
    
    private void OnUnloadOperationComplete(AsyncOperation asyncOp)
    {
        if (_loadOperations.Contains(asyncOp)) _loadOperations.Remove(asyncOp);
        Debug.Log("Unload Complete");
    }

    private void InstantiateSystemPrefabs()
    {
        foreach (var sysPrefab in systemPrefabs)
        {
            _instancedSystemPrefabs.Add(Instantiate(sysPrefab));
        }
    }

    private void Start()
    {
        // This game object should never be destroyed
        DontDestroyOnLoad(gameObject);
        InstantiateSystemPrefabs();

        // Test that load level method works
        LoadLevel("Main");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _instancedSystemPrefabs.ForEach(Destroy);
        _instancedSystemPrefabs.Clear();
    }
}
