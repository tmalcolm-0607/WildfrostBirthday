# CardAnimation

`CardAnimation` is an abstract class that serves as the base for all card animations in Wildfrost. It provides a foundation for implementing visual effects that can be applied to cards during gameplay, such as movement, explosions, or transitions.

## Class Definition

`CardAnimation` inherits from `ScriptableObject`, allowing animation behaviors to be defined as assets in the Unity editor.

```csharp
public abstract class CardAnimation : ScriptableObject
{
    public virtual IEnumerator Routine(object data, float startDelay = 0f)
    {
        return null;
    }
}
```

## Key Properties

As an abstract base class, `CardAnimation` doesn't define any properties directly. However, derived classes typically implement specific properties to customize their animation behaviors.

## Core Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `Routine(object data, float startDelay = 0f)` | `data`: Input object for the animation<br>`startDelay`: Optional delay before starting | Abstract method that derived classes must implement to define the animation sequence. Returns an IEnumerator for coroutine execution. |

## Usage

`CardAnimation` assets are designed to be loaded and executed during gameplay:

```csharp
// Load a card animation from resources
CardAnimation animation = AssetLoader.Lookup<CardAnimation>("CardAnimations", "AnimationName");

// Start the animation as a coroutine
new Routine(animation.Routine(targetObject));
```

## Derived Classes

`CardAnimation` serves as the base for multiple specialized animation types:

| Class Name | Description |
|------------|-------------|
| `CardAnimationBombardRocket` | Animates a rocket projectile falling onto a target. |
| `CardAnimationBombardRocketShoot` | Animates the launching of a rocket from a card. |
| `CardAnimationFlyToBackpack` | Animates a card flying toward the player's backpack/inventory. |
| *[Other derived animations]* | Various other specialized animations for different game effects. |

## Integration with Other Systems

- **TriggerBombard**: Uses `CardAnimationBombardRocketShoot` and `CardAnimationBombardRocket` to create the bombardment effect.
- **Entity.curveAnimator**: Many derived animations use this component to animate entity movements.
- **Events System**: Animations can trigger game events like screen shake or sound effects.

## Creating Custom Card Animations

To create a custom card animation:

1. Create a new class that inherits from `CardAnimation`.
2. Implement the `Routine` method with your animation logic.
3. Use the `CreateAssetMenu` attribute to make it available in the Unity editor.

Example structure:

```csharp
[CreateAssetMenu(fileName = "MyAnimation", menuName = "Card Animation/My Animation")]
public class MyCustomCardAnimation : CardAnimation
{
    // Define animation-specific properties here
    
    public override IEnumerator Routine(object data, float startDelay = 0f)
    {
        // Animation implementation
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);
            
        // Animation steps...
    }
}
```

## Best Practices

1. **Data Type Safety**: Always check the type of the `data` parameter before using it.
2. **Error Handling**: Add null checks and graceful exits if the data is invalid.
3. **Performance**: Keep animations lightweight to avoid performance issues during gameplay.
4. **Configurability**: Use serialized fields to make animations customizable in the Unity editor.
5. **Event Integration**: Use the Events system to integrate with other game systems.

## Related Classes

- **CardAnimationProfile**: Defines animation curves and parameters that can be reused across animations.
- **BombardRocket**: A MonoBehaviour representing the rocket object used in bombardment animations.
- **CurveAnimator**: Component used for smoothly animating card movements along curves.
