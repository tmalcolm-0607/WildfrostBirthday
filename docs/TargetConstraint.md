# TargetConstraint Documentation

## Overview
`TargetConstraint` is an abstract base class used to define conditions for targeting entities or cards in Wildfrost. Derived classes implement specific constraints, allowing for flexible and customizable targeting logic for cards, abilities, and effects.

## Key Properties
- `not`: A boolean flag that inverts the result of the constraint check.

## Key Methods

### Check(Entity target)
Checks if the constraint is satisfied for a given entity.
```csharp
public abstract bool Check(Entity target);
```

### Check(CardData targetData)
Checks if the constraint is satisfied for a given card data.
```csharp
public abstract bool Check(CardData targetData);
```

## Derived Classes

### Logical Operators

#### TargetConstraintAnd
Combines multiple constraints using a logical AND operation.
- **File**: `TargetConstraintAnd.cs`
- **Menu Name**: "Target Constraints/And"
- **Properties**: `constraints` (Array of `TargetConstraint`)

#### TargetConstraintOr
Combines multiple constraints using a logical OR operation.
- **File**: `TargetConstraintOr.cs`
- **Menu Name**: "Target Constraints/Or"
- **Properties**: `constraints` (Array of `TargetConstraint`)

### Card Type Constraints

#### TargetConstraintIsCardType
Checks if the target is one of the specified card types.
- **File**: `TargetConstraintIsCardType.cs`
- **Menu Name**: "Target Constraints/Is Card Type"
- **Properties**: `allowedTypes` (Array of `CardType`)

#### TargetConstraintIsUnit
Checks if the target is a unit card.
- **File**: `TargetConstraintIsUnit.cs`
- **Menu Name**: "Target Constraints/Is Unit"

#### TargetConstraintIsItem
Checks if the target is an item card.
- **File**: `TargetConstraintIsItem.cs`
- **Menu Name**: "Target Constraints/Is Item"

#### TargetConstraintIsSpecificCard
Checks if the target is a specific card by name.
- **File**: `TargetConstraintIsSpecificCard.cs`
- **Menu Name**: "Target Constraints/Is Specific Card"
- **Properties**: `cardNames` (Array of strings)

#### TargetConstraintIsOffensive
Checks if the target is an offensive card.
- **File**: `TargetConstraintIsOffensive.cs`
- **Menu Name**: "Target Constraints/Is Offensive"

### Effect Constraints

#### TargetConstraintDoesSummon
Checks if the target has a summon effect.
- **File**: `TargetConstraintDoesSummon.cs`
- **Menu Name**: "Target Constraints/Does Summon"

#### TargetConstraintEffectsMoreThan
Checks if the target has more than a specified number of effects.
- **File**: `TargetConstraintEffectsMoreThan.cs`
- **Menu Name**: "Target Constraints/Effects More Than"
- **Properties**: `count` (int)

#### TargetConstraintHasAnyEffect
Checks if the target has any effect applied.
- **File**: `TargetConstraintHasAnyEffect.cs`
- **Menu Name**: "Target Constraints/Has Any Effect"

#### TargetConstraintHasEffectBasedOn
Checks if the target has an effect based on a specific status type.
- **File**: `TargetConstraintHasEffectBasedOn.cs`
- **Menu Name**: "Target Constraints/Has Effect Based On"
- **Properties**: `statusType` (string)

#### TargetConstraintHasAttackEffect
Checks if the target has a specific attack effect.
- **File**: `TargetConstraintHasAttackEffect.cs`
- **Menu Name**: "Target Constraints/Has Attack Effect"
- **Properties**: `effectType` (string)

#### TargetConstraintDoesAttack
Checks if the target has an attack action.
- **File**: `TargetConstraintDoesAttack.cs`
- **Menu Name**: "Target Constraints/Does Attack"

#### TargetConstraintDoesDamage
Checks if the target has a damage-dealing effect.
- **File**: `TargetConstraintDoesDamage.cs`
- **Menu Name**: "Target Constraints/Does Damage"

#### TargetConstraintDoesKill
Checks if the target has a kill effect.
- **File**: `TargetConstraintDoesKill.cs`
- **Menu Name**: "Target Constraints/Does Kill"

### Status Constraints

#### TargetConstraintStatusMoreThan
Checks if the target has a specific status effect with a count greater than a specified amount.
- **File**: `TargetConstraintStatusMoreThan.cs`
- **Menu Name**: "Target Constraints/Status More Than"
- **Properties**: `status` (StatusEffectData), `count` (int)

#### TargetConstraintHasStatusType
Checks if the target has a status effect of a specific type.
- **File**: `TargetConstraintHasStatusType.cs`
- **Menu Name**: "Target Constraints/Has Status Type"
- **Properties**: `statusType` (string)

