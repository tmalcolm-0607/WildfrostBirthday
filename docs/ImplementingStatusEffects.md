# Implementing Status Effects Guide

## Overview

Status effects are one of the core gameplay mechanics in Wildfrost, allowing for dynamic interactions between cards, units, and the game state. This guide will walk you through creating, customizing, and implementing new status effects for your Wildfrost mod.

## Types of Status Effects

Status effects in Wildfrost are classified based on their trigger conditions:

### 1. Time-Based Triggers
- **`StatusEffectApplyXOnTurn`**: Triggers at the beginning of each turn
- **`StatusEffectApplyXAtEndOfTurn`**: Triggers at the end of each turn

### 2. Event-Based Triggers
- **`StatusEffectApplyXWhenDestroyed`**: Triggers when the card is destroyed
- **`StatusEffectApplyXOnKill`**: Triggers when the card kills another card
- **`StatusEffectApplyXWhenDeployed`**: Triggers when the card is played
- **`StatusEffectApplyXWhenHit`**: Triggers when the card is attacked
- **`StatusEffectApplyXWhenAllyIsHit`**: Triggers when an ally is attacked
- **`StatusEffectApplyXWhenAttacking`**: Triggers when the card attacks

### 3. Summon Effects
- **`StatusEffectSummon`**: Summons a specified card on turn
- **`StatusEffectInstantSummon`**: Instantly summons a specified card

### 4. Other Specialized Effects
- **`StatusEffectIncreaseHealth`**: Increases health of the target
- **`StatusEffectIncreaseMaxHealth`**: Increases maximum health of the target
- **`StatusEffectIncreaseAttack`**: Increases attack of the target
- **`StatusEffectTeeth`**: Applies the "Teeth" status (specific game mechanic)

## Creating a New Status Effect

### Basic Method: Using AddStatusEffect

The `AddStatusEffect<T>` method is used to create a new status effect:

```csharp
AddStatusEffect<StatusEffectType>(
    "EffectID",                 // Unique identifier
    "Display Name",             // Name shown in-game
    data => {
        // Configure the effect properties
        data.property1 = value1;
        data.property2 = value2;
    },
    type: "EffectType",         // Optional: classification
    canBeBoosted: true,         // Whether the effect can be amplified
    stackable: true             // Whether multiple instances stack
);
```

### Example 1: Creating an "On Kill Heal" Effect

```csharp
AddStatusEffect<StatusEffectApplyXOnKill>(
    "On Kill Heal To Self",
    "Restore 2 on kill",
    data =>
    {
        data.effectToApply = Get<StatusEffectData>("Increase Health");
        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
    },
    canBeBoosted: true
);
```

### Example 2: Creating a "When Destroyed" Effect

```csharp
AddStatusEffect<StatusEffectApplyXWhenDestroyed>(
    "When Destroyed Add Health To Allies",
    "When destroyed, add 1 to all allies (Max)",
    data =>
    {
        data.effectToApply = TryGet<StatusEffectData>("Increase Max Health");
        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
    },
    type: "Increase Max Health",
    canBeBoosted: true
);
```

## Cloning an Existing Status Effect

Sometimes it's easier to modify an existing effect. Use `AddCopiedStatusEffect<T>` for this:

```csharp
AddCopiedStatusEffect<StatusEffectType>(
    "NewEffectID",
    "BasedOnExistingEffect",
    data => {
        // Modify properties as needed
        data.modifiedProperty = newValue;
    },
    text: "Display Text",              // Optional custom display text
    textInsert: "Additional Text Info" // Optional text insertion
);
```

### Example: Cloning a Summon Effect

```csharp
AddCopiedStatusEffect<StatusEffectSummon>(
    "Summon Beepop", 
    "On Turn Summon Soulrose",
    data =>
    {
        data.summonCard = TryGet<CardData>("companion-soulrose");
    },
    text: "{0}",
    textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-soulrose>"
);
```

## Applying Status Effects to Cards

Once you've created a status effect, you can apply it to cards using the `SStack` helper method:

```csharp
// Adding a status effect with 1 stack
SStack("EffectID", 1)

// Example: Adding to a card's starting effects
AddFamilyUnit(
    "unit_id", "Unit Name", "sprites/path",
    attack, health, counter, snow,
    "Description text",
    startSStacks: new[] { 
        SStack("When Destroyed Add Health To Allies", 1),
        SStack("Another Effect", 2)
    }
);
```

## Common Status Effect Properties

Different status effect types have different properties to configure:

### StatusEffectApplyX Properties
- **`effectToApply`**: The status effect to apply
- **`applyToFlags`**: Target selection (Self, Allies, Enemies, etc.)
- **`amount`**: Number of stacks to apply (default: 1)

### StatusEffectSummon Properties
- **`summonCard`**: The card to summon
- **`summonAmount`**: Number of cards to summon
- **`summonFlags`**: Where to summon the card

### StatusEffectInstantSummon Properties
- **`targetSummon`**: Reference to a StatusEffectSummon effect

## Advanced Topics

### Target Constraints

You can restrict which cards a status effect can target using constraints:

```csharp
data.targetConstraint = new TargetConstraintCardType(CardTypes.Companion);
```

### Custom Effects

For entirely new mechanics, you may need to create a custom StatusEffect class:

1. Create a class that inherits from an appropriate StatusEffect base class
2. Override necessary methods like `OnTurnStart()`, `OnCardDestroyed()`, etc.
3. Register your custom effect using `AddStatusEffect<YourCustomEffect>`

## Testing Status Effects

1. Use the game's debugging mode to test your effects
2. Add your effect to a test card and observe its behavior
3. Check for unintended interactions with other effects
4. Verify that effect stacking works as expected

## Common Issues and Solutions

| Issue | Solution |
|-------|----------|
| Effect doesn't trigger | Check if the trigger conditions are being met |
| Effect applies to wrong targets | Verify the `applyToFlags` property |
| Changes don't appear in-game | Ensure you've rebuilt the mod and restarted the game |
| Referencing issues | Make sure referenced effects and cards are registered before they're referenced |

## Best Practices

1. **Order matters**: Register base effects before referencing them in other effects
2. **Use descriptive IDs**: Choose clear, descriptive identifiers for your effects
3. **Balance testing**: Test effects with different stack counts to ensure balance
4. **Documentation**: Comment your code and maintain documentation of custom effects
5. **Performance**: Be cautious with effects that trigger frequently, as they can impact performance

## Related Documentation

- [StatusEffectData Class](StatusEffectData.md)
- [CardData Documentation](CardData.md)
- [AddFamilyUnit Guide](AddFamilyUnit.md)
- [CharmCreation Guide](CharmCreation.md)

## Last Updated
May 11, 2025
