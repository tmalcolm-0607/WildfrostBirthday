# CardUpgradeSaveData Documentation

## Overview
`CardUpgradeSaveData` is a serializable class used to save and load the state of a card upgrade in Wildfrost. It captures the essential attributes of an upgrade, such as its name and associated data.

## Key Properties

- `name`: The unique name of the card upgrade.

## Key Methods

### Constructor
Initializes a new instance of `CardUpgradeSaveData`.
```csharp
public CardUpgradeSaveData();
public CardUpgradeSaveData(string name);
```

### Load
Loads a `CardUpgradeData` object from the saved data.
```csharp
public CardUpgradeData Load();
```

## Example Usage

Here is an example of saving and loading a card upgrade:

```csharp
// Save a card upgrade
CardUpgradeSaveData saveData = new CardUpgradeSaveData("ExampleUpgrade");

// Load the card upgrade
CardUpgradeData upgrade = saveData.Load();
```

## Related Classes
- `CardUpgradeData`: Represents the upgrade being saved or loaded.
- `CardSaveData`: Represents the saved state of a card, which may include upgrades.

## Notes
- Use the `Load` method to recreate a `CardUpgradeData` object from saved data.
- Ensure that the upgrade name matches an existing `CardUpgradeData` asset in the game.
