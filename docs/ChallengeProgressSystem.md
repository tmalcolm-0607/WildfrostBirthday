# ChallengeProgressSystem

`ChallengeProgressSystem` is a core system in Wildfrost that tracks player progress on challenges across game sessions and campaigns.

## Class Overview

`ChallengeProgressSystem` inherits from `GameSystem` and manages the tracking, persistence, and updating of player progress on various in-game challenges.

## Key Properties

| Property | Type | Description |
|----------|------|-------------|
| `instance` | `ChallengeProgressSystem` | Static singleton instance of the system |
| `progress` | `List<ChallengeProgress>` | List of all challenge progress entries |
| `saveRequired` | `bool` | Flag indicating if progress needs to be saved |

## Key Methods

### Progress Management

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `GetProgress` | `string challengeName` | `int` | Static method to get the current progress value for a named challenge |
| `AddProgress` | `string challengeName, int add` | `void` | Static method to increment progress for a specific challenge |
| `LoadProgress` | None | `List<ChallengeProgress>` | Loads challenge progress from save data |

### Lifecycle and Save Management

| Method | Description |
|--------|-------------|
| `CheckSave()` | Saves challenge progress if changes have been made |
| `Load()` | Loads and reconciles challenge progress from both meta-progression and campaign-specific save data |

## ChallengeProgress Class

The system uses `ChallengeProgress` objects to track individual challenges:

| Property | Type | Description |
|----------|------|-------------|
| `challengeName` | `string` | Unique identifier for the challenge |
| `currentValue` | `int` | Current progress value for the challenge |
| `originalValue` | `int` | Original progress value when first loaded |

## Integration with Events

The system connects to these game events:

| Event | Handler | Purpose |
|-------|---------|---------|
| `Events.OnCampaignSaved` | `CheckSave` | Ensures challenge progress is saved when campaigns are saved |
| `Events.OnCampaignLoaded` | `Load` | Loads challenge progress when a campaign is loaded |

## Usage Examples

### Checking Challenge Progress

```csharp
// Get the current progress for a specific challenge
string challengeName = "defeat_boss_no_damage";
int progress = ChallengeProgressSystem.GetProgress(challengeName);

// Check if challenge is completed
ChallengeData challengeData = /* get challenge data */;
bool isCompleted = progress >= challengeData.requiredValue;
```

### Incrementing Progress

```csharp
// Player defeated a boss without taking damage
ChallengeProgressSystem.AddProgress("defeat_boss_no_damage", 1);

// Player collected multiple items
ChallengeProgressSystem.AddProgress("collect_rare_items", 5);
```

### Custom Challenge Tracking

```csharp
// In a card effect class
public override CardAction[] GetActions(Entity entity, Target target)
{
    // Track when a specific card effect is used
    ChallengeProgressSystem.AddProgress("use_special_effect", 1);
    
    // Continue with normal card actions
    // ...
}
```

## Save Mechanism

The system saves challenge progress in two ways:

1. **Meta-progression**: Using `SaveProgressData` to persist across multiple campaigns
2. **Campaign-specific**: Using `SaveCampaignData` to track within a single campaign

When loading, it reconciles these two sources, preferring campaign-specific values if they're higher.

## Challenge Types

The system can track various types of challenges:

- **Cumulative challenges**: Track total counts over time (e.g., "Defeat 100 enemies")
- **Achievement challenges**: Track one-time accomplishments (e.g., "Win with all leaders")
- **Campaign-specific challenges**: Track progress within a single campaign

## Modding Considerations

When extending the challenge system for mods:

1. **Custom Challenge Names**: Use a prefix for custom challenge names to avoid conflicts
2. **Progress Integration**: Connect to relevant events to update challenge progress
3. **Challenge Display**: Create UI elements to show custom challenge progress
4. **Reward Integration**: Connect completion to custom rewards or unlocks

Example of tracking a custom mod challenge:

```csharp
// In your mod's initialization
public void RegisterCustomChallenges()
{
    // Hook into relevant events
    Events.OnEntityKilled += (entity, deathType) => {
        if (entity.data.name == "YourModBoss") {
            // Increment challenge when mod boss is defeated
            ChallengeProgressSystem.AddProgress("mymod_defeat_boss", 1);
        }
    };
}
```

## Critical Connections

The ChallengeProgressSystem interacts closely with:

- **ChallengeSystem**: Defines the available challenges and their requirements
- **SaveSystem**: Persists challenge progress between game sessions
- **UnlockSystem**: May unlock content when challenges are completed
- **MetaprogressionSystem**: Provides persistent data across game runs
