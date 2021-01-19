public class AttackBattleCommand : IBattleCommand
{
    public event BattleCommandEvents.BattleCommandEvent OnBattleCommandStart;
    public event BattleCommandEvents.BattleCommandEvent OnBattleCommandComplete;

    private readonly BattleCharacterStats _attacker;
    private readonly BattleCharacterStats _attackTarget;

    public AttackBattleCommand(BattleCharacterStats attacker, BattleCharacterStats attackTarget)
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
        if (_attacker.characterDefinition.CurrentHealth > 0 && _attackTarget.characterDefinition.CurrentHealth > 0)
        {
            _attacker.OnBattleCharacterAttackComplete += HandleAttackComplete;
            _attacker.ExecuteAttack(_attackTarget);
        }
        else
        {
            HandleAttackComplete();
        }
    }
}
