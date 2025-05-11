# Status Effect Examples

## Overview

This document provides practical examples of different status effect implementations from the WildFamilyMod. Each example includes the actual code used, an explanation of how it works, and tips for effective implementation. Use these examples as a reference when creating your own status effects.

## Examples by Trigger Type

### 1. On Turn Effects (`StatusEffectApplyXOnTurn`)

On Turn effects trigger at the beginning of each turn, making them useful for consistent, repeating effects.

#### Example 1: Applying Teeth to Allies

This effect, used by the "Kaylee" unit, applies the Teeth status to all allies at the start of each turn:

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

**How it works:**
- Creates an effect that triggers at the beginning of each turn
- Applies the "Teeth" status effect to all allied units
- `canBeBoosted: true` allows the effect to be amplified by other effects

**Implementation in Kaylee:**
```csharp
AddFamilyUnit("kaylee", "Kaylee", "leaders/kaylee", 5, 4, 7, 50, 
    "Sharp-witted and sharper-fanged, Kaylee boosts all allies' bite.", 
    startSStacks: new[]
    {
        SStack("When Deployed Apply Teeth To Self", 4), // Kaylee starts with 4 teeth
        SStack("On Turn Apply Teeth To Allies", 2), // Applies teeth to all allies
    }
);
```

#### Example 2: Applying Ink to Random Enemy

This effect, used by the "Cassie" unit, applies the Ink (Null) status to enemies:

```csharp
AddStatusEffect<StatusEffectApplyXOnTurn>(
    "On Turn Apply Ink To RandomEnemy",
    "On hit, apply 2 Ink to the target",
    data =>
    {
        data.effectToApply = TryGet<StatusEffectData>("Null"); // "Null" = Ink
        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
    },
    canBeBoosted: true
);
```

### 2. When Destroyed Effects (`StatusEffectApplyXWhenDestroyed`)

These effects trigger when the card is destroyed, making them excellent for last-stand mechanics.

#### Example: Adding Health to Allies When Destroyed

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

**How it works:**
- Triggers upon the card's destruction
- Applies the "Increase Max Health" effect to all allied units
- `type: "Increase Max Health"` categorizes this effect
- Used by the "Soulrose" companion unit

**Implementation in Soulrose:**
```csharp
AddFamilyUnit(
    "soulrose", "Soulrose", "companions/soulrose",
    1, 0, 0, 0,
    "When destroyed, add +1 health to all allies",
    startSStacks: new[] { SStack("When Destroyed Add Health To Allies", 1) }
);
```

### 3. When Hit Effects (`StatusEffectApplyXWhenHit`)

These effects trigger when the card is hit by an attack, providing defensive or counter mechanisms.

#### Example: Applying Demonize to Attacker

This effect, used by the "Poppy" unit, applies the Demonize status to whoever attacks it:

```csharp
AddStatusEffect<StatusEffectApplyXWhenHit>(
    "When Hit Apply Demonize To Attacker",
    "When hit, apply 1 Demonize to the attacker",
    data =>
    {
        data.effectToApply = TryGet<StatusEffectData>("Demonize");
        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
    },
    canBeBoosted: true
);
```

**How it works:**
- Triggers whenever the card is hit by an attack
- Applies "Demonize" status to the attacker
- Works well with the "Smackback" trait (as used by Poppy)

**Implementation in Poppy:**
```csharp
AddFamilyUnit("poppy", "Poppy", "companions/poppy", 11, 2, 4, 50,
    "Ferocious little guardian who fights back hard.",
    startSStacks: new[] { SStack("When Hit Apply Demonize To Attacker", 2) }
)
.SetTraits(new CardData.TraitStacks[]
{
    new CardData.TraitStacks(TryGet<TraitData>("Smackback"), 1)
});
```

### 4. When Ally Is Hit Effects (`StatusEffectApplyXWhenAllyIsHit`)

These effects trigger when an allied card is hit, allowing for protection mechanics and team synergies.

#### Example: Applying Frost to Attacker

This effect, used by the "Lulu" unit, applies Frost to enemies that attack allies:

```csharp
AddStatusEffect<StatusEffectApplyXWhenAllyIsHit>(
    "When Ally is Hit Apply Frost To Attacker",
    "When ally is hit, apply 2 Frost to the attacker",
    data =>
    {
        data.effectToApply = TryGet<StatusEffectData>("Frost");
        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
    },
    canBeBoosted: true
);
```

**How it works:**
- Triggers whenever an ally is hit by an attack
- Applies the "Frost" status effect to the attacker
- Creates a protective synergy within your team

**Implementation in Lulu:**
```csharp
AddFamilyUnit("lulu", "Lulu", "companions/lulu", 6, 2, 3, 50,
    "Lulu defends her family with snowy retaliation.",
    startSStacks: new[] { SStack("When Ally is Hit Apply Frost To Attacker", 2) }
);
```

### 5. On Kill Effects (`StatusEffectApplyXOnKill`)

These effects trigger when the card kills an enemy, rewarding successful attacks.

#### Example: Healing Self on Kill

This effect, used by the "Alison" unit, restores health when killing an enemy:

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

**How it works:**
- Triggers when the card kills an enemy unit
- Applies the "Increase Health" effect to itself
- Rewards aggressive play and successful attacks

