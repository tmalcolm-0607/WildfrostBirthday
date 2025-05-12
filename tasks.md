# MadFamily Tribe Mod Migration & Refactor Project Plan

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
- [ ] Review and document current tribe logic
- [ ] Design and create new folder/file structure for tribes
- [ ] Migrate tribe logic to new structure
- [ ] Refactor and document tribe helpers/utilities
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

## Notes
- All changes must follow the rules in copilot_instructions.md.
- Discuss and document rationale for each migration/refactor.
- Use the docs/ folder for all documentation files.
- Update this file and tracking.md regularly.

