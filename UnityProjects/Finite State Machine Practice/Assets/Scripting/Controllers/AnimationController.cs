using System.Collections;
using UnityEngine;

namespace Scripting.Controllers
{
    public static class PlayerAnimations
    {
        public static string DeathA => "death";
        public static string DeathB => "death b";
        public static string Jump => "jump";
        public static string Idle => "idle";
        public static string Walking => "walking";
        public static string Running => "running";
        public static string MeleeAttacking => "melee attack";
        public static string RangedAttacking => "attacking";
        public static string TakingDamage => "taking damage";
        public static string CombatIdle => "combat idle";
        public static string Charging => "charging";

    }

    public class AnimationController : MonoBehaviour
    {
        // Animation clips whose normalized time variable is greater than 1.0 has played through the clip once
        public bool AnimationPlaying => _animator.GetCurrentAnimatorStateInfo(_baseLayerIndex).normalizedTime < 1.0;

        private int _baseLayerIndex = 0;
        private Animator _animator;
        private string _currentTrigger;
        private IEnumerator _animationClipOver;

        public void TriggerAnimation(string triggerName)
        {
            if (_currentTrigger != null)
            {
                if (_animator.GetBool(_currentTrigger)) _animator.SetBool(_currentTrigger, false);
                else _animator.ResetTrigger(_currentTrigger);
            }

            if (_animator.GetBool(triggerName)) _animator.SetBool(triggerName, true);
            else _animator.SetTrigger(triggerName);
            _currentTrigger = triggerName;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}