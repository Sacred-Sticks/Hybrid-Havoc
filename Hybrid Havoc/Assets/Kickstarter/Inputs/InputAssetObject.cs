using System;
using System.Collections.Generic;
using System.Linq;
using Kickstarter.Identification;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    public abstract class InputAssetObject : ScriptableObject
    {
        public abstract void Initialize(Gamepad[] gamepads);
        protected abstract void AddBindings();
        public abstract void EnableInput();
        public abstract void DisableInput();
        public abstract void AddDevice(InputDevice device);
        public abstract void RemoveDevice(InputDevice device);
    }

    public abstract class InputAssetObject<TType> : InputAssetObject where TType : struct
    {
        [SerializeField] protected string[] bindings;
        
        protected readonly Dictionary<InputDevice, Action<TType>> actionMap = new Dictionary<InputDevice, Action<TType>>();
        private InputDevice[] devices;
        protected InputAction inputAction;

        private bool actionsRegistered;

        private void OnEnable()
        {
            actionsRegistered = false;
        }

        public override void Initialize(Gamepad[] gamepads)
        {
            devices = new InputDevice[gamepads.Length + 1];
            inputAction = new InputAction(name: name, type: InputActionType.PassThrough);
            AddBindings();
            AddRegistration();
            StoreDevices(gamepads);
            CreateActionMappings(devices);
        }

        private void AddRegistration()
        {
            inputAction.performed += RegisterInput;
            inputAction.canceled += RegisterInput;
        }

        private void RegisterInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<TType>();
            var device = context.control.device;
            if (device == Mouse.current)
                device = Keyboard.current;
            if (actionMap.ContainsKey(device))
                actionMap[device]?.Invoke(value);
        }

        private void StoreDevices(IReadOnlyList<InputDevice> inputDevices)
        {
            devices[0] = Keyboard.current;
            for (int i = 1; i < devices.Length; i++)
            {
                devices[i] = inputDevices[i - 1];
            }
        }

        private void CreateActionMappings(InputDevice[] inputDevices)
        {
            foreach (var inputDevice in inputDevices)
            {
                actionMap.Add(inputDevice, null);
            }
            actionsRegistered = true;
        }

        private void AdjustActionMappings(InputDevice[] inputDevices, InputDevice device)
        {
            InputDevice deviceToOverride = null;
            foreach (var key in actionMap.Keys.Where(key => Array.IndexOf(inputDevices, key) == -1))
            {
                deviceToOverride = key;
                break;
            }
            if (deviceToOverride == null)
                return;
            var action = actionMap[deviceToOverride];
            actionMap.Remove(deviceToOverride);
            actionMap.Add(device, action);
        }

        public void SubscribeToInputAction(Action<TType> action, Player.PlayerIdentifier playerRegister)
        {
            if (!actionsRegistered)
                return;
            int playerIndex = playerRegister switch
            {
                Player.PlayerIdentifier.KeyboardAndMouse => 0,
                Player.PlayerIdentifier.ControllerOne => 1,
                Player.PlayerIdentifier.ControllerTwo => 2,
                Player.PlayerIdentifier.ControllerThree => 3,
                Player.PlayerIdentifier.ControllerFour => 4,
                _ => throw new ArgumentOutOfRangeException(nameof(playerRegister), playerRegister, null),
            };
            actionMap[devices[playerIndex]] += action;
        }
        
        public void UnsubscribeToInputAction(Action<TType> action, Player.PlayerIdentifier playerRegister)
        {
            if (!actionsRegistered)
                return;
            int playerIndex = playerRegister switch
            {
                Player.PlayerIdentifier.KeyboardAndMouse => 0,
                Player.PlayerIdentifier.ControllerOne => 1,
                Player.PlayerIdentifier.ControllerTwo => 2,
                Player.PlayerIdentifier.ControllerThree => 3,
                Player.PlayerIdentifier.ControllerFour => 4,
                _ => throw new ArgumentOutOfRangeException(nameof(playerRegister), playerRegister, null),
            };
            actionMap[devices[playerIndex]] -= action;
        }

        public override void AddDevice(InputDevice device)
        {
            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i] != null)
                    continue;
                devices[i] = device;
                AdjustActionMappings(devices, device);
                break;
            }
        }

        public override void RemoveDevice(InputDevice device)
        {
            var eligibleDeviceReplacements = new List<InputDevice>();
            if (Keyboard.current != null)
                eligibleDeviceReplacements.Add(Keyboard.current);
            eligibleDeviceReplacements.AddRange(Gamepad.all);
            for (int i = 0; i < devices.Length; i++)
            {
                if (eligibleDeviceReplacements.Contains(devices[i]))
                    eligibleDeviceReplacements.Remove(devices[i]);
                if (devices[i] != device)
                    continue;
                var newDevice = eligibleDeviceReplacements.FirstOrDefault();
                devices[i] = newDevice;
                AdjustActionMappings(devices, newDevice);
                break;
            }
        }

        public override void EnableInput()
        {
            inputAction.Enable();
        }

        public override void DisableInput()
        {
            inputAction.Disable();
        }

        [Serializable]
        protected class AxisCompositeBinding
        {
            [SerializeField] private string name = "Composite Binding";
            [SerializeField] private string negative;
            [SerializeField] private string positive;

            public string Negative
            {
                get
                {
                    return negative;
                }
            }
            public string Positive
            {
                get
                {
                    return positive;
                }
            }

            public override string ToString()
            {
                return name;
            }
        }
    }
}


