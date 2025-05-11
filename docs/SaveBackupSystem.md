# SaveBackupSystem

## Overview

The `SaveBackupSystem` is a critical utility in Wildfrost that manages the creation of backup files for save data. This system provides a safety net for players by automatically creating backups of important save files at key moments, such as when starting a campaign or after saving game progress. These backups can prevent data loss and help players recover from corrupted save files.

## Class Details

`SaveBackupSystem` inherits from `GameSystem` and implements backup functionality for various save files.

### Constants and Static Variables

| Name | Type | Description |
|------|------|-------------|
| `backupCampaignTimer` | `const float` | The constant value (5 seconds) for the backup cooldown |
| `backupCampaignCooldown` | `static float` | Tracks the remaining cooldown time before another backup can be made |

### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `OnEnable()` | `void` | Subscribes to campaign start and save events |
| `OnDisable()` | `void` | Unsubscribes from campaign start and save events |
| `Update()` | `void` | Updates the backup cooldown timer |
| `CampaignStart()` | `static void` | Creates backups when a campaign starts |
| `CampaignSaved()` | `static void` | Creates a backup when a campaign is saved |
| `Backup(string filePath)` | `static void` | Creates a backup of a specific file |

## Functionality

The `SaveBackupSystem` operates by:

1. Creating backups of essential save files when a campaign starts
2. Creating additional backups when the game is saved
3. Enforcing a cooldown period between backups to prevent excessive file operations
4. Using the Easy Save 3 (ES3) system to handle the actual backup creation

## Usage Examples

### Basic Setup

```csharp
// In a game initialization script
SaveBackupSystem backupSystem = gameObject.AddComponent<SaveBackupSystem>();
```

### Manual Backup Creation

```csharp
// Create a backup of a specific save file
string saveFilePath = SaveSystem.folderName + "/CustomSave.sav";
SaveBackupSystem.Backup(saveFilePath);
```

### Checking Backup Status

```csharp
// Check if backups can be made (not in cooldown)
bool canBackup = SaveBackupSystem.backupCampaignCooldown <= 0f;

if (canBackup) 
{
    Debug.Log("Backup system is ready to create new backups");
}
else 
{
    Debug.Log($"Backup system is cooling down. Next backup in {SaveBackupSystem.backupCampaignCooldown} seconds");
}
```

## Integration with Other Systems

The SaveBackupSystem integrates with several game systems:

1. **SaveSystem**: Works with the main save system to back up files stored in the save folder.

2. **Campaign System**: Responds to campaign start and save events to create backups at appropriate times.

3. **Easy Save 3**: Uses ES3 functionality to handle the technical aspects of file backup.

4. **Event System**: Listens to `OnCampaignStart` and `OnCampaignSaved` events.

5. **GameMode System**: Respects the game mode's save settings by only backing up campaign files for modes that support saving.

## Modding Considerations

### Extending Backup Functionality

To extend the backup functionality:

1. Create a class that inherits from `SaveBackupSystem`.
2. Override or add methods to implement custom backup logic.
3. Subscribe to additional events to trigger backups at different moments.

Example:

```csharp
public class EnhancedBackupSystem : SaveBackupSystem
{
    public int maxBackupCopies = 5;
    public bool compressBackups = true;
    
    public void OnEnable()
    {
        base.OnEnable();
        // Subscribe to additional events
        Events.OnBattleEnd += BattleEnded;
    }
    
    public void OnDisable()
    {
        base.OnDisable();
        Events.OnBattleEnd -= BattleEnded;
    }
    
    public void BattleEnded(bool victory)
    {
        if (victory && backupCampaignCooldown <= 0f)
        {
            CreateRotatingBackup();
        }
    }
    
    public void CreateRotatingBackup()
    {
        // Create a backup with rotation (keeping only X most recent copies)
        // ...
        backupCampaignCooldown = backupCampaignTimer;
    }
}
```

### Adding Additional Backup Types

You can extend the system to back up additional types of data:

```csharp
public static void BackupUserSettings()
{
    Backup(SaveSystem.folderName + "/Settings.sav");
    Backup(SaveSystem.folderName + "/Controls.sav");
}
```

### Error Handling for Backup Recovery

Add custom error handling and recovery from backups:

```csharp
public static bool TryRestoreFromBackup(string filePath)
{
    try
    {
        if (ES3.BackupExists(filePath, SaveSystem.settings))
        {
            ES3.RestoreBackup(filePath, SaveSystem.settings);
            Debug.Log($"Restored from backup: {filePath}");
            return true;
        }
    }
    catch (System.Exception e)
    {
        Debug.LogError($"Failed to restore backup: {e.Message}");
    }
    return false;
}
```

## Backup File Storage

Backup files are stored alongside the original save files with an additional extension (typically `.bak`). The exact structure depends on the ES3 backup implementation, but generally follows this pattern:

```
SavesFolder/
  ├── Save.sav
  ├── Save.sav.bak
  ├── Stats.sav
  ├── Stats.sav.bak
  ├── History.sav
  ├── History.sav.bak
  ├── Campaign_Normal.sav
  └── Campaign_Normal.sav.bak
```

## Performance Considerations

The system includes a cooldown mechanism (`backupCampaignCooldown`) to prevent creating too many backups in a short period, which could impact performance. This cooldown is set to 5 seconds by default, meaning backups will not be created more frequently than once every 5 seconds, even if save events are triggered more often.
