# Wildfrost Mod Element Audit

## Item: Poison Pepper

- [x] **Name:** Poison Pepper
- [x] **Type:** Item
- [x] **Passive Effects (`startWithEffects`):**
  - [x] Shroom (2 stacks)
  - [x] Spice (10 stacks)
  - [x] Both are appropriate as passive effects for an item
- [x] **Attack Effects (`attackEffects`):**
  - [x] None (correct for this item)
- [x] **Scripts:**
  - [x] None attached (correct for this item)
- [x] **Traits:**
  - [x] Consume (1 stack) — appropriate for a consumable item
- [x] **Target Constraints:**
  - [x] `canPlayOnHand = false` (cannot be used on cards in hand)
  - [x] `canPlayOnEnemy = true` (can be used on enemy units)
  - [x] `playOnSlot = false` (cannot be played on empty slots)
  - [x] No explicit `targetConstraints` array, but play restrictions are set via flags
- [x] **Builder Usage:**
  - [x] Used `.SubscribeToAfterAllBuildEvent` for all assignments
  - [x] Used `mod.TStack` and `mod.SStack` helpers for traits and effects
- [x] **Documentation:**
  - [x] Flavour text is clear: "A spicy pepper laced with toxins. Applies 2 Shroom and 10 Spice."
  - [x] Name and sprite paths are descriptive
- [x] **Tested In-Game:**
  - [ ] (To be confirmed by playtest)

**Summary:**
- The Poison Pepper item is correctly implemented as a consumable item that applies Shroom and Spice as passive effects. All builder patterns and trait/effect assignments follow best practices. Play restrictions are set via flags. No issues found.

---

_Repeat this format for each card, effect, script, and upgrade in your mod for a complete audit._
# MadFamily Mod Audit Report

_Last Updated: 2025-05-14_

This audit provides a comprehensive breakdown of all planned mod items, cards, and effects (from plan.md and Effects.csv), their current implementation status, effect assignments, and recommendations for missing or improvable features.

---

## Table: Planned Mod Items/Components vs. Implementation & Effects

| Item/Component         | Exists in Codebase | Effect(s) Assigned                | Effect(s) Implemented? | Uses Base Effect? | Custom Effect Needed? | Notes/Recommendations |
|-----------------------|--------------------|-----------------------------------|-----------------------|-------------------|----------------------|----------------------|
| Snow Pillow           | Yes                | Heal, Snow                        | Yes                  | Yes              | No                   | Uses base effects    |
| Refreshing Water      | Yes                | Cleanse With Text                 | Yes                  | No               | Yes                  | Custom effect exists |
| Wisp Mask             | Yes                | Summon Wisp                       | Yes                  | No               | Yes                  | Custom effect exists |
| Soulrose Mask         | Yes                | Summon Soulrose                   | Yes                  | No               | Yes                  | Custom effect exists |
| Cheese Crackers       | Yes                | Increase Attack, MultiHit         | Yes                  | Yes              | No                   | Uses base effects    |
| Foam Bullets          | Yes                | Hit All Enemies, Noomlin          | Yes                  | Yes              | No                   | Uses base effects    |
| Stock of Foam Bullets | No                 | (Not implemented)                 | N/A                  | N/A              | N/A                  | Not in codebase      |
| Tack Spray            | Yes                | Hit All Enemies                   | Yes                  | Yes              | No                   | Uses base effects    |
| Ink Egg               | Yes                | Ink                               | Yes                  | Yes              | No                   | Uses base effects    |
| Detonation Strike     | No                 | (Not implemented)                 | N/A                  | N/A              | N/A                  | Not in codebase      |
| Dynamo Roller         | Yes                | Barrage                           | Yes                  | Yes              | No                   | Uses base effects    |
| Berry Cake            | Yes                | Increase HP All Allies            | Yes                  | Yes              | No                   | Uses base effects    |
| Foam Rocket           | Yes                | (None/Basic)                      | Yes                  | Yes              | No                   | Uses base effects    |
| Freezing Treat        | No                 | (Not implemented)                 | N/A                  | N/A              | N/A                  | Not in codebase      |
| Plywood Sheet         | Yes                | Add Junk                          | Yes                  | Yes              | No                   | Uses base effects    |
| Azul Book             | Yes                | Overload, Barrage                 | Yes                  | Yes              | No                   | Uses base effects    |
| Blaze Berry           | Yes                | Reduce Max HP, MultiHit           | Yes                  | Yes              | No                   | Uses base effects    |
| Dice of Destiny       | Yes                | MultiHit, Randomize Attacks       | Yes                  | Yes              | No                   | Uses base effects    |
| Azul Torch            | Yes                | Overload                          | Yes                  | Yes              | No                   | Uses base effects    |
| Junk Pile             | Yes                | (None/Basic)                      | Yes                  | Yes              | No                   | Uses base effects    |
| Haze Tacks            | Yes                | Barrage, Teeth, Haze              | Yes                  | Yes              | No                   | Uses base effects    |

