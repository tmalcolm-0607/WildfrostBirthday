# BattleMusicSystem

## Overview
The `BattleMusicSystem` class manages the music during battles, including transitions, intensity changes, and miniboss introductions. It integrates with FMOD for audio playback and supports saving/loading its state.

## Key Properties

### Serialized Fields
- **`startingIntensity`**: The initial intensity of the battle music.
- **`normalIntensity`**: The default intensity level.
- **`winJingle`**: The event reference for the victory jingle.
- **`loseJingle`**: The event reference for the defeat jingle.
- **`minibossIntroDefault`**: The default event reference for miniboss introductions.
- **`minibossIntros`**: An array of `MinibossIntroRef` objects mapping minibosses to their intro events.
- **`minibossIntroDuration`**: The duration of miniboss intro music.

### Instance Properties
- **`minibossIntroLookup`**: A dictionary mapping miniboss names to their intro events.
- **`currentScene`**: The current scene.
- **`current`**: The current music event instance.
- **`minibossIntroInstance`**: The current miniboss intro event instance.
- **`intensity`**: The current intensity level of the music.
- **`bossEntered`**: A boolean indicating if a boss has entered the battle.
- **`volume`**: The current volume of the music.
- **`pitch`**: The current pitch of the music.
- **`targetVolume`**: The target volume for transitions.
- **`targetPitch`**: The target pitch for transitions.

## Key Methods

### Awake
Initializes the miniboss intro lookup dictionary:
```csharp
public void Awake()
{
    MinibossIntroRef[] array = minibossIntros;
    for (int i = 0; i < array.Length; i++)
    {
        MinibossIntroRef minibossIntroRef = array[i];
        minibossIntroLookup[minibossIntroRef.cardData.name] = minibossIntroRef.introEvent;
    }
}
```

### OnEnable
Registers event listeners for battle-related events:
```csharp
public void OnEnable()
{
    Events.OnSceneChanged += SceneChange;
    Events.OnBattlePhaseStart += BattlePhaseChange;
    Events.OnBattleEnd += BattleEnd;
    Events.OnEntityHit += EntityHit;
    Events.OnEntityMove += EntityMove;
    Events.OnMinibossIntro += MinibossIntro;
    Events.OnEntityChangePhase += EntityChangePhase;
    Check();
}
```

### OnDisable
Unregisters event listeners and stops the music:
```csharp
public void OnDisable()
{
    Events.OnSceneChanged -= SceneChange;
    Events.OnBattlePhaseStart -= BattlePhaseChange;
    Events.OnBattleEnd -= BattleEnd;
    Events.OnEntityHit -= EntityHit;
    Events.OnEntityMove -= EntityMove;
    Events.OnMinibossIntro -= MinibossIntro;
    Events.OnEntityChangePhase -= EntityChangePhase;
    StopMusic();
}
```

### Update
Handles music transitions and miniboss intro timing:
```csharp
public void Update()
{
    if (promptStartMiniboss > 0f)
    {
        promptStartMiniboss -= Time.deltaTime;
        if (promptStartMiniboss <= 0f)
        {
            StartMusic(References.GetCurrentArea().minibossMusicEvent);
            SetParam("bossHealth", 1f);
        }
    }
    // Additional logic for volume and pitch transitions...
}
```

## Usage
The `BattleMusicSystem` is used to:
- Play and manage battle music.
- Handle transitions for miniboss introductions.
- Adjust music intensity and parameters dynamically.

## References
- `BattleMusicSaveData`: Used for saving/loading the state of the music system.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.