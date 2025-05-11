## Examples

### Saving Traits
The `TraitStacks` class in `CardData` implements `ISaveable<TraitSaveData>`, allowing traits to be saved as follows:

```csharp
public TraitSaveData Save()
{
    return new TraitSaveData
    {
        name = data.name,
        count = count
    };
}
```

### Loading Traits
In `CardSaveData`, traits are loaded back into `CardData` using the `LoadList` method:

```csharp
cardData2.traits = traits.LoadList<CardData.TraitStacks, TraitSaveData>();
```

### Merging Traits
The `TraitStacks.Stack` method merges traits with the same data:

```csharp
public static void Stack(ref List<TraitStacks> traits, IEnumerable<TraitStacks> newTraits)
{
    foreach (TraitStacks newTrait in newTraits)
    {
        bool flag = false;
        foreach (TraitStacks trait in traits)
        {
            if (trait.data.Equals(newTrait.data))
            {
                trait.count += newTrait.count;
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            traits.Add(newTrait);
        }
    }
}
```