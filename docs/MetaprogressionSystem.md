# MetaprogressionSystem

`MetaprogressionSystem` is a core system in Wildfrost that manages meta-progression across multiple runs, including unlocks, progression data, and content availability.

## Class Overview

The `MetaprogressionSystem` inherits from `GameSystem` and serves as a central repository for tracking player progress and unlocked content throughout the game.

## Static Data Dictionary

The system maintains a static dictionary (`data`) that contains default content collections for the game:

| Key | Type | Description |
|-----|------|-------------|
| `"pets"` | `Dictionary<string, string>` | Maps pet IDs to unlock requirements |
| `"items"` | `List<string>` | List of unlockable items |
| `"companions"` | `List<string>` | List of unlockable companions |
| `"events"` | `List<string>` | List of unlockable campaign events |
| `"buildings"` | `List<string>` | List of unlockable town buildings |
| `"charms"` | `Dictionary<string, string>` | Maps charm unlock IDs to actual charm IDs |

## Key Methods

### Data Access Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `Get<T>` | `string key` | `T` | Gets a value from the static data dictionary |
| `Add<T, Y>` | `string key, Y keyValue, T value` | `void` | Adds a keyed value to a dictionary in the data |
| `Remove<T, Y>` | `string key, Y keyValue, T value` | `bool` | Removes a keyed value from a dictionary in the data |
| `Add<T>` | `string key, T value` | `void` | Adds a value to a list in the data |
| `Remove<T>` | `string key, T value` | `bool` | Removes a value from a list in the data |
| `Get<T>` | `string key, T defaultValue` | `T` | Gets a value from save data with fallback |
| `Set<T>` | `string key, T value` | `void` | Saves a value to the progression save data |

### Unlock Management Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `GetUnlockedList` | None | `List<string>` | Gets the list of all unlocked content IDs |
| `GetUnlocked` | `Predicate<UnlockData> match` | `IEnumerable<UnlockData>` | Gets unlocked content matching the predicate |
| `GetRemainingUnlocks` | `List<string> alreadyUnlocked = null` | `List<UnlockData>` | Gets content that is not yet unlocked |
| `IsUnlocked` | `UnlockData unlockData, List<string> alreadyUnlocked = null` | `bool` | Checks if specific content is unlocked |
| `IsUnlocked` | `string unlockDataName, List<string> alreadyUnlocked = null` | `bool` | Checks if content with the given name is unlocked |

### Game Content Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `GetLockedClasses` | None | `List<ClassData>` | Gets classes that haven't been unlocked yet |
| `GetLockedItems` | `List<UnlockData> remainingUnlocks` | `List<string>` | Gets items that haven't been unlocked yet |
| `GetLockedCompanions` | `List<UnlockData> remainingUnlocks` | `List<string>` | Gets companions that haven't been unlocked yet |
| `GetLockedCharms` | `List<UnlockData> remainingUnlocks` | `List<string>` | Gets charms that haven't been unlocked yet |
| `GetAllPets` | None | `string[]` | Gets all pet IDs |
| `GetPetDict` | None | `Dictionary<string, string>` | Gets the pet dictionary |
| `GetUnlockedPets` | None | `string[]` | Gets the IDs of unlocked pets |

### Town and Unlock UI Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `SetUnlocksReady` | `string unlockName` | `void` | Marks an unlock as ready and updates the UI |
| `CheckUnlockRequirements` | `UnlockData unlock, ICollection<string> alreadyUnlocked` | `bool` | Checks if requirements are met for an unlock |
| `AnyUnlocksReady` | None | `bool` | Checks if any unlocks are ready in the town |

## Usage Examples

### Checking if Content is Unlocked

```csharp
// Check if a specific charm is unlocked
bool isUnlocked = MetaprogressionSystem.IsUnlocked("Charm 10");

// Get all unlocked pets
string[] unlockedPets = MetaprogressionSystem.GetUnlockedPets();
```

### Accessing Progression Data

```csharp
// Get win count from persistent save data
int winCount = MetaprogressionSystem.Get("winCount", 0);

// Update win count
MetaprogressionSystem.Set("winCount", winCount + 1);
```

### Managing Unlocks

```csharp
// Check for available unlocks
List<UnlockData> remainingUnlocks = MetaprogressionSystem.GetRemainingUnlocks();
List<string> lockedCharms = MetaprogressionSystem.GetLockedCharms(remainingUnlocks);

// Check if unlock requirements are met
List<string> unlockedList = MetaprogressionSystem.GetUnlockedList();
bool requirementsMet = MetaprogressionSystem.CheckUnlockRequirements(someUnlock, unlockedList);
```

## Integration with Other Systems

- **UnlockData**: Defines specific unlockable content in the game
- **SaveSystem**: Provides persistence for unlock and progression data
- **ClassData**: Character classes that can be unlocked through progression
- **UnlockReadyIcon**: UI component that shows when new unlocks are available
- **Town Buildings**: Various buildings can be unlocked as the player progresses

## Modding Considerations

When modifying or adding content that requires meta-progression:

1. **Adding Custom Unlocks**: Create new `UnlockData` scriptable objects and register them appropriately
2. **Adding Custom Content**: Add entries to the appropriate collections in the static data dictionary
3. **Requirements**: Set proper requirements for unlocks using the existing `UnlockData` system
4. **Save Compatibility**: Ensure custom unlocks work with the existing save system

To add a new pet with unlock requirements:
```csharp
// Inside your mod initialization
var petDict = MetaprogressionSystem.Get<Dictionary<string, string>>("pets");
petDict["MyCustomPet"] = "Pet CustomUnlock"; // Links to an UnlockData with name "Pet CustomUnlock"
MetaprogressionSystem.data["pets"] = petDict;
```
