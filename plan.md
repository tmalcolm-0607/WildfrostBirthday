
# MadFamily Tribe Mod Project Plan

_Last Updated: May 11, 2025_

## Overview
This document outlines the current implementation, project plan, and next steps for the MadFamily Tribe Mod for Wildfrost. It summarizes the features implemented so far, their structure, and planned improvements, with a focus on modularity, maintainability, and best practices.

---

## Current Implementation Summary

### Tribe & Units
- **Custom Tribe:** MadFamily tribe, registered via `TribeCopy` and integrated into the main game mode.
- **Units:** Multiple custom units (companions and leaders) created using `AddFamilyUnit`, each with unique stats, effects, and traits.
- **Status Effects:** Custom and copied status effects registered for unit abilities (e.g., On Kill Heal, When Destroyed Add Health, Apply Overload, etc.).
- **Helpers:** Utility methods for status stacks (`SStack`), trait stacks (`TStack`), and data copying.
- **Registration:** All tribe, unit, and effect registration is handled in `CreateFamilyUnits`.

### Charms
- **Custom Charms:** Several new charms added with unique effects, constraints, and event hooks.
- **Charm Registration:** Charms are registered after their required effects are defined, using `AddCharm`.
- **Constraints:** Some charms use target constraints and traits (e.g., Consume, Frenzy, Aimless).

### Effects
- **Base Effects:** New and copied status effects for custom unit and charm logic.
- **Effect Registration:** Effects are registered before any units or charms that reference them.
- **Helpers:** Methods for adding, copying, and modifying effects.

### Items (Planned Section)
- **Planned:** Modular item system for adding new items, consumables, or equipment.
- **To Do:** Design and implement item registration helpers, item data structure, and integration with units/charms.

### Utilities & Helpers
- **Centralized:** Helpers for asset management, data lookup, and prefab handling.
- **Duplication:** Some duplication exists; plan to scan and refactor for single-source helpers.

---

## Project Plan & Next Steps

1. **Modularize Tribe Logic**
   - Move tribe, unit, and effect logic into dedicated folders/files.
   - Refactor registration and helpers for clarity and reuse.
   - Document tribe structure and registration process.

2. **Modularize Charms & Effects**
   - Move charm and effect logic into their own folders/files.
   - Refactor helpers and constraints for reuse.
   - Document charm/effect creation and registration.

3. **Implement Item System**
   - Design item data structure and registration helpers.
   - Add support for new items, consumables, and equipment.
   - Document item system and integration points.

4. **Centralize & Refactor Utilities**
   - Scan for duplicate helpers/utilities and centralize them.
   - Document all utilities with usage examples.

5. **Testing & Validation**
   - Develop a testing plan for all migrated features.
   - Validate each feature after migration/refactor.
   - Document edge cases and known issues.

6. **Documentation & Tracking**
   - Update documentation for each migrated feature.
   - Maintain `tasks.md` and `tracking.md` for progress.

---

## Feature Stats (as of May 2025)
- **Custom Tribe:** 1 (MadFamily)
- **Custom Units:** 7+ (including leaders and companions)
- **Custom Charms:** 8+ (with unique effects/constraints)
- **Custom Status Effects:** 10+ (base and copied)
- **Helpers/Utilities:** 10+ (for registration, data, and asset management)
- **Planned Items:** 8 (to be implemented)

---

## Notes
- All changes must follow the rules in `copilot_instructions.md`.
- Never modify or add files in the Reference/Assembly-CSharp or decompiled game code folders.
- Discuss and document rationale for each migration/refactor.
- Use the `docs/` folder for all documentation files.
- Update this file and tracking files regularly.
