public delegate void Command();
public interface ICommand
{
    event Command OnCommandStart;
    event Command OnCommandComplete;

    void Execute();
}
