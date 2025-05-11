# SfxSystem

## Overview
The `SfxSystem` class manages sound effects in the game. It provides functionality for playing, managing, and controlling sound events, including cooldowns and event-specific parameters.

## Key Properties

### Static Properties
- **`instance`**: A static reference to the active `SfxSystem` instance.
- **`DRAG_THRESHOLD`**: A constant defining the drag threshold for triggering sound effects.
- **`cooldownTimers`**: A dictionary mapping sound event names to their cooldown durations.
- **`cooldowns`**: A dictionary of `Cooldown` objects for managing sound event cooldowns.

### Serialized Fields
- **`pathRevealPitch`**: An `AnimationCurve` for controlling pitch during path reveal events.
- **`test`**: An `EventReference` for testing sound events.

### Instance Properties
- **`running`**: A list of currently running sound event instances.
- **`dragging`**: The entity currently being dragged.
- **`draggingItem`**: The item currently being dragged.
- **`dragTrigger`**: A boolean indicating if a drag event has been triggered.
- **`dragFrom`**: The starting position of a drag event.
- **`itemAim`**: An `EventInstance` for item aiming sound effects.
- **`revealActionsInQueue`**: The number of reveal actions queued for sound effects.
- **`flipMulti`**: An `EventInstance` for multi-flip sound effects.
- **`transitionSnow`**: An `EventInstance` for snow transition sound effects.
- **`goldCounter`**: An `EventInstance` for gold counter sound effects.
- **`muncherFeed`**: An `EventInstance` for muncher feed sound effects.
- **`drawMulti`**: An `EventInstance` for multi-draw sound effects.
- **`townProgressionLoop`**: An `EventInstance` for town progression loop sound effects.
- **`goldDisplay`**: A reference to the `GoldDisplay` component.

## Key Methods

### OnEnable
Registers event listeners for various game events:
```csharp
public void OnEnable()
{
    instance = this;
    Events.OnEntityHit += EntityHit;
    Events.OnEntityHover += EntityHover;
    Events.OnEntityKilled += EntityKilled;
    Events.OnEntitySelect += EntitySelect;
    Events.OnEntityDrag += EntityDrag;
    Events.OnEntityRelease += EntityRelease;
    Events.OnEntityPlace += EntityPlace;
    Events.OnEntityFlipUp += EntityFlipUp;
}
```

### OnDisable
Unregisters event listeners:
```csharp
public void OnDisable()
{
    Events.OnEntityHit -= EntityHit;
    Events.OnEntityHover -= EntityHover;
    Events.OnEntityKilled -= EntityKilled;
    Events.OnEntitySelect -= EntitySelect;
    Events.OnEntityDrag -= EntityDrag;
    Events.OnEntityRelease -= EntityRelease;
    Events.OnEntityPlace -= EntityPlace;
    Events.OnEntityFlipUp -= EntityFlipUp;
}
```

## Usage
The `SfxSystem` is used to:
- Play sound effects for various game events.
- Manage sound effect cooldowns to prevent overlapping sounds.
- Control sound effect parameters dynamically.

## References
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.