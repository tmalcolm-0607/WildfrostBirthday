# BattleDataBuilder

## Overview
The `BattleDataBuilder` class is a utility for creating and modifying `BattleData` objects. It provides a fluent API for setting various properties of a battle, such as title, wave pools, and generation scripts. This class is particularly useful for modding workflows.

## Key Methods

### Constructor
The builder can be initialized with or without a `WildfrostMod` instance:
```csharp
public BattleDataBuilder(WildfrostMod mod)
    : base(mod)
{
}

public BattleDataBuilder()
{
}
```

### WithTitle
Sets the title of the battle:
```csharp
public BattleDataBuilder WithTitle(string title)
{
    _data.title = title;
    return this;
}
```

### WithPointFactor
Sets the point factor for the battle:
```csharp
public BattleDataBuilder WithPointFactor(float factor = 1f)
{
    _data.pointFactor = factor;
    return this;
}
```

### WithWaveCounter
Sets the wave counter for the battle:
```csharp
public BattleDataBuilder WithWaveCounter(int waveCounter = 4)
{
    _data.waveCounter = waveCounter;
    return this;
}
```

### WithPools
Adds wave pools to the battle:
```csharp
public BattleDataBuilder WithPools(params BattleWavePoolData[] pools)
{
    _data.pools = pools;
    return this;
}
```

### WithBonusUnitPool
Adds bonus unit pools to the battle:
```csharp
public BattleDataBuilder WithBonusUnitPool(params CardData[] pools)
{
    _data.bonusUnitPool = pools;
    return this;
}
```

### WithGenerationScript
Sets the generation script for the battle:
```csharp
public BattleDataBuilder WithGenerationScript(BattleGenerationScript s)
{
    _data.generationScript = s;
    return this;
}
```

### WithSprite
Sets the sprite for the battle:
```csharp
public BattleDataBuilder WithSprite(Sprite sprite)
{
    _data.sprite = sprite;
    return this;
}

public BattleDataBuilder WithSprite(string sprite)
{
    _data.sprite = Mod.GetImageSprite(sprite);
    return this;
}
```

### WithName
Sets the localized name for the battle:
```csharp
public BattleDataBuilder WithName(string name, SystemLanguage lang = SystemLanguage.English)
{
    StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
    collection.SetString(_data.name + "_ref", name);
    _data.nameRef = collection.GetString(_data.name + "_ref");
    return this;
}
```

## Usage
The `BattleDataBuilder` is used to create or modify `BattleData` objects for custom battles. For example:
```csharp
BattleDataBuilder builder = new BattleDataBuilder();
builder.WithTitle("Epic Battle")
       .WithPointFactor(2.0f)
       .WithWaveCounter(5)
       .WithPools(pool1, pool2)
       .WithGenerationScript(myScript)
       .WithSprite("battle_sprite")
       .WithName("Epic Battle", SystemLanguage.English);
BattleData battleData = builder.Build();
```

## References
- `BattleData`: The data object being built.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.