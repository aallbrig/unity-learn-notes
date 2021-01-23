using System.Collections;
using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public class CombatIdlePlayerState : BasePlayerState
    {
        private const float CombatIdleWaitTime = 10;
        private IEnumerator _combatIdleDuration;
        public override void Enter(PlayerController playerController)
        {
            playerController.AnimationController.TriggerAnimation("combat idle");

            _combatIdleDuration = CombatIdleDuration(playerController);
            playerController.StartCoroutine(_combatIdleDuration);
        }

        public override void Tick(PlayerController playerController)
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerController.TransitionToState(playerController.JumpingState);
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                playerController.TransitionToState(playerController.ChargingState);
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            {
                playerController.TransitionToState(playerController.DeadState);
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0)
            {
                playerController.TransitionToState(playerController.AttackingState);
            }
        
            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0)
            {
                playerController.TransitionToState(playerController.TakingDamageState);
            }
        }

        public override void Leave(PlayerController playerController)
        {
            playerController.StopCoroutine(_combatIdleDuration);
        }

        public override void OnCollisionEnter(PlayerController playerController, Collision other) {}

        private IEnumerator CombatIdleDuration(PlayerController playerController)
        {
            yield return new WaitForSeconds(CombatIdleWaitTime);
            
            playerController.TransitionToState(playerController.IdleState);
        }
    }
}
