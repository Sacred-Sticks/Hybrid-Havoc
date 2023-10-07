using System;
using System.Linq;
using Kickstarter.Categorization;
using UnityEngine;

namespace Kickstarter.Progression.Types
{
    public class CheckpointRegister : MonoBehaviour
    {
        [SerializeField] private CategoryType checkpointType;

        public Action<Transform> CheckpointActivated;
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out ObjectCategories categories))
                return;
            if (!categories.Categories.Contains(checkpointType))
                return;
            CheckpointActivated?.Invoke(other.gameObject.transform);
        }
    }
}
