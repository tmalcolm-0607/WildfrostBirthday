# Wildfrost Modding Documentation

## CardUpgradeData

### Overview
`CardUpgradeData` is a core class for creating card upgrades in Wildfrost. It represents charms, tokens, and crowns that can be applied to cards to modify their stats, effects, and behaviors.

### Class Definition
```csharp
[CreateAssetMenu(fileName = "CardUpgradeData", menuName = "Card Upgrade Data")]
public class CardUpgradeData : DataFile, ISaveable<CardUpgradeSaveData>
```

This class:
- Inherits from `DataFile`
- Implements `ISaveable<CardUpgradeSaveData>` for save/load functionality
- Uses the `CreateAssetMenu` attribute to allow creation in the Unity editor

### Upgrade Types
CardUpgradeData supports three types of upgrades:
```csharp
public enum Type
{
    None,
    Charm,
    Token,
    Crown
}
```

Each type has specific characteristics:
- **Charms**: General upgrades that occupy charm slots
- **Tokens**: Special enhancements that occupy token slots
- **Crowns**: Powerful upgrades that occupy crown slots

### Key Properties

#### Basic Properties
- `tier`: Determines the upgrade's power level or rarity
- `titleKey`: Localized string for the upgrade's name
- `textKey`: Localized string for the upgrade's description
- `image`: Sprite used to display the upgrade
- `type`: The upgrade type (Charm, Token, Crown)

#### Effect Properties
- `attackEffects`: Status effects applied when the card attacks
- `effects`: Status effects the card starts with
- `giveTraits`: Traits given to the card
- `scripts`: Custom scripts to run when applied

#### Constraint Properties
- `targetConstraints`: An array of constraints that determine valid target cards
- `takeSlot`: Whether this upgrade takes up a slot (defaults to true)

#### Stat Modification
- `damage`, `hp`, `counter`, `uses`: Values to add to respective stats
- `effectBonus`: Value to add to effects/traits that can stack
- `setDamage`, `setHp`, `setCounter`, `setUses`: If true, sets the stat to an exact value instead of adding

### Important Methods

#### Assigning Upgrades
```csharp
public void Assign(CardData cardData)
```
Applies the upgrade to a card, modifying its stats, effects, and traits.

```csharp
public IEnumerator Assign(Entity entity)
```
Applies the upgrade to an entity, modifying its data and updating its visual representation.

#### Validation
```csharp
public bool CanAssign(Entity card)
public bool CanAssign(CardData cardData)
```
Checks if the upgrade can be applied to a card based on constraints and slot availability.

#### Removal
```csharp
public void UnAssign(CardData assignedTo)
```
Removes the upgrade and its effects from a card.

### Using TargetConstraint

The `targetConstraints` array is a key part of CardUpgradeData. It specifies conditions that a card must meet to receive this upgrade:

```csharp
[Header("Constraints for applying this to a card")]
[SerializeField]
public TargetConstraint[] targetConstraints;
```

Each `TargetConstraint` has a `Check` method that evaluates whether a card or entity meets the constraint's requirements.

### Slot Management

The `CheckSlots` method determines if a card has available slots for the upgrade:

```csharp
public bool CheckSlots(CardData cardData)
```

This checks the card's available slots based on the upgrade's type (Charm, Token, Crown) and whether the upgrade takes up a slot.

### Creating a CardUpgradeData

To create a new card upgrade:

1. Create a ScriptableObject asset using Unity's CreateAssetMenu
2. Set the basic properties (title, text, image, type)
3. Define stat changes and effects
4. Set up appropriate constraints using TargetConstraint objects

### Example Usage

```csharp
// Example of creating a basic Charm upgrade that adds 2 damage
var upgrade = ScriptableObject.CreateInstance<CardUpgradeData>();
upgrade.type = CardUpgradeData.Type.Charm;
upgrade.damage = 2;
upgrade.titleKey = [Localized string for "Sharp Edge"];
upgrade.textKey = [Localized string for "+2 Damage"];
upgrade.image = [Sprite for charm];
```

### Notes
- When applying multiple upgrades, consider the order as it may affect the outcome
- Use constraints to ensure upgrades are only applied to appropriate cards
- Consider slot limitations when designing upgrade systems
