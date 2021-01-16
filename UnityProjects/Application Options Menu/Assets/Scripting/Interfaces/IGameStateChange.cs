public interface IGameStateChange
{
    void Notify(GameManager.GameState prevState, GameManager.GameState currentState);
}