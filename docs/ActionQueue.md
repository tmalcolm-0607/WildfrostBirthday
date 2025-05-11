# ActionQueue

## Overview

The `ActionQueue` system is a core component of Wildfrost's game architecture that manages the execution of game actions in a controlled, sequential manner. It serves as a central pipeline for all game actions, ensuring they execute in the correct order and with proper timing. This system is crucial for the game's turn-based mechanics, animations, and effect sequencing.

The ActionQueue implements a priority-based queue system where actions can be added, stacked, or inserted at specific positions. It also supports parallel action execution for operations that can occur simultaneously.

## Class Architecture

### ActionQueue

The main singleton class that manages the queue of actions.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `queue` | `List<PlayAction>` | The list containing all queued actions |
| `count` | `int` | The current number of actions in the queue |
| `delayBefore` | `float` | Global delay added before each action |
| `delayAfter` | `float` | Global delay added after each action |
| `current` | `static PlayAction` | The currently executing action |
| `parallelClump` | `static Routine.Clump` | Container for actions running in parallel |
| `Empty` | `static bool` | Whether the queue is currently empty |

#### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Insert(int index, PlayAction action, bool fixedPosition)` | `static PlayAction` | Inserts an action at a specific position in the queue |
| `Add(PlayAction action, bool fixedPosition)` | `static PlayAction` | Adds an action to the end of the queue |
| `Stack(PlayAction action, bool fixedPosition)` | `static PlayAction` | Adds an action to the start of the queue |
| `GetActions()` | `static PlayAction[]` | Returns an array of all actions in the queue |
| `IndexOf(PlayAction action)` | `static int` | Gets the index of an action in the queue |
| `Remove(PlayAction action)` | `static bool` | Removes an action from the queue |
| `Wait(bool includeParallel)` | `static IEnumerator` | Waits until all actions in the queue are completed |
| `Restart()` | `static void` | Clears the queue and restarts the routine |
| `Routine()` | `IEnumerator` | The main coroutine that processes the queue |
| `RunActionRoutine()` | `IEnumerator` | Processes a single action from the queue |
| `GetIndexOfHighestPriorityAction(IReadOnlyList<PlayAction> actions)` | `static int` | Finds the index of the highest priority action |
| `Run(PlayAction action)` | `IEnumerator` | Runs a single action completely |
| `PerformAction(PlayAction action)` | `IEnumerator` | Executes an action's main functionality |
| `PostAction(PlayAction action)` | `IEnumerator` | Handles post-action events and delays |
| `RunParallel(PlayAction action)` | `static void` | Runs an action in parallel with other actions |

### PlayAction

The abstract base class for all actions that can be queued.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `pauseBefore` | `float` | Delay before this action executes |
| `pauseAfter` | `float` | Delay after this action completes |
| `priority` | `int` | Priority of this action (higher numbers execute first) |
| `fixedPosition` | `bool` | Whether the position in queue is fixed |
| `parallel` | `bool` | Whether this action can run in parallel |
| `note` | `string` | Optional description of the action |
| `IsRoutine` | `virtual bool` | Whether this action runs as a coroutine |
| `Name` | `virtual string` | The name of this action |

#### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Process()` | `virtual void` | Executes the action (for non-routine actions) |
| `Run()` | `virtual IEnumerator` | Executes the action as a coroutine |

### ActionSequence

A common implementation of PlayAction that executes a coroutine.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `routine` | `Routine` | The routine this sequence will execute |

#### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Run()` | `override IEnumerator` | Runs the contained routine until completion |

## Usage Examples

### Basic Action Queue Usage

```csharp
// Add a simple action to move a card
Entity card = someCard;
CardContainer targetContainer = someContainer;
ActionQueue.Add(new ActionMove(card, targetContainer));

// Add an action sequence with custom timing
ActionSequence sequence = new ActionSequence(MyCustomRoutine())
{
    pauseBefore = 0.2f,
    pauseAfter = 0.5f
};
ActionQueue.Add(sequence);

// Add a high-priority action that will execute before others
ActionQueue.Add(new ActionKill(enemyEntity) 
{
    priority = 100
});
```

### Creating Parallel Actions

```csharp
// Create actions that run simultaneously
ActionMove moveAction1 = new ActionMove(card1, container1);
moveAction1.parallel = true;
ActionQueue.Add(moveAction1);

ActionMove moveAction2 = new ActionMove(card2, container2);
moveAction2.parallel = true;
ActionQueue.Add(moveAction2);

// Wait for all parallel actions to complete
yield return ActionQueue.Wait(includeParallel: true);
```

### Custom Action Sequencing

```csharp
// Create a complex sequence of game actions
IEnumerator GameTurnSequence()
{
    // Draw cards first
    ActionQueue.Add(new ActionDrawHand());
    yield return ActionQueue.Wait();
    
    // Then play an enemy unit
    ActionQueue.Add(new ActionMove(enemyUnit, battleField));
    yield return ActionQueue.Wait();
    
    // Apply effects to all units on the field
    foreach (Entity entity in entitiesOnField)
    {
        ActionQueue.Add(new ActionEffectApply(entity, someEffect));
    }
    yield return ActionQueue.Wait();
}

// Add this sequence to the queue
ActionQueue.Add(new ActionSequence(GameTurnSequence()));
```

