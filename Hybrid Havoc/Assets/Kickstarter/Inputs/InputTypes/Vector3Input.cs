using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Vector3 Input", menuName = "Kickstarter/Inputs/Input Assets/Vector3")]
    public sealed class Vector3Input : InputAssetObject<Vector3>
    {
        [SerializeField] private ThreeAxisCompositeBinding[] compositeBindings;

        protected override void AddBindings()
        {
            foreach (string binding in bindings)
                inputAction.AddBinding(binding);
            foreach (var binding in compositeBindings)
            {
                inputAction.AddCompositeBinding("3DVector")
                    .With(nameof(binding.Up), binding.Up)
                    .With(nameof(binding.Down), binding.Down)
                    .With(nameof(binding.Left), binding.Left)
                    .With(nameof(binding.Right), binding.Right)
                    .With(nameof(binding.Forward), binding.Forward)
                    .With(nameof(binding.Backward), binding.Backward);
            }
        }

        [System.Serializable]
        private class ThreeAxisCompositeBinding
        {
            [SerializeField] private string name = "Composite Binding";
            [SerializeField] private string up;
            [SerializeField] private string down;
            [SerializeField] private string left;
            [SerializeField] private string right;
            [SerializeField] private string forward;
            [SerializeField] private string backward;

            public string Up
            {
                get
                {
                    return up;
                }
            }
            public string Down
            {
                get
                {
                    return down;
                }
            }
            public string Left
            {
                get
                {
                    return left;
                }
            }
            public string Right
            {
                get
                {
                    return right;
                }
            }
            public string Forward
            {
                get
                {
                    return forward;
                }
            }
            public string Backward
            {
                get
                {
                    return backward;
                }
            }

            public override string ToString()
            {
                return name;
            }
        }
    }
}
