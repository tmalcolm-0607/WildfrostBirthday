# CardData Documentation

## Overview
`CardData` is a core class in Wildfrost that represents individual cards in the game. It contains properties, methods, and behaviors that define a card's attributes, effects, and interactions.

## Key Properties

### General Properties
- `id`: Unique identifier for the card.
- `name`: Internal name of the card.
- `title`: Display name of the card.
- `hp`: Health points of the card.
- `damage`: Damage value of the card.
- `counter`: Counter value for triggering effects.
- `random3`: Random vector used for unique card generation.
- `customData`: Dictionary for storing custom data related to the card.

### Effects and Traits
- `attackEffects`: Array of status effects applied during attacks.
- `startWithEffects`: Array of status effects the card starts with.
- `traits`: List of traits that define the card's behavior.
- `injuries`: List of injuries affecting the card.

### Upgrades
- `upgrades`: List of `CardUpgradeData` objects applied to the card.

## Key Methods

### Clone
Creates a copy of the card.
```csharp
public CardData Clone(bool runCreateScripts = true);
public CardData Clone(Vector3 random3, bool runCreateScripts = true);
public CardData Clone(Vector3 random3, ulong id, bool runCreateScripts = true);
```

### RunCreateScripts
Executes creation scripts associated with the card.
```csharp
public void RunCreateScripts();
```

### SetCustomData
Sets a custom data key-value pair for the card.
```csharp
public void SetCustomData(string key, object value);
```

### TryGetCustomData
Attempts to retrieve a custom data value by key.
```csharp
public bool TryGetCustomData<T>(string key, out T value, T defaultValue);
```

### Save
Saves the card's state to a `CardSaveData` object.
```csharp
public CardSaveData Save();
```

## Example Usage

Here is an example of creating a card and applying an upgrade:

```csharp
CardData card = AddressableLoader.Get<CardData>("CardData", "ExampleCard").Clone();
CardUpgradeData upgrade = AddressableLoader.Get<CardUpgradeData>("CardUpgradeData", "ExampleUpgrade");
upgrade.Assign(card);
```

## Related Classes
- `CardUpgradeData`: Represents upgrades that can be applied to cards.
- `CardSaveData`: Represents the saved state of a card.
- `CardScript`: Defines custom behaviors for cards.

## Notes
- Use the `Clone` method to create unique instances of cards.
- Custom data can be used to store mod-specific information.
- Ensure that upgrades and effects are compatible with the card's type and attributes.
