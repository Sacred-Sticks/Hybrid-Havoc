using UnityEngine;

namespace Kickstarter.Categorization
{
    public class ObjectCategories : MonoBehaviour
    {
        [SerializeField] private CategoryType[] categories;

        public CategoryType[] Categories
        {
            get
            {
                return categories;
            }
        }
    }
}
