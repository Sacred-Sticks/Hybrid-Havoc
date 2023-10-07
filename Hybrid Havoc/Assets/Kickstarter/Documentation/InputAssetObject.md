# InputAssetObject Class Documentation

The `InputAssetObject` class is an abstract `ScriptableObject` used as a base class for creating input asset objects in the input management system. It provides methods for initializing, enabling/disabling, and handling input for specific input types. This class is part of the `Kickstarter.Inputs` namespace.

## Class: InputAssetObject

### Inherits from: ScriptableObject

### Methods

- `public abstract void Initialize(Gamepad[] gamepads)`: Initializes the input asset object by setting up the necessary input bindings and registering input actions. It takes an array of `Gamepad` objects as a parameter, representing the available gamepads.
- `protected abstract void AddBindings()`: Adds the necessary input bindings for the specific input asset object. This method must be implemented in derived classes.
- `public abstract void EnableInput()`: Enables input for the input asset object.
- `public abstract void DisableInput()`: Disables input for the input asset object.
- `public abstract void AddDevice(InputDevice device)`: Adds an input device to the input asset object. It takes an `InputDevice` object as a parameter.
- `public abstract void RemoveDevice(InputDevice device)`: Removes an input device from the input asset object. It takes an `InputDevice` object as a parameter.

The `InputAssetObject` class provides a common interface for handling input asset objects in a unified manner. It allows you to define and manage input behavior for specific input types by implementing the necessary methods in derived classes.

## Class: InputAssetObject<TType>

### Type Parameter: TType

The `InputAssetObject<TType>` class is an abstract generic class that derives from `InputAssetObject`. It provides additional functionality for handling input actions with a specific input value type (`TType`).

### Methods

The `InputAssetObject<TType>` class inherits all the methods from the base `InputAssetObject` class and provides additional methods for subscribing to input actions and managing input device changes.

- `public void SubscribeToInputAction(Action<TType> action, Player.PlayerIdentifier playerRegister)`: Subscribes to the input action with the specified `Action<TType>` callback for a specific player. It takes the input action callback and a `PlayerIdentifier` enum value as parameters.
- `public override void AddDevice(InputDevice device)`: Adds an input device to the input asset object and adjusts the input action mappings accordingly. It takes an `InputDevice` object as a parameter.
- `public override void RemoveDevice(InputDevice device)`: Removes an input device from the input asset object and adjusts the input action mappings accordingly. It takes an `InputDevice` object as a parameter.

The `InputAssetObject<TType>` class allows you to define and handle input actions with a specific input value type. It provides methods for subscribing to input actions for individual players and managing input device changes by dynamically adjusting the input action mappings.

### Derived Classes

The `InputAssetObject` class has several derived classes that provide specialized input handling for different input value types:

- `InputAssetObject<bool>`: Handles boolean input values.
- `InputAssetObject<int>`: Handles integer input values.
- `InputAssetObject<float>`: Handles floating-point input values.
- `InputAssetObject<Vector2>`: Handles 2D vector input values. Supports both standard and composite bindings.
- `InputAssetObject<Vector3>`: Handles 3D vector input values. Supports both standard and composite bindings.

The `InputAssetObject<Vector2>` and `InputAssetObject<Vector3>` classes allow for composite bindings, enabling you to map multiple input actions to a single input object. For example, you can use composite bindings to assign a variety of keys to a single Vector2 or Vector3 input, such as WASD on a Vector2 input.
