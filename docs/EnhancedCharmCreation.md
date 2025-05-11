# Enhanced Charm Creation Guide

## Overview
This document provides recommendations for enhancing charm creation in the WildFamilyMod by utilizing the `TargetConstraint` system. Adding constraints to charms allows for more specialized and powerful effects targeted at specific card types.

## Current Charm Implementation

The current `AddCharm` method in the mod creates charms with:
- Basic information (ID, title, description, pool, sprite, tier)
- Status effects through the `effects` parameter
- Additional customization via `SubscribeToAfterAllBuildEvent`

Some charms in the mod already use target constraints, such as:

```csharp
var sodaCharm = AddCharm("soda_charm", "Soda Charm", "Gain Barrage, Frenzy x3, Consume. Halve all current effects.", "GeneralCharmPool", "charms/soda_charm", 3)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        // Other settings...
        
        data.targetConstraints = new TargetConstraint[]
        {
            ScriptableObject.CreateInstance<TargetConstraintIsItem>()
        };
    });
```

## Enhanced AddCharm Method

A more powerful `AddCharm` method can directly support target constraints:

```csharp
private CardUpgradeDataBuilder AddCharm(
    string id, 
    string title, 
    string cardText, 
    string charmPool, 
    string spritePath, 
    int tier, 
    StatusEffectStacks[]? effects = null,
    TargetConstraint[]? constraints = null,
    TraitStacks[]? traits = null)
{
    string cardId = "charm-" + id;

    var builder = new CardUpgradeDataBuilder(this)
        .Create(cardId)
        .AddPool(charmPool)
        .WithType(CardUpgradeData.Type.Charm)
        .WithImage(spritePath + ".png")
        .WithTitle(title)
        .WithText(cardText)
        .WithTier(tier);

    builder.SubscribeToAfterAllBuildEvent(data => 
    {
        if (effects != null)
            data.effects = effects;
        
        if (constraints != null)
            data.targetConstraints = constraints;
        
        if (traits != null)
            data.giveTraits = traits;
    });

    assets.Add(builder);
    return builder;
}
```

## Recommended Charm Types

### 1. Type-Specific Charms

Create charms that only work for specific card types (units, items, etc.):

```csharp
// Item-specific charm
var itemCharm = AddCharm(
    "item_charm", "Item Charm",
    "Only applies to item cards. Adds Consume and Barrage.",
    "GeneralCharmPool", "charms/item_charm", 2,
    traits: new[] { TStack("Consume", 1), TStack("Barrage", 1) },
    constraints: new[] { ScriptableObject.CreateInstance<TargetConstraintIsItem>() }
);

// Unit-specific charm
var unitCharm = AddCharm(
    "unit_charm", "Unit Charm", 
    "Only applies to units. Gain +2 Counter.",
    "GeneralCharmPool", "charms/unit_charm", 2,
    effects: new[] { SStack("Increase Max Counter", 2) },
    constraints: new[] { ScriptableObject.CreateInstance<TargetConstraintIsUnit>() }
);
```

### 2. Stat-Based Charms

Create charms that only apply to cards with specific stats:

```csharp
// For high-health units
var tankCharm = AddCharm(
    "tank_charm", "Tank Charm", 
    "Apply to units with 5+ health. Gain Taunt.",
    "GeneralCharmPool", "charms/tank_charm", 2,
    traits: new[] { TStack("Taunt", 1) },
    constraints: new[] {
        ScriptableObject.CreateInstance<TargetConstraintHealthMoreThan>().FreeModify(
            constraint => constraint.health = 4)
    }
);

// For offensive units
var strikerCharm = AddCharm(
    "striker_charm", "Striker Charm",
    "Apply to units with 3+ attack. Gain Frenzy.",
    "GeneralCharmPool", "charms/striker_charm", 2,
    effects: new[] { SStack("MultiHit", 1) },
    constraints: new[] {
        ScriptableObject.CreateInstance<TargetConstraintAttackMoreThan>().FreeModify(
            constraint => constraint.attack = 2)
    }
);
```

### 3. Combined Constraints

Create more sophisticated charms using logical operators:

