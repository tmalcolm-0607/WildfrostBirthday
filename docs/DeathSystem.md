# DeathSystem

`DeathSystem` is a core system in Wildfrost that handles entity death events, visual effects, and team-based death interactions.

## Class Overview

`DeathSystem` inherits from `GameSystem` and is responsible for:
- Handling different types of entity deaths (destroy, sacrifice, consume)
- Managing death animations and visual effects
- Tracking team-based death interactions
- Handling special cases where entities should be treated as belonging to a different team

## Properties

| Property | Type | Description |
|----------|------|-------------|
| `treatAsTeam` | `Dictionary<ulong, int>` | Static dictionary mapping card IDs to team values for special team handling |

## Event Handlers

| Method | Event | Description |
|--------|-------|-------------|
| `EntityKilled` | `Events.OnEntityKilled` | Called when an entity is killed, determines death type and triggers corresponding death effect |
| `EntityCreated` | `Events.OnEntityCreated` | Called when an entity is created, resets any special team handling for the entity |

## Death Type Handlers

| Method | Parameters | Description |
|--------|------------|-------------|
| `Destroy` | `Card card` | Handles standard destruction of a card, applying knockback and visual effects |
| `Sacrifice` | `Card card` | Handles sacrificial death of a card, applying special sacrifice visuals |
| `Consume` | `Card card` | Handles consumption of a card, applying special consume visuals |

## Team Interaction Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `KilledByOwnTeam` | `Entity entity` | `bool` | Checks if an entity was killed by another entity on the same team |
| `TreatAsTeam` | `ulong cardDataId, int team` | `void` | Sets a card to be treated as belonging to a specific team |
| `CheckTeamIsAlly` | `Entity entity, Entity checkAgainst` | `bool` | Checks if two entities are allies, respecting special team overrides |

## Death Types

The system handles three distinct death types, each with different visual effects and gameplay implications:

| Death Type | Class | Visual Effect | Gameplay Impact |
|------------|-------|---------------|----------------|
| `DeathType.Normal` | `CardDestroyed` | Knockback animation | Standard death, may trigger on-death effects |
| `DeathType.Sacrifice` | `CardDestroyedSacrifice` | Sacrifice animation | Triggers sacrifice-specific effects |
| `DeathType.Consume` | `CardDestroyedConsume` | Consume animation | Triggers consume-specific effects |

## Usage Examples

### Handling Entity Death

When an entity dies, the system determines how it should be visually handled:

```csharp
public void EntityKilled(Entity entity, DeathType deathType)
{
    if ((bool)entity && entity.display is Card card && card.GetComponent<ICardDestroyed>() == null)
    {
        switch (deathType)
        {
        default:
            Destroy(card);
            break;
        case DeathType.Sacrifice:
            Sacrifice(card);
            break;
        case DeathType.Consume:
            Consume(card);
            break;
        }
    }
}
```

### Checking Team Interactions

When determining if entities are allies:

```csharp
// Inside game logic
bool areAllies = DeathSystem.CheckTeamIsAlly(entity1, entity2);

// Special cases where entities need to behave as if on a different team
DeathSystem.TreatAsTeam(myCardId, enemyTeamId);
```

## Integration with Other Systems

- **Entity**: The core entity class that represents cards and other game objects
- **Card**: The visual component that represents an entity on the battlefield
- **Events.OnEntityKilled**: Event triggered when an entity is killed
- **DeathType**: Enum defining how an entity died (Normal, Sacrifice, Consume)
- **CardDestroyed/CardDestroyedSacrifice/CardDestroyedConsume**: Component classes that handle death animations

## Modding Considerations

When modifying or extending death behavior:

1. **Custom Death Types**: Create new death types and corresponding handler methods
2. **Custom Death Animations**: Create new classes implementing `ICardDestroyed` interface
3. **Team Handling**: Use `TreatAsTeam` to create special team interactions
4. **On-Death Effects**: Connect to the `Events.OnEntityKilled` event to add custom behavior

Example of implementing a custom death handler:

```csharp
// Custom death animation component
public class CardDestroyedExplosive : MonoBehaviour, ICardDestroyed
{
    public void Start()
    {
        // Create explosion effect
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        // Damage nearby entities
        List<Entity> nearbyEntities = FindNearbyEntities();
        foreach (Entity entity in nearbyEntities)
        {
            entity.Damage(3);
        }
        
        // Start fade-out animation
        StartCoroutine(FadeOutAndDestroy());
    }
}

// To use the custom death effect
public void ExplosiveDeath(Card card)
{
    card.gameObject.AddComponent<CardDestroyedExplosive>();
    card.transform.parent = transform;
}
```
