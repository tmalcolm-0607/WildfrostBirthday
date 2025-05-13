# MadFamily Tribe Mod Project Plan

_Last Updated: May 12, 2025_
_Version: 0.5.0_

> Recent updates: Modularized item/effect registration, expanded documentation, improved item pool integration.

## Overview
This document outlines the current implementation, project plan, and next steps for the MadFamily Tribe Mod for Wildfrost. It summarizes implemented features, project structure, and planned improvements, with a focus on modularity, maintainability, and best practices.

This plan is intended for contributors, testers, and advanced modders. For hands-on guides and technical references, see the `docs/` folder and the modding tutorials listed below. This document is updated regularly as the project evolves.

---

## Current Implementation Summary


### Tribe & Units
- **Custom Tribe:** MadFamily tribe, registered via `TribeCopy` and integrated into the main game mode.  
  _See: `docs/Tutorial5_CreatingATribe.md` for tribe creation steps._
- **Units:** Multiple custom units (companions and leaders) created using `AddFamilyUnit`, each with unique stats, effects, and traits.
- **Status Effects:** Custom and copied status effects registered for unit abilities (e.g., On Kill Heal, When Destroyed Add Health, Apply Overload, etc.).
- **Helpers:** Utility methods for status stacks (`SStack`), trait stacks (`TStack`), and data copying.  
  _Centralization in progress; see Utilities section below._
- **Registration:** All tribe, unit, and effect registration is currently handled in `CreateFamilyUnits`.  
  _Refactor to modular registration is ongoing._


### Charms
- **Custom Charms:** Several new charms added with unique effects, constraints, and event hooks.  
  _See: `docs/Tutorial3_CharmsAndKeywords.md` for charm and keyword creation._
- **Charm Registration:** Charms are registered after their required effects are defined, using `AddCharm`.  
  _Refactor to modular charm registration is planned._
- **Constraints:** Some charms use target constraints and traits (e.g., Consume, Frenzy, Aimless).  
  _See: `docs/TargetConstraint.md` for details on constraints._


### Effects
- **Base Effects:** New and copied status effects for custom unit and charm logic.  
  _See: `docs/Tutorial2_CardsAndStatusEffects.md` and `docs/ImplementingStatusEffects.md` for effect creation._
- **Effect Registration:** Effects are registered before any units or charms that reference them.  
  _Custom effects are now registered in a dedicated phase to avoid load order issues._
- **Helpers:** Methods for adding, copying, and modifying effects.  
  _Centralization and documentation of effect helpers is ongoing._


### Items
- **Current:** Custom items are now available as rewards and use modular registration helpers.  
  _See: `docs/Tutorial2_CardsAndStatusEffects.md` for item/effect integration and `docs/data/Reward Pools.md` for reward pool info._
- **Planned:** Continue to expand the modular item system for new items, consumables, and equipment.
- **To Do:** Further document item registration helpers, item data structure, and integration with units/charms.


### Utilities & Helpers
- **Centralized:** Helpers for asset management, data lookup, and prefab handling.  
  _See: `docs/ModdingToolsAndTechniques.md` for utility usage and examples._
- **Duplication:** Some duplication exists; plan to scan and refactor for single-source helpers.  
  _Documentation of all utilities is in progress._

---


## Project Plan & Next Steps

1. **Modularize Tribe Logic**
   - Move tribe, unit, and effect logic into dedicated folders/files for better maintainability and clarity.
   - Refactor registration and helpers for clarity and reuse.
   - Document tribe structure and registration process in `docs/Tutorial5_CreatingATribe.md` and related files.

2. **Modularize Charms & Effects**
   - Move charm and effect logic into their own folders/files for improved modularity.
   - Refactor helpers and constraints for reuse and maintainability.
   - Document charm/effect creation and registration in `docs/Tutorial3_CharmsAndKeywords.md`, `docs/ImplementingStatusEffects.md`, and related docs.

3. **Implement Item System**
   - Continue to expand the modular item system for adding new items, consumables, and equipment.
   - Design and document item data structure and registration helpers in `docs/Tutorial2_CardsAndStatusEffects.md` and `docs/data/Reward Pools.md`.
   - Integrate item system with units and charms as needed.

4. **Centralize & Refactor Utilities**
   - Scan for duplicate helpers/utilities and centralize them in a single location.
   - Document all utilities with usage examples in `docs/ModdingToolsAndTechniques.md` and related files.

