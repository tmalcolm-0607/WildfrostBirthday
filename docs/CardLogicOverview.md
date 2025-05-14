# 9. Advanced Builder Patterns & Automation

## Overview
This section provides a comprehensive guide to advanced usage of `CardDataBuilder` for cards and `StatusEffectDataBuilder` for status effects, including extensibility, automation, and helper patterns. It is intended for modders who want to go beyond basic tutorial usage and create maintainable, scalable, and data-driven card and effect systems.

---

## CardDataBuilder: Full API & Usage Breakdown

### Purpose
`CardDataBuilder` is a fluent builder for creating and configuring cards. It allows you to set all properties, attach effects, traits, scripts, and register the result for use in the game.

### Method-by-Method Breakdown

| Method/Property                | What it does / When to use                                      |
|-------------------------------|---------------------------------------------------------------|
| `.CreateUnit(cardId, name)`    | Unique ID and display name for the card (unit)                |
| `.CreateItem(cardId, name)`    | Unique ID and display name for the card (item)                |
| `.SetSprites(main, bg)`        | Sets the main and background sprites                          |
| `.SetStats(hp, atk, counter)`  | Sets health, attack, and counter values                       |
| `.WithCardType(type)`          | Sets the card type (e.g., Friendly, Leader, Item)             |
| `.WithFlavour(text)`           | Sets card flavor text                                         |
| `.WithValue(value)`            | Sets the card's value                                         |
| `.AddPool(poolName)`           | Assigns to a reward pool                                      |
| `.SubscribeToAfterAllBuildEvent` | Post-process, attach effects/traits/scripts                  |
| `data.attackEffects`           | Array of status effects applied on attack (use `SStack`)      |
| `data.startWithEffects`        | Array of status effects at start (use `SStack`)               |
| `data.traits`                  | List of traits (use `TStack`)                                 |
| `data.createScripts`           | Array of custom scripts                                       |

### Example: Full Usage

```csharp
var card = new CardDataBuilder(this)
    .CreateUnit("unit-example", "Example Unit")
    .SetSprites("unit.png", "unit_bg.png")
    .SetStats(5, 2, 3)
    .WithCardType("Friendly")
    .WithFlavour("A sample unit.")
    .WithValue(50)
    .AddPool("MagicUnitPool")
    .SubscribeToAfterAllBuildEvent(data => {
        data.attackEffects = new[] { SStack("On Hit Apply Snow", 1) };
        data.startWithEffects = new[] { SStack("On Turn Heal Self", 1) };
        data.traits = new List<TraitStacks> { TStack("Taunt", 1) };
        data.createScripts = new CardScript[] { ScriptableObject.CreateInstance<CardScriptChangeBackground>() };
    });
assets.Add(card);
```

---

## StatusEffectDataBuilder: Full API & Usage Breakdown

### Purpose
`StatusEffectDataBuilder` is a fluent builder for creating and configuring status effects. It allows you to set all properties, triggers, constraints, and register the result for use in the game.

### Method-by-Method Breakdown

| Method/Property                | What it does / When to use                                      |
|-------------------------------|---------------------------------------------------------------|
| `.Create<T>(id)`               | Unique ID and type for the effect                             |
| `.WithText(text)`              | Sets the effect's display text                                |
| `.WithType(type)`              | Sets the effect's classification/type                         |
| `.WithCanBeBoosted(bool)`      | Whether the effect can be boosted                             |
| `.WithStackable(bool)`         | Whether the effect can stack                                  |
| `.WithTextInsert(text)`        | Inserts additional text into the effect's description         |
| `.SubscribeToAfterAllBuildEvent` | Post-process, attach triggers, constraints, etc.             |

### Example: Full Usage

```csharp
var effect = new StatusEffectDataBuilder(this)
    .Create<StatusEffectApplyXOnTurn>("OnTurnApplyTeethToAllies")
    .WithText("While Active: Teeth to Allies")
    .WithType("Buff")
    .WithCanBeBoosted(true)
    .SubscribeToAfterAllBuildEvent(data => {
        data.effectToApply = TryGet<StatusEffectData>("Teeth");
        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
    });
assets.Add(effect);
```

