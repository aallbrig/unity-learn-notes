using UnityEngine;

namespace Scripting.PlayerStates
{
    public abstract class BasePlayerState
    {
        public abstract void Enter(PlayerController playerCtl);
        public abstract void Tick(PlayerController playerCtl);
        public abstract void OnCollisionEnter(PlayerController playerCtl, Collision other);
    }
}
