# Wildfrost Modding Documentation

## TargetConstraint

### Overview
`TargetConstraint` is an abstract ScriptableObject class that defines conditions for targeting or filtering entities in Wildfrost. It's used extensively to determine which cards or entities can be affected by abilities, upgrades, or effects.

### Class Definition
```csharp
public abstract class TargetConstraint : ScriptableObject
{
    [SerializeField]
    public bool not;

    public virtual bool Check(Entity target)
    {
        throw new NotImplementedException();
    }

    public virtual bool Check(CardData targetData)
    {
        throw new NotImplementedException();
    }
}
```

### Key Properties
- `not`: A boolean that can invert the result of the constraint. When true, the constraint's logic is reversed.

### Core Methods
- `Check(Entity target)`: Evaluates if an Entity meets the constraint's conditions.
- `Check(CardData targetData)`: Evaluates if CardData meets the constraint's conditions.

### Usage in CardUpgradeData
In `CardUpgradeData`, `targetConstraints` is an array that restricts which cards can receive an upgrade:

```csharp
[Header("Constraints for applying this to a card")]
[SerializeField]
public TargetConstraint[] targetConstraints;
```

When checking if an upgrade can be applied:
```csharp
TargetConstraint[] array = targetConstraints;
for (int num = 0; num < array.Length; num++)
{
    if (!array[num].Check(card))
    {
        return false;
    }
}
```

### Built-in Constraint Types

Wildfrost includes many built-in constraint types for different purposes:

#### Basic Constraints
- `TargetConstraintIsAlive`: Checks if an entity is alive
- `TargetConstraintIsUnit`: Checks if a card is a unit
- `TargetConstraintIsItem`: Checks if a card is an item
- `TargetConstraintIsCardType`: Checks if a card is one of the specified card types
- `TargetConstraintIsSpecificCard`: Checks if a card is one of the specified cards

#### Attribute Constraints
- `TargetConstraintHasHealth`: Checks if a card has health
- `TargetConstraintHealthMoreThan`: Checks if a card's health exceeds a threshold
- `TargetConstraintAttackMoreThan`: Checks if a card's attack exceeds a threshold
- `TargetConstraintMaxCounterMoreThan`: Checks if a card's counter exceeds a threshold
- `TargetConstraintDamaged`: Checks if an entity is damaged (health below max)

#### Effect & Trait Constraints
- `TargetConstraintHasStatus`: Checks if an entity has a specific status effect
- `TargetConstraintHasStatusType`: Checks if an entity has a status of a specific type
- `TargetConstraintHasTrait`: Checks if an entity has a specific trait
- `TargetConstraintHasAttackEffect`: Checks if a card has a specific attack effect
- `TargetConstraintHasAnyEffect`: Checks if a card has any effects
- `TargetConstraintStatusMoreThan`: Checks if a status effect count exceeds a threshold
- `TargetConstraintEffectsMoreThan`: Checks if effect counts exceed a threshold
- `TargetConstraintCanBeBoosted`: Checks if a card's effects can be boosted

#### Card Behavior Constraints
- `TargetConstraintDoesAttack`: Checks if a card performs attacks
- `TargetConstraintDoesDamage`: Checks if a card deals damage
- `TargetConstraintDoesKill`: Checks if a card can kill
- `TargetConstraintDoesSummon`: Checks if a card performs summoning
- `TargetConstraintNeedsTarget`: Checks if a card requires a target
- `TargetConstraintIsOffensive`: Checks if a card is offensive (can damage enemies)
- `TargetConstraintPlayType`: Checks a card's play type
- `TargetConstraintPlayOnSlot`: Checks where a card can be played

#### Upgrade-specific Constraints
- `TargetConstraintHasCrown`: Checks if a card has a crown
- `TargetConstraintCanTakeCrown`: Checks if a card can be given a crown
- `TargetConstraintHasUpgradeOfType`: Checks if a card already has an upgrade of a specific type

#### Board Position Constraints
- `TargetConstraintOnBoard`: Checks if an entity is on the game board
- `TargetConstraintIsInDeck`: Checks if a card is in the player's deck

#### Logical Constraints
- `TargetConstraintAnd`: Combines multiple constraints with AND logic
- `TargetConstraintOr`: Combines multiple constraints with OR logic

### Creating Custom Constraints

To create a custom constraint:

1. Inherit from `TargetConstraint`
2. Override the `Check` methods
3. Use `CreateAssetMenu` attribute to make it accessible in the editor

Example:
```csharp
[CreateAssetMenu(fileName = "Custom Constraint", menuName = "Target Constraints/Custom Constraint")]
public class CustomConstraint : TargetConstraint
{
    [SerializeField]
    public int threshold;

    public override bool Check(Entity target)
    {
        // Custom logic for entity
        bool result = /* your condition */;
        return not ? !result : result;
    }

    public override bool Check(CardData targetData)
    {
        // Custom logic for card data
        bool result = /* your condition */;
        return not ? !result : result;
    }
}
```

### Using Constraints with CardUpgradeDataBuilder

When creating card upgrades programmatically, you can set constraints using the `CardUpgradeDataBuilder`:

```csharp
var upgradeBuilder = new CardUpgradeDataBuilder(mod)
    .CreateCharm("MyCustomCharm", "GeneralCharmPool")
    .WithTier(2)
    .ChangeDamage(3)
    .SetConstraints(
        // Array of constraints
        ScriptableObject.CreateInstance<TargetConstraintDoesAttack>(),
        ScriptableObject.CreateInstance<TargetConstraintIsUnit>()
    );
```

### Best Practices

- Use `TargetConstraintAnd` and `TargetConstraintOr` to create complex constraint logic
- Remember that the `not` property can invert any constraint's result
- When creating custom constraints, always handle both Entity and CardData versions
- Consider performance implications when checking many constraints
- Test constraints thoroughly with various card types

### Common Constraint Combinations

#### Only for Attacking Units
```csharp
SetConstraints(
    ScriptableObject.CreateInstance<TargetConstraintIsUnit>(),
    ScriptableObject.CreateInstance<TargetConstraintDoesAttack>()
)
```

#### Only for Units with Health
```csharp
SetConstraints(
    ScriptableObject.CreateInstance<TargetConstraintIsUnit>(),
    ScriptableObject.CreateInstance<TargetConstraintHasHealth>()
)
```

#### Only for Units without Crowns
```csharp
var hasCrown = ScriptableObject.CreateInstance<TargetConstraintHasCrown>();
hasCrown.not = true;

SetConstraints(
    ScriptableObject.CreateInstance<TargetConstraintIsUnit>(),
    hasCrown
)
```
