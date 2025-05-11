# WaveDeploySystem

## Overview

The `WaveDeploySystem` is a core battle mechanic in Wildfrost that manages how enemy units are deployed in waves during combat. This system creates tension and progression in battles by deploying enemies from the opponent's reserve onto the battlefield at regular intervals. The system uses a countdown timer to indicate when the next wave will deploy.

The game includes three variants of this system:
- `WaveDeploySystem` - The standard wave deployment system with a visual counter
- `WaveDeploySystemNoLimit` - A variant that deploys enemies regardless of board space limitations
- `WaveDeploySystemOverflow` - A sophisticated version that handles overflow by rescheduling units that can't be deployed

## Class Architecture

### WaveDeploySystem

The base class that manages the core wave deployment functionality.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `visible` | `bool` | Controls whether the wave counter UI is visible |
| `animator` | `Animator` | Controls animations for the wave counter UI |
| `dotManager` | `WaveDeployerDots` | Manages the visual dots representing upcoming waves |
| `counterText` | `TMP_Text` | The text display for the countdown timer |
| `group` | `GameObject` | The parent GameObject containing all UI elements |
| `counterStart` | `int` | The starting value for the countdown timer |
| `recallWhenUnsuccessful` | `bool` | If true, units return to reserve when deployment fails |
| `damageToOpponent` | `int` | Damage dealt to the player when deployment fails |
| `damageIncreasePerTurn` | `int` | Additional damage added each turn when deployment fails |
| `pauseAfterDeploy` | `float` | The pause duration after a unit is deployed |
| `inBattle` | `bool` | Whether the system is currently active in battle |
| `damageToOpponentCurrent` | `int` | The current damage amount to opponent |
| `currentWave` | `int` | The index of the current wave |
| `waveManager` | `BattleWaveManager` | Reference to the BattleWaveManager component |
| `counter` | `int` | The current countdown value |
| `counterMax` | `int` | The maximum countdown value (wave interval) |
| `reset` | `bool` | Flag to reset the counter |
| `deploySuccessful` | `bool` | Whether the last deployment was successful |
| `deployed` | `List<Entity>` | List of entities that were deployed |

#### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `BattlePhaseStart(Battle.Phase phase)` | `void` | Handles logic at the start of battle phases |
| `SceneChanged(Scene scene)` | `void` | Responds to scene changes |
| `BattleStart()` | `void` | Initializes the system at the start of a battle |
| `Close()` | `void` | Closes the wave counter UI |
| `Open()` | `void` | Opens the wave counter UI |
| `Show()` | `void` | Shows and reveals the wave counter UI |
| `Hide()` | `void` | Hides the wave counter UI |
| `CountDown()` | `IEnumerator` | Coroutine that handles the countdown logic |
| `SetCounter(int value)` | `void` | Sets the countdown counter to a specific value |
| `SetCounterMax(int value)` | `void` | Sets the maximum countdown value |
| `CheckEarlyDeploy()` | `bool` | Checks if an early deployment should occur |
| `Activate()` | `IEnumerator` | Activates the wave deployment process |
| `TryDeploy(int rowIndex)` | `IEnumerator` | Attempts to deploy entities to the battlefield |
| `Deploy(Entity entity, int targetRow, int targetColumn)` | `void` | Deploys an entity to a specific position |
| `RevealBoard()` | `IEnumerator` | Reveals all cards on the opponent's board |

### WaveDeploySystemNoLimit

A simpler variant that doesn't enforce strict limitations on deployments.

#### Key Differences from Base Class

- Uses a popup counter instead of a persistent UI
- Doesn't have damage mechanics for failed deployments
- Can deploy based on remaining enemy count with `countWhenXEnemies`
- Has special UI scaling behavior for countdown markers

### WaveDeploySystemOverflow

A more complex variant that handles overflowing units by creating additional waves.

#### Key Differences from Base Class

- Implements the `ISaveable<BattleWaveData>` interface
- Can offer gold rewards for early deployment
- Creates overflow waves for units that can't be deployed
- Tracks deployed units by ID to prevent duplicates
- Has UI features like glowing effects and early deployment button

## Related Classes

### BattleWaveManager

Manages wave data and entities during battle.

#### Key Components

- `Wave` class: Contains data for a specific wave (counter, units, boss status)
- `WaveData` abstract class: Base class for storing wave data
- `WaveDataBasic` and `WaveDataFull`: Two implementations of wave data storage
- Methods for adding, peeking, and pulling wave entities

### BattleWaveData

Serializable data structure for saving and loading wave deployment state.

#### Key Components

- `Wave` class: Serializable version of `BattleWaveManager.Wave`
- Properties for tracking deployment state (counter, current wave, deployed units)

### BattleGenerationScriptWaves

Creates waves for battles based on battle data and available points.

#### Key Functionality

