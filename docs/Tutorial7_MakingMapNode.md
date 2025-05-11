# Tutorial 7: Making a Map Node

*Michael edited this page on Oct 23, 2024 · 5 revisions*
*By Michael Coopman*

---

## Contents
- [Overview](#overview)
- [Background](#background)
- [Setup](#setup)
  - [New Setup Code](#new-setup-code)
  - [Revised Setup Code](#revised-setup-code)
  - [Old Setup Code](#old-setup-code)
- [CampaignNodeTypeBuilder](#campaignnodetypebuilder)
- [MapNode](#mapnode)
- [Map Node Generation](#map-node-generation)
  - [Mandatory Nodes (Preset Editing)](#mandatory-nodes-preset-editing)
  - [Common Nodes (Populator Editing)](#common-nodes-populator-editing)
  - [Rare Nodes (Special Event System)](#rare-nodes-special-event-system)
- [CampaignNodeType Class Creation](#campaignnodetype-class-creation)
  - [Setup](#setup-1)
  - [HasMissingData](#hasmissingdata)
  - [Run](#run)
- [Final Comments: Event Routines](#final-comments-event-routines)

---

## Overview
This tutorial covers how to use the `CampaignNodeTypeBuilder` to make a map node. It showcases three ways to add the map node into a run, explains custom `CampaignNodeType` classes, and touches on making an `EventRoutine`.

---

## Background
Assumes you have read Tutorial 0, publicized your assembly, and are familiar with conventions and helpers from earlier tutorials. No patching is required. Addressables can be used for map sprites, but are not covered here.

---

## Setup
### New Setup Code
- **ScaledSprite:** Helper for sprite scaling.
- **PrefabHolder:** Static GameObject to store reference prefabs, created in `Load()` and destroyed in `Unload()`.
- **SStack:** Helper for status effect stacks, must be internal/public.
- **Instance:** Declare in the constructor, not in `Load()`.

### Revised Setup Code
- Use internal/public SStack.
- Declare instance in the constructor.

### Old Setup Code
- See previous tutorials for assets, AddAssets, CreateModAssets, CreateLocalizedStrings, Load/Unload, TryGet.

---

## CampaignNodeTypeBuilder
- Use the builder to create a new node type (e.g., PortalNode).
- Set properties like `WithCanEnter`, `WithInteractable`, `WithCanSkip`, `WithCanLink`, `WithLetter`.
- Use `SubscribeToAfterAllBuildEvent` to set up the prefab and localized strings.

---

## MapNode
- Clone an existing MapNode prefab (e.g., Blingsnail Cave).
- Set the name, assign to `data.mapNodePrefab`.
- Set up localized ribbon title and assign sprites for open/closed states.
- Store the prefab in `PrefabHolder`.

---

## Map Node Generation
### Mandatory Nodes (Preset Editing)
- Use `Events.OnCampaignLoadPreset` to insert your node into the preset blueprint.
- Insert your node after a specific node (e.g., after Snowdwell).
- Ensure the node letter is unique and single-character.

### Common Nodes (Populator Editing)
- Modify the `campaignPopulator` of the game mode to add your node to reward pools for specific tiers.
- Use `AddToPopulator` and `RemoveFromPopulator` methods.

### Rare Nodes (Special Event System)
- Hook into `Events.OnSceneLoaded` to add your node to the `SpecialEventsSystem` in the Campaign scene.
- Configure event properties (minTier, perTier, perRun, etc).

---

## CampaignNodeType Class Creation
### Setup
- Store all randomness and data in `SetUp` using serializable types.
- Example: track access count, event order, and chosen charm.

### HasMissingData
- Check for missing/invalid data (e.g., missing charm).
- Return true if data is missing, false otherwise.

### Run
- Increment access count, determine event, and perform the event (bling, injury, charm).
- Use helper methods for each event and display feedback with localized strings and popups.
- Mark node as cleared when finished.

---

## Final Comments: Event Routines
- To make a full event, derive from `CampaignNodeTypeEvent` and create a prefab.
- Study existing events and use Unity Explorer to inspect components.
- Most of the work is UI and prefab setup.

---

[Back to Modding Tutorials](index.md)
