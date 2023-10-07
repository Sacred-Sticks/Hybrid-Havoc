using UnityEngine;
using UnityEngine.UIElements;

namespace Kickstarter.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class Dropdowns : UIElement<DropdownField>
    {
        [SerializeField] private Element<string>[] elements;

        private void Awake()
        {
            InitializeElements(elements);
        }
    }
}
