# Refactoring Patterns

This document provides an overview of the refactoring patterns used to modernize the WildfrostBirthday mod codebase.

## Card Refactoring

### Old Pattern (Before)
```csharp
// Using helper methods
mod.AddFamilyUnit(
    "id", "Name", "path/sprite",
    5, 4, 3, // HP, ATK, Counter
    "Description", 45,
    startSStacks: new[] {
        mod.SStack("Effect1", 1)
    },
    traitSStacks: new CardData.TraitStacks[] {
        new CardData.TraitStacks(mod.TryGet<TraitData>("Trait1"), 1)
    }
);
```

### New Pattern (After)
```csharp
// Using builder pattern directly
var builder = new CardDataBuilder(mod)
    .CreateUnit("id", "Name")
    .SetSprites("path/sprite.png", "bg.png")
    .SetStats(5, 4, 3) // HP, ATK, Counter
    .WithFlavour("Description")
    .WithCardType("Friendly")
    .WithValue(45)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.startWithEffects = new[] {
            mod.SStack("Effect1", 1)
        };
        
        data.traits = new List<CardData.TraitStacks> {
            new CardData.TraitStacks(mod.TryGet<TraitData>("Trait1"), 1)
        };
    });
    
mod.assets.Add(builder);
```

## Item Refactoring

### Old Pattern (Before)
```csharp
mod.AddItemCard(
    "id", "Name", "items/sprite",
    "Description", value,
    attackSStacks: new[] {
        mod.SStack("Effect1", amount1),
        mod.SStack("Effect2", amount2)
    }
);
```

### New Pattern (After)
```csharp
var builder = new CardDataBuilder(mod)
    .CreateItem("id", "Name")
    .SetSprites("items/sprite.png", "bg.png")
    .WithFlavour("Description")
    .WithCardType("Item")
    .WithValue(value)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.attackEffects = new CardData.StatusEffectStacks[] {
            mod.SStack("Effect1", amount1),
            mod.SStack("Effect2", amount2)
        };
    });
    
mod.assets.Add(builder);
```

## Charm Refactoring

### Old Pattern (Before)
```csharp
mod.AddCharm("id", "Name", "Description", "Pool", "charms/sprite", tier)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.effects = new CardData.StatusEffectStacks[] {
            mod.SStack("Effect", amount)
        };
    });
```

### New Pattern (After)
```csharp
var builder = new CardUpgradeDataBuilder(mod)
    .Create("id")
    .AddPool("Pool") 
    .WithType(CardUpgradeData.Type.Charm)
    .WithImage("charms/sprite.png")
    .WithTitle("Name")
    .WithText("Description")
    .WithTier(tier)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.effects = new CardData.StatusEffectStacks[] {
            mod.SStack("Effect", amount)
        };
    });
    
mod.assets.Add(builder);
```

## Status Effect Refactoring

### Old Pattern (Before)
```csharp
mod.AddStatusEffect("Effect Name", "Effect description", "sprite", data => {
    data.targetConstraints = new TargetConstraint[0];
    data.canBeBoosted = true;
});
```

### New Pattern (After)
```csharp
namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_Example
    {
        public static void Register(WildfrostBirthday.WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectType>("Effect Name")
                .WithText("Effect description")
                .WithCanBeBoosted(true)
                .SubscribeToAfterAllBuildEvent<StatusEffectType>(data =>
                {
                    // Configure effect properties
                });
                
            mod.assets.Add(builder);
        }
    }
}
```

## Card Script Helpers

Helper methods for creating card scripts were moved from the main mod class to a dedicated `CardScriptHelpers` class:

```csharp
// Use helper methods from CardScriptHelpers instead of WildFamilyMod
CardScript script = CardScriptHelpers.GetRandomHealthScript(1, 3);
```

## Benefits of the New Pattern

1. **Consistency**: All registrations follow the same pattern using builders
2. **Type safety**: Builders provide better type checking and IDE autocomplete
3. **Maintainability**: Easier to understand what each entity does
4. **Isolation**: Each entity is defined in its own file
5. **Extensibility**: Easier to add new features to existing entities
