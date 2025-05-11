# AbilityTargetSystem

## Overview
The `AbilityTargetSystem` is a game system in Wildfrost responsible for visually indicating targets for card abilities. When a card or ability needs to highlight potential targets (such as for bombardment, area effects, or targeted abilities), this system manages the visual indicators that show players which card slots are being targeted. The system maintains a dictionary of containers and their corresponding target indicator objects.

## Class Details

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `targetGroup` | Transform | Parent transform for all target indicator objects |
| `targetPrefab` | GameObject | Prefab used to instantiate target indicators |
| `currentTargets` | Dictionary<CardContainer, GameObject> | Mapping of card containers to their target indicator objects |

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `OnEnable` | None | void | Subscribes to events for target addition/removal and scene changes |
| `OnDisable` | None | void | Unsubscribes from events and clears all targets |
| `AddTarget` | CardContainer container | void | Creates a target indicator for the specified container |
| `RemoveTarget` | CardContainer container | void | Removes the target indicator for the specified container |
| `SceneChanged` | Scene scene | void | Clears all targets when the scene changes |
| `Clear` | None | void | Removes all target indicators and cleans up references |

## Events Integration

The system responds to the following events:

| Event | Description |
|-------|-------------|
| `Events.OnAbilityTargetAdd` | Called when a container should be marked as a target |
| `Events.OnAbilityTargetRemove` | Called when a container should no longer be marked as a target |
| `Events.OnSceneChanged` | Called when the scene changes, triggering cleanup |

## Usage Examples

### Adding Targets
```csharp
// Mark a card slot as a target
CardContainer targetContainer = someCardSlot;
Events.InvokeAbilityTargetAdd(targetContainer);
```

### Removing Targets
```csharp
// Remove targeting from a card slot
CardContainer targetContainer = someCardSlot;
Events.InvokeAbilityTargetRemove(targetContainer);
```

### Creating Multiple Targets
```csharp
// Mark multiple slots as targets
foreach (CardContainer container in targetContainers)
{
    Events.InvokeAbilityTargetAdd(container);
    yield return new WaitForSeconds(0.1f); // Add slight delay for visual effect
}
```

### Clearing All Targets
```csharp
// Find the ability target system and clear all targets
AbilityTargetSystem abilityTargetSystem = FindObjectOfType<AbilityTargetSystem>();
abilityTargetSystem?.Clear();
```

## Integration with Other Systems

### StatusEffectBombard Integration
The `StatusEffectBombard` class is a major consumer of this system, using it to display which slots will be targeted by bombardment attacks:

```csharp
public IEnumerator SetTargets()
{
    // Remove existing targets
    foreach (CardContainer target in targetList)
    {
        Events.InvokeAbilityTargetRemove(target);
    }
    
    // Set up new targets
    targetList.Clear();
    // ... logic to determine which slots to target ...
    
    // Add new targets with a delay for visual effect
    foreach (CardContainer target in targetList)
    {
        Events.InvokeAbilityTargetAdd(target);
        yield return Sequences.Wait(delayBetweenTargets);
    }
}
```

### SFX System Integration
The `SfxSystem` plays sounds when targets are added:

```csharp
public void AbilityTargetAdd(CardContainer container)
{
    OneShot("event:/sfx/ui/targeter");
}
```

## Modding Considerations

### Custom Target Indicators
To customize the appearance of target indicators:

1. Create a custom prefab with your desired visuals
2. Find the `AbilityTargetSystem` instance in the battle scene
3. Replace the `targetPrefab` reference with your custom prefab

### Extending Targeting Functionality
To extend the targeting system for custom abilities:

1. Create a custom status effect or ability class
2. Use `Events.InvokeAbilityTargetAdd` and `Events.InvokeAbilityTargetRemove` to manage targets
3. Consider adding a delay between target additions for visual appeal

### Custom Targeting Logic
Implementation example for a custom area-of-effect targeting system:

```csharp
public class CustomAoETargeting : MonoBehaviour
{
    public List<CardContainer> targetContainers = new List<CardContainer>();
    
    // Call this when your ability is activated
    public IEnumerator ShowTargets(Entity sourceEntity)
    {
        // Clear previous targets
        ClearTargets();
        
        // Find valid targets based on your custom logic
        List<CardContainer> validTargets = FindValidTargets(sourceEntity);
        
        // Add targets with visual delay
        foreach (CardContainer target in validTargets)
        {
            targetContainers.Add(target);
            Events.InvokeAbilityTargetAdd(target);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    public void ClearTargets()
    {
        foreach (CardContainer container in targetContainers)
        {
            Events.InvokeAbilityTargetRemove(container);
        }
        targetContainers.Clear();
    }
    
    private List<CardContainer> FindValidTargets(Entity source)
    {
        // Your custom targeting logic here
        // ...
    }
}
```

### Target Animation and Effects
To add custom animations to targets:

1. Modify the target prefab to include particle systems or animators
2. Access the instantiated target objects from `AbilityTargetSystem.currentTargets`
3. Add custom animation triggers in your ability implementation
