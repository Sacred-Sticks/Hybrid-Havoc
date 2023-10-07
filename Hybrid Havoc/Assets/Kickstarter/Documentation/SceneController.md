# SceneController Class

The `SceneController` class is responsible for managing scene loading in Unity. It allows you to load scenes by name using event-driven architecture.

## Methods

### `Start()`

- **Description:** This method is called when the `SceneController` component is enabled. It sets up the scene indices dictionary by calling the `SetSceneIndices()` method.

### `OnEnable()`

- **Description:** This method is called when the `SceneController` component is enabled. It adds a listener for the `SceneChangeEvent` event using the event manager.

### `OnDisable()`

- **Description:** This method is called when the `SceneController` component is disabled. It removes the listener for the `SceneChangeEvent` event from the event manager.

### `SetSceneIndices()`

- **Description:** This method retrieves the scene indices from the build settings and populates the scene indices dictionary with the scene names and their corresponding indices.

### `GetSceneNameFromBuildIndex(int index)`

- **Parameters:**
    - `index` (int): The build index of the scene.
- **Returns:** The name of the scene corresponding to the provided build index.
- **Description:** This method retrieves the scene name from the build index using the `SceneUtility.GetScenePathByBuildIndex()` method.

### `LoadScene(SceneChangeEvent parameters)`

- **Parameters:**
    - `parameters` (SceneChangeEvent): The event parameters containing the scene name to load.
- **Description:** This method is the event handler for the `SceneChangeEvent` event. It retrieves the scene name from the event parameters and loads the scene using the scene index obtained from the scene indices dictionary.

## SceneChangeEvent Class

The `SceneChangeEvent` class represents the event parameters for the scene change event.

### Properties

#### `SceneName`

- **Type:** `string`
- **Description:** The name of the scene to load.

### Constructor

#### `SceneChangeEvent(string sceneName)`

- **Parameters:**
    - `sceneName` (string): The name of the scene to load.
- **Description:** This constructor initializes a new instance of the `SceneChangeEvent` class with the specified scene name.

