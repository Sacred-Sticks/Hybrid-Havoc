using UnityEngine;
using UnityEngine.UIElements;

namespace Kickstarter.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MinMaxSliders : UIElement<MinMaxSlider>
    {
        [SerializeField] private Element<Vector2>[] elements;

        private void Awake()
        {
            InitializeElements(elements);
        }
    }
}
