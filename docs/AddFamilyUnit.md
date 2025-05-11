# AddFamilyUnit Method Guide

## Overview
The `AddFamilyUnit` method is a core component of the MadFamily Tribe Mod, designed to create custom family-themed units for the Wildfrost game. This method creates both leader and companion units with specific stats, effects, and traits.

## Method Signature
```csharp
private CardDataBuilder AddFamilyUnit(
    string id,                          // Unique identifier for the unit
    string displayName,                 // Name displayed in-game
    string spritePath,                  // Path to the unit's sprite image
    int hp,                            // Health points
    int atk,                           // Attack value
    int counter,                       // Counter value
    int blingvalue,                    // Value when sacrificed
    string flavor,                     // Flavor text/description
    CardData.StatusEffectStacks[]? attackSStacks = null, // Effects applied when attacking
    CardData.StatusEffectStacks[]? startSStacks = null,  // Effects present at the start
    bool isLeader = false              // Whether the unit is a leader
)
```

## Parameters Explained

### Basic Information
- **`id`**: A unique identifier used to reference the unit in code. This is prefixed with "leader-" or "companion-" based on the `isLeader` parameter.
- **`displayName`**: The name shown to players in-game.
- **`spritePath`**: Path to the unit's sprite image, without the file extension. The method automatically appends ".png".
  - The method also expects a background image at `[spritePath]_bg.png`.
- **`flavor`**: Description or lore text for the unit.

### Stats
- **`hp`**: Health points. Determines how much damage the unit can take before being destroyed.
- **`atk`**: Attack value. Determines how much damage the unit deals when attacking.
- **`counter`**: Counter value. Used for abilities that trigger after a certain number of turns.
- **`blingvalue`**: The "bling" (currency) value of the unit if sacrificed or otherwise converted to currency.

### Effects
- **`attackSStacks`**: Array of status effects applied when the unit attacks. Created using the `SStack` helper method.
- **`startSStacks`**: Array of status effects the unit starts with. Created using the `SStack` helper method.

### Type
- **`isLeader`**: Boolean flag that determines if the unit is a leader or companion.
  - Leaders are added to the "LeaderPool" and have the card type "Leader".
  - Companions are added to the "GeneralUnitPool" and have the card type "Friendly".
  - Leaders automatically receive random stat bonuses and a Crown upgrade.

## Return Value
The method returns a `CardDataBuilder` object, which can be further modified using builder methods like `SubscribeToAfterAllBuildEvent` and `SetTraits`.

## Usage Examples

### Example 1: Creating a Basic Companion
```csharp
AddFamilyUnit(
    "soulrose", "Soulrose", "companions/soulrose",
    1, 0, 0, 0,
    "When destroyed, add +1 health to all allies",
    startSStacks: new[] { SStack("When Destroyed Add Health To Allies", 1) }
);
```
This creates a weak companion with 1 HP and no attack that grants +1 health to all allies when destroyed.

### Example 2: Creating a Leader with Attack Effects
```csharp
AddFamilyUnit(
    "alison", "Alison", "leaders/alison", 
    9, 3, 3, 50, 
    "Restore 2 HP on kill", 
    attackSStacks: new[] { SStack("On Kill Heal To Self", 2) },
    isLeader: true
);
```
This creates a leader with 9 HP, 3 attack, and 3 counter that restores 2 HP to itself when it kills an enemy.

### Example 3: Creating a Unit with Complex Effects
```csharp
AddFamilyUnit("cassie", "Cassie", "leaders/cassie",
    5, 1, 3, 50,
    "Joyful and chaotic, Cassie bounces through battle with ink and impulse.",
    startSStacks: new[] {
        SStack("MultiHit", 2),                   // Frenzy x2
        SStack("On Turn Apply Ink To RandomEnemy", 2)
    },
    isLeader: true
)
.SetTraits(new CardData.TraitStacks[] {
    new CardData.TraitStacks(TryGet<TraitData>("Aimless"), 1)
});
```
This creates a leader that has both starting effects and a trait, using method chaining to set the traits after creation.

## Behind the Scenes
The `AddFamilyUnit` method:
1. Constructs appropriate IDs based on the unit type (leader or companion)
2. Sets up sprite paths for both the main image and background
3. Creates a `CardDataBuilder` with the specified properties
4. Applies leader-specific bonuses if `isLeader` is true
5. Adds the builder to the mod's assets for registration

Leaders automatically receive:
- A Crown upgrade (via `GiveUpgrade()`)
- Random health bonus between -1 and +3 (via `AddRandomHealth(-1, 3)`)
- Random damage bonus between 0 and +2 (via `AddRandomDamage(0, 2)`)
- Random counter bonus between -1 and +1 (via `AddRandomCounter(-1, 1)`)

## Tips and Best Practices
1. **Balanced Stats**: Aim for balanced stats based on unit function. Leaders typically have higher stats than companions.
2. **Unique Effects**: Give each unit a unique ability or combination of effects that make it stand out.
3. **Asset Management**: Ensure sprite files are in the correct locations with the correct naming conventions.
4. **Effect Synergies**: Consider how unit effects synergize with each other and with existing game mechanics.
5. **Testing**: Always test units in-game to ensure balanced gameplay and correct effect interactions.

## Related Methods
- **`SStack(string name, int amount)`**: Creates a `CardData.StatusEffectStacks` with the specified effect and stack count.
- **`TStack(string name, int amount)`**: Creates a `CardData.TraitStacks` with the specified trait and stack count.
- **`AddCharm(...)`**: Similar method for creating charms instead of units.
- **`AddStatusEffect<T>(...)`**: Method for creating new status effects.

## Related Documentation
- [CardData Documentation](CardData.md)
- [StatusEffectData Documentation](StatusEffectData.md)
- [Tracking Document](tracking.md)

## Last Updated
May 10, 2025