#### TargetConstraintHasStatus
Checks if the target has a specific status effect.
- **File**: `TargetConstraintHasStatus.cs`
- **Menu Name**: "Target Constraints/Has Status"
- **Properties**: `status` (StatusEffectData)

#### TargetConstraintHasReaction
Checks if the target has a specific reaction.
- **File**: `TargetConstraintHasReaction.cs`
- **Menu Name**: "Target Constraints/Has Reaction"
- **Properties**: `reaction` (string)

### Stat Constraints

#### TargetConstraintAttackMoreThan
Checks if the target's attack is greater than a specified value.
- **File**: `TargetConstraintAttackMoreThan.cs`
- **Menu Name**: "Target Constraints/Attack More Than"
- **Properties**: `attack` (int)

#### TargetConstraintHealthMoreThan
Checks if the target's health is greater than a specified value.
- **File**: `TargetConstraintHealthMoreThan.cs`
- **Menu Name**: "Target Constraints/Health More Than"
- **Properties**: `health` (int)

#### TargetConstraintMaxCounterMoreThan
Checks if the target's maximum counter is greater than a specified value.
- **File**: `TargetConstraintMaxCounterMoreThan.cs`
- **Menu Name**: "Target Constraints/Max Counter More Than"
- **Properties**: `count` (int)

#### TargetConstraintHasHealth
Checks if the target has health (is not a spell card).
- **File**: `TargetConstraintHasHealth.cs`
- **Menu Name**: "Target Constraints/Has Health"

### State Constraints

#### TargetConstraintIsAlive
Checks if the target is alive.
- **File**: `TargetConstraintIsAlive.cs`
- **Menu Name**: "Target Constraints/Is Alive"

#### TargetConstraintDamaged
Checks if the target is damaged (current health < max health).
- **File**: `TargetConstraintDamaged.cs`
- **Menu Name**: "Target Constraints/Is Damaged"

#### TargetConstraintCanBeBoosted
Checks if the target can be boosted.
- **File**: `TargetConstraintCanBeBoosted.cs`
- **Menu Name**: "Target Constraints/Can Be Boosted"

#### TargetConstraintCanBeHit
Checks if the target can be hit.
- **File**: `TargetConstraintCanBeHit.cs`
- **Menu Name**: "Target Constraints/Can Be Hit"

#### TargetConstraintOnBoard
Checks if the target is on the board.
- **File**: `TargetConstraintOnBoard.cs`
- **Menu Name**: "Target Constraints/On Board"

#### TargetConstraintIsInDeck
Checks if the target is in the player's deck.
- **File**: `TargetConstraintIsInDeck.cs`
- **Menu Name**: "Target Constraints/Is In Deck"

### Special Constraints

#### TargetConstraintHasUpgradeOfType
Checks if the target has an upgrade of a specific type.
- **File**: `TargetConstraintHasUpgradeOfType.cs`
- **Menu Name**: "Target Constraints/Has Upgrade Of Type"
- **Properties**: `type` (CardUpgradeData.Type), `countRequired` (int), `ignore` (Array of CardUpgradeData)

#### TargetConstraintHasTrait
Checks if the target has a specific trait.
- **File**: `TargetConstraintHasTrait.cs`
- **Menu Name**: "Target Constraints/Has Trait"
- **Properties**: `trait` (TraitData)

#### TargetConstraintHasCrown
Checks if the target has a crown.
- **File**: `TargetConstraintHasCrown.cs`
- **Menu Name**: "Target Constraints/Has Crown"

#### TargetConstraintCanTakeCrown
Checks if the target can take a crown.
- **File**: `TargetConstraintCanTakeCrown.cs`
- **Menu Name**: "Target Constraints/Can Take Crown"

#### TargetConstraintPlayType
Checks the play type of the target.
- **File**: `TargetConstraintPlayType.cs`
- **Menu Name**: "Target Constraints/Play Type"
- **Properties**: `type` (PlayType)

#### TargetConstraintPlayOnSlot
Checks if the target can be played on a slot.
- **File**: `TargetConstraintPlayOnSlot.cs`
- **Menu Name**: "Target Constraints/Play On Slot"

#### TargetConstraintNeedsTarget
Checks if the target needs a target to be played.
- **File**: `TargetConstraintNeedsTarget.cs`
- **Menu Name**: "Target Constraints/Needs Target"

## Example Usage

### Basic Constraint Usage

Here's how constraints are used in the game to check if a target meets specific conditions:

```csharp
// Get a target constraint (typically assigned in the Inspector)
[SerializeField]
private TargetConstraint myConstraint;

// Check if an entity meets the constraint
Entity someEntity = /* get entity reference */;
bool canTarget = myConstraint.Check(someEntity);

// Check if a card data meets the constraint
CardData someCardData = /* get card data reference */;
bool canTargetCard = myConstraint.Check(someCardData);

// Using the 'not' property to invert results
myConstraint.not = true; // Inverts all check results
```

### Creating a Custom Constraint

