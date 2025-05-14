# Effect Logic Overview

_Last Updated: 2025-05-14_

## Modularization & Registration Pattern (MadFamily Mod)

All effects in the MadFamily mod are implemented in their own files (e.g., `StatusEffect_OnKillHealToSelf.cs`) as static classes with a `Register(WildFamilyMod mod)` method. This follows best practices:

- **One effect per file:** Each effect is defined in its own file for clarity and maintainability.
- **Minimal entry point:** The `Register` method only handles effect asset creation and registration, using only approved/documented helpers.
- **Approved helpers only:** Only helpers/utilities documented in [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md) and [UtilitiesOverview.md](UtilitiesOverview.md) are used (e.g., `TryGet`, `DataList`).
- **Documentation:** Each file is commented to explain its structure and registration logic.

See the file and comments for a full example. This pattern should be followed for all future effects.

_Last Updated: May 12, 2025_

## Purpose
This document provides a high-level overview of effect (status effect) logic in the MadFamily Tribe Mod for Wildfrost. It explains how effects are defined, registered, and managed, and outlines best practices for modularity, helpers/utilities, and migration.

---

## 1. What is an Effect?
An **effect** (status effect) is a gameplay mechanic applied to cards, units, or the game state, often with its own triggers, stacking rules, and implementation details. Effects are referenced by cards, charms, and other systems.

---

## 2. Effect System Components
- **Effect Data:** Defines the effect's logic, triggers, and visuals.
- **Effect Registration:** Adds the effect to the mod and makes it available for use.
- **Helpers/Utilities:** Used for safe registration, batch lookup, and modularization (see tutorials/Assembly-CSharp only).
- **Reward Pools:** Effects may be referenced in reward pools or by cards/charms.

---

## 3. Effect Logic Flow
1. **Define effect data** (logic, triggers, visuals).
2. **Register the effect** using supported helpers or direct registration (see tutorials).
3. **Reference the effect** in cards, charms, or other systems.
4. **Test and validate** effect registration and gameplay.

---

## 4. Best Practices & Rationale
- **One class/file per effect** for clarity and maintainability.
- **Use only helpers/utilities from tutorials or Assembly-CSharp.**
- **Document all effects and helpers** with usage examples.
- **Never modify decompiled game code.**
- **Review and refactor regularly.**

---

## 5. Quick Reference Table
| System         | Key Helpers/Methods                | Example Usage/Code Snippet                  | Detailed Docs                        |
|----------------|-----------------------------------|---------------------------------------------|--------------------------------------|
| Effect Data    | StatusEffectData, AddStatusEffect | `AddStatusEffect("OnKillHealToSelf", ...)` | Tutorial2, ImplementingStatusEffects |
| Registration   | AddStatusEffect, DataList         | See tutorials for registration              | Tutorial2, ImplementingStatusEffects |
| Reward Pools   | DataList, CreateRewardPool        | `CreateRewardPool(..., DataList<StatusEffectData>(...))` | data/Reward Pools.md           |
| Utilities      | RemoveNulls, TryGet               | `RemoveNulls(effects)`<br>`TryGet<StatusEffectData>(...)` | ModdingToolsAndTechniques.md     |

---

**Note:** Only helpers and patterns from the official tutorials or Assembly-CSharp are currently supported. New helpers will be introduced and documented as the project evolves. If a helper is not documented in the tutorials or Assembly-CSharp, it should not be used or referenced yet.

---

## 5.1 Migration & Refactor Checklist

Use this checklist when migrating, refactoring, or reviewing effect logic:

**General Migration Checklist:**
- [ ] Review all effect logic for references to legacy or unsupported helpers. Remove or replace with tutorial/Assembly-CSharp patterns.
- [ ] Ensure all helpers/utilities used are documented in the official tutorials or Assembly-CSharp.
- [ ] Update all code snippets and examples to match current best practices.
- [ ] Validate that all effect registration follows the recommended order and uses supported helpers.
- [ ] Modularize effect logic: one class/file per effect where possible.
- [ ] Update documentation to reflect any changes in logic, helpers, or workflow.
- [ ] Run tests or manual checks to confirm effect registration and gameplay work as expected.

**Advanced Refactor Checklist:**
- [ ] Use `RemoveNulls` on unload to clean up effect lists.
- [ ] Scan for duplicate helpers/utilities and consolidate as needed.
- [ ] Assign a reviewer to check both code and documentation for consistency and clarity after major changes.

**Documentation & Review Checklist:**
- [ ] Update this overview and all referenced docs after each migration or refactor.
- [ ] Add or update usage examples for every helper in both this overview and [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md).
- [ ] Schedule regular documentation reviews and set a "Next Review Date" in this file.

**Next Review Date:** _(Set after each major migration or refactor)_

---

## 6. Advanced Pitfalls & Migration Tips
- **Load order issues:** Register effects before referencing them in cards or charms. Use helpers (as described in the tutorials) to ensure correct order.
- **Null references:** Use `RemoveNulls` on unload to clean up effect lists. Always validate effect lists after migration or refactor.
- **Asset duplication:** Use helpers like `DataList` to avoid duplicating logic or assets.
- **Documentation drift:** After each migration or refactor, update this overview and all referenced docs to match the new structure and logic.
- **Review workflow:** After major changes, assign a reviewer to check both code and documentation for consistency and clarity.

---

## 7. Feedback & Review
- Testing plan and validation steps are now tracked in `docs/TestingPlan.md` (added 2025-05-14). All effect logic and registration should be validated using this plan after each migration or refactor.
- Share this document with collaborators for feedback.
- Revise as effect logic evolves.

---

## 8. References
- [Tutorial2_CardsAndStatusEffects.md](Tutorial2_CardsAndStatusEffects.md)
- [ImplementingStatusEffects.md](ImplementingStatusEffects.md)
- [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md)
- [data/Reward Pools.md](data/Reward%20Pools.md)
