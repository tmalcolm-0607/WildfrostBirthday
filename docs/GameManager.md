# GameManager

## Overview
The `GameManager` class is responsible for managing the overall state of the game. It handles initialization, application lifecycle events, and global game settings.

## Key Properties

### Constants
- **`CARD_WIDTH`**: The width of a card.
- **`CARD_HEIGHT`**: The height of a card.
- **`CARD_SIZE`**: A `Vector2` representing the size of a card.
- **`LARGE_UI`**: A constant for large UI scaling.

### Serialized Fields
- **`targetFrameRate`**: The target frame rate for the application.
- **`editorTargetFrameRate`**: The target frame rate for the editor.

### Static Properties
- **`tasksInProgress`**: Tracks the number of tasks currently in progress.
- **`init`**: Indicates whether the game has been initialized.
- **`End`**: A boolean indicating if the game has ended.
- **`paused`**: A boolean indicating if the game is paused.
- **`CultureInfo`**: The culture info used for formatting and localization.
- **`Busy`**: A boolean indicating if the game is busy (tasks in progress or not initialized).
- **`Ready`**: A boolean indicating if the game is ready (initialized).

## Key Methods

### Start
Initializes the game and sets up the application:
```csharp
public IEnumerator Start()
{
    Thread.CurrentThread.CurrentCulture = CultureInfo;
    Application.targetFrameRate = targetFrameRate;
    UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    Debug.Log("RELEASE = TRUE");
    yield return null;
    yield return new WaitUntil(() => Bootstrap.Count <= 0);
    init = true;
    Events.InvokeGameStart();
}
```

### OnApplicationQuit
Handles application quit events:
```csharp
public void OnApplicationQuit()
{
    Debug.Log(">>>> GAME END <<<<");
    End = true;
    Events.InvokeGameEnd();
}
```

### Quit
Quits the application:
```csharp
public static void Quit()
{
    Application.Quit();
}
```

## Usage
The `GameManager` is used to:
- Manage the game's initialization and lifecycle.
- Track global game states such as `Busy` and `Ready`.
- Handle application quit events.

## References
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.