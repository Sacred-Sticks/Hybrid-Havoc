# PlayerDataController Script

The `PlayerDataController` script is responsible for controlling the data saving and loading functionality for the player in Unity. It provides different save types such as checkpoints, save stations, and quick saves.

## Table of Contents
- [Public Properties](#public-properties)
- [Public Methods](#public-methods)
- [Private Methods](#private-methods)
- [Events](#events)

## Public Properties<a name="public-properties"></a>

### `saveType` (Serialized Field)

- **Type:** `SaveType` (enum)
- **Description:** Specifies the type of save system to use. Can be one of the following values: `Checkpoints`, `SaveStations`, or `QuickSave`.

  - Checkpoints:
    - A Checkpoint Register class will be automatically added to the gameObject to search for any checkpoints and collect as needed.
  - Save Stations:
    - A Station Register class will be automatically added to the gameObject to listen for anything that might activate the save.
  - Quick Save:
    - A QuickSave Register will be automatically added to the gameObject to listen through the event manager for an event with TriggerQuickSaveEvent as the class with "[playerIdentifier].[quickSaveKeySpecifier]" for the key.

### `quickSaveKeySpecifier` (Serialized Field)

- **Type:** `string`
- **Description:** Specifies the key specifier for quick save data. Only applicable when `saveType` is set to `QuickSave`.

### `SaveTarget` (Property)

- **Type:** `Transform`
- **Description:** Gets or sets the target transform to save the player data. This is set by the checkpoint, save station, or quick save system when activated.

## Public Methods<a name="public-methods"></a>

### `SaveData()`

- **Description:** Saves the player data. Calls the `SaveAll()` method from the base class `SerializationManager` and triggers a save event.

### `LoadData()`

- **Description:** Loads the player data. Calls the `LoadAll()` method from the base class `SerializationManager`.

## Private Methods<a name="private-methods"></a>

### `Awake()`

- **Description:** Called when the script instance is being loaded. Initializes the `player` reference, assigns the file ID and save event key, and sets up the save type.

### `SetupSaveType()`

- **Description:** Sets up the save type based on the `saveType` value. Adds the appropriate save system component and subscribes to the save activated event.

### `SetupCheckpointRegister()`

- **Description:** Sets up the checkpoint register system. Adds the `CheckpointRegister` component and subscribes to the `CheckpointActivated` event.

### `SetupSaveStations()`

- **Description:** Sets up the save stations system. Adds the `StationRegister` component and subscribes to the `SaveStationActivated` event.

### `SetupQuickSave()`

- **Description:** Sets up the quick save system. Adds the `QuickSaveRegister` component, initializes it with the player and quick save key specifier, and subscribes to the `QuickSave` event.

### `Start()`

- **Description:** Called before the first frame update. Loads the player data.

## Events<a name="events"></a>

### `SaveEvent` (Nested Class)

- **Description:** Serves as event parameters within the Kickstarter Event System
