using UnityEngine;
using Kickstarter.Variables;
using UnityEngine.Serialization;

namespace Kickstarter.Groups
{
    [CreateAssetMenu(fileName = "2D Float", menuName = "Kickstarter/Variables/Groups/Bidirectional Float")]
    public class BidirectionalFloat : DirectionalFloat
    {
        public FloatVariable Right;
        public FloatVariable Left;

        public float X
        {
            get
            {
                return Right.Value - Left.Value;
            }
        }
    }
}
