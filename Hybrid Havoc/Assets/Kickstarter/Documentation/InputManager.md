# InputManager Documentation

The `InputManager` class is a `ScriptableObject` used for managing input devices and initializing input objects in your game. It is part of the `Kickstarter.Inputs` namespace.

## Class: InputManager

### Inherits from: ScriptableObject

### Fields

- `private int maxPlayerCount`: Specifies the maximum number of players supported by the input manager. The value is clamped between 1 and 4.
- `private InputAssetObject[] inputObjects`: An array of `InputAssetObject` instances representing the input objects managed by the input manager.

### Methods

- `public void Initialize()`: Initializes the input manager by subscribing to the `InputSystem.onDeviceChange` event, initializing the input objects with the available gamepads, and enabling all input objects.
- `private void OnInputDevicesChange(InputDevice device, InputDeviceChange changeType)`: A callback method triggered when input devices are added or removed. It updates the input objects accordingly.
- `public void EnableAll()`: Enables all input objects managed by the input manager.
- `public void DisableAll()`: Disables all input objects managed by the input manager.

The `InputManager` class provides a centralized way to manage input devices and input objects in your game. By using this class, you can easily initialize and enable input objects, as well as handle changes in input device availability.

To use the `InputManager`, follow these steps:

1. Create an instance of the `InputManager` as a `ScriptableObject` asset in your project.
2. Set the `maxPlayerCount` field to specify the maximum number of players supported by the input manager.
3. Create and configure the necessary `InputAssetObject` instances and assign them to the `inputObjects` array field.
4. Call the `Initialize()` method to initialize the input manager and enable input objects.
5. Optionally, use the `EnableAll()` and `DisableAll()` methods to manually control the input objects' enable/disable state.

By utilizing the `InputManager` class, you can streamline the management of input devices and input objects in your game.
