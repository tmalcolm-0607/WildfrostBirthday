# OverallStatsSystem

`OverallStatsSystem` is a core system in Wildfrost that tracks and persists player statistics across multiple campaign runs.

## Class Overview

`OverallStatsSystem` inherits from `GameSystem` and serves as the long-term statistics tracker for the player's entire game history, as opposed to the `StatsSystem` which tracks statistics for a single campaign run.

## Properties

| Property | Type | Description |
|----------|------|-------------|
| `instance` | `OverallStatsSystem` | Static instance of the system for global access |
| `stats` | `CampaignStats` | The overall statistics across all campaigns |

## Key Methods

### Core Functionality

| Method | Description |
|--------|-------------|
| `Get()` | Static accessor to get the overall `CampaignStats` |
| `GameStart()` | Loads statistics data when a game is started |
| `CampaignEnd(Campaign.Result result, CampaignStats stats, PlayerData playerData)` | Processes and merges campaign statistics at the end of a run |
| `ResetWinStreak()` | Resets win streak counters when a run is lost or restarted |
| `Save()` | Persists overall statistics to disk |

### Static Utility Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `Combine` | `CampaignStats stats, CampaignStats other` | Merges two sets of statistics |
| `Change` | `string stat, string key, int value, ref Dictionary<string, Dictionary<string, int>> values, Func<int, int, int> action` | Helper method for modifying statistics |
| `Add` | `int value, int add` | Adds two values for add-based statistics |
| `Max` | `int value, int max` | Takes the maximum of two values for max-based statistics |

## Event Handlers

| Method | Event | Description |
|--------|-------|-------------|
| `CampaignEnd` | `Events.OnCampaignEnd` | Records campaign results and updates overall stats |
| `GameStart` | `Events.OnGameStart` | Loads overall statistics at game start |

## Campaign Results Tracking

The system tracks different campaign outcomes:

| Result Type | Description | Stats Updated |
|------------|-------------|---------------|
| `Campaign.Result.Win` | Regular victory | `results.win`, win streaks, win times, tribe wins |
| `Campaign.Result.Win` + `trueWin` | True ending victory | `results.trueWin`, true win streaks |
| `Campaign.Result.Lose` | Game over | `results.lose`, resets win streaks |
| `Campaign.Result.Restart` | Manual restart | `results.restart`, resets win streaks |

For game mode specific tracking:

- **Main Game Mode**: Tracks win streaks and best times
- **Daily Run**: Tracks separate daily run statistics

## Usage Examples

### Accessing Overall Stats

```csharp
// Get the overall stats
CampaignStats overallStats = OverallStatsSystem.Get();

// Check win count
int totalWins = overallStats.Get("results", "win");
int trueWins = overallStats.Get("results", "trueWin");

// Check best win streak
int bestWinStreak = overallStats.GetMax("bestWinStreak");
```

### Tracking Custom Statistics

```csharp
// Add custom stats when a mod-specific event happens
void OnModAchievement(string achievementType)
{
    // Get the overall stats
    CampaignStats stats = OverallStatsSystem.Get();
    
    // Record the achievement
    stats.Add("modAchievements", achievementType, 1);
    
    // Save the updated stats
    OverallStatsSystem.instance.Save();
}
```

## Statistics Categories

The system maintains multiple categories of statistics:

| Category | Examples |
|----------|----------|
| **Results** | Wins, losses, restarts, true wins |
| **Streaks** | Current win streak, best win streak |
| **Tribes** | Wins/losses with specific tribes |
| **Cards** | Wins with specific cards in deck |
| **Daily Runs** | Daily challenge results |
| **Time** | Total play time, best win times |

## Integration with Other Systems

- **StatsSystem**: Provides campaign-specific stats that are merged into overall stats
- **CampaignStats**: The data structure that holds all statistics
- **SaveSystem**: Persists statistics between game sessions using `SaveStatsData`
- **Events**: Connects to game events to track campaign outcomes
- **Campaign**: Accesses campaign results and data

## Modding Considerations

When extending the overall stats system:

1. **Custom Statistics**: Add new statistics categories for mod-specific tracking
2. **Integration Points**: Connect to `Events.OnCampaignEnd` to record mod-specific outcomes
3. **Persistence**: Use the existing Save() method to ensure stats are saved
4. **UI Integration**: Create UI elements to display mod statistics

Example of adding mod-specific statistic tracking:

```csharp
// Connect to game events
Events.OnCampaignEnd += (Campaign.Result result, CampaignStats campaignStats, PlayerData playerData) => {
    // Get the overall stats
    CampaignStats overallStats = OverallStatsSystem.Get();
    
    // Add mod-specific statistics
    if (ModHasCustomContent(playerData)) {
        overallStats.Add("modContentUsed", "runCount", 1);
        
        if (result == Campaign.Result.Win) {
            overallStats.Add("modContentUsed", "winCount", 1);
        }
    }
    
    // No need to call Save() as it will be called by the main CampaignEnd handler
};
```

## Key Differences from StatsSystem

While `StatsSystem` tracks statistics for the current campaign run only, `OverallStatsSystem`:
- Persists across multiple game sessions and campaign runs
- Tracks career totals and records (total wins, best streaks)
- Combines data from multiple campaigns
- Is saved in a separate location using `SaveStatsData` rather than `SaveCampaignData`
