using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts.StateMachines
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract  void Tick(float deltaTime);
        public abstract void Exit();

        protected float GetNormalizedTime(Animator animator, string tag)
        {
            // number 1 is index of second layer inside Animator
            // My big animators have more than 1 layer (number 0) so I need to check correct layer
            AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

            if(animator.IsInTransition(0) && nextInfo.IsTag(tag))
            {
                return nextInfo.normalizedTime;
            }
            else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag) )
            {
                return currentInfo.normalizedTime;
            }
            else
            {
                // error handler
                return 0f;
            }
        }
    }
}

