# Automated Component Registration System

## Overview
This document describes the automated component registration system implemented for the WildfrostBirthday mod.
The system eliminates the need to manually add each new component to registration methods in the `WildFamilyMod.cs` file.

## How It Works

### The Component Registration Pattern
The system uses reflection to automatically discover and register mod components that follow this pattern:

1. Each component is defined in a static class named with a prefix that identifies its type:
   - `StatusEffect_*` for status effects
   - `Card_*` for cards/units
   - `Item_*` for items
   - `Charm_*` for charms
   - `Tribe_*` for tribes

2. Each component class provides a static `Register` method that takes a `WildFamilyMod` instance as its parameter.

The system scans the entire assembly to find all classes matching these patterns and automatically registers them by calling their `Register` methods.

### Benefits
- **Automatic discovery**: New components are registered automatically without manual updating of registration methods
- **Organization**: Components remain grouped by their type in the code
- **Maintainability**: Reduces the risk of forgetting to register new components
- **Consistency**: Enforces a consistent registration pattern for all components

## Usage

### For Mod Development
The registration process is now fully automated. When you add a new component:

1. Create your component class following the naming pattern (e.g., `StatusEffect_MyNewEffect`, `Card_MyNewCard`, etc.)
2. Implement the static `Register` method that takes a `WildFamilyMod` parameter
3. That's it! The component will be automatically registered when the mod loads

### Registration Process
At mod load time:
1. `WildFamilyMod.Load()` calls `this.RegisterAllComponents()`
2. `RegisterAllComponents()` calls individual registration methods for each category
3. Each category registration method uses reflection to find and register components of that type

### Template Files
The template files in each category folder follow the correct pattern, so new components created from templates will be automatically registered:
- `StatusEffect_Template.cs`
- `Card_Template.cs`
- `Item_Template.cs`
- `Charm_Template.cs`

### Technical Details

The implementation is in `ComponentRegistration.cs` in the `Helpers` namespace. Key methods:

- `RegisterAllComponents`: Main entry point that registers all component types
- `RegisterAllStatusEffects`: Registers all status effects
- `RegisterAllCards`: Registers all cards/units
- `RegisterAllItems`: Registers all items
- `RegisterAllCharms`: Registers all charms
- `RegisterAllTribes`: Registers all tribes

These methods are implemented as extension methods for `WildFamilyMod` for easy use.

The system uses a cache to store discovered component types for better performance when the registration process is run multiple times.

## Notes
- The system skips classes with "Template" in their name, as these are meant to be examples
- Error handling is included to ensure that failure to register one component doesn't prevent others from being registered
- Debug logging is included to provide visibility into the registration process
