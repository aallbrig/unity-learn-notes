using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public abstract class BasePlayerState
    {
        public abstract void Enter(PlayerController playerController);
        public abstract void Tick(PlayerController playerController);
        public abstract void Leave(PlayerController playerController);
        public abstract void OnCollisionEnter(PlayerController playerController, Collision other);
    }
}
