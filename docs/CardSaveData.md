# CardSaveData Documentation

## Overview
`CardSaveData` is a serializable class used to save and load the state of a card in Wildfrost. It captures all essential attributes of a card, including its stats, effects, traits, and custom data.

## Key Properties

### General Properties
- `id`: Unique identifier for the card.
- `name`: Internal name of the card.
- `title`: Display name of the card.
- `hp`: Health points of the card.
- `damage`: Damage value of the card.
- `counter`: Counter value for triggering effects.
- `random3`: Random vector used for unique card generation.

### Effects and Traits
- `attackEffects`: Array of status effects applied during attacks.
- `startWithEffects`: Array of status effects the card starts with.
- `traits`: List of traits that define the card's behavior.
- `injuries`: List of injuries affecting the card.

### Upgrades
- `upgrades`: Array of `CardUpgradeSaveData` objects representing applied upgrades.

### Custom Data
- `customData`: Dictionary for storing custom data related to the card.

## Key Methods

### Constructor
Initializes a new instance of `CardSaveData` from a `CardData` object.
```csharp
public CardSaveData(CardData cardData);
```

### Load
Loads a `CardData` object from the saved data.
```csharp
public CardData Load(bool keepId);
```

### Peek
Retrieves the `CardData` object without fully loading it.
```csharp
public CardData Peek();
```

## Example Usage

Here is an example of saving and loading a card:

```csharp
// Save a card
CardData card = AddressableLoader.Get<CardData>("CardData", "ExampleCard");
CardSaveData saveData = new CardSaveData(card);

// Load the card
CardData loadedCard = saveData.Load(keepId: true);
```

## Related Classes
- `CardData`: Represents the card being saved or loaded.
- `CardUpgradeSaveData`: Represents the saved state of a card upgrade.
- `StatusEffectSaveData`: Represents the saved state of a status effect.
- `TraitSaveData`: Represents the saved state of a trait.

## Notes
- Use the `Load` method to recreate a `CardData` object from saved data.
- Custom data can be used to store mod-specific information.
- Ensure that all related save data classes (e.g., `CardUpgradeSaveData`) are properly implemented for seamless integration.
