# InventorySaveData

## Overview
The `InventorySaveData` class is responsible for saving and loading inventory data, including the deck, reserve cards, upgrades, and gold. It implements the `ILoadable<Inventory>` interface, allowing it to convert between save data and runtime objects.

## Properties
- **`deck`**: An array of `CardSaveData` representing the player's deck.
- **`reserve`**: An array of `CardSaveData` representing reserve cards.
- **`upgrades`**: An array of `CardUpgradeSaveData` representing card upgrades.
- **`gold`**: An integer representing the player's gold.

## Methods

### Constructor
The class provides two constructors:

1. **Default Constructor**:
   ```csharp
   public InventorySaveData()
   {
   }
   ```

2. **Parameterized Constructor**:
   ```csharp
   public InventorySaveData(CardSaveData[] deck, CardSaveData[] reserve, CardUpgradeSaveData[] upgrades, int gold)
   {
       this.deck = deck;
       this.reserve = reserve;
       this.upgrades = upgrades;
       this.gold = gold;
   }
   ```

### Load
The `Load` method converts `InventorySaveData` back into an `Inventory` object:

```csharp
public Inventory Load()
{
    Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
    foreach (CardSaveData cardSaveData in deck)
    {
        inventory.deck.Add(cardSaveData.Load());
    }
    foreach (CardSaveData cardSaveData2 in reserve)
    {
        inventory.reserve.Add(cardSaveData2.Load());
    }
    foreach (CardUpgradeSaveData cardUpgradeSaveData in upgrades)
    {
        inventory.upgrades.Add(cardUpgradeSaveData.Load());
    }
    inventory.gold = new SafeInt(gold);
    return inventory;
}
```

## Related Classes

### Inventory
The `Inventory` class implements `ISaveable<InventorySaveData>` and provides the `Save` method to convert runtime inventory data into `InventorySaveData`:

```csharp
public InventorySaveData Save()
{
    return new InventorySaveData(
        deck.SaveArray<CardData, CardSaveData>(),
        reserve.SaveArray<CardData, CardSaveData>(),
        upgrades.SaveArray<CardUpgradeData, CardUpgradeSaveData>(),
        ((SafeInt)(ref gold)).Value + goldOwed
    );
}
```

## References
- `CharacterSaveData.cs`: Contains a property `inventoryData` of type `InventorySaveData`.
- `RunHistory.cs`: Contains a property `inventory` of type `InventorySaveData`.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.