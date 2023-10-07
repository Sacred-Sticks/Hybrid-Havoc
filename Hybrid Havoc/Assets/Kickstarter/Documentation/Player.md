# Player Documentation

The `Player` class is a MonoBehaviour component used for identifying and categorizing player entities in your game. It is part of the `Kickstarter.Identification` namespace.

## Class: Player

### Inherits from: MonoBehaviour

### Fields

- `private PlayerIdentifier playerID`: Specifies the identifier for the player. It is serialized for ease of configuration in the Unity Inspector.

### Properties

- `public PlayerIdentifier PlayerID`: Gets the identifier of the player. This allows other systems to query and identify individual players.

### Enum: PlayerIdentifier

The `PlayerIdentifier` enum represents the different player identifiers:

- `KeyboardAndMouse`: Represents a player using a keyboard and mouse input.
- `ControllerOne`: Represents the first controller input.
- `ControllerTwo`: Represents the second controller input.
- `ControllerThree`: Represents the third controller input.
- `ControllerFour`: Represents the fourth controller input.

The `Player` class provides a mechanism for identifying and categorizing players in your game. By assigning a unique identifier to each player entity, you can differentiate between different input sources or control methods for player input handling and other game-related functionality.

To use the `Player` class, follow these steps:

1. Attach the `Player` component to your player GameObjects.
2. In the Unity Inspector, set the `playerID` field to the appropriate identifier for each player.
3. Access the `PlayerID` property of the `Player` component to retrieve the identifier for each player entity.

By utilizing the `Player` class, you can easily manage and identify individual players in your game.
