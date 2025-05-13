# Charm & Effect Logic Overview

_Last Updated: May 12, 2025_

## Purpose
This document provides a high-level overview of charm and effect logic in the MadFamily Tribe Mod for Wildfrost. It explains how charms and effects are defined, registered, and managed, and outlines best practices for modularity, helpers/utilities, and migration.

---

## 1. What is a Charm or Effect?
A **charm** is a modifier that can be attached to cards, granting new abilities or altering stats. An **effect** is a status or action applied to cards, units, or the game state, often triggered by specific events.

---

## 2. Charm & Effect System Components
- **Charm Data:** Defines the charm's properties, effects, and visuals.
- **Effect Data:** Defines the effect's logic, triggers, and visuals.
- **Registration:** Adds charms/effects to the mod and makes them available for use.
- **Helpers/Utilities:** Used for safe registration, batch lookup, and modularization (see tutorials/Assembly-CSharp only).
- **Reward Pools:** Charms and effects are referenced in reward pools and by cards.

---

## 3. Charm & Effect Logic Flow
1. **Define charm/effect data** (properties, triggers, visuals).
2. **Register the charm/effect** using supported helpers or direct registration (see tutorials).
3. **Reference the charm/effect** in cards, reward pools, or other systems.
4. **Test and validate** registration and gameplay.

---

## 4. Best Practices & Rationale
- **One class/file per charm/effect** for clarity and maintainability.
- **Use only helpers/utilities from tutorials or Assembly-CSharp.**
- **Document all charms/effects and helpers** with usage examples.
- **Never modify decompiled game code.**
- **Review and refactor regularly.**

---

## 5. Quick Reference Table
| System         | Key Helpers/Methods                | Example Usage/Code Snippet                  | Detailed Docs                        |
|----------------|-----------------------------------|---------------------------------------------|--------------------------------------|
| Charm Data     | CardUpgradeData, AddCharm         | `AddCharm("PugCharm", ...)`                | Tutorial3, EnhancedCharmCreation.md  |
| Effect Data    | StatusEffectData, AddStatusEffect | `AddStatusEffect("OnKillHealToSelf", ...)` | Tutorial2, ImplementingStatusEffects |
| Registration   | AddCharm, AddStatusEffect         | See tutorials for registration              | Tutorial2, Tutorial3                |
| Reward Pools   | DataList, CreateRewardPool        | `CreateRewardPool(..., DataList<CardUpgradeData>(...))` | data/Reward Pools.md           |
| Utilities      | RemoveNulls, TryGet               | `RemoveNulls(charms)`<br>`TryGet<CardUpgradeData>(...)` | ModdingToolsAndTechniques.md     |

---

**Note:** Only helpers and patterns from the official tutorials or Assembly-CSharp are currently supported. New helpers will be introduced and documented as the project evolves. If a helper is not documented in the tutorials or Assembly-CSharp, it should not be used or referenced yet.

---

## 5.1 Migration & Refactor Checklist

Use this checklist when migrating, refactoring, or reviewing charm/effect logic:

**General Migration Checklist:**
- [ ] Review all charm/effect logic for references to legacy or unsupported helpers. Remove or replace with tutorial/Assembly-CSharp patterns.
- [ ] Ensure all helpers/utilities used are documented in the official tutorials or Assembly-CSharp.
- [ ] Update all code snippets and examples to match current best practices.
- [ ] Validate that all charm/effect registration follows the recommended order and uses supported helpers.
- [ ] Modularize charm/effect logic: one class/file per charm/effect where possible.
- [ ] Update documentation to reflect any changes in logic, helpers, or workflow.
- [ ] Run tests or manual checks to confirm charm/effect registration and gameplay work as expected.

**Advanced Refactor Checklist:**
- [ ] Use `RemoveNulls` on unload to clean up charm/effect lists.
- [ ] Scan for duplicate helpers/utilities and consolidate as needed.
- [ ] Assign a reviewer to check both code and documentation for consistency and clarity after major changes.

**Documentation & Review Checklist:**
- [ ] Update this overview and all referenced docs after each migration or refactor.
- [ ] Add or update usage examples for every helper in both this overview and [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md).
- [ ] Schedule regular documentation reviews and set a "Next Review Date" in this file.

**Next Review Date:** _(Set after each major migration or refactor)_

---

## 6. Advanced Pitfalls & Migration Tips
- **Load order issues:** Register effects before referencing them in charms or cards. Use helpers (as described in the tutorials) to ensure correct order.
- **Null references:** Use `RemoveNulls` on unload to clean up charm/effect lists. Always validate lists after migration or refactor.
- **Asset duplication:** Use helpers like `DataList` to avoid duplicating logic or assets.
- **Documentation drift:** After each migration or refactor, update this overview and all referenced docs to match the new structure and logic.
- **Review workflow:** After major changes, assign a reviewer to check both code and documentation for consistency and clarity.

---

## 7. Feedback & Review
- Share this document with collaborators for feedback.
- Revise as charm/effect logic evolves.

---

## 8. References
- [Tutorial2_CardsAndStatusEffects.md](Tutorial2_CardsAndStatusEffects.md)
- [Tutorial3_CharmsAndKeywords.md](Tutorial3_CharmsAndKeywords.md)
- [EnhancedCharmCreation.md](EnhancedCharmCreation.md)
- [ImplementingStatusEffects.md](ImplementingStatusEffects.md)
- [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md)
- [data/Reward Pools.md](data/Reward%20Pools.md)
