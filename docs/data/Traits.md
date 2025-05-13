---
title: Traits
description: |
  This file lists all traits available in Wildfrost, including their in-game name, effects, overrides, and notes. Traits are used to define special behaviors or properties for cards, units, and items.
file_type: table
updated: 2025-05-12

## Summary
This file lists all traits available in Wildfrost, including their in-game name, effects, overrides, and notes. Traits are used to define special behaviors or properties for cards, units, and items.

Below is the trait data in JSON format for easy programmatic use:

```json
{
  "title": "Traits",
  "description": "This file lists all traits available in Wildfrost, including their in-game name, effects, overrides, and notes. Traits are used to define special behaviors or properties for cards, units, and items.",
  "updated": "2025-05-12",
  "traits": [
    {"name": "Aimless", "ig_name": "Aimless", "effects_used": "Hit Random Target", "overrides": "Barrage, Longshot", "notes": ""},
    {"name": "Backline", "ig_name": "Backline", "effects_used": "Low Priority Position", "overrides": "Aimless, Longshot", "notes": "No, this is not a mistake"},
    {"name": "Barrage", "ig_name": "Barrage", "effects_used": "Hit All Enemies In Row", "overrides": "Aimless, Longshot", "notes": ""},
    {"name": "Bombard 1", "ig_name": "Bombard", "effects_used": "Bombard 1", "overrides": "", "notes": ""},
    {"name": "Bombard 2", "ig_name": "Bombard", "effects_used": "Bombard 2", "overrides": "", "notes": ""},
    {"name": "Combo", "ig_name": "Critical", "effects_used": "While Last In Hand Double Effects To Self", "overrides": "", "notes": ""},
    {"name": "Consume", "ig_name": "Consume", "effects_used": "Destroy After Use", "overrides": "", "notes": ""},
    {"name": "Crush", "ig_name": "Crush", "effects_used": "Crush", "overrides": "", "notes": ""},
    {"name": "Draw", "ig_name": "Draw", "effects_used": "Draw Cards", "overrides": "", "notes": ""},
    {"name": "Effigy", "ig_name": "Faith", "effects_used": "Add Attack & Health To Summon", "overrides": "", "notes": ""},
    {"name": "Explode", "ig_name": "Explode", "effects_used": "When Destroyed Apply Damage To EnemiesInRow", "overrides": "", "notes": ""},
    {"name": "Fragile", "ig_name": "Fragile", "effects_used": "Cannot Increase Max Health", "overrides": "", "notes": ""},
    {"name": "Frontline", "ig_name": "Frontline", "effects_used": "High Priority Position", "overrides": "", "notes": ""},
    {"name": "Fury", "ig_name": "Fury", "effects_used": "Increase Attack While Alone", "overrides": "", "notes": ""},
    {"name": "Greed", "ig_name": "Greed", "effects_used": "Bonus Damage Equal To Gold Factor 0.02", "overrides": "", "notes": ""},
    {"name": "Heartburn", "ig_name": "Flourish", "effects_used": "When Healed Gain Max Health", "overrides": "", "notes": ""},
    {"name": "Hellbent", "ig_name": "Hellbent", "effects_used": "", "overrides": "", "notes": ""},
    {"name": "Knockback", "ig_name": "Boop", "effects_used": "On Hit Push Target", "overrides": "", "notes": ""},
    {"name": "Longshot", "ig_name": "Longshot", "effects_used": "Hit Furthest Target", "overrides": "Aimless, Barrage", "notes": ""},
    {"name": "Noomlin", "ig_name": "Noomlin", "effects_used": "Free Action", "overrides": "Zoomlin", "notes": ""},
    {"name": "Pigheaded", "ig_name": "Hogheaded", "effects_used": "Cannot Recall", "overrides": "", "notes": ""},
    {"name": "Pull", "ig_name": "Yank", "effects_used": "On Hit Pull Target", "overrides": "", "notes": ""},
    {"name": "Recycle", "ig_name": "Recycle", "effects_used": "Recycle Junk", "overrides": "", "notes": ""},
    {"name": "Smackback", "ig_name": "Smackback", "effects_used": "Trigger Against Attacker When Hit", "overrides": "", "notes": ""},
    {"name": "Soulbound", "ig_name": "Soulbound", "effects_used": "Take 100 Damage When Soulbound Unit Sacrificed", "overrides": "", "notes": ""},
    {"name": "Spark", "ig_name": "Spark", "effects_used": "Trigger When Deployed", "overrides": "", "notes": ""},
    {"name": "Summoned", "ig_name": "Summoned", "effects_used": "Summoned", "overrides": "", "notes": ""},
    {"name": "Trash", "ig_name": "Trash", "effects_used": "On Card Played Add Junk To Hand", "overrides": "", "notes": ""},
    {"name": "Unmovable", "ig_name": "Unmovable", "effects_used": "Unmovable", "overrides": "", "notes": ""},
    {"name": "Wild", "ig_name": "Wild", "effects_used": "Gain Frenzy When Wild Unit Killed", "overrides": "", "notes": ""},
    {"name": "Zoomlin", "ig_name": "Zoomlin", "effects_used": "Free Action (Zoomlin), On Card Played Lose Zoomlin, When Deployed Lose Zoomlin", "overrides": "", "notes": ""}
  ]
}
```