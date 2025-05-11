# StatusEffectApplyX System

## Overview

The `StatusEffectApplyX` system is a powerful and flexible framework in Wildfrost for applying status effects, damage, or other card modifications based on various game triggers. This abstract base class serves as the foundation for dozens of specialized effect classes that apply effects under specific conditions, forming one of the core mechanics for card interactions in the game.

The system follows a consistent pattern where specific triggers (like "when deployed" or "when health is lost") cause status effects to be applied to targeted entities. This modular design allows for a rich variety of card behaviors without requiring custom code for each case.

## Class Architecture

### StatusEffectApplyX

The abstract base class that defines common functionality for all status effect application scripts.

#### Key Properties

| Property | Type | Description |
|----------|------|-------------|
| `dealDamage` | `bool` | If true, applies damage instead of a status effect |
| `effectToApply` | `StatusEffectData` | The status effect to apply |
| `selectScript` | `SelectScript<Entity>` | Script for selecting target entities |
| `applyConstraints` | `TargetConstraint[]` | Additional constraints on targets |
| `applyEqualAmount` | `bool` | Whether to apply an amount equal to a context value |
| `contextEqualAmount` | `ScriptableAmount` | Amount scriptable used when applying equal amounts |
| `equalAmountBonusMult` | `float` | Multiplier for equal amount bonus |
| `scriptableAmount` | `ScriptableAmount` | Amount scriptable used when not applying equal amounts |
| `applyToFlags` | `ApplyToFlags` | Flags defining which entities to apply effects to |
| `applyInReverseOrder` | `bool` | Whether to apply effects in reverse order |
| `canRetaliate` | `bool` | Whether targets can retaliate when effects are applied |
| `countsAsHit` | `bool` | Whether applying the effect counts as a hit |
| `targetMustBeAlive` | `bool` | Whether target must be alive for effect to trigger |
| `pauseAfter` | `float` | Duration to pause after applying effect |

#### Enums

**ApplyToFlags**: Defines which entities receive the effect

- `Self`: Apply to the card itself
- `Hand`: Apply to cards in the player's hand
- `EnemyHand`: Apply to enemy hand cards
- `Allies`: Apply to all allied units
- `AlliesInRow`: Apply to allies in the same row
- `FrontAlly`/`BackAlly`: Apply to allies at front/back
- `Enemies`: Apply to all enemy units
- `EnemiesInRow`: Apply to enemies in the same row
- `FrontEnemy`: Apply to front enemy
- `Attacker`: Apply to the entity that attacked
- `Target`: Apply to the targeted entity
- `RandomAlly`/`RandomEnemy`/`RandomUnit`: Apply to a random entity
- And many more target options...

#### Key Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `GetAmount(Entity entity, bool equalAmount, int equalTo)` | `virtual int` | Get the amount of effect to apply |
| `TargetSilenced()` | `virtual bool` | Check if the target is silenced |
| `AppliesTo(ApplyToFlags applyTo)` | `bool` | Check if effect applies to a specific flag |
| `Run(List<Entity> targets, int amount)` | `IEnumerator` | Run the effect application logic |
| `GetTargets(Hit hit)` | `List<Entity>` | Get list of targets based on apply flags |
| `Sequence(List<Entity> targets, int amount)` | `IEnumerator` | Sequence for applying effects to targets |
| `CanAffect(Entity entity)` | `bool` | Check if effect can affect an entity |

### Common Derived Classes

There are dozens of specialized StatusEffectApplyX classes, each triggering on different game events:

#### Combat Triggers

- **StatusEffectApplyXWhenHit**: Applies effect when the card is hit
- **StatusEffectApplyXWhenDamageTaken**: Applies effect when taking damage
- **StatusEffectApplyXWhenHealthLost**: Applies effect when health is lost
- **StatusEffectApplyXWhenHealed**: Applies effect when the unit is healed
- **StatusEffectApplyXWhenUnitIsKilled**: Applies effect when a unit is killed

#### State Change Triggers

- **StatusEffectApplyXWhenDeployed**: Applies effect when deployed to the battlefield
- **StatusEffectApplyXWhenYAppliedTo**: Applies effect X when status Y is applied
- **StatusEffectApplyXWhenYLost**: Applies effect X when status Y is lost
- **StatusEffectApplyXWhenBuilt**: Applies effect when a structure is built

#### Card Movement Triggers

- **StatusEffectApplyXWhenDrawn**: Applies effect when a card is drawn
- **StatusEffectApplyXWhenDiscarded**: Applies effect when a card is discarded
- **StatusEffectApplyXWhenDestroyed**: Applies effect when a card is destroyed

## Usage Examples

### Basic Status Effect Application

```csharp
// Create a simple "Apply Frost when deployed" effect
StatusEffectApplyXWhenDeployed frostOnDeploy = ScriptableObject.CreateInstance<StatusEffectApplyXWhenDeployed>();
frostOnDeploy.name = "Apply Frost When Deployed";
frostOnDeploy.effectToApply = AddressableLoader.Get<StatusEffectData>("StatusEffectData", "frost");
frostOnDeploy.scriptableAmount = CreateScriptableAmount(2); // Apply 2 Frost
frostOnDeploy.applyToFlags = StatusEffectApplyX.ApplyToFlags.FrontEnemy; // Apply to front enemy
frostOnDeploy.whenSelfDeployed = true;
```

### Triggering Effects Based on Other Status Effects

