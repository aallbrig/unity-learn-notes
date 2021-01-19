public class BattleCommand : IBattleCommand
{
    public event BattleCommandEvents.BattleCommandEvent OnBattleCommandStart;
    public event BattleCommandEvents.BattleCommandEvent OnBattleCommandComplete;

    private readonly BattleCharacterStats _attacker;
    private readonly BattleCharacterStats _attackTarget;

    public BattleCommand(BattleCharacterStats attacker, BattleCharacterStats attackTarget)
    {
        _attacker = attacker;
        _attackTarget = attackTarget;
    }

    private void HandleAttackComplete()
    {
        OnBattleCommandComplete?.Invoke();
        _attacker.OnBattleCharacterAttackComplete -= HandleAttackComplete;
    }
    public void Execute()
    {
        OnBattleCommandStart?.Invoke();
        _attacker.OnBattleCharacterAttackComplete += HandleAttackComplete;
        _attacker.ExecuteAttack(_attackTarget);
    }
}
