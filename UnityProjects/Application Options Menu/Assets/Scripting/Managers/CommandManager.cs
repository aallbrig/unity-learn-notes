using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : Singleton<CommandManager>
{
    public bool IsRunning { get; private set; }
    private bool isExecuting;
    [SerializeField] private readonly List<ICommand> _commandQueue = new List<ICommand>();
    private readonly float _waitTime = 0.25f;
    private IEnumerator _coroutine;

    public void Add(ICommand cmd)
    {
        _commandQueue.Add(cmd);

        if (!IsRunning)
        {
            IsRunning = true;
            _coroutine = SequentialExecutionOfCommands();
            StartCoroutine(_coroutine);
        }
    }

    public void Remove(ICommand cmd) => _commandQueue.Remove(cmd);

    public void Clear()
    {
        _commandQueue.Clear();
        
        if (_coroutine != null) StopCoroutine(_coroutine);
        if (IsRunning) IsRunning = false;
        if (isExecuting) isExecuting = false;
    }

    private IEnumerator SequentialExecutionOfCommands()
    {
        while (_commandQueue.Count > 0)
        {
            if (!isExecuting)
            {
                isExecuting = true;
                var cmd = _commandQueue[0];
                cmd.OnCommandComplete += () =>
                {
                    Remove(cmd);
                    isExecuting = false;
                };
                cmd.Execute();
            }

            yield return new WaitForSeconds(_waitTime);
        }

        IsRunning = false;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