---

## Advanced Helper & Automation Patterns

### Bulk Adders
Create helpers to add multiple effects, traits, or scripts in one call:

```csharp
public static CardDataBuilder AddAttackEffects(this CardDataBuilder builder, params StatusEffectStacks[] effects) { /* ... */ }
public static CardDataBuilder AddTraits(this CardDataBuilder builder, params TraitStacks[] traits) { /* ... */ }
public static CardDataBuilder AddScripts(this CardDataBuilder builder, params CardScript[] scripts) { /* ... */ }
public static StatusEffectDataBuilder AddTriggers(this StatusEffectDataBuilder builder, params object[] triggers) { /* ... */ }
```

### Data-Driven Creation
Automate card/effect creation from config/data:

```csharp
public static CardDataBuilder FromConfig(this CardDataBuilder builder, CardConfig config) { /* ... */ }
public static StatusEffectDataBuilder FromConfig(this StatusEffectDataBuilder builder, EffectConfig config) { /* ... */ }
```

### Validation & Debugging
Helpers for asset uniqueness, reference checks, and builder chain debugging:

- `AssetUniquenessChecker`: Prevent duplicate IDs/names.
- `ReferenceValidator`: Ensure all referenced effects/traits/scripts exist.
- `BuilderChainDebugger`: Inspect builder state at each step.

### Registration Hooks
Allow other mods to register callbacks or modify your builders before finalization:

```csharp
mod.OnRegisterCard += (builder) => { /* ... */ };
mod.OnRegisterStatusEffect += (builder) => { /* ... */ };
```

---

## Migration Checklist: Manual to Automated Builder Usage
- [ ] Identify repetitive or manual card/effect creation code.
- [ ] Replace with builder helpers and automation patterns.
- [ ] Use bulk adders for effects/traits/scripts/triggers.
- [ ] Validate all references and uniqueness.
- [ ] Register all assets using batch helpers.
- [ ] Document new helpers and update usage examples.

---

## Best Practices
- Use builder helpers for all new cards/effects.
- Automate repetitive logic with data-driven or batch patterns.
- Use constraints and triggers to limit applicability and improve balance.
- Validate and debug builder chains during development.
- Expose extension points for mod interoperability.

---

## References
- [AddFamilyUnit Guide](AddFamilyUnit.md)
- [Implementing Status Effects Guide](ImplementingStatusEffects.md)
- [StatusEffectData Class Documentation](StatusEffectData.md)
- [CardData Documentation](CardData.md)
- [StatusEffectExamples.md](StatusEffectExamples.md)

---

_Last updated: May 12, 2025_
# Card Logic Overview

_Last Updated: May 12, 2025_

## Purpose
This document provides a high-level overview of card logic in the MadFamily Tribe Mod for Wildfrost. It explains how cards are defined, registered, and managed, and outlines best practices for modularity, helpers/utilities, and migration.

---

## 1. What is a Card?
A **card** in Wildfrost represents a playable entity (unit, item, spell, etc.) with stats, effects, and traits. Cards are central to gameplay and are referenced by tribes, reward pools, and inventory systems.

---

## 2. Card System Components
- **Card Data:** Defines the card's stats, effects, traits, and visuals.
- **Card Registration:** Adds the card to the mod and makes it available for use.
- **Helpers/Utilities:** Used for batch asset lookup, safe registration, and modularization (see tutorials/Assembly-CSharp only).
- **Inventory/Reward Pools:** Cards are referenced in starting decks and as possible rewards.

---

## 3. Card Logic Flow
1. **Define card data** (stats, effects, traits, visuals).
2. **Register the card** using supported helpers or direct registration (see tutorials).
3. **Reference the card** in tribe inventory, reward pools, or other systems.
4. **Test and validate** card registration and gameplay.

---

