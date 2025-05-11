# CardAnimationBombardRocketShoot

`CardAnimationBombardRocketShoot` is a specialized animation class that handles the visual and mechanical aspects of rocket launching animations in Wildfrost's bombardment system. It inherits from the abstract `CardAnimation` class and implements the specific behavior for the initial launch phase of the bombardment attack.

## Class Definition

```csharp
[CreateAssetMenu(fileName = "BombardRocketShoot", menuName = "Card Animation/Bombard Rocket Shoot")]
public class CardAnimationBombardRocketShoot : CardAnimation
{
    // Properties and methods
}
```

## Properties

### Shoot Particles Properties

| Property | Type | Description |
|----------|------|-------------|
| `shootFxPrefab` | `ParticleSystem` | The particle system prefab that creates the launch effect. |
| `shootAngle` | `Vector3` | The orientation of the launch effect (default: 0, 0, 135). |
| `shootFxOffset` | `Vector3` | Position offset for the launch effect relative to the entity (default: 0, 1, 0). |
| `shootScreenShake` | `float` | Intensity of the screen shake when firing (default: 1.0). |

### Recoil Animation Properties

| Property | Type | Description |
|----------|------|-------------|
| `recoilOffset` | `Vector3` | How far the card moves during recoil (default: 1, -1, 0). |
| `recoilCurve` | `AnimationCurve` | Animation curve defining the recoil movement pattern. |
| `recoilDuration` | `float` | How long the recoil animation lasts (default: 1.0). |

## Methods

### Routine

```csharp
public override IEnumerator Routine(object data, float startDelay = 0f)
```

**Parameters:**
- `data`: The entity performing the bombardment attack (expected to be an `Entity` object).
- `startDelay`: Optional delay before starting the animation (default: 0).

**Description:**
This method orchestrates the bombardment shoot animation sequence:
1. Checks if the provided data is an Entity.
2. Instantiates the particle effect at the proper position and rotation.
3. Triggers screen shake via the Events system.
4. Broadcasts the bombardment shoot event.
5. Animates the card with recoil movement.
6. Waits for the particle effect to complete.

## Usage in Game

The `CardAnimationBombardRocketShoot` class is primarily used by the `TriggerBombard` class to create the visual effect of a card launching rockets during a bombardment attack:

```csharp
// Example from TriggerBombard.Animate()
public override IEnumerator Animate()
{
    new Routine(AssetLoader.Lookup<CardAnimation>("CardAnimations", "BombardRocketShoot").Routine(entity));
    yield return Sequences.Wait(0.2f);
}
```

## Key Integration Points

- **Entity.curveAnimator**: Used to animate the card's recoil movement.
- **Events.InvokeScreenShake**: Triggers screen shake for impact feedback.
- **Events.InvokeBombardShoot**: Triggers the bombardment shoot event, which can be picked up by audio systems for sound effects.

## Visual Effect Sequence

1. **Card Preparation**: The card prepares to launch rockets.
2. **Launch Effect**: Particle effects appear at the specified offset from the card.
3. **Screen Shake**: The screen shakes to emphasize the impact of the launch.
4. **Recoil Animation**: The card moves in the specified recoil pattern.
5. **Follow-up**: After a short delay (0.2s), the `TriggerBombard.RainRockets()` method follows up with the actual rocket animations.

## Relationship with Other Classes

- **CardAnimation**: Base class providing the animation framework.
- **CardAnimationBombardRocket**: Handles the rocket projectile animations that follow the shoot animation.
- **TriggerBombard**: Orchestrates the entire bombardment system, including shoot and rocket animations.
- **BombardRocket**: Represents the rocket object that's created during the subsequent rocket animation.

## Customizing the Animation

To modify the behavior of the rocket shoot animation:

1. Find the ScriptableObject asset in your project's assets.
2. Adjust the particle effect, angles, offsets, and recoil parameters.
3. You can also replace the `shootFxPrefab` with a different particle system for a completely different visual effect.

## Technical Notes

- The animation relies on the entity having a `curveAnimator` component to perform the recoil movement.
- The animation waits for the particle system to complete before ending, ensuring synchronization with subsequent animations.
