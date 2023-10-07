using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Input Manager", menuName = "Kickstarter/Inputs/Input Manager")]
    public class InputManager : ScriptableObject
    {
        [Range(1, 4)]
        [SerializeField] private int maxPlayerCount = 1;
        [Space(20)]
        [SerializeField] private InputAssetObject[] inputObjects;

        public void Initialize()
        {
            InputSystem.onDeviceChange += OnInputDevicesChange;
            var gamepads = Gamepad.all.Take(maxPlayerCount).ToArray();
            foreach (var inputObject in inputObjects)
            {
                inputObject.Initialize(gamepads);
            }
            EnableAll();
        }

        private void OnInputDevicesChange(InputDevice device, InputDeviceChange changeType)
        {
            foreach (var inputObject in inputObjects)
            {
                switch (changeType)
                {
                    case InputDeviceChange.Added:
                        inputObject.AddDevice(device);
                        break;
                    case InputDeviceChange.Removed:
                        inputObject.RemoveDevice(device);
                        break;
                }
            }
        }

        public void EnableAll()
        {
            foreach (var inputObject in inputObjects)
            {
                inputObject.EnableInput();
            }
        }

        public void DisableAll()
        {
            foreach (var inputObject in inputObjects)
            {
                inputObject.DisableInput();
            }
        }
    }
}