```csharp
// Create "Apply Thorns when Block is lost" effect
StatusEffectApplyXWhenYLost thornsWhenBlockLost = ScriptableObject.CreateInstance<StatusEffectApplyXWhenYLost>();
thornsWhenBlockLost.name = "Apply Thorns When Block Lost";
thornsWhenBlockLost.effectToApply = AddressableLoader.Get<StatusEffectData>("StatusEffectData", "thorns");
thornsWhenBlockLost.scriptableAmount = CreateScriptableAmount(1); // Apply 1 Thorns
thornsWhenBlockLost.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self; // Apply to self
thornsWhenBlockLost.statusType = "block"; // Trigger when block is lost
thornsWhenBlockLost.whenAllLost = false; // Trigger when any block is lost, not just all
```

### Conditional Damage on Attack

```csharp
// Create "Deal damage to enemies with Frost when attacked"
StatusEffectApplyXWhenHit damageOnHit = ScriptableObject.CreateInstance<StatusEffectApplyXWhenHit>();
damageOnHit.name = "Damage Frosty Enemies When Hit";
damageOnHit.dealDamage = true; // Apply damage instead of a status effect
damageOnHit.scriptableAmount = CreateScriptableAmount(3); // Deal 3 damage
damageOnHit.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker; // Apply to attacker
damageOnHit.applyConstraints = new TargetConstraint[] {
    AddressableLoader.Get<TargetConstraint>("TargetConstraint", "has_status_frost")
}; // Only target entities with Frost
```

## Integration with Other Systems

The StatusEffectApplyX system integrates with several core game systems:

1. **ActionQueue**: Effects are added to the action queue for sequenced execution.

2. **Card System**: Effects target and modify card entities in the game.

3. **Battle System**: Effects respond to battle events like attacks, damage, and deployments.

4. **Status Effect System**: Applies various status effects to cards.

5. **Target Constraint System**: Uses constraints to filter which cards can receive effects.

6. **Animation System**: Controls timing of effect application and related animations.

## Modding Considerations

### Creating Custom Status Effect Triggers

To create a custom effect trigger:

1. Create a class that inherits from `StatusEffectApplyX`.
2. Implement the necessary event handlers for your trigger condition.
3. Use the `Run()` method to apply effects when your condition is met.

Example:

```csharp
[CreateAssetMenu(menuName = "Status Effects/Specific/Apply X When Mana Changes", fileName = "Apply X When Mana Changes")]
public class StatusEffectApplyXWhenManaChanges : StatusEffectApplyX
{
    [SerializeField]
    public int triggerThreshold = 3; // Trigger when mana reaches this value
    
    private int lastManaValue = 0;
    
    public override void Init()
    {
        Events.OnManaChanged += ManaChanged;
        lastManaValue = References.Player.mana;
    }
    
    public void OnDestroy()
    {
        Events.OnManaChanged -= ManaChanged;
    }
    
    public void ManaChanged(int newMana)
    {
        if (newMana >= triggerThreshold && lastManaValue < triggerThreshold && target.IsAliveAndExists())
        {
            ActionQueue.Stack(new ActionSequence(ApplyEffects()) 
            {
                note = name,
                priority = eventPriority
            });
        }
        lastManaValue = newMana;
    }
    
    public IEnumerator ApplyEffects()
    {
        yield return Run(GetTargets());
    }
}
```

### Customizing Effect Amount Calculation

To create custom amount calculations:

```csharp
public class CustomAmountScript : ScriptableAmount
{
    public override int Get(Entity entity)
    {
        // Calculate amount based on entity's state
        int baseAmount = 1;
        
        // Increase amount for each allied unit
        if (entity.owner == References.Player)
        {
            baseAmount += Battle.GetCardsOnBoard(References.Player).Count;
        }
        
        // Cap at a maximum value
        return Mathf.Min(baseAmount, 5);
    }
}

// Then use this in a StatusEffectApplyX
statusEffect.scriptableAmount = customAmountScript;
```

### Target Selection Strategies

You can combine different targeting approaches:

```csharp
// Create an effect that applies to different targets based on conditions
StatusEffectApplyXWhenDeployed complexTargeting = ScriptableObject.CreateInstance<StatusEffectApplyXWhenDeployed>();
complexTargeting.name = "Complex Targeting Example";
complexTargeting.effectToApply = someStatusEffect;

// Primary targeting using flags
complexTargeting.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;

// Further filtering with constraints
complexTargeting.applyConstraints = new TargetConstraint[] {
    AddressableLoader.Get<TargetConstraint>("TargetConstraint", "is_organic"),
    AddressableLoader.Get<TargetConstraint>("TargetConstraint", "hp_less_than_5")
};

// Custom targeting with select script
complexTargeting.selectScript = CustomSelectScript.CreateInstance<CustomSelectScript>();
```

## Key Design Patterns

1. **Composition over Inheritance**: While there are many derived classes, each focuses on a specific trigger condition, keeping class responsibilities clear.

2. **Event-Driven**: The system responds to game events rather than polling for changes.

3. **Scriptable Objects**: Uses ScriptableObjects for the flexibility to create and modify effects in the Unity editor.

4. **Flags and Constraints**: Combines flag-based targeting with constraint filtering for precise effect targeting.

## Common Issues and Solutions

1. **Multiple Effects Triggering Simultaneously**: If multiple effects trigger at the same time, use the `eventPriority` property to control execution order.

2. **Effect Not Applying to Expected Targets**: Check both `applyToFlags` and `applyConstraints` to ensure targets meet all criteria.

3. **Effects on Silenced Cards**: Remember that `targetMustBeAlive` and `TargetSilenced()` can prevent effects from triggering.

4. **Animation Timing Issues**: Use `waitForAnimationEnd` and `pauseAfter` to ensure visual effects have time to play before the next action.

## Example Status Effect Types

The system can apply many types of status effects:

- **Stat Modifiers**: Block, Frenzy, Strength, etc.
- **DOTs**: Poison, Burn, Freeze, Frost
- **Utility**: Shield, Thorns, Taunt
- **Special States**: Stealth, Flying, Ephemeral
