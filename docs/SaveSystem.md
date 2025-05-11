# SaveSystem

`SaveSystem` is a core component in Wildfrost that handles saving and loading game data across different contexts, including campaign progress, battle states, and player statistics.

## Class Overview

`SaveSystem` inherits from `GameSystem` and provides a centralized service for data persistence throughout the game. It utilizes the Easy Save 3 (ES3) asset for file operations and implements backup mechanisms to prevent data corruption.

## Key Components

### Saver Class

The `SaveSystem.Saver` inner class handles file operations for specific save contexts:

| Property | Type | Description |
|----------|------|-------------|
| `baseFileName` | `string` | Template for save file names |
| `settings` | `ES3Settings` | Settings for ES3 save operations |

#### Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `SaveValue<T>` | `string key, T value, string folderName, string fileName` | Saves a value to a specific file |
| `LoadValue<T>` | `string key, string folderName, T defaultValue, string fileName` | Loads a value with fallback to default |
| `FileExists` | `string folderName, string fileName` | Checks if a save file exists |
| `KeyExists` | `string key, string folderName, string fileName` | Checks if a key exists in a save file |
| `Delete` | `string folderName, string fileName` | Deletes a save file |
| `DeleteKey` | `string key, string folderName, string fileName` | Deletes a key from a save file |
| `CheckBackup` | `string folderName, string fileName` | Verifies data integrity and restores from backup if needed |

### Static Properties

| Property | Type | Description |
|----------|------|-------------|
| `instance` | `SaveSystem` | Singleton instance of the system |
| `profileFolder` | `string` | Folder for player profiles |
| `folderName` | `string` | Current save data folder |
| `Profile` | `string` | Current active profile |
| `Enabled` | `bool` | Whether saving is currently enabled |
| `progressSaver` | `Saver` | Handles meta-progression saves |
| `campaignSaver` | `Saver` | Handles campaign saves |
| `battleSaver` | `Saver` | Handles battle state saves |
| `statsSaver` | `Saver` | Handles statistics saves |
| `historySaver` | `Saver` | Handles player history saves |

## Save Types

The system handles multiple types of saves:

| Save Type | Description | Persistence |
|-----------|-------------|-------------|
| **Progress** | Meta-progression data (unlocks, etc.) | Persists across campaigns |
| **Campaign** | Current campaign state and map | Persists until campaign ends |
| **Battle** | Current battle state and entities | Persists until battle ends |
| **Stats** | Player statistics and achievements | Persists across campaigns |
| **History** | Record of past campaigns | Persists indefinitely |

## Key Methods

### Setup and Profiles

| Method | Description |
|--------|-------------|
| `Initialize()` | Sets up the save system and initializes savers |
| `SetProfile(string profile)` | Changes the current player profile |
| `GetProfilePath()` | Gets the file path for the current profile |
| `CreateDefaultSettings()` | Creates default ES3 save settings |

### Campaign Save Methods

| Method | Description |
|--------|-------------|
| `SaveCampaignData<T>(GameModeData gameMode, string key, T value)` | Saves campaign-specific data |
| `LoadCampaignData<T>(GameModeData gameMode, string key, T defaultValue)` | Loads campaign-specific data |
| `DeleteCampaignData(GameModeData gameMode, string key)` | Deletes campaign-specific data |
| `DeleteAllCampaignData(GameModeData gameMode)` | Deletes all data for a campaign |

### Progress Save Methods

| Method | Description |
|--------|-------------|
| `SaveProgressData<T>(string key, T value)` | Saves persistent progression data |
| `LoadProgressData<T>(string key, T defaultValue)` | Loads persistent progression data |
| `DeleteProgressData(string key)` | Deletes specific progression data |

### Battle Save Methods

| Method | Description |
|--------|-------------|
| `SaveBattleData<T>(string key, T value)` | Saves current battle state |
| `LoadBattleData<T>(string key, T defaultValue)` | Loads battle state |
| `DeleteBattleData(string key)` | Deletes specific battle data |
| `DeleteAllBattleData()` | Clears all battle save data |

### Stats and History

| Method | Description |
|--------|-------------|
| `SaveStats<T>(string key, T value)` | Saves player statistics |
| `LoadStats<T>(string key, T defaultValue)` | Loads player statistics |
| `SaveHistory<T>(string key, T value)` | Saves campaign history |
| `LoadHistory<T>(string key, T defaultValue)` | Loads campaign history |

## Usage Examples

### Saving and Loading Campaign Data

```csharp
// Save campaign data
GameModeData gameMode = Campaign.Data.GameMode;
PlayerData playerData = new PlayerData(/* parameters */);
SaveSystem.SaveCampaignData(gameMode, "player", playerData);

// Load campaign data
PlayerData loadedData = SaveSystem.LoadCampaignData(gameMode, "player", null);
```

### Managing Progress Data

```csharp
// Save progression unlock
List<string> unlockedItems = new List<string> { "Item1", "Item2" };
SaveSystem.SaveProgressData("unlockedItems", unlockedItems);

// Load progression data
List<string> savedUnlocks = SaveSystem.LoadProgressData("unlockedItems", new List<string>());
```

### Working with Battle State

```csharp
// Save battle state
BattleSaveData battleData = new BattleSaveData(/* battle state */);
SaveSystem.SaveBattleData("currentBattle", battleData);

// Check if battle save exists
bool hasBattleSave = SaveSystem.BattleSaveExists();

// Load battle state
BattleSaveData loadedBattle = SaveSystem.LoadBattleData("currentBattle", null);
```

## Integration with Other Systems

- **GameModeData**: Determines campaign-specific save folders
- **BattleSaveSystem**: Higher-level system for battle state serialization
- **MetaprogressionSystem**: Uses progress saves for persistent unlocks
- **StatsSystem**: Uses stats saves for achievements and records
- **Campaign**: Uses campaign saves for run persistence

## Modding Considerations

When extending the save system for mods:

1. **Custom Save Keys**: Use unique prefixes for custom save data to avoid conflicts
2. **Data Compatibility**: Ensure serializable data structures for custom content
3. **Profile Awareness**: Use the current profile path for mod-specific saves
4. **Backup Handling**: Implement error handling for data corruption

Example of adding mod-specific save data:

```csharp
// Save mod data to the progress system (persists across campaigns)
public void SaveModData<T>(string modId, string key, T value)
{
    string combinedKey = $"mod_{modId}_{key}";
    SaveSystem.SaveProgressData(combinedKey, value);
}

// Load mod data
public T LoadModData<T>(string modId, string key, T defaultValue)
{
    string combinedKey = $"mod_{modId}_{key}";
    return SaveSystem.LoadProgressData(combinedKey, defaultValue);
}
```

## Error Handling

The SaveSystem includes robust error handling to prevent save corruption:

1. **Automatic Backups**: ES3 creates automatic backups of save files
2. **Corruption Detection**: Attempts to detect corrupt data files
3. **Backup Restoration**: Automatically restores from backups when corruption is detected
4. **Fallback Values**: Uses default values when data cannot be restored
