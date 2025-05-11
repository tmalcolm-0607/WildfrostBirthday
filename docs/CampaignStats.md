# CampaignStats

`CampaignStats` is a core component responsible for tracking and managing statistics throughout a campaign in Wildfrost. It provides a flexible system for recording, incrementing, and querying various metrics during gameplay.

## Class Properties

| Property | Type | Description |
|----------|------|-------------|
| `time` | `float` | The accumulated time (in seconds, up to 3600) for the current campaign. |
| `hours` | `int` | Full hours accumulated for the current campaign. |
| `add` | `Dictionary<string, Dictionary<string, int>>` | Dictionary storing additive statistics in a nested structure. |
| `max` | `Dictionary<string, Dictionary<string, int>>` | Dictionary storing maximum/minimum value statistics in a nested structure. |

## Core Functionality

The `CampaignStats` class tracks statistics in two main categories:
1. Additive stats (`add`) - Values that accumulate over time (e.g., damage dealt, gold collected)
2. Maximum/minimum stats (`max`) - Values that track extremes (e.g., highest damage dealt, fastest completion time)

Stats are stored in a two-level dictionary structure, where:
- The outer key is the stat category (e.g., "damageDealt", "winsWithTribe")
- The inner key can be subcategories or specific identifiers (e.g., "fire" damage, specific card names)
- The value is the numerical stat value

## Key Methods

### Modifying Stats

| Method | Parameters | Description |
|--------|------------|-------------|
| `Add(string stat, int value)` | `stat`: Stat category<br>`value`: Amount to add | Adds value to a stat with empty subcategory. |
| `Add(string stat, string key, int value)` | `stat`: Stat category<br>`key`: Subcategory<br>`value`: Amount to add | Adds value to a specific stat subcategory. |
| `Max(string stat, int value)` | `stat`: Stat category<br>`value`: Value to compare | Sets the stat to the higher of current or new value. |
| `Max(string stat, string key, int value)` | `stat`: Stat category<br>`key`: Subcategory<br>`value`: Value to compare | Sets the specific stat subcategory to the higher of current or new value. |
| `Min(string stat, int value)` | `stat`: Stat category<br>`value`: Value to compare | Sets the stat to the lower of current or new value. |
| `Min(string stat, string key, int value)` | `stat`: Stat category<br>`key`: Subcategory<br>`value`: Value to compare | Sets the specific stat subcategory to the lower of current or new value. |
| `Set(string stat, int value)` | `stat`: Stat category<br>`value`: Value to set | Sets a stat to a specific value. |
| `Set(string stat, string key, int value)` | `stat`: Stat category<br>`key`: Subcategory<br>`value`: Value to set | Sets a specific stat subcategory to a specific value. |
| `SetBest(string stat, int value)` | `stat`: Stat category<br>`value`: Value to set | Sets a max/min stat to a specific value. |
| `SetBest(string stat, string key, int value)` | `stat`: Stat category<br>`key`: Subcategory<br>`value`: Value to set | Sets a specific max/min stat subcategory to a specific value. |
| `Delete(string stat)` | `stat`: Stat category to delete | Removes a stat category from the `add` dictionary. |
| `DeleteBest(string stat)` | `stat`: Stat category to delete | Removes a stat category from the `max` dictionary. |

### Querying Stats

| Method | Parameters | Description |
|--------|------------|-------------|
| `Get(string stat, int defaultValue)` | `stat`: Stat category<br>`defaultValue`: Value returned if stat doesn't exist | Gets the value of a stat with empty subcategory. |
| `Get(string stat, string key, int defaultValue)` | `stat`: Stat category<br>`key`: Subcategory<br>`defaultValue`: Value returned if stat doesn't exist | Gets the value of a specific stat subcategory. |
| `Best(string stat, int defaultValue)` | `stat`: Stat category<br>`defaultValue`: Value returned if stat doesn't exist | Gets the maximum value across all subcategories in a stat. |
| `Best(string stat, string key, int defaultValue)` | `stat`: Stat category<br>`key`: Subcategory<br>`defaultValue`: Value returned if stat doesn't exist | Gets the maximum value for a specific stat subcategory. |
| `Count(string stat)` | `stat`: Stat category | Gets the sum of all subcategory values in a stat. |

### Other Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `Change(string stat, int value, ref Dictionary<string, Dictionary<string, int>> values, Func<int, int, int> action)` | Internal method for modifying stats | The main implementation for all stat modifications. |
| `Clone()` | None | Creates a deep copy of the CampaignStats object. |

## Usage Examples

### Tracking Battle Damage

```csharp
// Player deals 5 fire damage
statsObject.Add("damageDealt", "fire", 5);

// Track maximum damage dealt in a single hit
statsObject.Max("highestDamageDealt", "fire", 5);

// Get total damage dealt
int totalDamage = statsObject.Count("damageDealt");

// Get highest fire damage dealt
int highestFireDamage = statsObject.Best("highestDamageDealt", "fire", 0);
```

### Tracking Win Streaks

```csharp
// Increment win streak
statsObject.Add("currentWinStreak", 1);

// Update best win streak record
statsObject.Max("bestWinStreak", statsObject.Get("currentWinStreak", 1));

// Reset win streak on loss
statsObject.Set("currentWinStreak", 0);
```

### Tracking Campaign Time

```csharp
// Time is automatically tracked in the StatsSystem.Update method:
stats.time += Time.deltaTime;
if (stats.time >= 3600f)
{
    stats.hours++;
    stats.time -= 3600f;
}

// Get total seconds (for timing purposes)
int totalSeconds = Mathf.FloorToInt(stats.time) + (stats.hours * 3600);
```

## Integration with Other Systems

- `StatsSystem`: Manages the current campaign's stats and handles stat updates during gameplay.
- `OverallStatsSystem`: Manages persistent stats across multiple campaigns and saves them between game sessions.
- `GameStatData`: Used to display stats in a formatted way to the player.
- `Events.OnStatChanged`: Triggered whenever a stat value changes, allowing other systems to react accordingly.

## Saving and Loading

The `CampaignStats` class can be serialized and deserialized by the game's save system, as it implements the `[Serializable]` attribute. This allows stats to be saved between play sessions.

Stats are typically saved:
1. When a campaign is saved via `SaveSystem.SaveCampaignData()`
2. When the player completes a run via `OverallStatsSystem.CampaignEnd()`

## Common Stat Categories

| Category | Description | Example Keys |
|----------|-------------|-------------|
| `damageDealt` | Damage dealt by player | Damage types: `"fire"`, `"physical"`, etc. |
| `damageTaken` | Damage taken by player | Damage types |
| `damageBlocked` | Damage blocked by shields | Damage types |
| `results` | Campaign results | `"win"`, `"lose"`, `"trueWin"` |
| `currentWinStreak` | Current consecutive wins | Empty string key (`""`) |
| `bestWinStreak` | Best consecutive win streak | Empty string key (`""`) |
| `winsWithTribe` | Wins with specific tribes | Tribe IDs |
| `winsWithCardInDeck` | Wins with specific cards | Card names |
| `cardsHit` | Number of times cards were hit | Card names |
| `cardsSacrificed` | Cards sacrificed | Card names |
| `cardsPlayed` | Cards played | Card names |
| `goldSpent` | Gold spent | Shop types |
| `goldGained` | Gold gained | Source types |
| `battlesWon` | Battles won | Empty string key or battle names |