Example of creating a custom constraint that combines multiple conditions:

```csharp
[CreateAssetMenu(fileName = "Custom Constraint", menuName = "Target Constraints/Custom Constraint")]
public class CustomConstraint : TargetConstraint
{
    [SerializeField]
    private TargetConstraint[] constraints;

    public override bool Check(Entity target)
    {
        foreach (var constraint in constraints)
        {
            if (!constraint.Check(target))
            {
                return not; // Return 'not' if any constraint fails
            }
        }
        return !not; // Return '!not' if all constraints pass
    }

    public override bool Check(CardData targetData)
    {
        foreach (var constraint in constraints)
        {
            if (!constraint.Check(targetData))
            {
                return not; // Return 'not' if any constraint fails
            }
        }
        return !not; // Return '!not' if all constraints pass
    }
}
```

### Building Complex Targeting Rules

Example of setting up complex targeting logic through the inspector:

1. Create a `TargetConstraintAnd` asset
2. Assign child constraints to its `constraints` array:
   - `TargetConstraintIsUnit`
   - `TargetConstraintHealthMoreThan` (value: 3)
   - `TargetConstraintHasStatusType` (statusType: "frozen")

This creates a constraint that only targets units with more than 3 health that have the "frozen" status.

### Integration with Card Abilities

Example of how constraints are used in card abilities:

```csharp
// CardScript that uses a target constraint
[CreateAssetMenu(fileName = "CustomAttackScript", menuName = "Card Scripts/Custom Attack")]
public class CustomAttackScript : CardScript
{
    [SerializeField]
    private TargetConstraint targetConstraint;
    
    [SerializeField]
    private int damage = 2;
    
    public override CardAction[] GetActions(Entity entity, Target target)
    {
        if (target.entity != null && targetConstraint.Check(target.entity))
        {
            // Target meets our constraint, do damage
            return new CardAction[] 
            { 
                new ActionEffectApply(entity, target.entity, new Hit(entity, target.entity, damage)) 
            };
        }
        
        // Target doesn't meet our constraint
        return new CardAction[0];
    }
}
```

## Modding Considerations

### Creating Custom Constraints

When creating custom constraints for mods:

1. **Inherit from TargetConstraint**: Create a new class that inherits from `TargetConstraint`
2. **Implement Both Check Methods**: Always implement both `Check(Entity)` and `Check(CardData)` methods
3. **Handle the 'not' Property**: Remember to respect the `not` property in your return values
4. **Use CreateAssetMenu**: Add the `CreateAssetMenu` attribute so your constraint appears in the Unity editor
5. **Use Descriptive Naming**: Follow the naming convention "Target Constraints/Your Constraint Name"

### Using Constraints in Mods

Target constraints can be used in mods for:

1. **Custom Card Effects**: Defining what cards can be targeted by custom abilities
2. **AI Decision Making**: Determining what entities the AI should target
3. **Event Triggers**: Creating event conditions based on entity state
4. **UI Interactions**: Controlling what cards can be selected in UI elements

### Advanced Example: Custom Status Constraint

```csharp
[CreateAssetMenu(fileName = "Has Custom Status", menuName = "Target Constraints/Has Custom Status")]
public class TargetConstraintHasCustomStatus : TargetConstraint
{
    [SerializeField]
    public string customStatusId;
    
    [SerializeField]
    public int minimumStacks = 1;
    
    public override bool Check(Entity target)
    {
        if (target == null || target.statusEffects == null)
            return not;
            
        StatusEffectData status = target.statusEffects.Find(s => s.type == customStatusId);
        if (status == null || status.count < minimumStacks)
            return not;
            
        return !not;
    }
    
    public override bool Check(CardData targetData)
    {
        if (targetData == null || targetData.startWithEffects == null)
            return not;
            
        foreach (var effect in targetData.startWithEffects)
        {
            if (effect.data.type == customStatusId && effect.count >= minimumStacks)
                return !not;
        }
        
        return not;
    }
}
```

## Best Practices

- **Combine Constraints**: Use `TargetConstraintAnd` and `TargetConstraintOr` to create complex targeting rules instead of writing custom constraints when possible
- **Handle Null Cases**: Always check for null references in your constraint implementations
- **Respect the 'not' Property**: Always consider the `not` property in your return statements
- **Test Both Entity and CardData**: Ensure your constraints work correctly for both Entity and CardData targets
- **Descriptive Naming**: Use clear, descriptive names for your constraint assets
- **Use SerializeField**: Use the `[SerializeField]` attribute for properties that should be configurable in the Unity editor

## Related Documentation

- [Enhanced Charm Creation Guide](EnhancedCharmCreation.md): How to use target constraints with charms
- [CardUpgradeData Documentation](CardUpgradeData.md): Understanding the CardUpgradeData system
- [Charm Creation Guide](CharmCreation.md): Basic charm creation techniques

## Last Updated
May 11, 2025
