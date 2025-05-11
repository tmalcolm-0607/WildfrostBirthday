# BattleLogSystem

## Overview
The `BattleLogSystem` class manages the logging of events during battles. It tracks actions such as hits, kills, and status effects, and provides localized strings for displaying these events in the game's UI.

## Key Properties
- **`list`**: A list of `BattleLog` objects representing logged events.
- **`damageTypes`**: A dictionary mapping damage types to their corresponding sprite names.
- **Localized Strings**: Various `LocalizedString` fields for different types of battle events, such as:
  - `logTurnKey`: Logs the start of a turn.
  - `logHitKey`: Logs a hit event.
  - `logDamageKey`: Logs damage dealt.
  - `logSpecialDamageKey`: Logs special damage types.
  - `logDestroyKey`: Logs destruction events.
  - `logConsumedKey`: Logs consumption events.
  - `logEatenKey`: Logs eating events.
  - `logSacrificedKey`: Logs sacrifice events.
  - `logBlockKey`: Logs block events.
  - `logStatusKey`: Logs status effects applied.
  - `logHealKey`: Logs healing events.
  - `logBattleWinKey`: Logs battle victories.
  - `logBattleLoseKey`: Logs battle losses.

## Key Methods

### OnEnable
Registers event listeners for various battle events:
```csharp
public void OnEnable()
{
    Events.OnBattlePhaseStart += BattlePhaseStart;
    Events.OnBattleTurnEnd += TurnEnd;
    Events.OnEntityHit += Hit;
    Events.OnEntityMove += EntityMove;
    Events.OnStatusEffectApplied += StatusApplied;
    Events.OnEntityPostHit += PostHit;
    Events.OnEntityKilled += EntityKilled;
    Events.OnEntityFlee += EntityFlee;
}
```

### OnDisable
Unregisters event listeners:
```csharp
public void OnDisable()
{
    Events.OnBattlePhaseStart -= BattlePhaseStart;
    Events.OnBattleTurnEnd -= TurnEnd;
    Events.OnEntityHit -= Hit;
    Events.OnEntityMove -= EntityMove;
    Events.OnStatusEffectApplied -= StatusApplied;
    Events.OnEntityPostHit -= PostHit;
    Events.OnEntityKilled -= EntityKilled;
    Events.OnEntityFlee -= EntityFlee;
}
```

## Usage
The `BattleLogSystem` is used to:
- Track and log battle events for display in the UI.
- Provide localized strings for different types of events.
- Integrate with other systems via event listeners.

## References
- `BattleLogDisplayBuilder.cs`: Uses `BattleLogSystem` to display logged events.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.