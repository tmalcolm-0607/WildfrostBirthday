# AmbienceSystem

## Overview
The `AmbienceSystem` class manages ambient sound effects in the game. It plays, stops, and adjusts parameters for ambient audio based on the current scene.

## Key Properties

### Serialized Fields
- **`validScenes`**: An array of scene names where ambient audio is allowed (e.g., "Battle", "Event").

### Static Properties
- **`current`**: The current `EventInstance` for the ambient audio.

## Key Methods

### OnEnable
Registers the `SceneChanged` event listener:
```csharp
public void OnEnable()
{
    Events.OnSceneChanged += SceneChanged;
}
```

### OnDisable
Unregisters the `SceneChanged` event listener and stops the current audio:
```csharp
public void OnDisable()
{
    Events.OnSceneChanged -= SceneChanged;
    Stop();
}
```

### SceneChanged
Handles scene changes and plays ambient audio if the scene is valid:
```csharp
public void SceneChanged(Scene scene)
{
    Stop();
    if (IArrayExt.Contains<string>(validScenes, scene.name))
    {
        Play(References.GetCurrentArea().ambienceEvent);
    }
}
```

### Play
Plays an ambient audio event:
```csharp
public static void Play(EventReference eventId)
{
    Play(eventId.Guid);
}

public static void Play(GUID eventGUID)
{
    try
    {
        current = RuntimeManager.CreateInstance(eventGUID);
        current.start();
    }
    catch (EventNotFoundException message)
    {
        UnityEngine.Debug.LogWarning(message);
    }
}
```

### SetParam
Sets a parameter for the current audio event:
```csharp
public static void SetParam(string name, float value)
{
    if (IsRunning(current))
    {
        current.setParameterByName(name, value);
    }
}
```

### Stop
Stops the current audio event:
```csharp
public static void Stop(FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
{
    if (IsRunning(current))
    {
        current.stop(stopMode);
        current.release();
    }
}
```

### IsRunning
Checks if an audio event is currently running:
```csharp
public static bool IsRunning(EventInstance instance)
{
    if (instance.isValid())
    {
        instance.getPlaybackState(out var state);
        if (state != PLAYBACK_STATE.STOPPED)
        {
            return true;
        }
    }
    return false;
}
```

## Usage
The `AmbienceSystem` is used to:
- Play ambient audio in specific scenes.
- Adjust audio parameters dynamically.
- Stop ambient audio when leaving a scene.

## References
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.