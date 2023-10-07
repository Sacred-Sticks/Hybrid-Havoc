using UnityEngine;
using UnityEngine.UIElements;

namespace Kickstarter.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class Radios : UIElement<RadioButton>
    {
        [SerializeField] private Element<bool>[] elements;

        private void Awake()
        {
            InitializeElements(elements);
        }
    }
}
