# CardScript Documentation

## Overview
`CardScript` is an abstract base class used to define custom behaviors for cards in Wildfrost. Derived classes implement specific actions, such as modifying stats, copying effects, or destroying cards.

## Key Methods

### Run
Executes the script's behavior on a target card.
```csharp
public abstract void Run(CardData target);
```

## Derived Classes

### CardScriptCopyEffectsFromOtherCardInDeck
Copies effects from another card in the deck to the target card.
- **File**: `CardScriptCopyEffectsFromOtherCardInDeck.cs`
- **Menu Name**: "Card Scripts/Copy Effects From Other Card In Deck"

### CardScriptDestroyCard
Removes the target card from the deck and destroys its entities.
- **File**: `CardScriptDestroyCard.cs`
- **Menu Name**: "Card Scripts/Destroy Card"

### CardScriptCopyPreviousCharm
Copies the effects of the last applied charm to the target card.
- **File**: `CardScriptCopyPreviousCharm.cs`
- **Menu Name**: "Card Scripts/Copy Previous Charm"

### ScriptChangeCardStats
Applies a random set of stat changes to the target card.
- **File**: `ScriptChangeCardStats.cs`
- **Menu Name**: "Scripts/Change Card Stats"

### ScriptRunScriptsOnDeck
Runs a set of scripts on cards in the player's deck.
- **File**: `ScriptRunScriptsOnDeck.cs`
- **Menu Name**: "Scripts/Run Scripts On Deck"

## Example Usage

Here is an example of using a `CardScript` to modify a card:

```csharp
CardData card = AddressableLoader.Get<CardData>("CardData", "ExampleCard");
CardScript script = AddressableLoader.Get<CardScript>("CardScript", "ModifyCardScript");
script.Run(card);
```

## Notes
- `CardScript` is a flexible system for defining card behaviors.
- Derived classes should implement the `Run` method to define specific actions.
- Scripts can be combined with other systems, such as `TargetConstraint`, for more complex logic.
