using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    #region events
    public delegate void RewindBehaviour();
    public static event RewindBehaviour OnRewindStart;
    public static event RewindBehaviour OnRewindComplete;
    #endregion

    #region Singleton
    private static CommandManager _instance;
    public static CommandManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Command Manager instance is null");
            }
            return _instance;
        }
    }
    #endregion

    private List<ICommand> _commandBuffer = new List<ICommand>();

    public void Add(ICommand cmd)
    {
        _commandBuffer.Add(cmd);
    }

    public void AddAndExecute(ICommand cmd)
    {
        Add(cmd);
        cmd.Execute();
    }

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    private IEnumerator RewindRoutine()
    {
        OnRewindStart?.Invoke();
        foreach (var cmd in Enumerable.Reverse(_commandBuffer))
        {
            cmd.Undo();
            yield return new WaitForEndOfFrame();
        }
        OnRewindComplete?.Invoke();
    }

    private void Awake()
    {
        _instance = this;
    }
}
