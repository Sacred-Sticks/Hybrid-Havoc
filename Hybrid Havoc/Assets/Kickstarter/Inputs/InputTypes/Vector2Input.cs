using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Vector2 Input", menuName = "Kickstarter/Inputs/Input Assets/Vector2")]
    public sealed class Vector2Input : InputAssetObject<Vector2>
    {
        [SerializeField] private TwoAxisCompositeBinding[] compositeBindings;

        protected override void AddBindings()
        {
            foreach (string binding in bindings)
                inputAction.AddBinding(binding);
            foreach (var binding in compositeBindings)
            {
                inputAction.AddCompositeBinding("2DVector")
                    .With(nameof(binding.Up), binding.Up)
                    .With(nameof(binding.Down), binding.Down)
                    .With(nameof(binding.Left), binding.Left)
                    .With(nameof(binding.Right), binding.Right);
            }
        }

        [System.Serializable]
        private class TwoAxisCompositeBinding
        {
            [SerializeField] private string name = "Composite Binding";
            [SerializeField] private string up;
            [SerializeField] private string down;
            [SerializeField] private string left;
            [SerializeField] private string right;

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

            public override string ToString()
            {
                return name;
            }
        }
    }
}
