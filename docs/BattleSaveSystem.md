# BattleSaveSystem

## Overview
The `BattleSaveSystem` class manages the saving and loading of battle states in the game. It ensures that the current state of a battle, including turn count, statuses, and other relevant data, is preserved and restored as needed.

## Key Properties
- **`instance`**: A static reference to the active `BattleSaveSystem` instance.
- **`loading`**: Indicates whether a battle state is currently being loaded.
- **`justLoaded`**: Tracks if the system has just completed loading a battle state.
- **`state`**: The current `BattleSaveData` object representing the battle state.
- **`saveRequired`**: Flags whether a save operation is needed.
- **`campaignNodeIdSet`**: Indicates if the campaign node ID has been set.
- **`campaignNodeId`**: Stores the ID of the current campaign node.

## Key Methods

### OnEnable
Registers event listeners and sets the static `instance` reference:
```csharp
public void OnEnable()
{
    instance = this;
    Events.OnBattlePreTurnStart += BattleTurnEnd;
    Events.OnCampaignFinal += CampaignFinal;
}
```

### OnDisable
Unregisters event listeners:
```csharp
public void OnDisable()
{
    Events.OnBattlePreTurnStart -= BattleTurnEnd;
    Events.OnCampaignFinal -= CampaignFinal;
}
```

### Save
Saves the current battle state:
```csharp
public void Save()
{
    if (state == null)
    {
        Debug.LogWarning("Cannot save Battle State right now!");
        return;
    }
    Debug.Log("> Saving Battle State...");
    SaveSystem.SaveCampaignData(Campaign.Data.GameMode, "battleState", state);
}
```

### BuildBattleState
Constructs the `BattleSaveData` object based on the current battle state:
```csharp
public void BuildBattleState()
{
    Events.InvokeBattleStateBuild();
    StopWatch.Start();
    if (!campaignNodeIdSet)
    {
        campaignNodeId = Campaign.FindCharacterNode(References.Battle.player).id;
    }
    state = new BattleSaveData
    {
        gold = ((SafeInt)(ref References.PlayerData.inventory.gold)).Value + References.PlayerData.inventory.goldOwed,
        campaignNodeId = campaignNodeId,
        turnCount = References.Battle.turnCount + 1,
        statuses = (from e in StatusEffectSystem.activeEffects
            where (bool)e && e.count > e.temporary && (bool)e.target && e.target.alive
            select new BattleSaveData.Status(e)).ToArray()
    };
    // Additional logic for storing mid-battle data...
}
```

## Usage
The `BattleSaveSystem` is used to:
- Save the state of a battle when required.
- Load a saved battle state to resume gameplay.
- Track and manage mid-battle data, such as statuses and turn counts.

## References
- `CampaignNodeTypeBattle.cs`: Uses `BattleSaveSystem` to load battle states.
- `BattleUndoSystem.cs`: References `BattleSaveSystem` for undo functionality.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.