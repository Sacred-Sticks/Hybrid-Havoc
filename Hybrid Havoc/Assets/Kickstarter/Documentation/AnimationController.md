# AnimationController Documentation

The `AnimationController` script is an abstract class that serves as a base for creating animation controllers in Unity. It provides common functionality for controlling animations using an `Animator` component, through code rather than through the visual node system in the Unity Animator. This script is part of the `Kickstarter.Animations` namespace.

## Class: AnimationController

### Inherits from: MonoBehaviour

### Required Components

- `[RequireComponent(typeof(Animator))]`: This script requires the presence of an `Animator` component on the same GameObject.

### Fields

- `private Animator animator`: A reference to the `Animator` component attached to the same GameObject.

### Methods

- `protected void Awake()`: This method is called when the script instance is being loaded. It retrieves the `Animator` component from the GameObject. Create a new Awake and call `base.Awake()` when anything else needs to be added to the awake method of the specific animation controller.

- `protected void SetAnimation(AnimationSetData parameters)`: Plays an animation with the specified parameters.

- `protected void TransitionAnimation(AnimationTransitionData parameters)`: Cross-fades between the current animation state and the specified animation state.

- `protected void ChangeParameter<TValueType>(AnimationParameterChangeData parameters, TValueType value)`: Changes the value of a parameter in the animator based on the specified value type.

- `protected void ChangeParameter(AnimationParameterChangeData parameters)`: Triggers a parameter in the animator.

### Nested Structs

- `protected struct AnimationSetData`: Contains data for setting an animation state, including the animation state name and the animation layer.

- `protected struct AnimationTransitionData`: Contains data for transitioning between animation states, including the animation state name, animation layer, and transition duration.

- `protected struct AnimationParameterChangeData`: Contains data for changing animator parameters, including the parameter name.

---

The `AnimationController` script provides a foundation for creating animation controllers in Unity. By inheriting from this script and implementing custom animation control logic, you can easily manage and manipulate animations using the attached `Animator` component.