### Waiting for Actions to Complete

```csharp
// Add multiple actions
ActionQueue.Add(new ActionMove(card1, container1));
ActionQueue.Add(new ActionMove(card2, container2));

// Wait for all actions to complete before continuing
yield return ActionQueue.Wait();

// Proceed with next phase
Debug.Log("All actions completed!");
```

## Integration with Other Systems

The ActionQueue is a central system that integrates with many core game systems:

1. **Battle System**: Combat actions flow through the queue to ensure proper turn order and visual flow.

2. **Animation System**: Card movements, effects, and other visual elements are synchronized through the queue.

3. **Status Effect System**: The `StatusEffectSystem.ActionPerformedEvent` is called after each action completes.

4. **Event System**: The queue triggers events when actions are queued (`InvokeActionQueued`), performed (`InvokeActionPerform`), and finished (`InvokeActionFinished`).

5. **Game State System**: The queue integrates with `GameManager.paused` to pause action execution when the game is paused.

## Common Action Types

Wildfrost includes many specialized action types that inherit from `PlayAction`:

1. **ActionMove**: Moves a card from one container to another.
2. **ActionKill**: Removes an entity from the game.
3. **ActionApplyStatus**: Adds status effects to entities.
4. **ActionDrawHand**: Draws a hand of cards for a player.
5. **ActionSequence**: Executes a custom sequence of game logic.
6. **ActionReveal**: Reveals hidden cards.
7. **ActionTrigger**: Activates card effects.
8. **ActionEffectApply**: Applies effects to cards.

## Modding Considerations

### Creating Custom Actions

To create a custom action:

1. Create a class that inherits from `PlayAction`.
2. Override either `Process()` for immediate actions or `Run()` for coroutine-based actions.
3. Set properties like `pauseBefore`, `pauseAfter`, and `priority` as needed.

Example:

```csharp
public class ActionAddGold : PlayAction
{
    private int amount;
    
    public ActionAddGold(int goldAmount)
    {
        amount = goldAmount;
        pauseBefore = 0.2f; // Wait briefly before showing gold gain
        pauseAfter = 0.5f;  // Wait after to let the player see the effect
    }
    
    public override void Process()
    {
        // Add gold to the player's inventory
        References.Player.gold += amount;
        
        // Trigger visual feedback
        References.UI.ShowGoldGain(amount);
    }
}
```

### Customizing Action Queue Behavior

To modify the global behavior of the ActionQueue:

```csharp
// Adjust global delays
ActionQueue.instance.delayBefore = 0.1f; // Quicker action start
ActionQueue.instance.delayAfter = 0.2f;  // Shorter pauses between actions

// Create a custom action queue handler
public class FastActionQueue : MonoBehaviour
{
    void Start()
    {
        // Speed up all actions by reducing their pauses
        StartCoroutine(ProcessActionQueue());
    }
    
    IEnumerator ProcessActionQueue()
    {
        while (true)
        {
            PlayAction[] actions = ActionQueue.GetActions();
            foreach (PlayAction action in actions)
            {
                // Reduce pauses for faster gameplay
                action.pauseBefore *= 0.5f;
                action.pauseAfter *= 0.5f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
```

### Action Queue Event Hooks

You can hook into the ActionQueue's event system to respond to action execution:

```csharp
public class ActionTracker : MonoBehaviour
{
    void OnEnable()
    {
        Events.OnActionQueued += ActionQueued;
        Events.OnActionPerform += ActionPerformed;
        Events.OnActionFinished += ActionFinished;
    }
    
    void OnDisable()
    {
        Events.OnActionQueued -= ActionQueued;
        Events.OnActionPerform -= ActionPerformed;
        Events.OnActionFinished -= ActionFinished;
    }
    
    void ActionQueued(PlayAction action)
    {
        Debug.Log($"Action queued: {action.Name}");
        // Pre-process or modify the action before it executes
    }
    
    void ActionPerformed(PlayAction action)
    {
        Debug.Log($"Action performing: {action.Name}");
        // React to the action as it begins
    }
    
    void ActionFinished(PlayAction action)
    {
        Debug.Log($"Action finished: {action.Name}");
        // React to the action completion
    }
}
```

## Performance Considerations

1. **Queue Size**: The ActionQueue can grow large during complex game sequences. Consider monitoring the queue size in performance-critical situations.

2. **Parallel Actions**: While parallel actions can improve visual flow, too many simultaneous actions can impact performance.

3. **Action Priority**: Use the priority system to ensure critical actions (like UI updates or state changes) execute before visual or secondary actions.

4. **Wait Responsibly**: The `ActionQueue.Wait()` coroutine can block execution until the entire queue is processed. For long sequences, consider waiting for specific actions instead of the entire queue.