## 4. Best Practices & Rationale
- **One class/file per card** for clarity and maintainability.
- **Use only helpers/utilities from tutorials or Assembly-CSharp.**
- **Document all cards and helpers** with usage examples.
- **Never modify decompiled game code.**
- **Review and refactor regularly.**

---

## 5. Quick Reference Table
| System         | Key Helpers/Methods                | Example Usage/Code Snippet                  | Detailed Docs                        |
|----------------|-----------------------------------|---------------------------------------------|--------------------------------------|
| Card Data      | CardData, CardDataBuilder         | See tutorials for card definition           | Tutorial2, CardData.md               |
| Registration   | AddItemCard, DataList             | `AddItemCard("SnowPillow", ...)`           | Tutorial2, Tutorial5_CreatingATribe  |
| Inventory      | DataList, Inventory               | `inventory.deck.list = DataList<CardData>(...)` | Tutorial5_CreatingATribe.md      |
| Reward Pools   | DataList, CreateRewardPool        | `CreateRewardPool(..., DataList<CardData>(...))` | data/Reward Pools.md           |
| Utilities      | RemoveNulls, TryGet               | `RemoveNulls(cards)`<br>`TryGet<CardData>(...)` | ModdingToolsAndTechniques.md     |

---

**Note:** Only helpers and patterns from the official tutorials or Assembly-CSharp are currently supported. New helpers will be introduced and documented as the project evolves. If a helper is not documented in the tutorials or Assembly-CSharp, it should not be used or referenced yet.

---

## 5.1 Migration & Refactor Checklist

Use this checklist when migrating, refactoring, or reviewing card logic:

**General Migration Checklist:**
- [ ] Review all card logic for references to legacy or unsupported helpers. Remove or replace with tutorial/Assembly-CSharp patterns.
- [ ] Ensure all helpers/utilities used are documented in the official tutorials or Assembly-CSharp.
- [ ] Update all code snippets and examples to match current best practices.
- [ ] Validate that all card registration follows the recommended order and uses supported helpers.
- [ ] Modularize card logic: one class/file per card where possible.
- [ ] Update documentation to reflect any changes in logic, helpers, or workflow.
- [ ] Run tests or manual checks to confirm card registration and gameplay work as expected.

**Advanced Refactor Checklist:**
- [ ] Use `RemoveNulls` on unload to clean up card lists.
- [ ] Scan for duplicate helpers/utilities and consolidate as needed.
- [ ] Assign a reviewer to check both code and documentation for consistency and clarity after major changes.

**Documentation & Review Checklist:**
- [ ] Update this overview and all referenced docs after each migration or refactor.
- [ ] Add or update usage examples for every helper in both this overview and [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md).
- [ ] Schedule regular documentation reviews and set a "Next Review Date" in this file.

**Next Review Date:** _(Set after each major migration or refactor)_

---

## 6. Advanced Pitfalls & Migration Tips
- **Load order issues:** Register effects before referencing them in cards. Use helpers (as described in the tutorials) to ensure correct order.
- **Null references:** Use `RemoveNulls` on unload to clean up card lists. Always validate card lists after migration or refactor.
- **Asset duplication:** Use helpers like `DataList` to avoid duplicating logic or assets.
- **Documentation drift:** After each migration or refactor, update this overview and all referenced docs to match the new structure and logic.
- **Review workflow:** After major changes, assign a reviewer to check both code and documentation for consistency and clarity.

---

## 7. Feedback & Review
- Testing plan and validation steps are now tracked in `docs/TestingPlan.md` (added 2025-05-14). All card logic and registration should be validated using this plan after each migration or refactor.
- Share this document with collaborators for feedback.
- Revise as card logic evolves.

---

## 8. References
- [Tutorial2_CardsAndStatusEffects.md](Tutorial2_CardsAndStatusEffects.md)
- [Tutorial5_CreatingATribe.md](Tutorial5_CreatingATribe.md)
- [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md)
- [CardData.md](CardData.md)
- [data/Reward Pools.md](data/Reward%20Pools.md)