5. **Testing & Validation**
   - Develop a comprehensive testing plan for all migrated and new features.
   - Validate each feature after migration/refactor, including edge cases and in-game integration.
   - Document edge cases, known issues, and troubleshooting steps in the relevant docs and a new FAQ section.

6. **Documentation & Tracking**
   - Update documentation for each migrated feature and new addition.
   - Maintain `tasks.md` and `tracking.md` for progress and migration status.
   - Regularly review and update tutorials to address trouble areas and clarify complex steps.

---

## Feature Stats (as of May 2025)

**Custom Tribe:**  
  - **MadFamily** (registered via `TribeCopy`)  
  _See: `docs/Tutorial5_CreatingATribe.md` for details._

**Custom Units:**  
  - **Leaders:**  
    - **Alison:** HP: 9, Attack: 3, Counter: 3. Ability: Restore 2 HP on kill (On Kill Heal To Self x2)
    - **Tony:** HP: 8, Attack: 2, Counter: 4. Ability: Summon Soulrose (When Deployed Summon Soulrose, On Turn Summon Soulrose)
    - **Caleb:** HP: 8, Attack: 0, Counter: 6. Ability: When attacked, apply 2 Overload to attacker; gain +1 attack on hit (When Hit Apply Overload To Attacker x2, When Hit Gain Attack To Self (No Ping) x1)
    - **Kaylee:** HP: 7, Attack: 4, Counter: 7. Ability: Starts with 4 Teeth, applies 2 Teeth to all allies each turn (When Deployed Apply Teeth To Self x4, On Turn Apply Teeth To Allies x2)
    - **Cassie:** HP: 5, Attack: 1, Counter: 3. Ability: Frenzy x2, applies 2 Ink to random enemy each turn, Aimless (MultiHit x2, On Turn Apply Ink To RandomEnemy x2, Aimless)
  - **Companions:**  
    - **Soulrose:** HP: 1, Ability: When destroyed, add +1 health to all allies (When Destroyed Add Health To Allies x1)
    - **Wisp:** HP: 5, Attack: 4, Counter: 6. Ability: When an enemy is killed, apply 4 health to the attacker (When Enemy Is Killed Apply Health To Attacker x4)
    - **Lulu:** HP: 6, Attack: 2, Counter: 3. Ability: When ally is hit, apply 2 Frost to attacker (When Ally is Hit Apply Frost To Attacker x2)
    - **Poppy:** HP: 11, Attack: 2, Counter: 4. Ability: Smackback, when hit apply 2 Demonize to attacker (When Hit Apply Demonize To Attacker x2, Smackback)  
  _See: `docs/AddFamilyUnit.md` for unit creation and registration._

**Custom Charms:**  
  - **Pug Charm:** When an ally is hit, apply 1 frost to them (When Ally is Hit Apply Frost To Attacker x1)
  - **Golden Vial Charm:** Gain 1 Bling when triggered (Collect Bling On Trigger x1)
  - **Frost Moon Charm:** Gain +2 Counter and apply 5 Frost on attack (FrostMoon Increase Max Counter x2, FrostMoon Apply Frost On Attack x5)
  - **Soda Charm:** Gain Barrage, Frenzy x3, Consume. Halve all current effects. (Reduce Effects x2, MultiHit x3, Barrage, Consume, TargetConstraint: IsItem)
  - **Pizza Charm:** Hits all enemies. Consume. (Barrage, Consume, TargetConstraint: IsItem)
  - **Plant Charm:** Gain +1 Attack after attacking (On Turn Add Attack To Self x1)
  - **Book Charm:** Draw 1 on deploy and each turn (Draw x1)
  - **Duck Charm:** Gain Frenzy, Aimless, and set Attack to 1 (When Hit Add Frenzy To Self x1, Set Attack x1, MultiHit x1, Aimless)  
  _See: `docs/Tutorial3_CharmsAndKeywords.md` and `docs/EnhancedCharmCreation.md`._

