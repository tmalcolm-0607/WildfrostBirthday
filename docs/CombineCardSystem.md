# CombineCardSystem

## Overview
The `CombineCardSystem` is a game system in Wildfrost that manages the combination of specific cards into new, more powerful cards. This system monitors when cards enter the player's backpack and checks if the player's deck contains all cards required for a predefined combination. When a valid combination is detected, the system triggers a visually impressive sequence where the component cards are merged into a new, typically more powerful card.

## Class Details

### Structures

#### Combo
A structure that defines a valid card combination recipe.

| Property | Type | Description |
|----------|------|-------------|
| `cardNames` | string[] | Array of card names required for the combination |
| `resultingCardName` | string | The name of the card created by the combination |

**Methods**:
- `AllCardsInDeck(CardDataList deck)`: Checks if all required cards are in the player's deck
- `HasCard(string cardName, CardDataList deck)`: Checks if a specific card is in the player's deck

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `combineSceneName` | string | Name of the scene that contains the card combine sequence |
| `combos` | Combo[] | Array of all possible card combinations |

### Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `OnEnable` | None | void | Subscribes to the entity entering backpack event |
| `OnDisable` | None | void | Unsubscribes from the entity entering backpack event |
| `EntityEnterBackpack` | Entity entity | void | Checks if the entity entering the backpack triggers a combination |
| `CombineSequence` | Combo combo | IEnumerator | Coroutine that loads the combination scene and runs the combination sequence |

## CombineCardSequence

The `CombineCardSequence` class manages the visual effects and actual card transformation during a combination.

### Key Properties

| Property | Type | Description |
|----------|------|-------------|
| `fader` | Fader | Controls screen fading during the sequence |
| `flash` | Graphic | Provides flash effect during combination |
| `group` | Transform | Parent transform for card positioning during combination |
| `hitPs` | ParticleSystem | Particle effects played when cards collide |
| `combinedFx` | GameObject | Special effects shown when the combined card appears |
| `finalEntityParent` | Transform | Parent transform for the final combined card |

### Key Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `Run` | string[] cardsToCombine, string resultingCard | IEnumerator | Coroutine that runs the combination sequence from card names |
| `Run` | CardData[] cards, CardData finalCard | IEnumerator | Coroutine that runs the combination sequence from card data |
| `CreateEntities` | CardData[] cardDatas | Entity[] | Creates entity representations of the cards to be combined |
| `CreateFinalEntity` | CardData cardData | Entity | Creates the final combined card entity |

## Usage Examples

### Defining Card Combinations
```csharp
// In a ScriptableObject or initialization code
public void InitializeCombinations()
{
    // Create a combo that combines two common cards into a rare one
    Combo fishCombo = new Combo 
    {
        cardNames = new string[] { "Fish", "FishingRod" },
        resultingCardName = "MasterAngler"
    };
    
    // Create a combo that combines three elements into a powerful spell
    Combo elementalCombo = new Combo 
    {
        cardNames = new string[] { "FireElement", "IceElement", "WindElement" },
        resultingCardName = "ElementalStorm"
    };
    
    // Add combos to the system
    combos = new Combo[] { fishCombo, elementalCombo };
}
```

### Manually Triggering a Combination
```csharp
public void TryCardCombination()
{
    CombineCardSystem combineSystem = FindObjectOfType<CombineCardSystem>();
    
    // Find a valid combo
    Combo validCombo = null;
    foreach (var combo in combineSystem.combos)
    {
        if (combo.AllCardsInDeck(References.PlayerData.inventory.deck))
        {
            validCombo = combo;
            break;
        }
    }
    
    // If found, start the combination sequence
    if (validCombo != null)
    {
        StartCoroutine(combineSystem.CombineSequence(validCombo));
    }
}
```

### Creating a Mod with Custom Card Combinations
```csharp
public class CustomCombinationsSystem : GameSystem
{
    private CombineCardSystem combineCardSystem;
    
    public void OnEnable()
    {
        // Find the existing combine system
        combineCardSystem = FindObjectOfType<CombineCardSystem>();
        
        // Add our custom combinations
        if (combineCardSystem != null)
        {
            List<CombineCardSystem.Combo> existingCombos = 
                new List<CombineCardSystem.Combo>(combineCardSystem.combos);
                
            // Add new combo
            CombineCardSystem.Combo newCombo = new CombineCardSystem.Combo
            {
                cardNames = new string[] { "MyCustomCard", "AnotherCustomCard" },
                resultingCardName = "PowerfulCustomCard"
            };
            
            existingCombos.Add(newCombo);
            combineCardSystem.combos = existingCombos.ToArray();
        }
    }
}
```

## Integration with Other Systems

### Entity and Card Systems
The CombineCardSystem interacts directly with entities and cards:
- Monitors when cards enter the backpack (via the `Events.OnEntityEnterBackpack` event)
- Manipulates the player's deck by removing component cards and adding the resulting card
- Uses the `CardManager` to create card entities for the combination sequence

### Scene Management
The system uses Unity's `SceneManager` to:
- Load a temporary scene for the combination sequence
- Find the `CombineCardSequence` component in that scene
- Unload the scene after the combination is complete

### Visual Effects
The combination sequence includes numerous visual effects:
- Card animation and movement
- Screen flashes and fades
- Particle systems
- Camera shake
- Cinema bars with text

### Input System
The sequence waits for player input to continue after showing the combined card.

## Modding Considerations

### Adding New Combinations
To add new card combinations:
1. Access the existing `CombineCardSystem` instance (typically found in the campaign object)
2. Add new `Combo` structures to the `combos` array
3. Ensure all card names are valid and the resulting card exists

### Creating Custom Combination Visuals
To customize the combination visuals:
1. Create a new scene based on the existing combine scene
2. Modify the `CombineCardSequence` components and effects
3. Update the `combineSceneName` in the `CombineCardSystem`

### Triggering Combinations Differently
By default, combinations trigger when cards enter the backpack, but you can create alternative triggers:
1. Create a new system that inherits from `GameSystem`
2. Access the `CombineCardSystem` instance
3. Call `CombineSequence()` with your chosen combo when your custom condition is met

### Testing Combinations
To test combinations during development:
1. Ensure all required cards are in the player's deck
2. Trigger the `EntityEnterBackpack` event for one of the component cards
3. Alternatively, manually call the `CombineSequence` method with your test combo
