---
title: StatusTypes
description: |
  This file lists all status effect types used in Wildfrost, mapping each type to the effects or abilities that use it. Status types determine the visual and audio feedback for abilities and effects.
file_type: table
updated: 2025-05-12

## Summary
This file lists all status effect types used in Wildfrost, mapping each type to the effects or abilities that use it. Status types determine the visual and audio feedback for abilities and effects.

Below is the status type data in JSON format for easy programmatic use:

```json
{
  "title": "StatusTypes",
  "description": "This file lists all status effect types used in Wildfrost, mapping each type to the effects or abilities that use it. Status types determine the visual and audio feedback for abilities and effects.",
  "updated": "2025-05-12",
  "status_types": [
    {"type_name": "block", "used_by": "Block"},
    {"type_name": "demonize", "used_by": "Demonize"},
    {"type_name": "damage up", "used_by": "Double Attack, Increase Attack"},
    {"type_name": "nextphase", "used_by": "FinalBossPhase2, FrenzyBossPhase2, GoatWampusPhase2, SoulboundBossPhase2, SplitBossPhase2"},
    {"type_name": "freeaction", "used_by": "Free Action"},
    {"type_name": "frost", "used_by": "Frost"},
    {"type_name": "haze", "used_by": "Haze"},
    {"type_name": "heal", "used_by": "Heal (No Ping), Heal Full, Gain Equal Spice, Heal"},
    {"type_name": "frostimmune", "used_by": "ImmuneToFrost"},
    {"type_name": "snowimmune", "used_by": "ImmuneToSnow"},
    {"type_name": "spiceimmune", "used_by": "ImmuneToSpice"},
    {"type_name": "vimimmune", "used_by": "ImmuneToVim"},
    {"type_name": "max counter up", "used_by": "Increase Max Counter"},
    {"type_name": "max health up", "used_by": "Increase Max Health"},
    {"type_name": "lumin", "used_by": "Lumin"},
    {"type_name": "frenzy", "used_by": "MultiHit"},
    {"type_name": "ink", "used_by": "Null"},
    {"type_name": "overload", "used_by": "Overload"},
    {"type_name": "damage down", "used_by": "Reduce Attack"},
    {"type_name": "counter down", "used_by": "Reduce Counter"},
    {"type_name": "max counter down", "used_by": "Reduce Max Counter"},
    {"type_name": "max health down", "used_by": "Reduce Max Health (Must be ally), Reduce Max Health"},
    {"type_name": "shroomresist", "used_by": "ResistShroom"},
    {"type_name": "snowresist", "used_by": "ResistSnow"},
    {"type_name": "spiceresist", "used_by": "ResistSpice"},
    {"type_name": "scrap", "used_by": "Scrap"},
    {"type_name": "shell", "used_by": "Shell"},
    {"type_name": "shroom", "used_by": "Shroom"},
    {"type_name": "snow", "used_by": "Snow"},
    {"type_name": "spice", "used_by": "Spice"},
    {"type_name": "stealth", "used_by": "Stealth"},
    {"type_name": "teeth", "used_by": "Teeth"},
    {"type_name": "vim", "used_by": "Weakness"},
    {"type_name": "", "used_by": "Everything else"},
    {"type_name": "Notes", "used_by": "Type dictates the visual/audio effects associated with the ability"}
  ]
}
```