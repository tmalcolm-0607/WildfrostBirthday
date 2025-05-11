# Charm Creation and Balancing Guide

## Overview
Charms are special upgrades that can be applied to cards in Wildfrost, enhancing their abilities or granting new ones. This guide covers how to create, implement, and balance charms for the MadFamily Tribe Mod using the `AddCharm` method.

## The AddCharm Method

### Method Signature
```csharp
private CardUpgradeDataBuilder AddCharm(
    string id,                       // Unique identifier for the charm
    string title,                    // Name displayed in-game
    string cardText,                 // Description text displayed on the charm
    string charmPool,                // The pool this charm belongs to
    string spritePath,               // Path to the charm's sprite image
    int tier,                        // The tier/rarity of the charm
    StatusEffectStacks[]? effects = null // Optional initial effects
)
```

### Parameters Explained

#### Basic Information
- **`id`**: A unique identifier used to reference the charm in code. This is prefixed with "charm-" in the method.
- **`title`**: The name shown to players in-game.
- **`cardText`**: The description text displayed on the charm card, explaining its effects.
- **`spritePath`**: Path to the charm's sprite image, without the file extension. The method automatically appends ".png".

#### Game Integration
- **`charmPool`**: The pool this charm belongs to, which determines where it can appear in the game.
  - Common pools include "GeneralCharmPool", "SnowCharmPool", etc.
- **`tier`**: The tier or rarity of the charm (typically 1-3), affecting its power level and rarity.

#### Effects
- **`effects`**: An optional array of status effects that the charm applies when attached to a card.

### Return Value
The method returns a `CardUpgradeDataBuilder` object, which can be further customized using builder methods like `SubscribeToAfterAllBuildEvent`.

## Creating Charms

### Basic Charm Creation
```csharp
var plantCharm = AddCharm(
    "plant_charm", 
    "Plant Charm", 
    "Gain +1 Attack after attacking", 
    "GeneralCharmPool", 
    "charms/plant_charm", 
    2
)
.SubscribeToAfterAllBuildEvent(data =>
{
    data.effects = new StatusEffectStacks[]
    {
        SStack("On Turn Add Attack To Self", 1)
    };
});
```

### Charm with Custom Text Formatting
```csharp
var plantCharm = AddCharm(
    "plant_charm", 
    "Plant Charm", 
    "Gain +1 Attack after attacking", 
    "GeneralCharmPool", 
    "charms/plant_charm", 
    2
)
.SubscribeToAfterAllBuildEvent(data =>
{
    data.effects = new StatusEffectStacks[]
    {
        SStack("On Turn Add Attack To Self", 1)
    };
})
.WithText("On Turn Add {0} Attack To Self");
```
The `{0}` placeholder will be replaced with the effect stack count.

### Charm with Traits
```csharp
var duckCharm = AddCharm(
    "duck_charm", 
    "Duck Charm", 
    "Gain Frenzy, Aimless, and set Attack to 1", 
    "GeneralCharmPool", 
    "charms/duck_charm", 
    2
)
.SubscribeToAfterAllBuildEvent(data =>
{
    data.effects = new StatusEffectStacks[]
    {
        SStack("When Hit Add Frenzy To Self", 1),
        SStack("Set Attack", 1),
        SStack("MultiHit", 1)
    };
    
    data.giveTraits = new TraitStacks[]
    {
        TStack("Aimless", 1)
    };
});
```

### Charm with Targeting Constraints
```csharp
var sodaCharm = AddCharm(
    "soda_charm",
    "Soda Charm",
    "Gain Barrage, Frenzy x3, Consume. Halve all current effects.",
    "GeneralCharmPool",
    "charms/soda_charm",
    3
)
.SubscribeToAfterAllBuildEvent(data =>
{
    data.giveTraits = new TraitStacks[]
    {
        TStack("Barrage", 1),
        TStack("Consume", 1),
    };
    
    data.effects = new StatusEffectStacks[]
    {
        SStack("Reduce Effects", 2),
        SStack("MultiHit", 3)
    };
    
    data.targetConstraints = new TargetConstraint[]
    {
        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
    };
});
```
This charm has the `TargetConstraintIsItem` constraint, ensuring it can only be applied to item cards.

## Balancing Considerations

### Tier System
Charms in Wildfrost are generally balanced according to a tier system:

1. **Tier 1**: Minor effects that provide small bonuses or situational benefits.
   - Example: +1 to a specific stat or a minor effect on specific conditions
   - Should have minimal impact on gameplay balance

2. **Tier 2**: Moderate effects that provide noticeable benefits or alter gameplay in meaningful ways.
   - Example: Multiple stat boosts or effects that trigger regularly
   - May change how a card is played but shouldn't completely transform it

3. **Tier 3**: Powerful effects that can significantly change how a card functions.
   - Example: Combining multiple strong effects or fundamentally altering card behavior
   - Can be game-changing but should have appropriate costs or drawbacks

### Balance Considerations
When designing charms, consider the following:

1. **Power Level**: Higher tier charms should be more powerful, but also rarer.

2. **Synergy**: Consider how the charm interacts with different card types and other charms.

3. **Cost and Benefit**: Powerful effects should come with appropriate costs or drawbacks.
   - The `Consume` trait is often used to balance powerful one-time effects.

4. **Target Constraints**: Use constraints like `TargetConstraintIsItem` to limit where charms can be applied.

5. **Stack Limits**: Be careful with effects that can stack indefinitely, as they can lead to imbalance.

6. **Testing**: Always test charms in various scenarios to ensure they don't create unintended outcomes.

### Common Balance Issues
- **Infinite Scaling**: Effects that can increase without limit
- **Death Loops**: Interactions that cause endless cycles
- **Stat Inflation**: Too many sources of stat increases
- **Single-Card Dominance**: Charms that make one card so powerful it trivializes gameplay

## Tips for Charm Creation

1. **Theme Consistency**: Maintain a consistent theme for your charms that aligns with your mod's overall vision.

2. **Visual Clarity**: Ensure charm sprites clearly communicate their purpose or effect.

3. **Clear Descriptions**: Write concise, clear descriptions that explain exactly what the charm does.

4. **Effect Combinations**: Create interesting combinations of effects rather than just stat increases.

5. **Asset Management**: Organize your charm sprites in a dedicated folder structure for easy maintenance.

## Registering Charms

For your charms to appear in-game, they need to be:
1. Created using the `AddCharm` method
2. Added to appropriate charm pools
3. Registered via the `assets.Add(builder)` call (handled in the `AddCharm` method)

## Related Documentation
- [CardUpgradeData Documentation](CardUpgradeData.md)
- [Enhanced Charm Creation Guide](EnhancedCharmCreation.md) - Advanced techniques using target constraints
- [StatusEffectData Documentation](StatusEffectData.md)
- [AddFamilyUnit Guide](AddFamilyUnit.md)
- [Tracking Document](tracking.md)

## Last Updated
May 11, 2025
