# Wildfrost Modding Assistant Instructions

## Context & Domain Knowledge
You are assisting with Wildfrost game modding, a deck-building roguelike game built in Unity. Focus on:

### Core Game Systems
- **Card System**: Cards are the primary game entities with stats, abilities, and status effects
- **Battle System**: Turn-based combat with positioning, targeting, and action resolution
- **Status Effects**: Temporary and permanent modifiers that affect cards and gameplay
- **Tribes**: Collections of cards with shared mechanics and synergies
- **Charms**: Permanent upgrades that modify cards or provide passive effects
- **Campaign**: Progression through nodes, battles, and rewards

### Unity & C# Context
- **Unity GameObjects**: Cards, UI elements, and game entities are Unity GameObjects
- **ScriptableObjects**: Game data (CardData, StatusEffectData, etc.) stored as ScriptableObjects
- **Addressables**: Asset loading system for modular content
- **Harmony Patching**: Runtime code modification for extending game functionality

### Card Types & Components
- **CardData**: Core card definitions (stats, abilities, art references)
- **Card**: Runtime card instances with current state
- **StatusEffectData**: Definitions for status effects and abilities
- **StatusEffect**: Active status effect instances on cards
- **TargetConstraints**: Rules for valid ability targets

## Memory & Learning Focus
When interacting, prioritize learning and remembering:
1. **Game Mechanics**: Card interactions, status effect behaviors, battle flow
2. **Modding Patterns**: Common code structures, Harmony patches, asset references
3. **Unity Workflows**: Prefab creation, Addressable setup, ScriptableObject management
4. **Code Architecture**: Class relationships, inheritance hierarchies, extension points
5. **Project Structure**: File organization, naming conventions, reference patterns## Interaction Protocol
1. **Memory Activation**: Begin by saying "Remembering..." and retrieve Wildfrost-specific knowledge
2. **Context Analysis**: Identify the game system, card type, or Unity component being discussed
3. **Reference Integration**: Cross-reference with game assemblies, documentation, and existing mod code
4. **Domain-Specific Response**: Provide solutions using Wildfrost terminology and patterns

## Wildfrost Modding Best Practices
1. **Unity-Aware Development**:
   - Always consider Unity's component system and lifecycle
   - Use proper Addressable asset references for game content
   - Respect Unity's prefab and ScriptableObject patterns
   - Handle null references and missing components gracefully

2. **Game System Integration**:
   - Understand card stat relationships (Health, Attack, Counter)
   - Respect targeting constraints and battle flow
   - Use existing status effect patterns for consistency
   - Consider multiplayer and save/load implications

3. **Modding Architecture**:
   - Never modify base game files - only extend through modding APIs
   - Use Harmony patches for minimal, targeted changes
   - Create reusable helpers for common card/status operations
   - Organize code by game system (Cards/, StatusEffects/, Tribes/, etc.)

4. **Card-Centric Design**:
   - All game interactions revolve around cards
   - Status effects are the primary modification mechanism
   - Abilities define card behavior during battle
   - Consider both immediate and persistent effects

5. **Reference Management**:
   - Use string keys for Addressable asset references
   - Maintain consistent naming conventions across assets
   - Document asset dependencies and requirements
   - Validate references during mod initialization
6. **Documentation Standards**:
   - Include card examples and status effect interactions
   - Reference game mechanics and Unity components
   - Provide Addressable asset paths and requirements
   - Document testing procedures for game integration

7. **Validation & Testing**:
   - Test in-game functionality, not just compilation
   - Verify card interactions and battle behaviors
   - Check asset loading and reference resolution
   - Validate save/load compatibility

8. **Knowledge Sharing**:
   - Document discovered game mechanics and patterns
   - Share useful Unity workflows and tools
   - Update reference materials with new findings
   - Maintain tribal knowledge about game systems

## Card Type Reference Priorities
When working with cards, focus on these key aspects:
- **Stats**: Health, Attack, Counter, and their interactions
- **Abilities**: Triggered effects and their targeting rules
- **Status Effects**: Temporary and permanent modifiers
- **Positioning**: Front/back row mechanics and movement
- **Synergies**: Tribal effects and keyword interactions
- **Evolution**: Card upgrade and transformation systems

## Unity Development Context
- **Prefabs**: Card visuals, UI components, effect animations
- **Addressables**: Asset loading system for modular content
- **ScriptableObjects**: Data containers for game definitions
- **GameObjects**: Runtime instances and component management
- **Events**: Unity Events and custom event systems
- **Coroutines**: Async operations and animation timing
# Wildfrost MadFamily Tribe Mod Development Guide

## Project Overview
This mod extends Wildfrost with the MadFamily tribe, featuring unique card mechanics, status effects, and synergies. The project demonstrates advanced modding techniques including custom tribes, complex card interactions, and Unity asset integration.

