# Helper Classes Refactoring

## Overview
This document outlines the refactoring of helper methods from the `WildFamilyMod` class into specialized utility classes.

## Refactored Classes

### 1. CardScriptHelpers
Location: `WildfrostBirthday/Helpers/CardScriptHelpers.cs`

Contains methods for creating card script objects:
- `GetGiveUpgradeScript` - Creates a script that assigns an upgrade to a card
- `GetRandomHealthScript` - Creates a script that randomly modifies a card's health
- `GetRandomDamageScript` - Creates a script that randomly modifies a card's attack
- `GetRandomCounterScript` - Creates a script that randomly modifies a card's counter

### 2. StackHelpers
Location: `WildfrostBirthday/Helpers/StackHelpers.cs`

Contains extension methods for creating stacks:
- `SStack` - Creates a StatusEffectStacks object
- `TStack` - Creates a TraitStacks object

### 3. DataCopyHelpers
Location: `WildfrostBirthday/Helpers/DataCopyHelpers.cs`

Contains extension methods for copying data objects:
- `StatusCopy` - Creates a copy of a StatusEffectData with a new name
- `CardCopy` - Creates a copy of a CardData with a new name
- `TribeCopy` - Creates a copy of a ClassData (tribe) with a new name
- `DataCopy` - Generic method to copy any DataFile with a new name

### 4. DataUtilities
Location: `WildfrostBirthday/Helpers/DataUtilities.cs`

Contains extension methods for data operations:
- `DataList` - Creates an array of DataFile objects from their names
- `RemoveNulls` - Removes null and mod-added DataFile objects from an array

### 5. RewardPoolHelpers
Location: `WildfrostBirthday/Helpers/RewardPoolHelpers.cs`

Contains methods for reward pool operations:
- `CreateRewardPool` - Creates a reward pool with items
- `UnloadFromClasses` - Removes mod-added items from all tribe reward pools
- `FixImage` - Fixes card images that should use static images

## Usage

All helper methods are now extension methods, so they can be called directly on a `WildFamilyMod` instance:

```csharp
// Old way:
mod.SStack("Effect", 1);

// New way (identical syntax thanks to extension methods):
mod.SStack("Effect", 1);
```

The global using statements have been updated to make these helpers available throughout the project.

## Benefits

1. **Better organization**: Related functionality is grouped together
2. **Reduced coupling**: Helper methods no longer depend on the mod class
3. **Improved maintainability**: Easier to find, update, and test helper methods
4. **Better encapsulation**: Implementation details are hidden in specialized classes
5. **Enhanced extensibility**: New helper methods can be added without modifying the core mod class
