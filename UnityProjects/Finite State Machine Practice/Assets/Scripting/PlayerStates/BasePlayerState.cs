
public abstract class BasePlayerState
{
    public abstract void Enter(PlayerController playerCtl);
    public abstract void Update(PlayerController playerCtl);
    public abstract void OnCollisionEnter(PlayerController playerCtl);
}
