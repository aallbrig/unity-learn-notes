public interface IBattleCommand
{
    event BattleCommandEvents.BattleCommandEvent OnBattleCommandStart;
    event BattleCommandEvents.BattleCommandEvent OnBattleCommandComplete;
    void Execute();
}
