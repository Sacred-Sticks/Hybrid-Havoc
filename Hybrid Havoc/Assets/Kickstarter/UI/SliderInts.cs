using UnityEngine;
using UnityEngine.UIElements;

namespace Kickstarter.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class SliderInts : UIElement<SliderInt>
    {
        [SerializeField] private Element<int>[] elements;

        private void Awake()
        {
            InitializeElements(elements);
        }
    }
}
