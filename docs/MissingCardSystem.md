# MissingCardSystem

## Overview
The `MissingCardSystem` class is a static utility for handling missing card data in the game. It provides methods to check for missing cards, retrieve placeholder cards, and manage collections of card data.

## Key Methods

### Get
Retrieves the default placeholder card for missing data:
```csharp
public static CardData Get()
{
    return AddressableLoader.Get<CardData>("CardData", "MissingCard");
}
```

### GetClone
Creates a clone of the placeholder card with a custom title:
```csharp
public static CardData GetClone(string cardDataName, bool runCreateScripts = true)
{
    CardData cardData = Get();
    if (!cardData)
    {
        return null;
    }
    CardData cardData2 = cardData.Clone(runCreateScripts);
    cardData2.forceTitle = "Missing Card " + cardDataName;
    return cardData2;
}
```

### GetCloneWithId
Creates a clone of the placeholder card with a custom title and unique ID:
```csharp
public static CardData GetCloneWithId(string cardDataName, Vector3 random3, ulong id, bool runCreateScripts = true)
{
    CardData cardData = Get();
    if (!cardData)
    {
        return null;
    }
    CardData cardData2 = cardData.Clone(random3, id, runCreateScripts);
    cardData2.forceTitle = "Missing Card " + cardDataName;
    return cardData2;
}
```

### IsMissing
Checks if a card is missing based on its name or save data:
```csharp
public static bool IsMissing(string cardDataName)
{
    CardData cardData = AddressableLoader.Get<CardData>("CardData", cardDataName);
    if (!cardData)
    {
        return true;
    }
    if (cardData.name == "MissingCard")
    {
        return true;
    }
    return false;
}
```

### HasMissingData
Checks if a collection of card data contains any missing cards:
```csharp
public static bool HasMissingData(IEnumerable<string> cardDataNames)
{
    foreach (string cardDataName in cardDataNames)
    {
        if (IsMissing(cardDataName))
        {
            return true;
        }
    }
    return false;
}
```

## Usage
The `MissingCardSystem` is used throughout the game to handle scenarios where card data is unavailable or corrupted. For example:
- Replacing missing cards in a player's deck.
- Validating card data during game initialization.
- Ensuring save data integrity.

## References
- `CardSaveData.cs`: Uses `MissingCardSystem` to handle missing cards during save/load operations.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.