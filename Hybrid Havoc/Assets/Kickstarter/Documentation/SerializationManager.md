# SerializationManager Documentation

A base class for managing serialization of data in Unity.

## Namespace

- `Kickstarter.Progression`

## Inheritance

- `MonoBehaviour`

## Fields

- `private readonly List<Datapoint> allData`

  The list of all data points for serialization.

## Nested Classes

### `private class Datapoint`

Represents a single data point for serialization.

#### Properties

- `public string FileLocation { get; }`

  The file location where the data will be stored.

#### Constructors

- `public Datapoint(string fileLocation)`

  Creates a new instance of the `Datapoint` class with the specified file location.

### `private class Datapoint<TDataType> : Datapoint`

Represents a typed data point for serialization.

#### Properties

- `public TDataType Data`

  Gets the data by invoking the `SaveData` function.

- `public Action<TDataType> LoadData { get; }`

  The action to be invoked when loading the data.

#### Private Fields

- `private Func<TDataType> SaveData { get; }`

  The function to be invoked when saving the data.

#### Constructors

- `public Datapoint(string fileLocation, Action<TDataType> loadData, Func<TDataType> saveData) : base(fileLocation)`

  Creates a new instance of the `Datapoint<TDataType>` class with the specified file location, load action, and save function.

## Methods

### `protected void AddData<TDataType>(string fileLocation, Action<TDataType> loadData, Func<TDataType> saveData)`

Adds data of type `TDataType` for serialization.

#### Parameters

- `fileLocation` (string): The location of the file where the data will be stored.
- `loadData` (Action<TDataType>): The action to be invoked when loading the data.
- `saveData` (Func<TDataType>): The function to be invoked when saving the data.

### `public void RemoveData<TDataType>(string fileLocation)`

Removes data of type `TDataType` from serialization.

#### Parameters

- `fileLocation` (string): The location of the file where the data is stored.

### `protected void SaveAll()`

Saves all data points to their respective file locations.

Additional cases will need to be amended to the switch statement for any custom classes in need of serialization, plans are in progress to prevent this requirement.

### `protected void LoadAll()`

Loads all data points from their respective file locations.

Additional cases will need to be amended to the switch statement for any custom classes in need of serialization, plans are in progress to prevent this requirement.

### `private void SaveData<TDataType>(TDataType data, string fileName)`

Saves data of type `TDataType` to the specified file location.

#### Parameters

- `data` (TDataType): The data to be saved.
- `fileName` (string): The name of the file where the data will be stored.

### `private bool LoadData<TDataType>(string fileName, out TDataType output)`

Loads data of type `TDataType` from the specified file location.

#### Parameters

- `fileName` (string): The name of the file where the data is stored.
- `output` (out TDataType): The output parameter where the loaded data will be assigned.

### `private TDataType ConvertToDataType<TDataType>(string dataString)`

Converts a string to the specified data type.

Additional cases will need to be amended to the switch statement for any custom classes in need of serialization, plays are in progress to prevent this requirement.

#### Parameters

- `dataString` (string): The string representation of the data.
