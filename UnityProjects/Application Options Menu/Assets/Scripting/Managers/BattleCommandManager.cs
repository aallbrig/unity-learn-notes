
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCommandManager: Singleton<BattleCommandManager>
{
    private bool _canExecuteCommand = true;
    private const float BattleCommandProcessInterval = 1.5f;
    private IEnumerator _battleCommandCoroutine;
    private readonly List<IBattleCommand> _battleCommandQueue = new List<IBattleCommand>();

    public void Add(IBattleCommand cmd) => _battleCommandQueue.Add(cmd);
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
                var cmd = GetNextBattleCommand();
                cmd.OnBattleCommandComplete += () => _canExecuteCommand = true;
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
