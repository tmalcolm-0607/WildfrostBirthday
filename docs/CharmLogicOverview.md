# Charm Logic Overview

_Last Updated: May 12, 2025_

## Purpose
This document provides a high-level overview of charm logic in the MadFamily Tribe Mod for Wildfrost. It explains how charms are defined, registered, and managed, and outlines best practices for modularity, helpers/utilities, and migration.

---

## 1. What is a Charm?
A **charm** is a modifier that can be attached to cards, granting new abilities or altering stats. Charms have their own data, visuals, and registration logic, and are referenced by cards and reward pools.

---

## 2. Charm System Components
- **Charm Data:** Defines the charm's properties, effects, and visuals.
- **Charm Registration:** Adds the charm to the mod and makes it available for use.
- **Helpers/Utilities:** Used for safe registration, batch lookup, and modularization (see tutorials/Assembly-CSharp only).
- **Reward Pools:** Charms are referenced in reward pools and by cards.

---

## 3. Charm Logic Flow
1. **Define charm data** (properties, effects, visuals).
2. **Register the charm** using supported helpers or direct registration (see tutorials).
3. **Reference the charm** in cards, reward pools, or other systems.
4. **Test and validate** charm registration and gameplay.

---

## 4. Best Practices & Rationale
- **One class/file per charm** for clarity and maintainability.
- **Use only helpers/utilities from tutorials or Assembly-CSharp.**
- **Document all charms and helpers** with usage examples.
- **Never modify decompiled game code.**
- **Review and refactor regularly.**

---

## 5. Quick Reference Table
| System         | Key Helpers/Methods                | Example Usage/Code Snippet                  | Detailed Docs                        |
|----------------|-----------------------------------|---------------------------------------------|--------------------------------------|
| Charm Data     | CardUpgradeData, AddCharm         | `AddCharm("PugCharm", ...)`                | Tutorial3, EnhancedCharmCreation.md  |
| Registration   | AddCharm, DataList                | See tutorials for registration              | Tutorial3, EnhancedCharmCreation.md  |
| Reward Pools   | DataList, CreateRewardPool        | `CreateRewardPool(..., DataList<CardUpgradeData>(...))` | data/Reward Pools.md           |
| Utilities      | RemoveNulls, TryGet               | `RemoveNulls(charms)`<br>`TryGet<CardUpgradeData>(...)` | ModdingToolsAndTechniques.md     |

---

**Note:** Only helpers and patterns from the official tutorials or Assembly-CSharp are currently supported. New helpers will be introduced and documented as the project evolves. If a helper is not documented in the tutorials or Assembly-CSharp, it should not be used or referenced yet.

---

## 5.1 Migration & Refactor Checklist

Use this checklist when migrating, refactoring, or reviewing charm logic:

**General Migration Checklist:**
- [ ] Review all charm logic for references to legacy or unsupported helpers. Remove or replace with tutorial/Assembly-CSharp patterns.
- [ ] Ensure all helpers/utilities used are documented in the official tutorials or Assembly-CSharp.
- [ ] Update all code snippets and examples to match current best practices.
- [ ] Validate that all charm registration follows the recommended order and uses supported helpers.
- [ ] Modularize charm logic: one class/file per charm where possible.
- [ ] Update documentation to reflect any changes in logic, helpers, or workflow.
- [ ] Run tests or manual checks to confirm charm registration and gameplay work as expected.

**Advanced Refactor Checklist:**
- [ ] Use `RemoveNulls` on unload to clean up charm lists.
- [ ] Scan for duplicate helpers/utilities and consolidate as needed.
- [ ] Assign a reviewer to check both code and documentation for consistency and clarity after major changes.

**Documentation & Review Checklist:**
- [ ] Update this overview and all referenced docs after each migration or refactor.
- [ ] Add or update usage examples for every helper in both this overview and [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md).
- [ ] Schedule regular documentation reviews and set a "Next Review Date" in this file.

**Next Review Date:** _(Set after each major migration or refactor)_

---

## 6. Advanced Pitfalls & Migration Tips
- **Load order issues:** Register effects before referencing them in charms. Use helpers (as described in the tutorials) to ensure correct order.
- **Null references:** Use `RemoveNulls` on unload to clean up charm lists. Always validate charm lists after migration or refactor.
- **Asset duplication:** Use helpers like `DataList` to avoid duplicating logic or assets.
- **Documentation drift:** After each migration or refactor, update this overview and all referenced docs to match the new structure and logic.
- **Review workflow:** After major changes, assign a reviewer to check both code and documentation for consistency and clarity.

---

## 7. Feedback & Review
- Share this document with collaborators for feedback.
- Revise as charm logic evolves.

---

## 8. References
- [Tutorial3_CharmsAndKeywords.md](Tutorial3_CharmsAndKeywords.md)
- [EnhancedCharmCreation.md](EnhancedCharmCreation.md)
- [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md)
- [data/Reward Pools.md](data/Reward%20Pools.md)

---

## 9. Advanced Builder Patterns & Automation

