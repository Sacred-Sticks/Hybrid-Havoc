using UnityEngine;
using Kickstarter.Variables;
using UnityEngine.Serialization;

namespace Kickstarter.Groups
{
    [CreateAssetMenu(fileName = "1D Float", menuName = "Kickstarter/Variables/Groups/Directional Float")]
    public class DirectionalFloat : ScriptableObject
    {
        public FloatVariable Up;
        public FloatVariable Down;

        public float Y
        {
            get
            {
                return Up.Value - Down.Value;
            }
        }
    }
}
