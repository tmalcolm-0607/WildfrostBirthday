# Tribe Synergy Charm – Dynamic Stat Boost by Shared Tribe

## Overview
This example demonstrates how to create a custom Charm that **increases a unit’s Attack and Health** for each card in the player’s deck/reserve sharing that unit’s Tribe (clan). The effect dynamically recalculates based on the inventory.

## Charm Data Definition – “Tribal Bond Charm”
We use `CardUpgradeDataBuilder` to define the charm’s properties:

```csharp
var tribalBondCharm = new CardUpgradeDataBuilder(ModContext)
    .Create("Charm_TribalBond")
    .AddPool("GeneralCharmPool")
    .WithType(CardUpgradeData.Type.Charm)
    .WithTitle("Tribal Bond Charm")
    .WithDescription("Gain +<em>Attack</em> and +<em>Health</em> for each ally of the same Tribe in your deck and reserve.")
    .WithIcon("tribalbond_charm.png")
    .WithTier(1)
    .SetScripts(Get<CardScript>("CardScriptTribeStatBoost"))
    .Build();
```

## Custom Script – `CardScriptTribeStatBoost`

```csharp
[CreateAssetMenu(fileName = "Tribe Stat Boost Script", menuName = "Card Scripts/Tribe Stat Boost")]
public class CardScriptTribeStatBoost : CardScript 
{
    public override void Run(CardData target) 
    {
        var tribeData = GetClass(target);  
        int sameTribeCount = 0;
        if (tribeData != null && References.PlayerData != null) 
        {
            foreach (CardData card in References.PlayerData.inventory.deck) {
                if (card != target && GetClass(card) == tribeData) sameTribeCount++;
            }
            foreach (CardData card in References.PlayerData.inventory.reserve) {
                if (GetClass(card) == tribeData) sameTribeCount++;
            }
        }

        if (sameTribeCount > 0 && target != null) 
        {
            StatusEffect attackBuff = Get<StatusEffect>("StatusEffectOngoingAttack");
            StatusEffect healthBuff = Get<StatusEffect>("StatusEffectOngoingMaxHealth");

            target.startWithEffects = IArrayExt.With<CardData.StatusEffectStacks>(
                target.startWithEffects, new CardData.StatusEffectStacks(attackBuff, sameTribeCount));
            target.startWithEffects = IArrayExt.With<CardData.StatusEffectStacks>(
                target.startWithEffects, new CardData.StatusEffectStacks(healthBuff, sameTribeCount));
        }
    }
}
```

## How It Works
1. Determine the unit's tribe using its `ClassData`.
2. Count all other cards in deck and reserve that share the same tribe.
3. Apply `StatusEffectOngoingAttack` and `StatusEffectOngoingMaxHealth` equal to the count found.

## UI Behavior
- Buff appears as green +X values on the unit’s card.
- Tooltip and status icons display on-hover with current buff.

## Systems Extended
- `CardUpgradeDataBuilder`
- `CardScript` system
- `StatusEffect` system
- `Inventory`, `ClassData`, `PlayerData`

## Further Documentation Improvements

### Expand Builder Class References
Cover more examples like `BattleDataBuilder`, `UnlockDataBuilder`.

### Document Card Behaviors
Explain how to apply traits/statuses conditionally (e.g., on-hit, on-death).

### Clarify Trigger & Reaction System
List `StatusEffectTriggerWhen*` types and explain how they’re used.

### Other Systems to Cover
- Targeting rules (TargetConstraint classes)
- Crown system
- Summons and reserves
- Event scripting and map progression