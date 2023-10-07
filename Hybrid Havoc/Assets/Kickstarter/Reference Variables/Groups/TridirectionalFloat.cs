using UnityEngine;
using Kickstarter.Variables;

namespace Kickstarter.Groups
{
    [CreateAssetMenu(fileName = "3D Float", menuName = "Kickstarter/Variables/Groups/Tridirectional Float")]
    public class TridirectionalFloat : BidirectionalFloat
    {
        public FloatVariable Forward;
        public FloatVariable Backward;

        public float Z
        {
            get
            {
                return Forward.Value - Backward.Value;
            }
        }
    }
}
