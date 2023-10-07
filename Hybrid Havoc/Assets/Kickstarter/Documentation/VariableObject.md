# GenericVariable<TDataType> Class

The `GenericVariable<TDataType>` class is a generic variable implementation in Unity that allows you to define and manipulate variables of any type. It provides functionality for value change events and an option to reset the value to an initial state. These variables can be useful when there is a single data element used across a variety of classes, such as an int-variable for health points. One class could manage the health itself, another might be in charge of the UI elements for the health.

## Properties

### `value` (Serialized Field)

- **Type:** `TDataType`
- **Description:** The current value of the variable.

### `resetValue` (Serialized Field)

- **Type:** `bool`
- **Description:** Determines whether the value should be reset to an initial state.

### `initialValue` (Serialized Field)

- **Type:** `TDataType`
- **Description:** The initial value of the variable, used when `resetValue` is `true`.

### `ValueChanged` (Event)

- **Type:** `ValueDelegate`
- **Description:** An action that is triggered whenever the value of the variable changes.

## Methods

### `Value`

- **Type:** `TDataType`
- **Description:** The getter and setter for the `value` property. When the value is set, it triggers the `ValueChanged` event.

### `OnEnable()`

- **Description:** Called when the script instance is being enabled. If `resetValue` is `true`, the value is set to the initial value.

## Subclasses

### `BoolVariable`

- **Type:** `GenericVariable<bool>`

### `IntVariable`

- **Type:** `GenericVariable<int>`

### `FloatVariable`

- **Type:** `GenericVariable<float>`

### `Vector2Variable`

- **Type:** `GenericVariable<Vector2>`

### `Vector3Variable`

- **Type:** `GenericVariable<Vector3>`

Each subclass extends the `GenericVariable<TDataType>` class and provides specific implementations for the corresponding type: `bool`, `int`, `float`, `Vector2`, and `Vector3`. These subclasses inherit the properties and methods from the `GenericVariable<TDataType>` class, allowing you to create and manage variables of those specific types.
