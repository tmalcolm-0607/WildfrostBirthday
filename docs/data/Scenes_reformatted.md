---
title: Scenes
description: Reference for all scenes in Wildfrost, including their types, descriptions, call methods, and implementation notes
file_type: data_reference
data_format: json
updated: 2025-05-12
source_file: Scenes.md
---

## Summary
This document catalogs all scenes used in Wildfrost. Scenes represent distinct states or modes of the game, each responsible for specific functionality and presentation. Understanding scenes is crucial for modding and extending the game's functionality.

## Data Usage
- Use when implementing new game states or UI elements
- Reference when modifying existing scenes
- Understand the game flow and scene transitions

## JSON Data

```json
{
  "title": "Scenes",
  "description": "Reference for all scenes in Wildfrost, including their types, descriptions, call methods, and implementation notes",
  "scenes": [
    {"scene": "Battle", "type": "Active", "description": "The current battlefield.", "called": null, "notes": "The bottom part of the battle (deckpack, hand, draw/discard piles) are part of UI."},
    {"scene": "BattleWin", "type": "Temporary", "description": "Shows the Sunfriend and possible injuries or gunkfruit.", "called": null, "notes": null},
    {"scene": "Bootstrap", "type": "???", "description": "Essentially loading?", "called": null, "notes": "Probably should not mess with this."},
    {"scene": "BossReward", "type": "Temporary", "description": "Choose your boss rewards?", "called": null, "notes": null},
    {"scene": "Camera", "type": "Persistent", "description": "Inspect system and other things.", "called": null, "notes": null},
    {"scene": "Campaign", "type": "Persistent", "description": "Contains info about the player and the campaign", "called": null, "notes": "Created after journal name history"},
    {"scene": "CampaignEnd", "type": "Temporary?", "description": "Shows stats, achievements, and your leader?", "called": null, "notes": null},
    {"scene": "CardCombine", "type": "Temporary", "description": "Lumin Vase Scene", "called": "CombineCardSystem calls it on EntityEnterBackpack", "notes": "Very easy to modify. Just add a new combo to CombineCardSystem"},
    {"scene": "CardFramesUnlocked", "type": "Temporary", "description": "Shows the most recent cards that 'earned' new card frames.", "called": null, "notes": "This is a very good scene for modding purposes. Any info that you want to pop-up after a battle/event can use this."},
    {"scene": "Cards", "type": "Active?", "description": null, "called": null, "notes": "No clue where this occurs."},
    {"scene": "CharacterSelect", "type": "Active", "description": "Choose your tribe and your starting pet", "called": "Menu.StartGameOrContinue()", "notes": "Gate is hardcoded to GameModeNormal. However, if you disable th persistent call on the gate, you can add a listener to go to any game mode you want. Campaign Data loaded right before this scene."},
    {"scene": "Console", "type": "Persistent?", "description": "Shows the command line for debugging", "called": null, "notes": "This is only created via external methods such as mods, after which it can be toggled via the ~ or F12 keys"},
    {"scene": "ContinueRun", "type": "Temporary", "description": "Ask whether to continue the run or not?", "called": null, "notes": null},
    {"scene": "Credits", "type": "Active?", "description": "Self-explanatory", "called": null, "notes": null},
    {"scene": "CreditsEnd", "type": "Temporary?", "description": "Self-explanatory", "called": null, "notes": null},
    {"scene": "DemoEnd", "type": "???", "description": "Self-explanatory", "called": null, "notes": "'Demo' here is referring to the actual Wildfrost Demo. It showed up after the fourth fight"},
    {"scene": "Event", "type": "Active", "description": "Shops, trasures, companion ice, shade sculptor, gnome trader", "called": "CampaignNodeEvent.Run(node)", "notes": "Each of these events have their own CampaignNode class (determines map appearance and rewards) and EventRoutine class (Sequencing of events, interaction with the event)."},
    {"scene": "Global", "type": "Persistent", "description": "Various singletons live here.", "called": null, "notes": "Contains References (in GameManager) and the AddressableLoader (in AssetLoader). Useful for looking things up."},
    {"scene": "Input", "type": "Persistent", "description": "Manages the InputSystems for different control schemes", "called": null, "notes": null},
    {"scene": "JournalNameHistory", "type": "Temporary", "description": "List of names in the journal.", "called": "CharacterSelectScreen calls JournalAddNameSequence.LoadAndRun()", "notes": "Appears after CharacterSelect"},
    {"scene": "MainMenu", "type": "Active", "description": "The main menu", "called": null, "notes": null},
    {"scene": "MapNew", "type": "Active", "description": "The map and all mapnodes on it.", "called": null, "notes": "All nodes are created at the beginning of the Campaign through CampaignGenerator."},
    {"scene": "Mods", "type": "Temporary", "description": "Load and unload mods.", "called": null, "notes": null},
    {"scene": "NewFrostGuardian", "type": "Temporary?", "description": null, "called": null, "notes": null},
    {"scene": "PauseScreen", "type": "Persistent", "description": "The Pause Screen.", "called": null, "notes": "The scene holding the journal (pause menu)."},
    {"scene": "PetSelect", "type": "Temporary?", "description": null, "called": null, "notes": "I thought it was a temporary scene that shows up during characterSelect, but that might not be the case."},
    {"scene": "Saving", "type": "???", "description": null, "called": null, "notes": "Probably should not mess with this."},
    {"scene": "Systems", "type": "Persistent?", "description": null, "called": null, "notes": "Probably should not mess with this."},
    {"scene": "Town", "type": "Active", "description": "The town of Snowdwell and all buildings.", "called": null, "notes": null},
    {"scene": "TownUnlocks", "type": "Temporary", "description": "Show new town unlocks.", "called": null, "notes": null},
    {"scene": "UI", "type": "Persistent", "description": "Deals with hand and deckpack during the campaign.", "called": null, "notes": "Created right after campaign. Does not get unloaded after battle."}
  ]
}
```

## Scene Types Explained

### Persistent Scenes
Scenes marked as "Persistent" remain loaded throughout the game session and provide continuous functionality:
- `Global`: Contains core game singletons and managers
- `Campaign`: Maintains campaign state and player information
- `UI`: Handles interface elements that persist across different game states
- `Camera`: Controls viewing perspective and inspect functionality

### Active Scenes
These are the main playable scenes where most gameplay occurs:
- `Battle`: Where combat takes place
- `MapNew`: The campaign map for navigation
- `Town`: Snowdwell town hub for upgrades and progression
- `CharacterSelect`: Character creation and run setup

### Temporary Scenes
Short-lived scenes that appear for specific interactions or information display:
- `BattleWin`: End-of-battle results and rewards
- `CardCombine`: Special card combination interfaces
- `BossReward`: Special rewards after defeating bosses
- `CardFramesUnlocked`: Notification of new card frame unlocks

## Modding Opportunities
Several scenes are particularly well-suited for modding:
- `CardFramesUnlocked`: Can be repurposed for any post-battle/event notifications
- `CardCombine`: Easy to extend by adding new combinations to CombineCardSystem
- `Event`: Can be extended with new event types by creating custom CampaignNode and EventRoutine classes