**Custom Items:**  
  - **Snow Pillow:** Traits: —; Attack: —; Ability: Heal 6, apply 1 Snow on attack (Heal x6, Snow x1, No Damage)
  - **Refreshing Water:** Traits: Consume, Zoomlin; Attack: —; Ability: Cleanse With Text x1, No Damage
  - **Wisp Mask:** Traits: Consume, Zoomlin; Attack: —; Ability: Summon Wisp x1 (Can only be played on slot)
  - **Soulrose Mask:** Traits: Consume; Attack: —; Ability: Summon Soulrose x1, No Damage (Can only be played on slot)
  - **Cheese Crackers:** Traits: Aimless; Attack: —; Ability: Increase Attack by 1, MultiHit x2
  - **Rubber Bullets:** Traits: Noomlin; Attack: 0; Ability: Hits all enemies
  - **Stock of Rubber Bullets:** Traits: Consume; Attack: —; Ability: Add 4 Rubber Bullets to your hand
  - **Tack Spray:** Traits: —; Attack: 1; Ability: Hits all enemies
  - **Ink Egg:** Traits: Consume, Zoomlin; Attack: 1; Ability: Apply 7 Ink
  - **Detonation Strike:** Traits: Trash 1, Recycle 2; Attack: 16; Ability: Target must have Shell
  - **Dynamo Roller:** Traits: Recycle 3; Attack: 10; Ability: Barrage
  - **Berry Cake:** Traits: Consume; Attack: —; Ability: Increase HP of all allies by 3
  - **Water Balloon:** Traits: Noomlin; Attack: —; Ability: Cleanse
  - **Foam Rocket:** Traits: Noomlin; Attack: 2; Ability: —
  - **Freezing Treat:** Traits: Consume; Attack: 4; Ability: Apply 2 Shell and 2 Snow
  - **Plywood Sheet:** Traits: Consume; Attack: —; Ability: Add 3 Junk
  - **Azul Book:** Traits: Barrage; Attack: 0; Ability: Apply 1 Overburn
  - **Blaze Berry:** Traits: Consume; Attack: —; Ability: Reduce Max HP by 4 and MultiHit
  - **Dice of Destiny:** Traits: —; Attack: 0; Ability: MultiHit x2, before attacking randomize attacks between 1–6
  - **Azul Torch:** Traits: —; Attack: 0; Ability: Apply 4 Overburn
  - **Junk Pile:** Traits: Trash 10, Consume, Zoomlin; Attack: —; Ability: —
  - **Haze Tacks:** Traits: Barrage, Consume; Attack: 3; Ability: Apply 3 Teeth and 2 Haze  
  _See: `docs/Tutorial2_CardsAndStatusEffects.md` and `docs/data/Reward Pools.md` for item/effect integration and reward pool info._

**Custom Status Effects:**  
  - Cleanse With Text, When Destroyed Add Health To Allies, When Enemy Is Killed Apply Health To Attacker, On Kill Heal To Self, When Deployed Summon Soulrose, On Turn Summon Soulrose, On Turn Add Attack To Self, When Hit Apply Overload To Attacker, When Hit Gain Attack To Self (No Ping), On Turn Apply Teeth To Allies, On Turn Apply Teeth To Self, When Deployed Apply Teeth To Self, On Turn Apply Ink To RandomEnemy, When Ally is Hit Apply Frost To Attacker, Collect Bling On Trigger, FrostMoon Increase Max Counter, FrostMoon Apply Frostburn On Attack, When Hit Apply Demonize To Attacker, Summon Wisp, Summon Soulrose, Instant Summon Soulrose  
  _See: `docs/ImplementingStatusEffects.md` and `docs/data/Effects.md` for effect details and technical references._

**Helpers/Utilities:**  
  - SStack, TStack, StatusCopy, CardCopy, TribeCopy, DataList, DataCopy, RemoveNulls, AddCharm, AddFamilyUnit, AddItemCard, AddStatusEffect, AddCopiedStatusEffect, AddInstantStatusEffect, UnloadFromClasses, TryGet, GiveUpgrade, AddRandomHealth, AddRandomDamage, AddRandomCounter  
  _See: `docs/ModdingToolsAndTechniques.md` for usage and examples._

**Planned Items:**  
  - Modular item system for adding new items, consumables, or equipment (see item helpers and AddItemCard)  
  _Planned: Continue expanding item system and documentation._

---

_Legend:_
- **Trait/Ability notation:** (e.g., MultiHit x2, Consume, Aimless) follows in-game and code conventions. See `docs/data/Keywords.md` for definitions.

---

## Notes
- All changes must follow the rules in `copilot_instructions.md`.
- Never modify or add files in the Reference/Assembly-CSharp or decompiled game code folders.
- Discuss and document rationale for each migration/refactor.
- Use the `docs/` folder for all documentation files.
- Update this file and tracking files regularly.
