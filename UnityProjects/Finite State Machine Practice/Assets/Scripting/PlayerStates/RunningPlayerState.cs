using UnityEngine;

public class RunningPlayerState : BasePlayerState
{
    public override void Enter(PlayerController playerCtl)
    {
        playerCtl.TriggerAnimation("running");
    }

    public override void Update(PlayerController playerCtl)
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            playerCtl.TransitionToState(playerCtl.WalkingState);
        }
    }

    public override void OnCollisionEnter(PlayerController playerCtl)
    {
        throw new System.NotImplementedException();
    }
}
