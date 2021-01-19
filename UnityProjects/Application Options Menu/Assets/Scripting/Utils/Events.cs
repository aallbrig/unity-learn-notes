public static class BattleCommandEvents
{
    public delegate void BattleCommandEvent();

    private static event BattleCommandEvent OnBattleCommandStart;
    private static event BattleCommandEvent OnBattleCommandComplete;

}
