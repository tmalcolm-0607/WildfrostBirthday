# JournalPageUnlockSystem

## Overview

The `JournalPageUnlockSystem` manages the unlocking and insertion of journal pages into the game's campaign map. Journal pages are special nodes on the campaign map that provide lore, story elements, or gameplay tips to the player. This system handles when and where these journal pages appear based on various conditions, such as player progression, current game mode, and active modifiers.

## Class Details

`JournalPageUnlockSystem` inherits from `GameSystem` and manages the placement of journal pages on the campaign map.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `pages` | `JournalPageData[]` | Array of journal page data objects that define the available journal pages |

### Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `OnEnable()` | `void` | Subscribes to the `OnCampaignLoadPreset` event |
| `OnDisable()` | `void` | Unsubscribes from the `OnCampaignLoadPreset` event |
| `InsertJournalPages(ref string[] lines)` | `void` | Core method that processes campaign map data and inserts journal page nodes where appropriate |
| `IsLegal(JournalPageData page)` | `static bool` | Checks if a journal page can be unlocked based on game mode, tribe, and other requirements |
| `HasRequiredStormPoints(JournalPageData page)` | `static bool` | Checks if the player has enough storm points to unlock a page |
| `HasRequiredModifiers(JournalPageData page)` | `static bool` | Checks if the required game modifiers are active for a page |
| `CreateNode(float x, float y, string type, int positionIndex)` | `static CampaignGenerator.Node` | Creates a campaign node for a journal page |

## Related Classes

### JournalPageData

A ScriptableObject that defines the properties of a journal page.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `unlock` | `UnlockData` | The unlock data associated with this journal page |
| `unlockOnMap` | `bool` | If true, this page can appear on the campaign map |
| `mapTierIndex` | `char` | The tier/row on the map where this page should appear |
| `mapAfterLetter` | `char` | The map node type after which this page should be placed |
| `mapNodeType` | `CampaignNodeType` | The type of map node this journal page represents |
| `prefabRef` | `AssetReferenceGameObject` | Reference to the prefab for this journal page |
| `legalGameModes` | `GameMode[]` | Game modes where this page can appear |
| `legalTribes` | `ClassData[]` | Character tribes/classes for which this page can appear |
| `requiresModifiers` | `GameModifierData[]` | Game modifiers required for this page to appear |
| `requiresStormPoints` | `int` | Minimum storm points required to unlock this page |

### JournalPage

MonoBehaviour that represents a journal page in the game UI.

#### Key Methods

| Method | Return Type | Description |
|--------|-------------|-------------|
| `Open()` | `void` | Opens this journal page in the UI |
| `Close()` | `void` | Closes this journal page in the UI |

## Usage Examples

### Adding Journal Pages to a Campaign

```csharp
// In a campaign generation script
string[] campaignLines = new string[4] { 
    "BEMBE",  // Node types
    " B B ",  // Additional node types
    "11222",  // Tier indices
    "00000"   // Biome indices
};

// Find the JournalPageUnlockSystem
JournalPageUnlockSystem journalSystem = FindObjectOfType<JournalPageUnlockSystem>();

// Insert journal pages into the campaign lines
journalSystem.InsertJournalPages(ref campaignLines);

// campaignLines might be modified to insert journal page nodes:
// e.g., "JEMBE" where 'J' represents a journal page node
```

### Creating a Custom Journal Page

```csharp
// Creating a new journal page data object
JournalPageData newPage = ScriptableObject.CreateInstance<JournalPageData>();
newPage.unlock = myUnlockData;
newPage.unlockOnMap = true;
newPage.mapTierIndex = '2'; // Place on tier 2
newPage.mapAfterLetter = 'B'; // Place after battle nodes
newPage.mapNodeType = campaignNodeTypes.First(t => t.letter == 'J'); // Journal node type
newPage.legalGameModes = new GameMode[] { GameMode.Normal };
newPage.legalTribes = new ClassData[] { myTribeData };
newPage.requiresStormPoints = 3;

// Add it to the journal page system
JournalPageUnlockSystem journalSystem = FindObjectOfType<JournalPageUnlockSystem>();
List<JournalPageData> pagesList = new List<JournalPageData>(journalSystem.pages);
pagesList.Add(newPage);
journalSystem.pages = pagesList.ToArray();
```

