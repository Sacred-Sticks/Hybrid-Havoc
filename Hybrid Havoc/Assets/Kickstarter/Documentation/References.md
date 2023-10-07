# GenericReference<T> Class

The `GenericReference<T>` class is a generic reference implementation in Unity that allows you to reference and retrieve a value of a specific data type (`T`). It provides the flexibility to use a constant value or a variable of the specified data type.

## Properties

### `useConstant` (Serialized Field)

- **Type:** `bool`
- **Description:** Determines whether the reference uses a constant value or a variable.

### `constantValue` (Serialized Field)

- **Type:** `T`
- **Description:** The constant value when `useConstant` is `true`.

### `variable` (Serialized Field)

- **Type:** `TVariable`
- **Description:** The variable of type `T` when `useConstant` is `false`.

### `Value`

- **Type:** `T`
- **Description:** The getter and setter for the referenced value. If `useConstant` is `true`, it returns the constant value; otherwise, it returns the value of the associated variable.

### `Variable`

- **Type:** `TVariable`
- **Description:** The getter for the associated variable. It returns the variable of type `T` when `useConstant` is `false`.

## Methods

### `Value`

- **Type:** `T`
- **Description:** The getter and setter for the referenced value. If `useConstant` is `true`, it returns the constant value; otherwise, it returns the value of the associated variable.

### `Variable`

- **Type:** `TVariable`
- **Description:** The getter for the associated variable. It returns the variable of type `T` when `useConstant` is `false`.