```csharp
// For damaged units with high attack
var berserkerCharm = AddCharm(
    "berserker_charm", "Berserker Charm",
    "Apply to damaged units with 3+ attack. Double Attack.",
    "GeneralCharmPool", "charms/berserker_charm", 3,
    effects: new[] { SStack("Increase Attack", 4) },
    constraints: new[] {
        ScriptableObject.CreateInstance<TargetConstraintAnd>().FreeModify(
            constraint => constraint.constraints = new[] {
                ScriptableObject.CreateInstance<TargetConstraintDamaged>(),
                ScriptableObject.CreateInstance<TargetConstraintAttackMoreThan>().FreeModify(
                    c => c.attack = 2)
            }
        )
    }
);
```

### 4. Effect-Based Charms

Create charms that enhance existing effects:

```csharp
// For units with Frost effects
var frostAmplifierCharm = AddCharm(
    "frost_amplifier", "Frost Amplifier",
    "Apply to cards with Frost effects. Doubles all Frost effects.",
    "GeneralCharmPool", "charms/frost_amplifier", 3,
    constraints: new[] {
        ScriptableObject.CreateInstance<TargetConstraintHasEffectBasedOn>().FreeModify(
            constraint => constraint.statusType = "Frost")
    }
).SubscribeToAfterAllBuildEvent(data => {
    data.statusMultiplier = 2.0f;
    data.onlyMultiplyOfType = "Frost";
});
```

### 5. Special Ability Charms

Create charms that target cards with specific abilities:

```csharp
// For summoning cards
var summonerCharm = AddCharm(
    "summoner_charm", "Summoner's Charm",
    "Apply to cards that summon. Summon twice.",
    "GeneralCharmPool", "charms/summoner_charm", 3,
    constraints: new[] { ScriptableObject.CreateInstance<TargetConstraintDoesSummon>() }
).SubscribeToAfterAllBuildEvent(data => {
    data.statusMultiplier = 2.0f;
});

// For cards that attack
var attackBoostCharm = AddCharm(
    "attack_boost_charm", "Attack Boost Charm",
    "Apply to cards that attack. +3 Attack.",
    "GeneralCharmPool", "charms/attack_boost_charm", 2,
    effects: new[] { SStack("Increase Attack", 3) },
    constraints: new[] { ScriptableObject.CreateInstance<TargetConstraintDoesAttack>() }
);
```

## Implementation Notes

1. **Structure**: Keep the `AddCharm` method clean by accepting all necessary parameters directly.
   
2. **Constraints Creation**: Use `ScriptableObject.CreateInstance<>()` to create new constraint instances.

3. **Complex Constraints**: For complex constraints, use the `.FreeModify()` extension method to configure the constraint.

4. **Performance**: Consider that more specific charms might be less generally applicable, but can offer more powerful effects as a trade-off.

5. **UI Considerations**: Consider adding visual cues in the game UI to show which cards are compatible with which constraints.

## Example: Converting Existing Charms

Convert your existing sodaCharm to use the new parameters:

```csharp
// Current implementation
var sodaCharm = AddCharm("soda_charm", "Soda Charm", 
    "Gain Barrage, Frenzy x3, Consume. Halve all current effects.", 
    "GeneralCharmPool", "charms/soda_charm", 3)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.giveTraits = new TraitStacks[] {
            TStack("Barrage", 1),
            TStack("Consume", 1),
        };
        data.effects = new StatusEffectStacks[] {
            SStack("Reduce Effects", 2),
            SStack("MultiHit", 3)
        };
        data.targetConstraints = new TargetConstraint[] {
            ScriptableObject.CreateInstance<TargetConstraintIsItem>()
        };
    });

// Enhanced implementation
var sodaCharm = AddCharm(
    "soda_charm", "Soda Charm",
    "Gain Barrage, Frenzy x3, Consume. Halve all current effects.",
    "GeneralCharmPool", "charms/soda_charm", 3,
    effects: new[] {
        SStack("Reduce Effects", 2),
        SStack("MultiHit", 3)
    },
    traits: new[] {
        TStack("Barrage", 1),
        TStack("Consume", 1)
    },
    constraints: new[] {
        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
    }
);
```

## Related Documentation

- [TargetConstraint Documentation](TargetConstraint.md): Full reference for constraint types
- [CardUpgradeData Documentation](CardUpgradeData.md): Information about charm structure
- [CharmCreation Guide](CharmCreation.md): Basic charm creation guide

## Last Updated
May 11, 2025
