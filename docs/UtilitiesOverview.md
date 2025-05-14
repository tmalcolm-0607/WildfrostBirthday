# Utilities & Helpers Overview

_Last Updated: May 12, 2025_

## Purpose
This document provides an overview of shared utilities and helpers used in the MadFamily Tribe Mod for Wildfrost. It explains their purpose, usage, and best practices, and provides a migration/refactor checklist.

---

## 1. What are Utilities & Helpers?
**Utilities and helpers** are reusable functions or classes that centralize common logic for asset management, data lookup, prefab handling, and code reuse across all tribe-related systems. Only helpers from the official tutorials or Assembly-CSharp should be used at this stage.

---

## 2. Key Utilities & Helpers (Tutorial/Assembly-CSharp Only)
- `SStack`/`TStack` (StackHelpers): Create status effect or trait stacks for cards/effects.
- `DataList`/`RemoveNulls` (DataUtilities): Batch asset lookup and null cleanup.
- `StatusCopy`, `CardCopy`, `TribeCopy`, `DataCopy` (DataCopyHelpers): Clone and customize effects, cards, and tribes.
- `CreateRewardPool`, `UnloadFromClasses`, `FixImage` (RewardPoolHelpers): Reward pool creation, cleanup, and image fixes.
- `TryGet`, `IsAlreadyRegistered` (AssetHelpers): Safe asset lookup and registration checks.
- `RegisterAll*` (ComponentRegistration): Automated registration for all modular components.
- Card script helpers (CardScriptHelpers): Generate scripts for random stat assignment and upgrades.

---

## 3. Usage Examples
- `RemoveNulls(gameMode.classes)`
- `TryGet<ClassData>("MadFamily")`
- `DataList<CardData>("Alison", "Tony")`

See [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md) for more details and examples.

---

## 4. Best Practices & Rationale
- **Centralize all shared logic** in a Utilities/ folder or file.
- **Document every utility/helper** with usage examples.
- **Never use undocumented or legacy helpers.**
- **Review and refactor utilities regularly.**

---

## 5. Migration & Refactor Checklist

**General Migration Checklist:**
- [ ] Review all utilities for references to legacy or unsupported helpers. Remove or replace with tutorial/Assembly-CSharp patterns.
- [ ] Ensure all utilities used are documented in the official tutorials or Assembly-CSharp.
- [ ] Update all usage examples to match current best practices.
- [ ] Modularize utilities: one class/file per utility where possible.
- [ ] Update documentation to reflect any changes in logic or workflow.
- [ ] Run tests or manual checks to confirm utility behavior.

**Advanced Refactor Checklist:**
- [ ] Scan for duplicate utilities and consolidate as needed.
- [ ] Assign a reviewer to check both code and documentation for consistency and clarity after major changes.

**Documentation & Review Checklist:**
- [ ] Update this overview and all referenced docs after each migration or refactor.
- [ ] Add or update usage examples for every utility in both this overview and [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md).
- [ ] Schedule regular documentation reviews and set a "Next Review Date" in this file.

**Next Review Date:** _(Set after each major migration or refactor)_

---

## 6. Advanced Pitfalls & Migration Tips
- **Legacy helpers:** Remove or refactor any helpers not documented in the tutorials or Assembly-CSharp.
- **Duplication:** Consolidate duplicate utilities.
- **Documentation drift:** After each migration or refactor, update this overview and all referenced docs to match the new structure and logic.
- **Review workflow:** After major changes, assign a reviewer to check both code and documentation for consistency and clarity.

---

## 7. Feedback & Review
- Share this document with collaborators for feedback.
- Revise as utilities evolve.

---

## 8. References
- [ModdingToolsAndTechniques.md](ModdingToolsAndTechniques.md)
