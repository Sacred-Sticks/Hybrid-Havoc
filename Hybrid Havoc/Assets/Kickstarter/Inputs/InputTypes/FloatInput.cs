using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Float Input", menuName = "Kickstarter/Inputs/Input Assets/Float")]
    public sealed class FloatInput : InputAssetObject<float>
    {
        [SerializeField] private AxisCompositeBinding[] compositeBindings;
        
        protected override void AddBindings()
        {
            foreach (string binding in bindings)
            {
                inputAction.AddBinding(binding);
            }
            foreach (var binding in compositeBindings)
            {
                inputAction.AddCompositeBinding("1DAxis")
                    .With(nameof(binding.Negative), binding.Negative)
                    .With(nameof(binding.Positive), binding.Positive);
            }
        }
    }
}
