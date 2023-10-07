using UnityEngine;
using UnityEngine.UIElements;

namespace Kickstarter.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class TextFields : UIElement<TextField>
    {
        [SerializeField] private Element<string>[] elements;

        private void Awake()
        {
            InitializeElements(elements);
        }
    }
}