**Implementation in Alison:**
```csharp
AddFamilyUnit("alison", "Alison", "leaders/alison", 9, 3, 3, 50, 
    "Restore 2 HP on kill", 
    attackSStacks: new[]
    {
        SStack("On Kill Heal To Self", 2)
    }
);
```

### 6. When Deployed Effects (`StatusEffectApplyXWhenDeployed`)

These effects trigger when the card is played, providing immediate effects.

#### Example 1: Summoning a Companion

This effect summons a "Soulrose" companion when the card is played:

```csharp
AddCopiedStatusEffect<StatusEffectApplyXWhenDeployed>(
    "When Deployed Summon Wowee", 
    "When Deployed Summon Soulrose",
    data =>
    {
        data.effectToApply = TryGet<StatusEffectData>("Instant Summon Soulrose");
    },
    text: "Summon",
    textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-soulrose>"
);
```

**Implementation in Tony:**
```csharp
AddFamilyUnit("tony", "Tony", "leaders/tony", 8, 2, 4, 50, "Summon Soulrose")
   .SubscribeToAfterAllBuildEvent(data =>
   {
       data.startWithEffects = new[] {
        SStack("When Deployed Summon Soulrose", 1),
        SStack("On Turn Summon Soulrose", 1)
       };
   });
```

#### Example 2: Increasing Counter

This effect increases a card's counter when played:

```csharp
AddStatusEffect<StatusEffectApplyXWhenDeployed>(
    "FrostMoon Increase Max Counter",
    "When deployed, gain +2 counter",
    data =>
    {
        data.effectToApply = TryGet<StatusEffectData>("Increase Max Counter");
        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
    }
);
```

### 7. Summon Effects (`StatusEffectSummon`)

These effects summon other cards into battle.

#### Example: Summoning a Companion

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

**How it works:**
- Summons the specified card (in this case, "companion-soulrose")
- The text parameter allows for displaying the card name in the description
- Can be triggered by other effects or on turn

### 8. Instant Summon Effects (`StatusEffectInstantSummon`)

Similar to Summon Effects but triggers immediately rather than at the start of a turn.

```csharp
AddCopiedStatusEffect<StatusEffectInstantSummon>(
    "Instant Summon Fallow", 
    "Instant Summon Soulrose",
    data =>
    {
        data.targetSummon = TryGet<StatusEffectData>("Summon Soulrose") as StatusEffectSummon;
    }
);
```

## Applying Status Effects to Units

Status effects can be applied to units in several ways:

### 1. As Starting Effects

```csharp
AddFamilyUnit("unit_id", "Unit Name", "sprite/path", 
    attack, health, counter, snow,
    "Description text",
    startSStacks: new[] { 
        SStack("Effect ID", stacks) 
    }
);
```

### 2. As Attack Effects

```csharp
AddFamilyUnit("unit_id", "Unit Name", "sprite/path", 
    attack, health, counter, snow,
    "Description text",
    attackSStacks: new[] { 
        SStack("Effect ID", stacks) 
    }
);
```

### 3. Using the AfterBuild Event

```csharp
AddFamilyUnit("unit_id", "Unit Name", "sprite/path", 
    attack, health, counter, snow,
    "Description text")
.SubscribeToAfterAllBuildEvent(data =>
{
    data.startWithEffects = new[] {
        SStack("Effect 1", stacks1),
        SStack("Effect 2", stacks2)
    };
});
```

### 4. Through Charms

```csharp
AddCharm("charm_id", "Charm Name", "Description", "Pool", "sprite/path", cost)
.SubscribeToAfterAllBuildEvent(data =>
{
    data.effects = new StatusEffectStacks[]
    {
        SStack("Effect ID", stacks)
    };
});
```

## Common ApplyToFlags Options

The `applyToFlags` property determines which cards the effect applies to:

| Flag | Description |
|------|-------------|
| `Self` | Applies to the card with the effect |
| `Allies` | Applies to all allied cards |
| `Enemies` | Applies to all enemy cards |
| `Attacker` | Applies to the card that attacked |
| `Target` | Applies to the targeted card |
| `FrontEnemy` | Applies to the frontmost enemy |
| `RandomEnemy` | Applies to a random enemy |
| `RandomAlly` | Applies to a random ally |

## Best Practices for Status Effect Implementation

1. **Order Matters**: Always register base effects before referencing them in other effects or units
2. **Clear Naming**: Use descriptive IDs and names for your effects
3. **Balance Testing**: Test effects with different stack values to ensure balance
4. **Synergy Planning**: Consider how effects interact with other cards and existing game mechanics
5. **Error Handling**: Use `TryGet<T>()` when referencing other effects to handle missing references gracefully
6. **Documentation**: Comment your code to explain how complex effects work
7. **Performance**: Be cautious with effects that trigger frequently, as they can impact game performance

## Related Documentation
- [StatusEffectData Class](StatusEffectData.md)
- [Implementing Status Effects Guide](ImplementingStatusEffects.md)
- [CardData Documentation](CardData.md)
- [AddFamilyUnit Guide](AddFamilyUnit.md)
- [Tracking Document](tracking.md)

## Last Updated
May 10, 2025
