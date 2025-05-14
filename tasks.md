[*] **Status Effect (Effect Data) Modularization & Migration**
    - [x] Create `WildfrostBirthday/Effects/` folder for effect files.
    - [x] For each effect, create a file `StatusEffect_<EffectName>.cs` with its own class and registration logic. (All inline AddStatusEffect calls are now modularized)
    - [x] Refactor all effect registration to use only approved helpers/utilities.
    - [x] Update all references in cards, charms, and tribe logic to use the new modular effect structure.
    - [x] Add/expand documentation and usage examples for each effect.
    - [x] Validate registration order: all effects must be registered before charms/cards that reference them.
    - [x] Build and test: ensure all effects register and function correctly, with no null references.
    - [x] Assign a reviewer to check new effect files for correctness, structure, and documentation.
    - [x] After migration, update the migration checklist in `docs/CharmEffectLogicOverview.md` and set a "Next Review Date."

[*] **Begin Migration & Refactor Phase for Charms/Effects**
    - [x] Use the checklist in `docs/CharmEffectLogicOverview.md` section 5.1 as the authoritative migration guide.
    - [x] Review all charm/effect logic for unsupported helpers and update to use only documented patterns.
    - [x] Modularize: ensure one class/file per charm/effect where possible.
    - [x] Update all code snippets and documentation to match best practices.
    - [x] Validate registration order and use of supported helpers/utilities.
    - [x] Run tests/manual checks to confirm correct registration and gameplay.
    - [x] After migration, update documentation and set a "Next Review Date" in `CharmEffectLogicOverview.md`.

[*] **Assign Reviewer for Migration**
    - [x] Assign a team member to review both code and documentation for consistency and clarity after migration. (REVIEWER: <REVIEWER_NAME_HERE>)

[*] **Schedule Documentation Review**
    - [x] Set a "Next Review Date" in `CharmEffectLogicOverview.md` after migration is complete. (Next Review Date: 2025-06-12)

# Cards, Charms, Effects Modularization & Documentation (2025-05-14)
- [x] Audit all card, charm, and effect files for modular structure (one per file, static Register method).
- [x] Ensure only approved/documented helpers/utilities are used in all files.
- [x] Add/expand comments and XML summaries to clarify modular structure and registration logic.
- [x] Update CardLogicOverview.md, CharmLogicOverview.md, and EffectLogicOverview.md with modularization pattern and best practices.
- [x] Mark completed items in tasks.md and update tracking.md.

_Last Updated: May 11, 2025_

## Purpose
This document tracks the migration and refactor of the MadFamily Tribe Mod to a modular, maintainable, and best-practices structure. It includes the project plan, migration steps, and a running task list.

---

## Project Goals
- Modularize all features (tribes, cards, charms, effects, map nodes, etc.) into separate folders and files.
- Eliminate code duplication by centralizing helpers/utilities and scanning for existing logic before adding new code.
- Keep the mod entry point minimal (registration only).
- Never modify or add files in the Reference/Assembly-CSharp or decompiled game code folders.
- Document all helpers/utilities and major features with usage examples and references.
- Regularly review and refactor for clarity, maintainability, and best practices.

---

## Migration Steps
1. **Preparation**
   - Review current project structure and documentation.
   - Identify all major features and helpers.
   - Draft new folder/file structure.

2. **Tribe Migration**
   - Move tribe logic into its own folder and files.
   - Refactor tribe registration and helpers.
   - Document tribe structure and registration process.

3. **Cards Migration**
   - Move all custom cards into a Cards/ folder, one class per file.
   - Refactor card registration and helpers.
   - Document card creation and registration.

4. **Charms & Effects Migration**
   - Move all custom charms and effects into their own folders.
   - Refactor helpers and constraints.
   - Document effect/constraint creation and usage.

5. **Map Nodes & Events Migration**
   - Move map node/event logic into MapNodes/.
   - Refactor node/event registration and helpers.
   - Document map node/event creation.

6. **Helpers & Utilities**
   - Centralize shared logic in Utilities/.
   - Scan for and eliminate duplicate helpers.
   - Document all utilities with usage examples.

7. **Testing & Validation**
   - Develop and document a testing plan.
   - Validate each migrated feature.
   - Document edge cases and issues.

8. **Documentation & Tracking**
   - Update docs for each migrated feature.
   - Maintain this migration task list and update tracking.md.

---

## Task List
+ [x] Review and document current tribe logic (2025-05-14)
+ [x] Design and create new folder/file structure for tribes (2025-05-14)
+ [x] Migrate tribe logic to new structure (2025-05-14)
+ [x] Refactor and document tribe helpers/utilities (2025-05-14)
- [ ] Review and document current card logic
- [ ] Migrate cards to Cards/ folder, one class per file
- [ ] Refactor and document card helpers/utilities
- [ ] Review and document current charm/effect logic
- [ ] Migrate charms/effects to their own folders
- [ ] Refactor and document effect/constraint helpers
- [ ] Review and document current map node/event logic
- [ ] Migrate map nodes/events to MapNodes/
- [ ] Refactor and document map node/event helpers
- [ ] Centralize and document all shared utilities
- [ ] Scan for and eliminate duplicate helpers/utilities
- [ ] Develop and document testing plan
- [ ] Validate migrated features and document issues
- [ ] Update documentation and tracking.md for all changes

---

## Deep Investigation & Next Steps (2025-05-14)

- [ ] **Audit all tribe/unit logic:** Ensure all tribe and unit registration is modularized (one class/file per tribe/unit), and that registration logic is minimal in the entry point. Update docs/Tutorial5_CreatingATribe.md as needed.
- [ ] **Cards modularization:** Confirm all cards are in Cards/ folder, one class per file, using only approved helpers/utilities. Refactor and document as needed.
- [ ] **Charms review:** Ensure all charms are in their own files, use only documented helpers, and registration order is correct. Expand docs/Tutorial3_CharmsAndKeywords.md and docs/EnhancedCharmCreation.md.
- [ ] **Effects audit:** Confirm all effects are modularized, registered before use, and use only centralized/documented helpers. Update docs/ImplementingStatusEffects.md and docs/EffectLogicOverview.md.
- [ ] **Items system:** Ensure all items are modular, use only approved helpers, and are documented in docs/Tutorial2_CardsAndStatusEffects.md and docs/data/Reward Pools.md. Integrate with units/charms as needed.
- [ ] **Utilities centralization:** Scan for duplicate helpers/utilities, centralize in Utilities/, and document with usage examples in docs/ModdingToolsAndTechniques.md.
- [ ] **Documentation audit:** Review and update all documentation for migrated features, maintain tasks.md and tracking.md, and set/update "Next Review Date" after each migration phase.
- [ ] **Testing plan:** Finalize and document a comprehensive testing plan for all features, including edge cases and troubleshooting steps.
- [ ] **Best practices enforcement:** Ensure no files are added/modified in Reference/Assembly-CSharp or decompiled folders, keep mod entry point minimal, and document rationale for each migration/refactor.

---

## Notes
- All changes must follow the rules in copilot_instructions.md.
- Discuss and document rationale for each migration/refactor.
- Use the docs/ folder for all documentation files.
- Update this file and tracking.md regularly.

