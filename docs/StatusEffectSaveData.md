# StatusEffectSaveData

## Overview
The `StatusEffectSaveData` class is responsible for saving and loading status effects in the game. It implements the `ILoadable<CardData.StatusEffectStacks>` interface, allowing it to convert between save data and runtime objects.

## Properties
- **`name`**: The name of the status effect.
- **`count`**: The number of stacks of the status effect.

## Methods

### Save
The `Save` method in `StatusEffectStacks` converts runtime status effects into `StatusEffectSaveData` objects:

```csharp
public StatusEffectSaveData Save()
{
    return new StatusEffectSaveData
    {
        name = data.name,
        count = count
    };
}
```

### Stack
The `Stack` method combines existing and new status effects:

```csharp
public static StatusEffectStacks[] Stack(IEnumerable<StatusEffectStacks> currentEffects, IEnumerable<StatusEffectStacks> newEffects)
{
    List<StatusEffectStacks> list = new List<StatusEffectStacks>(currentEffects);
    foreach (StatusEffectStacks e in newEffects)
    {
        StatusEffectStacks statusEffectStacks = list.FirstOrDefault((StatusEffectStacks a) => a.data == e.data);
        if (statusEffectStacks != null)
        {
            statusEffectStacks.count += e.count;
        }
        else
        {
            list.Add(new StatusEffectStacks(e.data, e.count));
        }
    }
    return list.ToArray();
}
```

## Usage

### Saving Status Effects
In `CardSaveData`, status effects are saved using the `SaveArray` method:

```csharp
attackEffects = cardData.attackEffects.SaveArray<CardData.StatusEffectStacks, StatusEffectSaveData>();
startWithEffects = cardData.startWithEffects.SaveArray<CardData.StatusEffectStacks, StatusEffectSaveData>();
injuries = cardData.injuries.SaveArray<CardData.StatusEffectStacks, StatusEffectSaveData>();
```

### Loading Status Effects
Status effects are loaded back into `CardData` using the `LoadArray` and `LoadList` methods:

```csharp
cardData2.attackEffects = attackEffects.LoadArray<CardData.StatusEffectStacks, StatusEffectSaveData>();
cardData2.startWithEffects = startWithEffects.LoadArray<CardData.StatusEffectStacks, StatusEffectSaveData>();
cardData2.injuries = injuries.LoadList<CardData.StatusEffectStacks, StatusEffectSaveData>();
```

## References
- `CardData.cs`: Defines `StatusEffectStacks` and its interaction with `StatusEffectSaveData`.
- `CardSaveData.cs`: Demonstrates saving and loading of status effects.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.