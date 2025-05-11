# Wildfrost Modding Tools & Techniques

## Overview
Welcome prospective modders! This guide is intended for those who know the basics of coding and want to understand the source code and modding tools for Wildfrost. It covers essential tools, in-game exploration, and useful commands for modding and debugging.

## Contents
- [Object Browser (Visual Studio)](#object-browser-visual-studio)
- [Unity Explorer Mod](#unity-explorer-mod)
- [Another Console Mod](#another-console-mod)
- [Useful Commands](#useful-commands)
- [Vanilla References](#vanilla-references)
- [Publishing Mods](#publishing-mods)

---

## Object Browser (Visual Studio)
In the `GameRoot/Modded/Wildfrost_Data/Managed` folder, the `Assembly-CSharp` file contains most of the game's code. Add this to your references in Visual Studio. Use the Object Browser to:
- View class summaries, fields, and methods
- Double-click to see code details
- Pin the Object Browser for quick access

**Tip:** The Object Browser shows class structure but not Unity scene relationships. For that, use the Unity Explorer mod.

### Noteworthy Classes
- **Events:** Maintains all game events
- **References:** Access singleton objects (e.g., `References.PlayerData.inventory.deck`)
- **CampaignNodeX/EventRoutineX:** Map nodes and their events
- **Card, CardData, Entity:** Card data, logic, and appearance
- **StatusEffectData:** Status effect implementations

---

## Unity Explorer Mod
Subscribe to the Unity Explorer mod from the Steam Workshop. Enable it in-game via the mods menu. Once active, you can:
- Inspect live Unity scenes and objects
- Explore assets like `AddressableLoader` to see all available assets (cards, effects, etc.)
- Inspect `CardData`, `StatusEffectData`, and `CardUpgradeData` in detail

**Tip:** Internal card names may differ from display names (e.g., Bear = Scaven). Use the explorer to find and inspect cards by internal name.

---

## Another Console Mod
Subscribe to the Another Console mod from the Steam Workshop. Press `~` in-game to open the command line. Features include:
- Skipping battles, gaining cards, adding effects
- Inspecting cards and data files
- Generating builder-style code for cards/effects
- Streamlined commands for quick testing

**Note:** The console is disabled while Unity Explorer is open. Close it with F7 to use the console.

---

## Useful Commands
- `Add Effect <name> <amount>`: Add effect to hovered card
- `Add Status <name> <amount>`: Add status to hovered card
- `Add Trait <name> <amount>`: Add trait to hovered card
- `Add Upgrade <name> <amount>`: Add upgrade to hovered card
- `Battle Win`: Win the current battle
- `DataBuilder of <DataType> <name>`: Generate builder code for a card/effect/charm
- `Gain Card <name>`: Add card to deck/hand
- `Gain Upgrade <name>`: Add crown/charm to deck
- `Inspect <DataFile> <name>`: Open Unity Explorer on a data file
- `Inspect this`: Inspect hovered card/entity
- `Kill/Kill All`: Kill hovered card or all enemies
- `Map Jump`: Jump to a map node
- `Reroll`: Reroll event card choices
- `Spawn <name>`: Spawn a card at hovered location

---

## Vanilla References
A reference spreadsheet with base game data is available [here](../Reference/Wildfrost%20Reference.xlsx) or on the Vanilla References page.

---

## Console Mod
The Console Mod outputs logs from `Debug.Log()` and other internal messages. Logs are saved in `AppData/LocalLow/Deadpan Games/Wildfrost`.

---

## Mod Uploader
The Mod Uploader lets you add tags when publishing your mod, making it easier to discover. See the next tutorial for publishing instructions.

---

## Additional Resources
- [Wildfrost Modding Wiki](https://github.com/DeadpanGames/WildfrostModdingDocumentation.wiki)
- [Wildfrost Reference Spreadsheet](../Reference/Wildfrost%20Reference.xlsx)
- [Wildfrost Mod Tutorials](../WildfrostModTutorials-master/)

## Last Updated
May 11, 2025
