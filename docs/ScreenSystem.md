# ScreenSystem

`ScreenSystem` is a core system in Wildfrost that manages the game's display settings, screen resolution, and display modes.

## Class Overview

`ScreenSystem` inherits from `GameSystem` and provides functionality to control and switch between different screen modes (windowed, fullscreen, borderless), manage resolution settings, and handle vertical sync and framerate targets.

## Properties

### Display Properties

| Property | Type | Description |
|----------|------|-------------|
| `instance` | `ScreenSystem` | Static singleton instance of the system |
| `windowedWidth` | `int` | Width in pixels for windowed mode |
| `windowedHeight` | `int` | Height in pixels for windowed mode |
| `windowedMode` | `FullScreenMode` | The mode used for windowed display (usually `FullScreenMode.Windowed`) |
| `fullMode` | `FullScreenMode` | The mode used for fullscreen display (usually `FullScreenMode.FullScreenWindow`) |
| `_displayIndex` | `int` | Index of the current display |
| `current` | `int` | Current display mode (0 = windowed, 1 = fullscreen, 2 = borderless) |
| `vsync` | `int` | Vertical sync setting (0 = off, 1 = on) |
| `targetFramerate` | `int` | Target framerate setting index |

### Calculated Properties

| Property | Type | Description |
|----------|------|-------------|
| `fullScreenWidth` | `int` | Width of the current display in fullscreen mode |
| `fullScreenHeight` | `int` | Height of the current display in fullscreen mode |
| `displayIndex` | `int` | Safe accessor for display index (prevents out-of-range errors) |
| `display` | `Display` | The current Unity Display object |
| `IsWindowed` | `bool` | Static property that checks if the current mode is windowed |

## Key Methods

### Mode Setting Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `Set` | `int mode` | Sets the display mode (0 = windowed, 1 = fullscreen, 2 = borderless) |
| `SetWindowed` | `int forceWidth = 0, int forceHeight = 0` | Switches to windowed mode with optional custom resolution |
| `SetFull` | `int forceWidth = 0, int forceHeight = 0` | Switches to exclusive fullscreen mode with optional custom resolution |
| `SetBorderless` | `int forceWidth = 0, int forceHeight = 0` | Switches to borderless fullscreen with optional custom resolution |

### Performance Setting Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `SetTargetFramerate` | `int mode` | Sets the target framerate (0 = uncapped, 1 = 30fps, 2 = 60fps, 3 = 120fps, 4 = 240fps) |
| `SetVsync` | `int mode` | Enables/disables vertical sync (0 = off, 1 = on) |

### Static Resolution Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `SetResolutionFullscreen` | `int width, int height` | Static wrapper for setting fullscreen resolution |
| `SetResolutionBorderless` | `int width, int height` | Static wrapper for setting borderless resolution |
| `SetResolutionWindowed` | `int width, int height` | Static wrapper for setting windowed resolution |

## Event Handling

| Method | Event | Description |
|--------|-------|-------------|
| `SettingChanged` | `Events.OnSettingChanged` | Handles setting changes from other parts of the game |

## Usage Examples

### Changing Display Mode

```csharp
// Switch to fullscreen
ScreenSystem.instance.Set(1);

// Switch to borderless
ScreenSystem.instance.Set(2);

// Switch to windowed
ScreenSystem.instance.Set(0);
```

### Setting Custom Resolution

```csharp
// Set custom windowed resolution
ScreenSystem.SetResolutionWindowed(1280, 720);

// Set custom fullscreen resolution
ScreenSystem.SetResolutionFullscreen(1920, 1080);
```

### Changing Performance Settings

```csharp
// Set target framerate to 60fps
ScreenSystem.SetTargetFramerate(2);

// Enable vsync
ScreenSystem.SetVsync(1);
```

### Toggling Between Fullscreen and Windowed

```csharp
// Toggle between current modes
if (ScreenSystem.IsWindowed)
{
    Settings.Save("DisplayMode", 2); // Switch to borderless
}
else
{
    Settings.Save("DisplayMode", 0); // Switch to windowed
}
```

## Integration with Other Systems

- **Settings**: Loads and saves display settings between game sessions
- **Events.OnSettingChanged**: Responds to setting changes from UI or other systems
- **SetSettingInt**: UI component for changing integer-based settings

## Modding Considerations

When creating mods that affect the display system:

1. **Resolution Handling**: Use the existing ScreenSystem methods to change resolution
2. **UI Scaling**: Consider how changes in resolution affect UI elements
3. **Custom Display Modes**: Add new display modes by extending the Set() method
4. **Settings Integration**: Update the Settings system when changing display configurations

Example of adding a custom ultrawide mode:

```csharp
// Add a custom ultrawide mode
public static void SetUltrawide()
{
    // Save current settings
    if (ScreenSystem.IsWindowed)
    {
        ScreenSystem.instance.windowedWidth = Screen.width;
        ScreenSystem.instance.windowedHeight = Screen.height;
        ScreenSystem.instance.windowedMode = Screen.fullScreenMode;
    }
    
    // Set ultrawide resolution
    int ultrawideWidth = 3440;
    int ultrawideHeight = 1440;
    
    // Apply settings
    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    Screen.SetResolution(ultrawideWidth, ultrawideHeight, FullScreenMode.FullScreenWindow);
}
```

## Key Interactions

- **Alt+Enter Shortcut**: The system has a built-in listener for Alt+Enter to toggle between windowed and fullscreen modes
- **Display Changes**: The system preserves previous display settings when switching modes
- **Settings Persistence**: Display settings are saved and loaded via the Settings system
