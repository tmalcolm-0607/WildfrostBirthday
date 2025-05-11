# PlayAction System

`PlayAction` is a core abstract class in Wildfrost that defines the base functionality for all game actions that can be performed during gameplay, such as applying effects, killing entities, triggering abilities, and more.

## Class Overview

`PlayAction` serves as the foundation for the game's action system, providing timing control, execution priority, and the ability to run actions sequentially or in parallel. Most game mechanics that involve cards interacting with each other or the game state are implemented through derived `PlayAction` classes.

## Key Properties

| Property | Type | Description |
|----------|------|-------------|
| `pauseBefore` | `float` | Delay in seconds before the action starts |
| `pauseAfter` | `float` | Delay in seconds after the action completes |
| `priority` | `int` | Determines execution order when multiple actions are queued |
| `fixedPosition` | `bool` | Whether the action's position in the queue is fixed |
| `parallel` | `bool` | Whether the action can run in parallel with other actions |
| `note` | `string` | Optional descriptive text for debugging purposes |

## Key Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Process()` | `void` | Called when the action is processed without animation |
| `Run()` | `IEnumerator` | Coroutine that runs the action with animation |
| `Name` (property) | `string` | Gets the name of the action for debugging |
| `IsRoutine` (property) | `bool` | Whether the action runs as a coroutine (default: true) |

## Major Derived Classes

### Effect Application

- **ActionEffectApply**: Applies status effects to target entities
- **ActionApplyStatus**: Applies a specific status effect to targets

### Combat Actions

- **ActionKill**: Forces an entity to die
- **ActionConsume**: Consumes a target entity
- **ActionFlee**: Makes an entity flee from battle

### Card Management

- **ActionDraw**: Draws cards from the deck
- **ActionRedraw**: Redraws the player's hand
- **ActionDiscardEffect**: Discards cards with specific effects

### Entity Movement

- **ActionMove**: Moves an entity to a different position
- **ActionShove**: Pushes an entity to a different position

### Trigger System

- **ActionTrigger**: Base class for triggering entity abilities
- **ActionTriggerAgainst**: Triggers abilities against specific targets
- **ActionTriggerByCounter**: Triggers abilities based on counter values
- **ActionTriggerSubsequent**: Triggers follow-up abilities

### Game Flow Control

- **ActionEndTurn**: Ends the current player's turn
- **ActionChangePhase**: Changes the current battle phase
- **ActionRunEnableEvent**: Enables specific game events

## Example Usage

### Basic Action Execution

Actions are typically queued in the `ActionQueue` and executed sequentially:

```csharp
// Create an action to kill an entity
Entity targetEntity = /* some entity */;
ActionKill killAction = new ActionKill(targetEntity);

// Set timing properties
killAction.pauseBefore = 0.5f; // Wait half a second before executing
killAction.pauseAfter = 0.3f;  // Wait after execution

// Queue the action
ActionQueue.Add(killAction);
```

### Creating Action Sequences

Multiple actions can be combined for complex effects:

```csharp
// Create a sequence of actions
ActionSequence sequence = new ActionSequence();

// Add child actions to the sequence
sequence.Add(new ActionApplyStatus(entity, target, "frozen", 1));
sequence.Add(new ActionKill(target));
sequence.Add(new ActionDraw(1));

// Queue the entire sequence
ActionQueue.Add(sequence);
```

### Creating Custom Actions

Custom actions can be created by inheriting from `PlayAction`:

```csharp
public class ActionCustomEffect : PlayAction
{
    private Entity caster;
    private Entity target;
    private int amount;
    
    public ActionCustomEffect(Entity caster, Entity target, int amount)
    {
        this.caster = caster;
        this.target = target;
        this.amount = amount;
    }
    
    public override IEnumerator Run()
    {
        // Perform visual effects
        yield return Sequences.PlayEffect("CustomEffect", target.transform.position);
        
        // Apply game logic
        target.ApplyStatusEffect("custom_effect", amount);
        
        // Wait for animation
        yield return new WaitForSeconds(0.5f);
    }
}
```

## Integration with Other Systems

The PlayAction system interacts with several other key game systems:

- **ActionQueue**: Central queue that manages execution of all actions
- **Entity**: Game entities that actions are performed on or by
- **Trigger**: System for card abilities to trigger effects
- **StatusEffectApplyX**: System for applying status effects
- **Sequences**: Helper class for common animation sequences

## Action Stacking and Optimization

Some actions support "stacking" to optimize execution:

```csharp
// ActionEffectApply example of stacking similar effects
ActionEffectApply existingAction = /* existing action */;
List<Entity> newTargets = /* new targets */;
int amount = 2;

// Instead of creating a new action, stack with the existing one
existingAction.Stack(newTargets, amount);
```

## Modding Considerations

When creating custom PlayActions for mods:

1. **Inherit from PlayAction**: Create a new class that inherits from `PlayAction`
2. **Override Run()**: Implement the `Run()` method to define your action's behavior
3. **Override Process()**: Optionally override `Process()` for non-animated actions
4. **Set IsRoutine**: Set `IsRoutine` to `false` if your action doesn't need animation
5. **Use ActionQueue**: Add your action to the `ActionQueue` to execute it

Example of a simple modded action:

```csharp
public class ActionApplyModEffect : PlayAction
{
    private Entity source;
    private Entity target;
    
    public ActionApplyModEffect(Entity source, Entity target)
    {
        this.source = source;
        this.target = target;
    }
    
    public override IEnumerator Run()
    {
        // Custom effect logic
        if (target.IsAliveAndExists())
        {
            // Apply your mod-specific effect
            YourModEffectSystem.ApplyEffect(source, target);
            
            // Play a visual effect
            yield return Sequences.PlayEffect("YourModEffect", target.transform.position);
        }
    }
}
```
