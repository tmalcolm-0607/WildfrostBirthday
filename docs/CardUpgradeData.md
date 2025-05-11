# CardUpgradeData Documentation

## Overview
`CardUpgradeData` is a core class in Wildfrost that defines upgrades applicable to cards. These upgrades can modify card stats, apply effects, and add traits. They are categorized into three types: **Charm**, **Token**, and **Crown**.

## Upgrade Types

### Charm
- General upgrades that occupy charm slots.
- Example: Adding a damage boost to a card.

### Token
- Special enhancements that occupy token slots.
- Example: Granting a card additional uses.

### Crown
- Powerful upgrades that occupy crown slots.
- Example: Making a card permanently active.

## Key Properties

### General Properties
- `tier`: The tier of the upgrade.
- `titleKey`: Localized string for the upgrade's title.
- `textKey`: Localized string for the upgrade's description.
- `image`: Sprite representing the upgrade.
- `type`: The type of upgrade (Charm, Token, Crown).

### Stat Modifications
- `damage`: Adjusts the card's damage.
- `hp`: Adjusts the card's health points.
- `counter`: Adjusts the card's counter value.
- `uses`: Adjusts the card's number of uses.
- `effectBonus`: Boosts the effects applied by the card.

### Constraints
- `targetConstraints`: Array of `TargetConstraint` objects defining conditions for applying the upgrade.

## Methods

### Assign
Applies the upgrade to a card.
```csharp
public void Assign(CardData cardData)
```

### AdjustStats
Modifies the card's stats based on the upgrade.
```csharp
public void AdjustStats(CardData cardData)
```

### GainEffects
Applies effects and traits to the card.
```csharp
public void GainEffects(CardData cardData)
```

### CanAssign
Checks if the upgrade can be applied to a specific card.
```csharp
public bool CanAssign(CardData cardData)
```

## Example Usage

Here is an example of creating a charm upgrade:

```csharp
public void CreateExampleCharm()
{
    var builder = new CardUpgradeDataBuilder(this)
        .CreateCharm("PowerCharm", "GeneralCharmPool")
        .WithTier(1)
        .ChangeDamage(2)
        .WithLocalizedName("Power Charm")
        .WithLocalizedDesc("+2 Damage");

    builder.WithImage(LoadSprite("path/to/charm_image.png"));

    var doesAttack = ScriptableObject.CreateInstance<TargetConstraintDoesAttack>();
    builder.SetConstraints(doesAttack);

    builder.Build();
}
```

## Related Classes
- `CardData`: Represents the card to which upgrades are applied.
- `TargetConstraint`: Defines conditions for valid targets.

## Notes
- Ensure that the upgrade type matches the available slots on the card.
- Use `TargetConstraint` to limit the applicability of upgrades.
