
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCommandManager: Singleton<BattleCommandManager>
{
    public delegate void BattleCommand();
    public static BattleCommand OnBattleCommandStart;
    public static BattleCommand OnBattleCommandComplete;

    private bool _canExecuteCommand = true;
    private const float BattleCommandProcessInterval = 1.5f;
    private IEnumerator _battleCommandCoroutine;
    private readonly List<IBattleCommand> _battleCommandQueue = new List<IBattleCommand>();

    public void Add(IBattleCommand cmd) => _battleCommandQueue.Add(cmd);
    public void AddToFront(IBattleCommand cmd) => _battleCommandQueue.Insert(0, cmd);
    public void Remove(IBattleCommand cmd) => _battleCommandQueue.Remove(cmd);

    private IBattleCommand GetNextBattleCommand()
    {
        var cmd = _battleCommandQueue[0];
        Remove(cmd);
        return cmd;
    }

    private IEnumerator BattleCommandProcessor()
    {
        while (true)
        {
            if (_canExecuteCommand && _battleCommandQueue.Count > 0)
            {
                _canExecuteCommand = false;
                OnBattleCommandStart?.Invoke();
                var cmd = GetNextBattleCommand();
                cmd.OnBattleCommandComplete += () =>
                {
                    OnBattleCommandComplete?.Invoke();
                    _canExecuteCommand = true;
                };
                cmd.Execute();
            }
            yield return new WaitForSeconds(BattleCommandProcessInterval);
        }
    }

    private void Start()
    {
        _battleCommandCoroutine = BattleCommandProcessor();
        StartCoroutine(_battleCommandCoroutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(_battleCommandCoroutine);
    }
}