- Creates waves from battle data and point allocation
- Uses `BattleWavePoolData` to pull wave configurations
- Manages wave difficulty and distribution

## Usage Examples

### Standard Wave Deployment

```csharp
// In a battle initialization script
BattleWaveManager waveManager = enemyEntity.GetComponent<BattleWaveManager>();
WaveDeploySystem waveSystem = gameObject.AddComponent<WaveDeploySystem>();

// Configure the wave system
waveSystem.counterStart = 3;
waveSystem.recallWhenUnsuccessful = true;
waveSystem.damageToOpponent = 5;
waveSystem.damageIncreasePerTurn = 2;

// Initialize the battle
waveSystem.BattleStart();
```

### Creating Custom Waves

```csharp
// Create a new wave
BattleWaveManager.Wave wave = new BattleWaveManager.Wave();
wave.counter = 3; // Deploy after 3 turns
wave.isBossWave = false;

// Add units to the wave
CardData enemyCard1 = AddressableLoader.GetCardDataClone("enemy_snowhog");
CardData enemyCard2 = AddressableLoader.GetCardDataClone("enemy_frostmage");
wave.units = new List<CardData>() { enemyCard1, enemyCard2 };

// Add the wave to the manager
BattleWaveManager waveManager = enemyEntity.GetComponent<BattleWaveManager>();
waveManager.AddWave(wave);
```

### Early Wave Deployment (Overflow System)

```csharp
// Get reference to the overflow wave system
WaveDeploySystemOverflow waveSystem = FindObjectOfType<WaveDeploySystemOverflow>();

// Configure early deployment rewards
waveSystem.canCallEarly = true;
waveSystem.deployEarlyReward = 10;
waveSystem.deployEarlyRewardPerTurn = 5;

// Trigger early deployment
waveSystem.TryEarlyDeploy();
```

## Integration with Other Systems

The Wave Deploy System integrates with several other game systems:

1. **Battle System**: The wave system responds to battle phases and manages enemy deployments.

2. **Action Queue**: Deployment actions are added to the `ActionQueue` to sequence them properly with other game actions.

3. **Card System**: The wave system works with card entities to place them on the battlefield.

4. **UI System**: The wave counter visualization provides feedback to the player.

5. **Save System**: The `WaveDeploySystemOverflow` implements `ISaveable<BattleWaveData>` to persist state across saves.

6. **Sound System**: Wave events trigger appropriate sound effects through the `SfxSystem`.

## Modding Considerations

### Extending Wave Behavior

To create custom wave deployment behaviors:

1. Create a new class that inherits from one of the wave system variants.
2. Override methods like `CountDown()`, `TryDeploy()`, or `Activate()` to implement custom logic.
3. Register your custom wave system in place of the default.

Example:

```csharp
public class WaveDeploySystemWithBuffs : WaveDeploySystem
{
    public StatusEffectData buffForNewWaves;
    
    public override IEnumerator Activate()
    {
        // Deploy the wave
        yield return base.Activate();
        
        // Add buffs to newly deployed entities
        foreach (Entity entity in deployed)
        {
            entity.AddStatusEffect(buffForNewWaves);
        }
    }
}
```

### Custom Wave Generation

To create custom wave generation:

1. Create a new `BattleGenerationScript` that inherits from `BattleGenerationScriptWaves`.
2. Override the `Run()` method to implement custom wave generation logic.
3. Use the asset creation menu to make it available in the editor.

Example:

```csharp
[CreateAssetMenu(fileName = "EliteWaveBattleGenerator", menuName = "Battle Generation Scripts/Elite Waves")]
public class BattleGenerationScriptEliteWaves : BattleGenerationScriptWaves
{
    public CardData[] eliteEnemies;
    
    public override SaveCollection<BattleWaveManager.WaveData> Run(BattleData battleData, int points)
    {
        SaveCollection<BattleWaveManager.WaveData> waves = base.Run(battleData, points);
        
        // Add an elite enemy to the last wave
        if (waves.Count > 0)
        {
            BattleWaveManager.WaveData lastWave = waves.Last();
            CardData eliteEnemy = eliteEnemies[Random.Range(0, eliteEnemies.Length)];
            lastWave.AddCard(eliteEnemy);
        }
        
        return waves;
    }
}
```

### Wave UI Customization

The wave display UI can be customized through the animator and visual components:

1. Modify the `animator` parameter to use custom animations.
2. Replace the `dotManager` to visualize waves differently.
3. Customize the counter text through the `counterText` component.

## Common Issues

1. **Failed Deployments**: If `recallWhenUnsuccessful` is true, failed deployments return units to reserve. Otherwise, the player may take damage.

2. **Overflow Handling**: The `WaveDeploySystemOverflow` class handles situations where there isn't enough space for all units by creating additional waves.

3. **Early Deployment**: Some wave systems allow early deployment, either automatically when the board is clear or manually through player action.
