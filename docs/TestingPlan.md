# MadFamily Tribe Mod Testing Plan

_Last Updated: 2025-05-14_

## Purpose
This document outlines the comprehensive testing plan for the MadFamily Tribe Mod. It ensures that all migrated and new features function as intended, edge cases are handled, and gameplay remains stable and enjoyable.

---

## 1. Test Coverage Areas
- **Tribe Registration:** Tribe appears in selection, correct leaders/companions, correct starting inventory.
- **Cards:** All custom cards load, display, and function as intended (stats, effects, traits, visuals).
- **Charms:** All custom charms register, apply effects, and interact with cards as expected.
- **Effects:** All custom effects trigger correctly, stack, and interact with other systems.
- **Items:** All custom items are available in reward pools, can be obtained, and function as described.
- **Helpers/Utilities:** All helpers/utilities work as intended, no duplicate or legacy helpers remain.
- **Reward Pools:** All pools contain correct assets, no missing or duplicate entries.
- **Documentation:** All docs are up to date and match the codebase.

---

## 2. Manual Test Checklist
- [ ] Launch game with mod enabled; verify no errors on load.
- [ ] Select MadFamily tribe; verify correct leaders, companions, and starting deck.
- [ ] Play through a run; verify all custom cards, charms, and effects appear and function.
- [ ] Obtain each custom item from reward pools; verify correct behavior.
- [ ] Apply each custom charm to a card; verify effects and constraints.
- [ ] Trigger each custom effect in-game; verify correct logic and no null references.
- [ ] Remove/unload mod; verify no null references or errors in base game.
- [ ] Review all documentation for accuracy and completeness.

---

## 3. Edge Case & Regression Tests
- [ ] Attempt to use cards/charms/effects in unintended ways (e.g., apply to wrong card type).
- [ ] Test with other mods enabled to check for conflicts.
- [ ] Validate load order: effects must register before referencing cards/charms.
- [ ] Test reward pool randomization and asset uniqueness.
- [ ] Check for duplicate helpers/utilities and remove if found.

---

## 4. Automated/Scripted Tests (if applicable)
- [ ] Add unit tests for helpers/utilities (if test framework is set up).
- [ ] Add smoke tests for registration order and asset presence.

---

## 5. Reporting & Issue Tracking
- Log all test results and issues in `docs/tracking.md`.
- Update this plan as new features are added or bugs are found.

---

## 6. Next Review Date
- **2025-06-12** (align with documentation review)

---

## Notes
- All tests must be run after each major migration or refactor.
- Update this plan regularly as the mod evolves.
