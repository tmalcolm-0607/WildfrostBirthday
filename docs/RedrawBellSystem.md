# RedrawBellSystem

## Overview
The `RedrawBellSystem` is a core gameplay mechanic in Wildfrost that allows players to redraw their hand during battles. It functions as a tactical resource that recharges over time and can be influenced by various game modifiers and abilities. When activated, it discards the player's current hand and draws a new one, ending the current turn.

## Class Details

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `bell` | GameObject | Reference to the bell's inactive visual representation |
| `bellActive` | GameObject | Reference to the bell's active visual representation |
| `chargeParticleSystem` | ParticleSystem | Particle effect played when the bell becomes charged |
| `hitParticleSystem` | ParticleSystem | Particle effect played when the bell is activated |
| `navigationItem` | UINavigationItem | UI navigation component for the bell |
| `counterChange` | int | The amount the counter changes each turn (typically -1) |
| `counterIcon` | StatusIcon | Visual representation of the counter value |
| `popUpKeyword` | KeywordData | Keyword data for the tooltip popup |
| `popUpOffset` | Vector2 | Offset positioning for the tooltip popup |
| `textNotCharged` | LocalizedString | Text displayed when the bell is not charged |
| `textCharged` | LocalizedString | Text displayed when the bell is charged |
| `counter` | Stat | The current counter value tracking turns until the bell is charged |
| `reset` | bool | Flag indicating if the counter should be reset |
| `interactable` | bool | Flag indicating if the bell can be interacted with |
| `poppedUp` | bool | Flag indicating if the tooltip is displayed |
| `IsCharged` | bool (get) | Returns true if the counter is at or below 0 |

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `OnEnable` | None | void | Subscribes to events for scene changes, battle phases, and turn endings |
| `OnDisable` | None | void | Unsubscribes from events |
| `BecomeInteractable` | None | void | Makes the bell interactable and assigns it to the player |
| `Show` | None | void | Makes the bell visible, sets initial counter value, and triggers entrance animation |
| `Hide` | None | void | Hides the bell and disables interaction |
| `SceneChanged` | Scene scene | void | Hides the bell when leaving battle scenes |
| `BattlePhaseStart` | Battle.Phase phase | void | Shows/hides the bell based on battle phase |
| `CounterIncrement` | int turnNumber | void | Decrements the counter at the end of turns |
| `Assign` | Character owner, CardController controller | void | Assigns the owner and controller for the bell |
| `Activate` | None | void | Activates the redraw bell, discarding and drawing a new hand |
| `Counter` | None | void | Reduces the counter value and triggers effects if changed |
| `SetCounter` | int value | void | Sets the counter to a specific value and updates visuals |
| `Pop` | None | void | Shows the tooltip with relevant information |
| `UnPop` | None | void | Hides the tooltip |
| `AnimatorTrigger` | string name | void | Triggers an animation on the bell |
| `AnimatorSetHover` | bool value | void | Sets the hover animation state |
| `AnimatorSetPress` | bool value | void | Sets the press animation state |
| `PlayChargeParticleSystem` | None | void | Plays the charge particle effect |

## Usage Examples

### Basic Bell Usage
```csharp
// Find and activate the redraw bell
RedrawBellSystem redrawBell = FindObjectOfType<RedrawBellSystem>();
if (redrawBell && redrawBell.interactable)
{
    redrawBell.Activate(); // Discards hand and draws new cards, ends turn
}
```

### Modifying Bell Counter
```csharp
// Set the bell to be charged (ready for a free action)
redrawBell.SetCounter(0); // Setting to 0 makes it charged

// Reset the bell to maximum turns before charging
redrawBell.SetCounter(redrawBell.counter.max);
```

### Creating a Custom Bell Modifier
```csharp
public class CustomRedrawBellModifier : GameSystem
{
    private RedrawBellSystem _redrawBellSystem;
    
    // Cache reference to the bell system
    private RedrawBellSystem redrawBellSystem => 
        _redrawBellSystem ?? (_redrawBellSystem = FindObjectOfType<RedrawBellSystem>());
    
    public void OnEnable()
    {
        Events.OnBattleTurnStart += BattleTurnStart;
    }
    
    public void OnDisable()
    {
        Events.OnBattleTurnStart -= BattleTurnStart;
    }
    
    public void BattleTurnStart(int turn)
    {
        // Example: Charge bell faster when player has low health
        if (References.Player.entity.health <= 3)
        {
            int counter = Mathf.Max(0, redrawBellSystem.counter.current - 1);
            redrawBellSystem.SetCounter(counter);
        }
    }
}
```

## Integration with Other Systems

### ActionQueue Integration
The `RedrawBellSystem` interacts with the `ActionQueue` by adding `ActionRedraw` and `ActionEndTurn` actions when activated. These actions are processed in sequence, first redrawing the hand and then ending the current turn.

### Card Controller Integration
The bell disables the player's card controller when activated, preventing further card interactions during the redraw process.

### Events Integration
The RedrawBellSystem raises and responds to several events:
- `OnBattlePhaseStart`: Shows or hides the bell based on battle phase
- `OnBattleTurnEnd`: Decrements the bell counter
- `OnSceneChanged`: Hides the bell when leaving battles
- `OnRedrawBellHit`: Raised when the bell is activated
- `OnRedrawBellRevealed`: Raised when the bell is first shown

### Modifier System Integration
Many game modifiers can affect the RedrawBellSystem's behavior:
- `RedrawBellStartChargedModifierSystem`: Makes the bell start charged
- `RecallChargeRedrawBellModifierSystem`: Charges the bell when recalling cards
- `DrawWhenRedrawChargedModifierSystem`: Draws extra cards when the bell is charged
- `DrawExtraWhenRedrawNotChargedSystem`: Draws extra cards when the bell is not charged

## Modding Considerations

### Adding Custom Bell Behaviors
You can create custom systems that modify the RedrawBellSystem by:
1. Creating a class that inherits from `GameSystem`
2. Caching a reference to the `RedrawBellSystem` instance
3. Hooking into battle events to modify the bell counter or behavior
4. Creating a `GameModifierData` asset that adds your system

### Styling the Bell
The bell's appearance can be customized by:
1. Modifying the bell prefab's sprites
2. Adjusting the particle systems for visual effects
3. Changing the animation parameters

### Common Bell Modifiers
Consider these approaches when modifying bell behavior:
- Change counter increment/decrement logic for faster/slower charging
- Add free actions on specific triggers with `owner.freeAction = true`
- Modify card draw amounts when bell is activated
- Add special effects when bell is charged or activated

### Example: Bell that Charges on Card Play
```csharp
// System that reduces bell counter when playing cards
public class ChargeOnPlayModifierSystem : GameSystem
{
    private RedrawBellSystem _redrawBellSystem;
    
    private RedrawBellSystem redrawBellSystem => 
        _redrawBellSystem ?? (_redrawBellSystem = FindObjectOfType<RedrawBellSystem>());
    
    public void OnEnable()
    {
        Events.OnActionPerform += ActionPerform;
    }
    
    public void OnDisable()
    {
        Events.OnActionPerform -= ActionPerform;
    }
    
    public void ActionPerform(PlayAction action)
    {
        // Charge bell when playing cards
        if (action is ActionTrigger && action.entity.owner == References.Player)
        {
            int counter = Mathf.Max(0, redrawBellSystem.counter.current - 1);
            redrawBellSystem.SetCounter(counter);
        }
    }
}
```
