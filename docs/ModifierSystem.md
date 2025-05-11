# ModifierSystem

## Overview
The `ModifierSystem` is a core game system in Wildfrost that manages gameplay modifiers which affect the rules, difficulty, and unique mechanics of each run. This system is responsible for adding, removing, and executing game modifiers that can significantly alter gameplay through both positive and negative effects. Game modifiers are a key part of Wildfrost's replayability, allowing for different experiences across multiple playthroughs.

## Class Details

### Properties
The `ModifierSystem` class itself has no public properties, as it primarily acts as a manager class with static methods for handling game modifiers stored in the `CampaignData`.

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `OnEnable` | None | void | Subscribes to campaign events for init, start, load, and generation |
| `OnDisable` | None | void | Unsubscribes from campaign events |
| `AddModifier` | CampaignData data, GameModifierData modifier | void (static) | Adds a modifier to the campaign data |
| `RemoveModifier` | CampaignData data, GameModifierData modifier | void (static) | Removes a modifier from the campaign data |
| `AddSystems` | None | void (static) | Adds all systems defined by active modifiers to the campaign |
| `RunInitScripts` | None | IEnumerator (static) | Runs the initialization scripts of all active modifiers |
| `RunStartScripts` | None | Task | Runs the start scripts of all active modifiers asynchronously |
| `RunInitScripts` | IReadOnlyCollection<GameModifierData> modifiers | IEnumerator (static) | Runs initialization scripts for a specific collection of modifiers |
| `RunCampaignStartScripts` | IReadOnlyCollection<GameModifierData> modifiers | IEnumerator (static) | Runs campaign start scripts for a specific collection of modifiers |

## GameModifierData Class

The `GameModifierData` class is a ScriptableObject that defines a game modifier. It contains:

| Property | Type | Description |
|----------|------|-------------|
| `name` | string | Unique identifier for the modifier |
| `value` | int | Numerical value representing the modifier's power or effect strength |
| `visible` | bool | Whether the modifier is visible in the UI |
| `bellSprite` | Sprite | Icon displayed in the UI |
| `dingerSprite` | Sprite | Alternative icon used for notifications |
| `titleKey` | LocalizedString | Localized title of the modifier |
| `descriptionKey` | LocalizedString | Localized description of the modifier |
| `systemsToAdd` | string[] | Names of GameSystem classes to add when this modifier is active |
| `setupScripts` | Script[] | Scripts run during campaign initialization |
| `startScripts` | Script[] | Scripts run when campaign starts |
| `scriptPriority` | int | Priority for script execution order (higher = earlier) |
| `blockedBy` | GameModifierData[] | Other modifiers that prevent this one from being applied |
| `linkedStormBell` | HardModeModifierData | Reference to a hard mode modifier bell if linked |
| `ringSfxEvent` | EventReference | Sound effect played when the modifier is activated |
| `ringSfxPitch` | Vector2 | Pitch range for the sound effect |

## Usage Examples

### Adding a Modifier to a Campaign
```csharp
// Find a modifier by name
GameModifierData modifier = AddressableLoader.Get<GameModifierData>("GameModifierData", "DrawExtraCards");

// Add it to the current campaign
ModifierSystem.AddModifier(Campaign.Data, modifier);

// This will trigger the ModifierSystem to:
// 1. Add any systems the modifier requires
// 2. Run any setup or start scripts
// 3. Make the modifier visible in the UI
```

### Creating a Custom Modifier
```csharp
// Using the GameModifierDataBuilder from a mod
GameModifierDataBuilder builder = new GameModifierDataBuilder(myMod);
GameModifierData customModifier = builder
    .WithName("ExtraDamageModifier")
    .WithValue(50) // Positive value = beneficial
    .WithVisible(true)
    .WithBellSprite("power_bell")
    .WithTitle("Power Bell")
    .WithDescription("All friendly units deal 2 extra damage")
    .WithSystemsToAdd("ExtraDamageModifierSystem")
    .WithScriptPriority(100)
    .Build();

// Create the modifier system
public class ExtraDamageModifierSystem : GameSystem
{
    public void OnEnable()
    {
        Events.OnDamageCalculation += ModifyDamage;
    }
    
    public void OnDisable()
    {
        Events.OnDamageCalculation -= ModifyDamage;
    }
    
    public void ModifyDamage(ref int damage, Entity attacker, Entity target)
    {
        if (attacker.owner == References.Player)
        {
            damage += 2;
        }
    }
}
```