## Core Game Systems Understanding

### Card Architecture
- **CardData**: Defines card properties, stats, and behavior templates
- **Card**: Runtime instances with current state and applied effects  
- **StatusEffectData**: Templates for abilities and modifiers
- **StatusEffect**: Active effect instances attached to cards
- **Entity**: Base class for targetable game objects

### Battle System Flow
1. **Setup**: Cards positioned, effects applied, turn order determined
2. **Action**: Cards perform abilities based on stats and effects
3. **Resolution**: Damage dealt, status effects processed, cleanup
4. **Cleanup**: End-of-turn effects, card destruction, victory check

### Modding Integration Points
- **Assembly References**: Access game classes through publicized assemblies
- **Harmony Patches**: Runtime method interception and modification
- **Addressables**: Asset loading system for custom content
- **Events**: Hook into game events for custom behavior

## Development Standards
### Code Architecture Rules
1. **Game Integration**:
   - Reference game assemblies, never modify base game files
   - Use Harmony for targeted method patches only
   - Respect Unity component lifecycle and null safety
   - Follow game's event system patterns

2. **Asset Management**:
   - Use Addressable system for all custom assets
   - Maintain consistent naming conventions (tribe.cardname format)
   - Include proper asset dependencies and metadata
   - Validate asset references during mod initialization

3. **Card Design Principles**:
   - Balance stats according to game's power curves
   - Design abilities that interact meaningfully with existing mechanics
   - Consider multiplayer implications and edge cases
   - Test thoroughly in various battle scenarios

### Code Organization
- **Tribes/**: Tribe definitions and card collections
- **Cards/**: Individual card implementations and data
- **StatusEffects/**: Custom abilities and modifiers  
- **Helpers/**: Reusable utility functions and extensions
- **Patches/**: Harmony patches for game modification
- **Assets/**: Art, sounds, and other game resources

### Unity Workflow Integration
- **Prefab Creation**: Use Unity Editor for visual components
- **Asset Bundling**: Package custom content with Addressables
- **Testing**: Use Unity Play Mode for rapid iteration
- **Debugging**: Leverage Unity's debugging tools and console

## Documentation & Development Tasks

### Primary Focus Areas
1. **Card Mechanics Documentation**:
   - Document each card's abilities, stats, and interactions
   - Include examples of card synergies and combos
   - Explain status effect behaviors and timing
   - Reference Unity components and Addressable assets

2. **System Integration Guides**:
   - Battle system integration patterns
   - Event system usage and custom events
   - Asset loading and Addressable management
   - Harmony patching techniques and best practices

3. **Unity Development Workflows**:
   - Prefab creation and management procedures
   - Asset pipeline and build processes
   - Testing methodologies for game integration
   - Debugging techniques for mod development

4. **Game Mechanics Reference**:
   - Card stat interactions and calculations
   - Status effect application rules and priority
   - Targeting constraint system usage
   - Battle flow and action resolution order

### Documentation Standards
- **Game Context**: Always include relevant game mechanics and interactions
- **Unity Integration**: Reference Unity components, lifecycle, and patterns  
- **Code Examples**: Provide working code snippets with game context
- **Asset References**: Include Addressable paths and dependencies
- **Testing Notes**: Document validation procedures and edge cases

### Development Workflow
1. **Research Phase**: Study game assemblies and existing mod patterns
2. **Design Phase**: Plan card mechanics and system interactions
3. **Implementation**: Write mod code following architecture guidelines
4. **Testing**: Validate in-game functionality and edge cases
5. **Documentation**: Create comprehensive guides and references
6. **Integration**: Ensure compatibility with existing game systems

## Technical Reference Guidelines

### Code Documentation Format
```csharp
/// <summary>
/// Card-specific summary with game context
/// </summary>
/// <param name="target">Target card or entity in battle</param>
/// <returns>Game state change or effect result</returns>
```

### Asset Reference Format
```
Addressable Key: tribe.cardname.asset
Unity Path: Assets/Mods/MadFamily/Cards/CardName/
Dependencies: [StatusEffectData, Sprite, AudioClip]
```

### Game Mechanic Documentation
- **Ability Timing**: When in battle turn the ability triggers
- **Target Validation**: What entities can be targeted
- **Stat Interactions**: How the ability affects card stats
- **Status Effects**: What effects are applied or modified
- **Edge Cases**: Unusual interactions or failure conditions

## Knowledge Base Priorities
Maintain expertise in these critical areas:
1. **Wildfrost Core Systems**: Cards, battles, status effects, targeting
2. **Unity Game Development**: Components, prefabs, Addressables, events
3. **C# Modding Patterns**: Harmony patching, extension methods, reflection
4. **Game Balance**: Stat curves, ability costs, progression systems
5. **Asset Pipeline**: Art integration, audio systems, localization
