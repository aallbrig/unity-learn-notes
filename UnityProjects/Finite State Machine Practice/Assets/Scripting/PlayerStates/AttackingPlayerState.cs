﻿using System.Collections;
using Scripting.Controllers;
using UnityEngine;

namespace Scripting.PlayerStates
{
    public class AttackingPlayerState : BasePlayerState
    {
        private IEnumerator _attackCompleteDetection;

        public override void Enter(PlayerController playerController)
        {
            var attackTriggerAnimation = Random.Range(0f, 1f) > 0.5 ?
                PlayerAnimations.RangedAttacking : PlayerAnimations.MeleeAttacking;
            playerController.AnimationController.TriggerAnimation(attackTriggerAnimation);

            _attackCompleteDetection = DetectAttackComplete(playerController);
            playerController.StartCoroutine(_attackCompleteDetection);
        }

        public override void Tick(PlayerController playerController)
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0)
            {
                playerController.TransitionToState(playerController.IdleState);
            }
        }

        public override void Leave(PlayerController playerController)
        {
            playerController.StopCoroutine(_attackCompleteDetection);
        }

        public override void OnCollisionEnter(PlayerController playerController, Collision other) {}

        private IEnumerator DetectAttackComplete(PlayerController playerController)
        {
            while (playerController.AnimationController.AnimationPlaying)
            {
                yield return null;
            }

            playerController.TransitionToState(playerController.CombatIdleState);
        }
    }
}
