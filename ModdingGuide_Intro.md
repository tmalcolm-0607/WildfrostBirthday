# Wildfrost Modding Guide

## Introduction

This guide provides comprehensive documentation for modding Wildfrost, a deck-building roguelike game. Whether you want to create new cards, upgrades, effects, or mechanics, this documentation will help you understand the game's structure and how to modify it.

## Getting Started with Modding

### Prerequisites
- Basic understanding of C# programming
- Familiarity with Unity (helpful but not required)
- The game Wildfrost installed on your computer
- A code editor (like Visual Studio, Visual Studio Code, etc.)

### Setting Up Your Development Environment

1. **Set Up for Steam Workshop**:
   - Wildfrost supports Steam Workshop for mod distribution
   - Create a new folder for your mod project
   - Familiarize yourself with the Steam Workshop submission process

2. **Create Your Mod Project**:
   - Create a new C# class library project
   - Reference the necessary assemblies from your Wildfrost installation:
     - `Assembly-CSharp.dll`
     - `UnityEngine.dll`
     - `UnityEngine.CoreModule.dll`

3. **Add Basic Mod Structure**:
   ```csharp
   using System;
   using UnityEngine;
   using Deadpan.Enums.Engine.Components.Modding;
   
   namespace MyWildfrostMod
   {
       public class MyMod : MonoBehaviour
       {
           void Awake()
           {
               // Your initialization code here
               Debug.Log("My Wildfrost mod loaded!");
           }
           
           void Start()
           {
               // Your mod code here
           }
       }
   }
   ```

## Core Game Concepts

### Cards

Cards are the main gameplay element in Wildfrost. They represent characters, items, spells, and other entities in the game.

Key components:
- **CardData**: Stores all card information
- **CardType**: Defines the category of card (unit, item, spell, etc.)
- **CardScript**: Custom behavior scripts attached to cards

### Upgrades

Upgrades modify cards to enhance or change their abilities. They come in three types:
- **Charms**: General upgrades that occupy charm slots
- **Tokens**: Special enhancements that occupy token slots
- **Crowns**: Powerful upgrades that occupy crown slots

### Effects and Traits

- **StatusEffectData**: Effects that can be applied to cards (damage over time, buffs, etc.)
- **TraitData**: Inherent properties of cards that affect their behavior

### Targeting System

Wildfrost uses a robust targeting system to determine what cards or entities can be affected by abilities:
- **TargetConstraint**: Defines conditions for valid targets
- **TargetMode**: Determines how targets are selected

## Main Documentation Sections

- [Card Creation Guide](./CardData_Doc.md)
- [Creating Card Upgrades](./CardUpgradeData_Doc.md)
- [Target Constraint System](./TargetConstraint_Doc.md)
- [Status Effects and Traits](./StatusEffects_Doc.md)
- [Card Scripts and Behaviors](./CardScript_Doc.md)

## Example: Creating a Simple Charm

Here's a basic example of creating a charm that gives +2 damage:

```csharp
public void CreateExampleCharm()
{
    // Create a new charm using the builder
    var builder = new CardUpgradeDataBuilder(this)
        .CreateCharm("PowerCharm", "GeneralCharmPool")
        .WithTier(1)
        .ChangeDamage(2)
        .WithLocalizedName("Power Charm")
        .WithLocalizedDesc("+2 Damage");
    
    // Set an appropriate image
    builder.WithImage(LoadSprite("path/to/charm_image.png"));
    
    // Add targeting constraints
    var doesAttack = ScriptableObject.CreateInstance<TargetConstraintDoesAttack>();
    builder.SetConstraints(doesAttack);
    
    // Build and register the charm
    builder.Build();
}
```

## Resources

- [Wildfrost Discord](https://discord.gg/wildfrost)
- [Steam Workshop for Wildfrost](https://steamcommunity.com/app/1562240/workshop/)
- [Community Mod Repository](#)

## Contributing to This Documentation

This documentation is a community effort. If you'd like to contribute:

1. Add or update information in the relevant document files
2. Submit additions or corrections to the main repository
3. Share examples and case studies from your modding experience