### Removing a Modifier
```csharp
// Remove a modifier during gameplay
GameModifierData modifier = Campaign.Data.Modifiers.Find(m => m.name == "ExtraDamageModifier");
if (modifier != null)
{
    ModifierSystem.RemoveModifier(Campaign.Data, modifier);
}
```

## Integration with Other Systems

### Campaign System
The `ModifierSystem` is tightly integrated with the `Campaign` system:
- Modifiers are stored in the `CampaignData.Modifiers` list
- Scripts are executed during campaign initialization and start
- Systems are added to the campaign game object

### Events System
The `ModifierSystem` hooks into several campaign events:
- `OnCampaignInit`: Runs setup scripts for all modifiers
- `OnCampaignStart`: Adds all systems defined by modifiers
- `OnCampaignLoaded`: Reapplies systems when loading a saved campaign
- `OnCampaignGenerated`: Runs start scripts for all modifiers

### UI System
Modifiers are displayed in the UI through several classes:
- `ModifierDisplay`: Base class for displaying modifiers
- `ModifierDisplayCurrent`: Shows active modifiers during gameplay
- `HardModeModifierDisplay`: Shows available hard mode modifiers
- `ModifierIcon`: Visual representation of a modifier in the UI

## Modifier Types

### Standard Game Modifiers
These are applied through various game mechanisms like:
- Daily run modifiers
- Boss rewards
- Special events

### Hard Mode Modifiers (Storm Bells)
Special modifiers that increase difficulty:
- Unlocked through game progression
- Can be toggled on/off before starting a run
- Often have more severe effects
- May be linked to the storm level system

## Modding Considerations

### Creating Custom Modifiers
To create a custom modifier:

1. Create a `GameModifierData` asset:
   ```csharp
   GameModifierData modifier = ScriptableObject.CreateInstance<GameModifierData>();
   modifier.name = "MyCustomModifier";
   modifier.value = 50; // Positive = good, negative = bad
   modifier.systemsToAdd = new string[] { "MyCustomModifierSystem" };
   ```

2. Create the modifier system class:
   ```csharp
   public class MyCustomModifierSystem : GameSystem
   {
       public void OnEnable()
       {
           // Subscribe to events
       }
       
       public void OnDisable()
       {
           // Unsubscribe from events
       }
       
       // Custom logic for your modifier
   }
   ```

3. Register the modifier with the game.

### Using the GameModifierDataBuilder
If using the modding API, you can use the `GameModifierDataBuilder` for a fluent interface:

```csharp
GameModifierData modifier = new GameModifierDataBuilder(myMod)
    .WithName("MyModifier")
    .WithValue(-75) // Negative value = detrimental
    .WithVisible(true)
    .WithBellSprite("custom_bell")
    .WithTitle("Challenge Bell")
    .WithDescription("Reduces your starting health by 3")
    .WithSystemsToAdd("ReduceStartingHealthSystem")
    .WithStartScripts(reduceHealthScript)
    .WithScriptPriority(50)
    .Build();
```

### Modifier Scripting
Scripts in modifiers can be used for one-time effects:

```csharp
// Create a script that reduces starting health
ScriptChangeCardStats reduceHealthScript = ScriptableObject.CreateInstance<ScriptChangeCardStats>();
reduceHealthScript.includeLeaders = true;
reduceHealthScript.health = -3;
```

### Execution Order
When creating modifiers, be mindful of script priority:
- Higher priority scripts run first
- Setup scripts run during campaign initialization
- Start scripts run when the campaign starts
- System execution order depends on Unity's component ordering
