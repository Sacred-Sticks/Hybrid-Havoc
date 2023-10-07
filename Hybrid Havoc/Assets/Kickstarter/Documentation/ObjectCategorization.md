# Object Categorization Documentation

The Object Categorization system consists of two scripts: `CategoryType` and `ObjectCategories`. These scripts work together to provide a categorization system for objects or components in your game. Both scripts are part of the `Kickstarter.Categorization` namespace.

## Class: CategoryType

The `CategoryType` script is a `ScriptableObject` that represents a category in the categorization system. It is used to define and organize different categories in your game. The CategoryType serves to distinguish similar to what might be done with layers or tags, but without the requirement of string literals.

## Class: ObjectCategories

The `ObjectCategories` script is a `MonoBehaviour` component that holds an array of `CategoryType` objects. It allows you to assign and access the categories associated with an object or component in your game.

### Fields

- `[SerializeField] private CategoryType[] categories`: An array of `CategoryType` objects representing the categories associated with the object or component.

### Properties

- `public CategoryType[] Categories`: A property that returns the array of `CategoryType` objects assigned to the `categories` field. This allows access to the assigned categories without exposing the field directly.

---

The Object Categorization system provides a flexible solution for categorizing objects or components in your Unity project. By utilizing the `CategoryType` script to define categories and assigning them to the `ObjectCategories` component, you can easily organize and categorize your game elements based on specific criteria. Due to asset files being based on reference, this system allows for each gameobject to have a unique set of categories, and systems can search through the categories to find if a gameobject serves a specific purpose or not.

To use the system, follow these steps:

1. Create instances of `CategoryType` ScriptableObjects for each category you want to define.
2. Attach the `ObjectCategories` component to the objects or components you want to categorize.
3. Assign the desired `CategoryType` objects to the `categories` field in the `ObjectCategories` component in the Unity Inspector.
4. Access the assigned categories through the `Categories` property of the `ObjectCategories` component when needed.

By leveraging the Object Categorization system, you can efficiently manage and work with categorized objects or components in your game.