---

## Table: Effects from Effects.csv vs. Implementation

| Effect Name                        | Used By/Planned For         | Exists in Codebase | Base Effect? | Custom Effect? | Notes/Recommendation                  |
|------------------------------------|----------------------------|--------------------|--------------|---------------|---------------------------------------|
| Block                             | Block Charm, etc.           | Yes (base)         | Yes          | No            | Use base effect                      |
| Demonize                          | Poppy, Charms, etc.         | Yes (base)         | Yes          | No            | Use base effect                      |
| DestroyAfterUse (Consume)         | Many consumables            | Yes (base)         | Yes          | No            | Use base effect                      |
| Draw Cards                        | Book Charm, etc.            | Yes (base)         | Yes          | No            | Use base effect                      |
| On Kill Heal To Self              | Alison, others              | Yes (custom)       | No           | Yes           | Custom effect implemented             |
| When Destroyed Add Health To Allies| Soulrose                    | Yes (custom)       | No           | Yes           | Custom effect implemented             |
| When Enemy Is Killed Apply Health | Wisp                        | Yes (custom)       | No           | Yes           | Custom effect implemented             |
| On Turn Apply Ink To RandomEnemy  | Cassie                      | Yes (custom)       | No           | Yes           | Custom effect implemented             |
| Add Attack & Health To Summon     | Lamb Charm                  | No                 | N/A          | N/A           | Can use base effects (buffs)          |
| Bonus Damage Equal To X           | Various                     | Yes (base)         | Yes          | No            | Use base effect                      |
| Summon Wisp/Soulrose              | Wisp Mask, Soulrose Mask    | Yes (custom)       | No           | Yes           | Custom effect implemented             |
| Cleanse With Text                 | Refreshing Water            | Yes (custom)       | No           | Yes           | Custom effect implemented             |
| Hit All Enemies                   | Foam Bullets, Tack Spray    | Yes (base)         | Yes          | No            | Use base effect                      |
| Barrage, MultiHit, Aimless, etc.  | Many items                  | Yes (base)         | Yes          | No            | Use base effects                     |

---

## Key Findings & Recommendations

- **Most planned items and effects are implemented and use base game effects where possible.**
- **Custom effects are only implemented where unique logic is required.**
- **Missing items:** Stock of Foam Bullets, Detonation Strike, Freezing Treat (not implemented yet).
- **Missing effect:** "Add Attack & Health To Summon" is not implemented, but can be recreated using base game attack/health buffs.
- **No need to re-register or duplicate base game effects.**
- **Continue using helpers (SStack, TStack, etc.) to assign effects by name.**
- **For new items/effects, first check if the base game already provides the needed effect.**

---

This audit can be used as a checklist for future development and to ensure all planned features are either implemented, assigned, or have a clear path using existing effects.
