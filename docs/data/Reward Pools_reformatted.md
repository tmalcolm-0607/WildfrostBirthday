---
title: Reward Pools
description: Reference for all reward pools in Wildfrost, including their tribe availability, types, and included cards/items
file_type: data_reference
data_format: json
updated: 2025-05-12
source_file: Reward Pools.md
---

## Summary
This document catalogs all reward pools used in Wildfrost. Reward pools determine which cards, items, charms, and modifiers are available as rewards during a run, with availability varying by tribe selection.

## Data Usage
- Use when implementing new reward systems
- Reference when balancing tribe-specific rewards
- Understand the distribution of different reward types

## JSON Data

```json
{
  "title": "Reward Pools",
  "description": "Reference for all reward pools in Wildfrost, including their tribe availability, types, and included cards/items",
  "reward_pools": [
    {"name": "GeneralUnitPool", "tribe_availability": "All", "type": "Units", "cards_items": null},
    {"name": "GenralItemPool", "tribe_availability": "All", "type": "Items", "cards_items": null},
    {"name": "GeneralCharmPool", "tribe_availability": "All", "type": "Charms", "cards_items": null},
    {"name": "GeneralModifierPool", "tribe_availability": "Each tribe has their own identical version", "type": "Modifiers", "cards_items": null},
    {"name": "SnowUnitPool", "tribe_availability": "All (NOT JUST SNOWDWELLERS)", "type": "Units", "cards_items": "Snobble, Snoffel"},
    {"name": "SnowItemPool", "tribe_availability": "All (NOT JUST SNOWDWELLERS)", "type": "Items", "cards_items": "Storm Globe, Snowcake"},
    {"name": "SnowCharmPool", "tribe_availability": "All (NOT JUST SNOWDWELLERS)", "type": "Charms", "cards_items": "Snowball Charm"},
    {"name": "BasicUnitPool", "tribe_availability": "Snowdwellers", "type": "Units", "cards_items": null},
    {"name": "BasicItemPool", "tribe_availability": "Snowdwellers", "type": "Items", "cards_items": null},
    {"name": "BasicCharmPool", "tribe_availability": "Snowdwellers", "type": "Charms", "cards_items": null},
    {"name": "MagicUnitPool", "tribe_availability": "Shademancers", "type": "Units", "cards_items": null},
    {"name": "MagicItemPool", "tribe_availability": "Shademancers", "type": "Items", "cards_items": null},
    {"name": "MagicCharmPool", "tribe_availability": "Shademancers", "type": "Charms", "cards_items": null},
    {"name": "ClunkUnitPool", "tribe_availability": "Clunkmasters", "type": "Units", "cards_items": null},
    {"name": "ClunkItemPool", "tribe_availability": "Clunkmasters", "type": "Items", "cards_items": null},
    {"name": "ClunkCharmPool", "tribe_availability": "Clunkmasters", "type": "Charms", "cards_items": null}
  ]
}
```

## Pool Categories

### Universal Pools
Available regardless of tribe selection:
- `GeneralUnitPool`: Generic units available to all tribes
- `GenralItemPool`: Generic items available to all tribes
- `GeneralCharmPool`: Generic charms available to all tribes
- `GeneralModifierPool`: Game modifiers, with tribe-specific variants

### Snow-Themed Pools
Special pools focused on snow-themed content, available to all tribes:
- `SnowUnitPool`: Snow units like Snobble and Snoffel
- `SnowItemPool`: Snow-related items like Storm Globe and Snowcake
- `SnowCharmPool`: Snow-themed charms like Snowball Charm

### Tribe-Specific Pools
Pools that are exclusive to specific tribes:
- Snowdwellers: `BasicUnitPool`, `BasicItemPool`, `BasicCharmPool`
- Shademancers: `MagicUnitPool`, `MagicItemPool`, `MagicCharmPool`
- Clunkmasters: `ClunkUnitPool`, `ClunkItemPool`, `ClunkCharmPool`

## Implementation Notes
When modding or extending the reward system:
- New tribes should typically have their own unit, item, and charm pools
- Universal content can be added to the General pools
- Themed content that should be available across tribes can follow the Snow pool pattern