## Integration with Other Systems

The JournalPageUnlockSystem integrates with several game systems:

1. **Campaign Generation**: Modifies campaign map data to insert journal nodes at appropriate positions.

2. **Metaprogression System**: Checks unlock status via `MetaprogressionSystem.GetUnlockedList()`.

3. **Storm Bell System**: Uses `StormBellManager` to check if the player has enough storm points.

4. **Game Mode System**: Filters pages based on current game mode.

5. **Tribe/Class System**: Only shows pages relevant to the player's current tribe/class.

6. **Campaign Modifier System**: Can require specific game modifiers to be active.

## Modding Considerations

### Creating Custom Journal Pages

To create custom journal pages:

1. Create a new `JournalPageData` ScriptableObject.
2. Configure its unlock conditions, position, and requirements.
3. Create a prefab for the journal page content.
4. Add the page to the `pages` array in `JournalPageUnlockSystem`.

Example:

```csharp
// Create a custom journal page in a mod
public void AddCustomJournalPage()
{
    // Create data
    JournalPageData lorePageData = ScriptableObject.CreateInstance<JournalPageData>();
    lorePageData.name = "MyCustomLorePage";
    lorePageData.unlock = CreateUnlockData("custom_lore_unlock");
    lorePageData.unlockOnMap = true;
    lorePageData.mapTierIndex = '2';
    lorePageData.mapAfterLetter = 'E'; // After event nodes
    lorePageData.mapNodeType = FindNodeType('J');
    
    // Set requirements
    lorePageData.legalGameModes = new GameMode[] { GameMode.Normal, GameMode.Hard };
    lorePageData.legalTribes = new ClassData[] { /* your tribe data */ };
    
    // Get reference to system and add the page
    JournalPageUnlockSystem system = FindObjectOfType<JournalPageUnlockSystem>();
    AddPageToSystem(system, lorePageData);
}
```

### Changing Journal Page Appearance Logic

To modify how journal pages appear on the map:

1. Create a subclass of `JournalPageUnlockSystem`.
2. Override the `InsertJournalPages` method to implement custom placement logic.
3. Replace the default system with your custom implementation.

Example:

```csharp
public class EnhancedJournalPageSystem : JournalPageUnlockSystem
{
    public int maxPagesPerCampaign = 2;
    
    public new void InsertJournalPages(ref string[] lines)
    {
        // Get available pages
        List<JournalPageData> availablePages = new List<JournalPageData>();
        foreach (JournalPageData page in pages)
        {
            if (IsLegal(page) && page.unlockOnMap)
            {
                availablePages.Add(page);
            }
        }
        
        // Limit to max pages per campaign and randomly select
        if (availablePages.Count > maxPagesPerCampaign)
        {
            availablePages = availablePages
                .OrderBy(x => Random.value)
                .Take(maxPagesPerCampaign)
                .ToList();
        }
        
        // Insert selected pages using custom positioning logic
        foreach (JournalPageData page in availablePages)
        {
            // Custom insertion logic here
            // ...
        }
    }
}
```

## Common Integration Points

1. **Journal UI**: Journal pages are typically displayed in the game's journal UI, accessed from the game menu.

2. **Unlocks System**: Journal pages can be tied to the game's unlock progression system.

3. **Map Generation**: Journal pages are inserted into the campaign map during generation.

4. **Storytelling System**: Journal pages often contain lore or narratives that advance the game's story.

## Best Practices

1. **Balance Distribution**: Avoid placing too many journal pages in a single campaign to prevent overwhelming the player.

2. **Contextual Relevance**: Create journal pages that are relevant to the player's current situation or progression.

3. **Rewarding Discovery**: Use journal pages to reward exploration or achievement of specific goals.

4. **Progressive Reveals**: Structure journal pages to gradually reveal more complex lore or game mechanics.
