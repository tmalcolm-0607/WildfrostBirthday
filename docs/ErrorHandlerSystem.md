# ErrorHandlerSystem

## Overview

The `ErrorHandlerSystem` is a critical utility system in Wildfrost that manages error handling, logging, and user notification when exceptions occur during gameplay. This system provides a safety net for catching and logging errors, while also offering configurable options for how errors are presented to the player. It helps both developers during debugging and players encountering issues by providing clear error feedback.

## Class Details

`ErrorHandlerSystem` inherits from `GameSystem` and implements functionality for intercepting Unity's logging system to handle exceptions.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `displayErrors` | `bool` | Controls whether to display detailed error messages on screen |
| `errorDisplay` | `GameObject` | The UI element that displays detailed error information |
| `errorText` | `TMP_InputField` | Text field that shows the actual error message and stack trace |
| `freezeTimeScale` | `bool` | If true, pauses the game when an error occurs by setting time scale to 0 |
| `sfxEvent` | `EventReference` | FMOD sound effect played when an error occurs |
| `showPersistentMessage` | `bool` | Controls whether to show a simpler persistent error notification |
| `persistentMessage` | `GameObject` | The UI element for displaying a persistent error indicator |
| `timeScalePre` | `float` | Stores the time scale value before pausing the game |
| `errorCount` | `static int` | Tracks the number of errors that have occurred |
| `path` | `static string` | The file path where error logs are stored (`Application.persistentDataPath + "/Errors.log"`) |

### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `OnEnable()` | `void` | Subscribes to Unity's log message event when the system is enabled |
| `OnDisable()` | `void` | Unsubscribes from Unity's log message event when the system is disabled |
| `HandleLog(string log, string stacktrace, LogType type)` | `void` | Core method that processes log messages and handles exceptions |
| `ShowError(string text)` | `void` | Displays the error UI with the provided error text |
| `HideError()` | `void` | Hides the detailed error UI and restores the time scale |
| `ExitGame()` | `void` | Quits the game (typically called from error UI) |
| `ShowPersistentMessage()` | `void` | Shows the simplified persistent error notification |
| `HidePersistentMessage()` | `void` | Hides the persistent error notification |

## Usage Examples

### Basic Setup

```csharp
// In a game initialization script
ErrorHandlerSystem errorHandler = gameObject.AddComponent<ErrorHandlerSystem>();

// Configure the error handler
errorHandler.displayErrors = true;  // Show detailed errors in development
errorHandler.freezeTimeScale = true;  // Pause the game on errors
errorHandler.showPersistentMessage = true;  // Show persistent notification
```

### Custom Error UI Integration

```csharp
// Create a custom UI for errors
GameObject errorPanel = new GameObject("ErrorPanel");
TMP_InputField errorTextField = errorPanel.AddComponent<TMP_InputField>();

// Configure the error handler to use the custom UI
ErrorHandlerSystem errorHandler = FindObjectOfType<ErrorHandlerSystem>();
errorHandler.errorDisplay = errorPanel;
errorHandler.errorText = errorTextField;
```

### Reading Error Logs

```csharp
// Utility function to access error logs
public string GetErrorLogs()
{
    if (File.Exists(ErrorHandlerSystem.path))
    {
        return File.ReadAllText(ErrorHandlerSystem.path);
    }
    return "No errors recorded.";
}
```

## Integration with Other Systems

The ErrorHandlerSystem integrates with several game systems:

1. **Unity Logging System**: Hooks into Unity's `Application.logMessageReceived` to capture exceptions.

2. **UI System**: Uses UI elements to display error messages to the player.

3. **Time System**: Can pause the game by setting `Time.timeScale` to 0 when errors occur.

4. **Audio System**: Plays a sound effect through FMOD when errors occur.

5. **File System**: Writes error logs to a file in the persistent data path.

6. **Game Manager**: Can call `GameManager.Quit()` to exit the game when needed.

## Modding Considerations

### Extending Error Handling

To extend the error handling functionality:

1. Create a class that inherits from `ErrorHandlerSystem`.
2. Override methods like `HandleLog()` to implement custom error processing logic.
3. Add additional error types or specialized handling for specific exceptions.

Example:

```csharp
public class EnhancedErrorHandler : ErrorHandlerSystem
{
    public bool sendErrorReports = true;
    public string reportEndpoint = "https://your-analytics-service.com/report";
    
    // Override to add custom error reporting
    public new void HandleLog(string log, string stacktrace, LogType type)
    {
        // Call the base implementation first
        base.HandleLog(log, stacktrace, type);
        
        // Add custom error reporting
        if (type == LogType.Exception && sendErrorReports)
        {
            StartCoroutine(SendErrorReport(log, stacktrace));
        }
    }
    
    private IEnumerator SendErrorReport(string log, string stacktrace)
    {
        // Create web request and send error data to your service
        // ...
        yield return null;
    }
}
```

### Custom Error UI

You can customize the error UI by:

1. Creating custom prefabs for error displays.
2. Assigning them to the `errorDisplay` and `persistentMessage` properties.
3. Implementing custom animations or transitions for error notification.

## Error Log Format

Errors are logged to a file with the following format:

```
[DateTime] ErrorMessage
StackTrace
```

The log file is stored at `Application.persistentDataPath + "/Errors.log"` and can be retrieved for debugging or support purposes.

## Best Practices

1. **Development vs Production**: Consider disabling detailed error displays in production builds while keeping error logging enabled.

2. **Error Recovery**: When extending this system, implement recovery mechanisms for non-critical errors.

3. **User Guidance**: Provide clear instructions in error messages that guide users on next steps or troubleshooting.

4. **Log Management**: Implement log rotation or cleanup to prevent log files from growing too large over time.
