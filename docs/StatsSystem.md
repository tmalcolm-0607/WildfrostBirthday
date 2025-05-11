# StatsSystem

`StatsSystem` is a core game system in Wildfrost responsible for tracking, updating, and persisting player statistics throughout campaign runs.

## Class Overview

`StatsSystem` inherits from `GameSystem` and acts as the central hub for recording all game statistics related to player actions, enemy interactions, battle outcomes, and campaign progress.

## Properties

| Property | Type | Description |
|----------|------|-------------|
| `instance` | `StatsSystem` | Static instance of the system for global access |
| `stats` | `CampaignStats` | Current campaign statistics being tracked |
| `goldThisBattle` | `int` | Gold collected in the current battle |
| `sacrificedThisBattle` | `int` | Number of friendly units sacrificed in the current battle |
| `kingMokoExists` | `bool` | Tracks if King Moko exists in the campaign |
| `campaignEnded` | `bool` | Indicates if the current campaign has ended |

## Key Methods

### Core Functionality

| Method | Description |
|--------|-------------|
| `Get()` | Static accessor to get the current `CampaignStats` |
| `Set(CampaignStats stats)` | Sets the current campaign stats |
| `CampaignEnd(Campaign.Result result, CampaignStats stats, PlayerData playerData)` | Handles end of campaign |
| `CampaignSaved()` | Persists the stats when campaign is saved |
| `Update()` | Updates time-based statistics |

### Event Handlers

The system connects to numerous game events to track statistics:

| Event Handler | Tracked Statistics |
|--------------|-------------------|
| `EntityHit` | Damage dealt/taken, blocks, friendly fire |
| `PostEntityHit` | Healing statistics |
| `StatusApplied` | Status effect applications and maximum values |
| `EntityKilled` | Enemy and friendly unit deaths, sacrifice tracking |
| `EntityOffered` | Cards offered in selections |
| `EntityChosen` | Cards chosen from selections |
| `EntityFlee` | Enemy escape tracking |
| `EntityDiscarded` | Cards recalled/discarded |
| `EntitySummoned` | Unit summoning statistics |
| `EntityTriggered` | Trigger activation statistics |
| `CardInjured` | Card injury tracking |
| `BattleStart` | Battle initiation tracking |
| `BattleEnd` | Battle completion statistics |
| `DropGold`/`SpendGold` | Economy tracking |
| `ShopItemPurchase`/`ShopItemHaggled` | Shopping behavior |
| `KillCombo` | Combo tracking |
| `RedrawBellHit` | Redraw mechanic usage |
| `WaveDeployerEarlyDeploy` | Early deployment tracking |
| `BattleTurnStart` | Turn counting |
| `Rename` | Card renaming |
| `MuncherFeed` | Muncher feeding |
| `UpgradeGained`/`UpgradeAssigned` | Upgrade statistics |

## Usage Examples

### Accessing Stats

```csharp
// Get the current stats
CampaignStats currentStats = StatsSystem.Get();

// Check specific stat values
int enemiesKilled = currentStats.Get("enemiesKilled");
int damageDealt = currentStats.Get("damageDealt", "physical");
```

### Recording Custom Stats

```csharp
// Record a custom stat
StatsSystem.Get().Add("customStat", "category", 1);

// Record a maximum value
StatsSystem.Get().Max("highestDamage", 50);
```

### Saving Stats

```csharp
// Stats are automatically saved on campaign save
// But can be manually triggered
SaveSystem.SaveCampaignData(Campaign.Data.GameMode, "stats", StatsSystem.Get());
```

## Stat Categories Tracked

The system tracks a wide variety of statistics including:

| Category | Examples |
|----------|----------|
| **Combat** | Damage dealt/taken by type, highest damage, hits |
| **Entities** | Enemies killed, friendlies died, cards summoned |
| **Economy** | Gold earned/spent, shop interactions, haggling |
| **Status Effects** | Effects applied, highest effect counts |
| **Card Interactions** | Cards played, discarded, triggered |
| **Battle Progress** | Turn counts, battle wins, combos |
| **Campaign** | Time played, victories, losses |

## Integration with Other Systems

- **Events**: Connects to the event system to track all game actions
- **CampaignStats**: The data object that holds all statistics
- **SaveSystem**: Persists statistics between game sessions
- **Campaign**: Tracks overall campaign progress and outcomes
- **Battle**: Interfaces with battle mechanics and outcomes

## Modding Considerations

When extending the stats system:

1. **Adding New Stats**: Use the existing `Add()` method on `CampaignStats`
2. **Custom Events**: Connect to the Events system and add your own stat tracking
3. **Persistent Storage**: Use the SaveSystem for custom stat persistence
4. **Achievement Integration**: Connect stats to custom achievements

Example of recording custom mod statistics:

```csharp
// Inside your mod's setup
Events.OnCustomModAction += (param) => {
    StatsSystem.Get().Add("modCustomStat", param.type, 1);
    
    // Track maximum values
    if (param.value > StatsSystem.Get().GetMax("modHighestValue", param.type)) {
        StatsSystem.Get().Max("modHighestValue", param.type, param.value);
    }
};
```

## Critical Connections

The StatsSystem is tightly integrated with the game's achievement system, unlocks, and progression mechanics. Many unlocks and game features are gated behind specific stat thresholds or achievements tracked through this system.
