using System.Collections;
using UnityEngine;

namespace Scripting.Controllers
{
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

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
    }
}