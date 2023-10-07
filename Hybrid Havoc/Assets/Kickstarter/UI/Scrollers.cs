using UnityEngine;
using UnityEngine.UIElements;

namespace Kickstarter.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class Scrollers : UIElement<Scroller>
    {
        [SerializeField] private Element<float>[] elements;

        private void Awake()
        {
            InitializeElements(elements);
        }
    }
}
