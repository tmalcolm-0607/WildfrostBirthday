# StatusEffectData Class Documentation

## Overview
The `StatusEffectData` class is a crucial component in the Wildfrost modding framework. It represents various effects that can be applied to cards, such as buffs, debuffs, and other gameplay-altering effects. These status effects can be applied through various means, including charms, card abilities, and attacks.

### Namespace
`Assembly-CSharp`

## Usage in WildFamilyMod
In the WildFamilyMod project, status effects are frequently created and applied to cards through various methods:

### Creating Status Effects
Status effects are created using the `AddStatusEffect<T>` method, which allows for type-specific customization:

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

### Applying Status Effects
Status effects are applied to cards using the `SStack` helper method:

```csharp
SStack("On Turn Add Attack To Self", 1)
```

This creates a `CardData.StatusEffectStacks` object with the specified effect and stack count.

## Key Properties

### Common Properties
- **`canBeBoosted`**: Determines if the effect can be boosted (amplified) by cards or abilities.
- **`isStatus`**: Indicates if this is considered a status effect for gameplay purposes.
- **`stackable`**: Determines if multiple instances of this effect can stack.

### Types of Status Effects
Status effects in Wildfrost can be categorized by their trigger mechanisms:

1. **On Turn**: Effects that trigger at the start/end of turns
   - Example: `StatusEffectApplyXOnTurn`

2. **On Specific Events**: Effects that trigger when certain events occur
   - Example: `StatusEffectApplyXWhenDestroyed`, `StatusEffectApplyXOnKill`

3. **When Hit/Attacked**: Effects that trigger when the card is hit or attacks
   - Example: `StatusEffectApplyXWhenHit`, `StatusEffectApplyXWhenAllyIsHit`

4. **When Deployed**: Effects that trigger when the card is played
   - Example: `StatusEffectApplyXWhenDeployed`

## Code Examples

### Example 1: Creating a Status Effect
```csharp
AddStatusEffect<StatusEffectApplyXOnTurn>(
    "On Turn Apply Teeth To Allies",
    "While Active Teeth To Allies (Kaylee)",
     data =>
     {
         data.effectToApply = TryGet<StatusEffectData>("Teeth");
         data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
     },
    canBeBoosted: true
);
```

### Example 2: Copying an Existing Status Effect
```csharp
AddCopiedStatusEffect<StatusEffectSummon>(
    "Summon Beepop", "On Turn Summon Soulrose",
    data =>
    {
        data.summonCard = TryGet<CardData>("companion-soulrose");
    },
    text: "{0}",
    textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-soulrose>"
);
```

## Interactions with Other Classes

### `CardData`
- Status effects are stored in `CardData.attackEffects` and `CardData.startWithEffects` arrays.
- The `CardData.StatusEffectStacks` class pairs a `StatusEffectData` with a stack count.

### `CardUpgradeData`
- Charms can apply status effects to cards through the `effects` property.
- Status effects from upgrades are merged with existing effects when an upgrade is assigned.

### `Entity`
- Status effects can be triggered by game events related to entities.
- Entities can gain, lose, or modify status effects during gameplay.

## Related Links
- [CardUpgradeData Documentation](CardUpgradeData.md)
- [Implementing Status Effects Guide](ImplementingStatusEffects.md)
- [Tracking Document](tracking.md)

## Last Updated
May 11, 2025
