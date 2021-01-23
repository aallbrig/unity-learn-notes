using System.Collections;
using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public class TakingDamagePlayerState : BasePlayerState
    {
        private IEnumerator _detectTakingDamageComplete;
        
        public override void Enter(PlayerController playerController)
        {
            playerController.AnimationController.TriggerAnimation("taking damage");

            _detectTakingDamageComplete = DetectTakingDamageComplete(playerController);
            playerController.StartCoroutine(_detectTakingDamageComplete);
        }

        public override void Tick(PlayerController playerController)
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0)
            {
                playerController.TransitionToState(playerController.IdleState);
            }
        }

        public override void Leave(PlayerController playerController)
        {
            playerController.StopCoroutine(_detectTakingDamageComplete);
        }

        public override void OnCollisionEnter(PlayerController playerController, Collision other) {}
        
        private IEnumerator DetectTakingDamageComplete(PlayerController playerController)
        {
            while (playerController.AnimationController.AnimationPlaying)
            {
                yield return null;
            }

            playerController.TransitionToState(playerController.CombatIdleState);
        }
    }
}
