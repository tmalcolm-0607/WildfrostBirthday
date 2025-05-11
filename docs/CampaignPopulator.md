# CampaignPopulator

`CampaignPopulator` is a ScriptableObject responsible for populating a campaign with characters, battles, rewards, and other interactive elements in Wildfrost. This component connects the procedurally generated campaign map structure with actual gameplay content.

## Class Properties

| Property | Type | Description |
|----------|------|-------------|
| `removeLockedCards` | `bool` | Flag determining whether locked cards should be removed from the player's deck. |
| `tiers` | `CampaignTier[]` | Array of campaign tier definitions used for generating content based on map position. |
| `playerStartNodeId` | `int` | ID of the node where the player starts on the campaign map. |
| `battleLockers` | `BattleLockData[]` | Array of battle lockers that determine which battles are currently locked. |

## Inner Classes

### Tier

A helper class that manages nodes, battles, and rewards for a specific tier of the campaign.

| Property | Type | Description |
|----------|------|-------------|
| `number` | `int` | The tier number. |
| `nodes` | `List<CampaignNode>` | List of campaign nodes belonging to this tier. |
| `battles` | `List<BattleData>` | Pool of available battles for this tier. |
| `rewards` | `List<CampaignNodeType>` | Pool of available rewards for this tier. |
| `campaignTier` | `CampaignTier` | Reference to the campaign tier data asset. |

#### Tier Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `PullBattle(CampaignPopulator campaignPopulator)` | `campaignPopulator`: Reference to the CampaignPopulator instance | Selects and removes a random battle from the available battles pool. Repopulates the pool if empty. |
| `GetBattlePoints()` | None | Generates a random number of battle points within the tier's range to determine battle difficulty. |
| `PullReward()` | None | Selects and removes a random reward from the available rewards pool. Repopulates the pool if empty. |

## Main Methods

### Campaign Setup

| Method | Parameters | Description |
|--------|------------|-------------|
| `LoadCharacters(Campaign campaign, CharacterSaveData[] data)` | `campaign`: Campaign to populate<br>`data`: Character save data array | Loads characters from save data and adds them to the campaign. |
| `Populate(Campaign campaign)` | `campaign`: Campaign to populate | Main coroutine that populates the entire campaign with content. |

### Helper Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `BattleIsLocked(string battleName)` | `battleName`: Name of the battle to check | Checks if a specific battle is currently locked. |
| `LinkNodes(List<Tier> currentTiers)` | `currentTiers`: List of campaign tiers | Static method that creates connections between similar nodes to ensure consistent rewards/battles. |

## Populate Process

The `Populate` coroutine follows these main steps:

1. **Character Setup**:
   - Creates the player character based on the selected class
   - Assigns the player to the starting node
   - Sets up character rewards and deck

2. **Map Tier Assignment**:
   - Reads the campaign's battle tier structure
   - Groups nodes by tier
   - Creates Tier objects to manage content for each tier

3. **Content Population**:
   - For battle nodes: assigns battles and generates waves
   - For reward nodes: assigns reward types
   - Handles linking of similar nodes

4. **Node Setup**:
   - Calls each node type's setup method
   - Copies data between linked nodes

## Usage Example

CampaignPopulator is typically used during campaign creation and loading:

```csharp
// During campaign creation
IEnumerator CreateCampaign()
{
    // First generate the map structure with CampaignGenerator
    yield return gameMode.generator.Generate(campaign);
    
    // Then populate the map with content
    yield return gameMode.populator.Populate(campaign);
}

// When loading saved characters
void LoadSavedCampaign()
{
    CampaignSaveData saveData = SaveSystem.LoadCampaignData<CampaignSaveData>(gameMode, "data");
    gameMode.populator.LoadCharacters(campaign, saveData.characters);
    // ...further initialization...
}
```

## Node Linking System

The node linking system is an important feature that ensures consistency in the campaign:

- Nodes with the same type (e.g., same reward type) can be linked across different paths
- When a player visits one node, its linked counterpart gets updated accordingly
- This prevents getting duplicate rewards from parallel paths
- Links are created based on node type and path ID

## Integration with Other Systems

- **CampaignGenerator**: Creates the structural map before the populator adds content
- **Campaign**: Holds the populated content and manages the game flow
- **BattleData**: Defines battles that are assigned to battle nodes
- **CampaignNodeType**: Defines different types of nodes (battles, rewards, etc.)
- **CharacterRewards**: Manages player rewards based on class and progression

## Modding Considerations

When modifying or extending the Campaign Populator:

1. **Adding New Content**: Create new `BattleData` scriptable objects and add them to the tier's battle pools
2. **Custom Node Types**: Create new `CampaignNodeType` scriptable objects for custom interactions
3. **Tier Balancing**: Adjust the `CampaignTier` scriptable objects to change difficulty progression
4. **Battle Locking**: Create custom `BattleLockData` scriptable objects to lock content behind progression gates

Custom implementations can:
- Create new node types by extending `CampaignNodeType`
- Create new battle generation scripts by extending `BattleGenerationScript`
- Override the `Populate` method to implement custom population logic