### Overview
This section provides a comprehensive guide to advanced usage of `CardUpgradeDataBuilder` for charms, including extensibility, automation, and helper patterns. It is intended for modders who want to go beyond basic tutorial usage and create maintainable, scalable, and data-driven charm systems.

---

### CardUpgradeDataBuilder: Full API & Usage Breakdown

#### Purpose
`CardUpgradeDataBuilder` is a fluent builder for creating and configuring charms and other upgrades. It allows you to set all properties, attach effects, traits, constraints, and scripts, and register the result for use in the game.

#### Method-by-Method Breakdown

| Method/Property                | What it does / When to use                                      |
|-------------------------------|---------------------------------------------------------------|
| `.Create(cardId)`              | Unique ID for the charm/upgrade                               |
| `.AddPool(poolName)`           | Assigns to a reward pool                                      |
| `.WithType(type)`              | Sets upgrade type (usually `Charm`)                           |
| `.WithImage(path)`             | Sets the sprite/image                                         |
| `.WithTitle(title)`            | Sets display name                                             |
| `.WithText(text)`              | Sets card text/flavor                                         |
| `.WithTier(tier)`              | Sets rarity/tier                                              |
| `.SubscribeToAfterAllBuildEvent` | Post-process, attach effects/traits/constraints/scripts      |
| `data.effects`                 | Array of status effects (use `SStack`)                        |
| `data.giveTraits`              | Array of traits (use `TStack`)                                |
| `data.targetConstraints`       | Array of constraints (e.g., only items)                       |
| `data.scripts`                 | Array of custom scripts                                       |

#### Example: Full Usage

```csharp
var charm = new CardUpgradeDataBuilder(this)
    .Create("charm-example")
    .AddPool("GeneralCharmPool")
    .WithType(CardUpgradeData.Type.Charm)
    .WithImage("charms/example.png")
    .WithTitle("Example Charm")
    .WithText("Does something cool")
    .WithTier(2)
    .SubscribeToAfterAllBuildEvent(data => {
        data.effects = new StatusEffectStacks[] { SStack("SomeEffect", 1) };
        data.giveTraits = new TraitStacks[] { TStack("SomeTrait", 1) };
        data.targetConstraints = new TargetConstraint[] { ScriptableObject.CreateInstance<TargetConstraintIsItem>() };
        data.scripts = new CardScript[] { ScriptableObject.CreateInstance<CardScriptChangeMain>() };
    });
assets.Add(charm);
```

---

### Advanced Helper & Automation Patterns

#### Bulk Adders
Create helpers to add multiple effects, traits, or constraints in one call:

```csharp
public static CardUpgradeDataBuilder AddEffects(this CardUpgradeDataBuilder builder, params StatusEffectStacks[] effects) { /* ... */ }
public static CardUpgradeDataBuilder AddTraits(this CardUpgradeDataBuilder builder, params TraitStacks[] traits) { /* ... */ }
public static CardUpgradeDataBuilder AddConstraints(this CardUpgradeDataBuilder builder, params TargetConstraint[] constraints) { /* ... */ }
```

#### Data-Driven Creation
Automate charm creation from config/data:

```csharp
public static CardUpgradeDataBuilder FromConfig(this CardUpgradeDataBuilder builder, CharmConfig config) { /* ... */ }
```

#### Validation & Debugging
Helpers for asset uniqueness, reference checks, and builder chain debugging:

- `AssetUniquenessChecker`: Prevent duplicate IDs/names.
- `ReferenceValidator`: Ensure all referenced effects/traits/constraints exist.
- `BuilderChainDebugger`: Inspect builder state at each step.

#### Registration Hooks
Allow other mods to register callbacks or modify your builders before finalization:

```csharp
mod.OnRegisterCharm += (builder) => { /* ... */ };
```

---

### Constraint System: Full Reference & Examples
See [TargetConstraint.md](TargetConstraint.md) for a full list of constraint types and usage. Example:

```csharp
constraints: new[] {
    ScriptableObject.CreateInstance<TargetConstraintIsItem>(),
    ScriptableObject.CreateInstance<TargetConstraintAttackMoreThan>().FreeModify(c => c.attack = 2)
}
```

---

### Migration Checklist: Manual to Automated Builder Usage
- [ ] Identify repetitive or manual charm creation code.
- [ ] Replace with builder helpers and automation patterns.
- [ ] Use bulk adders for effects/traits/constraints.
- [ ] Validate all references and uniqueness.
- [ ] Register all assets using batch helpers.
- [ ] Document new helpers and update usage examples.

---

### Best Practices
- Use builder helpers for all new charms/upgrades.
- Automate repetitive logic with data-driven or batch patterns.
- Use constraints to limit applicability and improve balance.
- Validate and debug builder chains during development.
- Expose extension points for mod interoperability.

---

### References
- [EnhancedCharmCreation.md](EnhancedCharmCreation.md)
- [TargetConstraint.md](TargetConstraint.md)
- [UtilitiesOverview.md](UtilitiesOverview.md)

---

_Last updated: May 12, 2025_
