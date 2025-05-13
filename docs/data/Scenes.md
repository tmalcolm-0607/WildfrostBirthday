
---
title: Scenes
description: |
  This file lists all scenes in Wildfrost, including their type, description, how they are called, and notes. Useful for modding and understanding game flow.
file_type: table
updated: 2025-05-12
---

## Summary
This file lists all scenes in Wildfrost, including their type, description, how they are called, and notes. Useful for modding and understanding game flow.

Below is the scene data in JSON format for easy programmatic use:

```json
{
  "title": "Scenes",
  "description": "This file lists all scenes in Wildfrost, including their type, description, how they are called, and notes. Useful for modding and understanding game flow.",
  "scenes": [
    {"scene": "Battle", "type": "Active", "description": "The current battlefield.", "called": "nan", "notes": "The bottom part of the battle (deckpack, hand, draw/discard piles) are part of UI."},
    {"scene": "BattleWin", "type": "Temporary", "description": "Shows the Sunfriend and possible injuries or gunkfruit.", "called": "nan", "notes": "nan"},
    {"scene": "Bootstrap", "type": "???", "description": "Essentially loading?", "called": "nan", "notes": "Probably should not mess with this."},
    {"scene": "BossReward", "type": "Temporary", "description": "Choose your boss rewards?", "called": "nan", "notes": "nan"},
    {"scene": "Camera", "type": "Persistent", "description": "Inspect system and other things.", "called": "nan", "notes": "nan"},
    {"scene": "Campaign", "type": "Persistent", "description": "Contains info about the player and the campaign", "called": "nan", "notes": "Created after journal name history"},
    {"scene": "CampaignEnd", "type": "Temporary?", "description": "Shows stats, achievements, and your leader?", "called": "nan", "notes": "nan"},
    {"scene": "CardCombine", "type": "Temporary", "description": "Lumin Vase Scene", "called": "CombineCardSystem calls it on EntityEnterBackpack", "notes": "Very easy to modify. Just add a new combo to CombineCardSystem"},
    {"scene": "CardFramesUnlocked", "type": "Temporary", "description": "Shows the most recent cards that 'earned' new card frames.", "called": "nan", "notes": "This is a very good scene for modding purposes. Any info that you want to pop-up after a battle/event can use this."},
    {"scene": "Cards", "type": "Active?", "description": "nan", "called": "nan", "notes": "No clue where this occurs."},
    {"scene": "CharacterSelect", "type": "Active", "description": "Choose your tribe and your starting pet", "called": "Menu.StartGameOrContinue()", "notes": "Gate is hardcoded to GameModeNormal. However, if you disable th persistent call on the gate, you can add a listener to go to any game mode you want. Campaign Data loaded right before this scene."},
    {"scene": "Console", "type": "Persistent?", "description": "Shows the command line for debugging", "called": "nan", "notes": "This is only created via external methods such as mods, after which it can be toggled via the ~ or F12 keys"},
    {"scene": "ContinueRun", "type": "Temporary", "description": "Ask whether to continue the run or not?", "called": "nan", "notes": "nan"},
    {"scene": "Credits", "type": "Active?", "description": "Self-explanatory", "called": "nan", "notes": "nan"},
    {"scene": "CreditsEnd", "type": "Temporary?", "description": "Self-explanatory", "called": "nan", "notes": "nan"},
    {"scene": "DemoEnd", "type": "???", "description": "Self-explanatory", "called": "nan", "notes": "'Demo' here is referring to the actual Wildfrost Demo. It showed up after the fourth fight"},
    {"scene": "Event", "type": "Active", "description": "Shops, trasures, companion ice, shade sculptor, gnome trader", "called": "CampaignNodeEvent.Run(node)", "notes": "Each of these events have their own CampaignNode class (determines map appearance and rewards) and EventRoutine class (Sequencing of events, interaction with the event)."},
    {"scene": "Global", "type": "Persistent", "description": "Various singletons live here.", "called": "nan", "notes": "Contains References (in GameManager) and the AddressableLoader (in AssetLoader). Useful for looking things up."},
    {"scene": "Input", "type": "Persistent", "description": "Manages the InputSystems for different control schemes", "called": "nan", "notes": "nan"},
    {"scene": "JournalNameHistory", "type": "Temporary", "description": "List of names in the journal.", "called": "CharacterSelectScreen calls JournalAddNameSequence.LoadAndRun()", "notes": "Appears after CharacterSelect"},
    {"scene": "MainMenu", "type": "Active", "description": "The main menu", "called": "nan", "notes": "nan"},
    {"scene": "MapNew", "type": "Active", "description": "The map and all mapnodes on it.", "called": "nan", "notes": "All nodes are created at the beginning of the Campaign through CampaignGenerator."},
    {"scene": "Mods", "type": "Temporary", "description": "Load and unload mods.", "called": "nan", "notes": "nan"},
    {"scene": "NewFrostGuardian", "type": "Temporary?", "description": "nan", "called": "nan", "notes": "nan"},
    {"scene": "PauseScreen", "type": "Persistent", "description": "The Pause Screen.", "called": "nan", "notes": "The scene holding the journal (pause menu)."},
    {"scene": "PetSelect", "type": "Temporary?", "description": "nan", "called": "nan", "notes": "I thought it was a temporary scene that shows up during characterSelect, but that might not be the case."},
    {"scene": "Saving", "type": "???", "description": "nan", "called": "nan", "notes": "Probably should not mess with this."},
    {"scene": "Systems", "type": "Persistent?", "description": "nan", "called": "nan", "notes": "Probably should not mess with this."},
    {"scene": "Town", "type": "Active", "description": "The town of Snowdwell and all buildings.", "called": "nan", "notes": "nan"},
    {"scene": "TownUnlocks", "type": "Temporary", "description": "Show new town unlocks.", "called": "nan", "notes": "nan"},
    {"scene": "UI", "type": "Persistent", "description": "Deals with hand and deckpack during the campaign.", "called": "nan", "notes": "Created right after campaign. Does not get unloaded after battle."}
  ]
}
```