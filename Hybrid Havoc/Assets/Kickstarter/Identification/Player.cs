using UnityEngine;

namespace Kickstarter.Identification
{
    public class Player : MonoBehaviour
    {
        public enum PlayerIdentifier
        {
            KeyboardAndMouse,
            ControllerOne,
            ControllerTwo,
            ControllerThree,
            ControllerFour,
        }

        [SerializeField] private PlayerIdentifier playerID;

        public PlayerIdentifier PlayerID
        {
            get
            {
                return playerID;
            }
            set
            {
                playerID = value;
            }
        }
    }
}