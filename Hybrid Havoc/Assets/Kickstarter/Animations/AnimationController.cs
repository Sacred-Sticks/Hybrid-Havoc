using System;
using UnityEngine;

namespace Kickstarter.Animations
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimationController : MonoBehaviour
    {
        private Animator animator;

        protected void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected void SetAnimation(AnimationSetData parameters)
        {
            animator.Play(parameters.AnimationState, parameters.Layer);
        }

        protected void TransitionAnimation(AnimationTransitionData parameters)
        {
            animator.CrossFade(parameters.AnimationState, parameters.Duration, parameters.Layer);
        }

        protected void ChangeParameter<TValueType>(AnimationParameterChangeData parameters, TValueType value)
        {
            switch (value)
            {
                case float data:
                    animator.SetFloat(parameters.ParameterName, data);
                    break;
                case int data:
                    animator.SetInteger(parameters.ParameterName, data);
                    break;
                case bool data:
                    animator.SetBool(parameters.ParameterName, data);
                    break;
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException();
            }
        }

        protected void ChangeParameter(AnimationParameterChangeData parameters)
        {
            animator.SetTrigger(parameters.ParameterName);
        }

        [Serializable]
        protected struct AnimationSetData
        {
            [SerializeField] private string animationState;
            [SerializeField] private int layer;
            
            public string AnimationState
            {
                get
                {
                    return animationState;
                }
            }
            public int Layer
            {
                get
                {
                    return layer;
                }
            }
        }

        [Serializable]
        protected struct AnimationTransitionData
        {
            [SerializeField] private string animationState;
            [SerializeField] private int layer;
            [SerializeField] private float duration;
            
            public string AnimationState
            {
                get
                {
                    return animationState;
                }
            }
            public int Layer
            {
                get
                {
                    return layer;
                }
            }
            public float Duration
            {
                get
                {
                    return duration;
                }
            }
        }

        [Serializable]
        protected struct AnimationParameterChangeData
        {
            [SerializeField] private string parameterName;
            
            public string ParameterName
            {
                get
                {
                    return parameterName;
                }
            }
        }
    }
}
