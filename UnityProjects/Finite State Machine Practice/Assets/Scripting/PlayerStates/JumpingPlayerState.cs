public class JumpingPlayerState : BasePlayerState
{
    public override void Enter(PlayerController playerCtl)
    {
        playerCtl.TriggerAnimation("jump");
    }

    public override void Update(PlayerController playerCtl) {}

    public override void OnCollisionEnter(PlayerController playerCtl)
    {
        playerCtl.TransitionToState(playerCtl.IdleState);
    }
}
