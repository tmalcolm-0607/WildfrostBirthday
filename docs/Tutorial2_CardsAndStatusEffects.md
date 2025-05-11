# Tutorial 2: Making Cards and Status Effects

*Phan edited this page 3 weeks ago · 28 revisions*
*By Michael Coopman*

---

## Overview
Hello modders! In this tutorial, we will finally make a card with effects. Actually, we will make two: a Companion that summons a card on deploy, and a second card that triggers whenever its summoner attacks. We'll also cover how to create and assign custom status effects, and how to use builder patterns for cards and effects.

---

## Initial Setup
- Start with a clean project (or the result of Tutorial 1).
- In your mod class constructor, set up a static instance:

```csharp
public Tutorial2(string modDirectory) : base(modDirectory)
{
    Instance = this;
}
public static Tutorial2 Instance;
public static List<object> assets = new List<object>();
private bool preLoaded = false;

private void CreateModAssets()
{
    // Add status effects
    // Add cards
    preLoaded = true;
}
protected override void Load()
{
    if(!preLoaded) { CreateModAssets(); }
    base.Load();
}
protected override void Unload()
{
    base.Unload();
}
```

- Implement `AddAssets<T, Y>()` to return the correct asset builders:

```csharp
public override List<T> AddAssets<T, Y>()
{
    var tAssets = assets.OfType<T>();
    if (tAssets.Any())
        Debug.LogWarning($"[{Title}] adding {typeof(Y).Name}s: {tAssets.Count()}");
    return tAssets.ToList();
}
```

---

## CardData and CardDataBuilder

Add two cards:

```csharp
// Card 0: Shade Serpent
assets.Add(
    new CardDataBuilder(this).CreateUnit("shadeSerpent", "Shade Serpent")
    .SetSprites("ShadeSerpent.png", "ShadeSerpent BG.png")
    .SetStats(8,1,3)
    .WithCardType("Friendly")
    .WithFlavour("I don't have an ability yet :/")
    .WithValue(50)
    .AddPool("MagicUnitPool")
);

// Card 1: Shade Snake
assets.Add(
    new CardDataBuilder(this).CreateUnit("shadeSnake", "Shade Snake")
    .SetSprites("ShadeSnake.png", "ShadeSnake BG.png")
    .SetStats(4, 3, 0)
    .WithCardType("Summoned")
    .WithValue(50)
    .WithFlavour("Hissssssssss")
);
```

- Place your card images in an `Images` subfolder next to your .dll.
- Build and test: Shade Serpent should appear in the journal, Shade Snake may not.

---

## StatusEffectData and StatusEffectDataBuilder

### Helper Methods
Add these helpers to your mod class:

```csharp
public T TryGet<T>(string name) where T : DataFile
{
    T data;
    if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
        data = base.Get<StatusEffectData>(name) as T;
    else if (typeof(KeywordData).IsAssignableFrom(typeof(T)))
        data = (AddressableLoader.Get<KeywordData>("KeywordData", Extensions.PrefixGUID(name, this).ToLower()) 
        ?? base.Get<KeywordData>(name.ToLower())) as T;
    else
        data = base.Get<T>(name);
    if (data == null)
        throw new Exception($"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{name}] or [{Extensions.PrefixGUID(name, this)}]");
    return data;
}
public CardData.StatusEffectStacks SStack(string name, int amount) => new CardData.StatusEffectStacks(TryGet<StatusEffectData>(name), amount);
public StatusEffectDataBuilder StatusCopy(string oldName, string newName)
{
    StatusEffectData data = TryGet<StatusEffectData>(oldName).InstantiateKeepName();
    data.name = GUID + "." + newName;
    data.targetConstraints = new TargetConstraint[0];
    StatusEffectDataBuilder builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>();
    builder.Mod = this;
    return builder;
}
```

---

### Adding and Creating Status Effects

#### Copying and Modifying Existing Effects

- Copy and modify "Summon Fallow" to create "Summon Shade Snake":

```csharp
assets.Add(
    StatusCopy("Summon Fallow", "Summon Shade Snake")
    .SubscribeToAfterAllBuildEvent<StatusEffectSummon>(data =>
    {
        data.summonCard = TryGet<CardData>("shadeSnake");
    })
);
```

- Copy and modify "Instant Summon Fallow":

```csharp
assets.Add(
    StatusCopy("Instant Summon Fallow", "Instant Summon Shade Snake")
    .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummon>(data =>
    {
        data.targetSummon = TryGet<StatusEffectSummon>("Summon Shade Snake");
    })
);
```

- Copy and modify "When Deployed Summon Wowee":

```csharp
assets.Add(
    StatusCopy("When Deployed Summon Wowee", "When Deployed Summon Shade Snake")
    .WithText("When deployed, summon {0}")
    .WithTextInsert("<card=mhcdc9.wildfrost.tutorial.shadeSnake>")
    .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(data =>
    {
        data.effectToApply = TryGet<StatusEffectData>("Instant Summon Shade Snake");
    })
);
```

- Update Shade Serpent to use the new effect:

```csharp
new CardDataBuilder(this).CreateUnit("shadeSerpent", "Shade Serpent")
.SetSprites("ShadeSerpent.png", "ShadeSerpent BG.png")
.SetStats(8,1,3)
.WithCardType("Friendly")
.WithValue(50)
.AddPool("MagicUnitPool")
.SubscribeToAfterAllBuildEvent(data =>
{
    data.startWithEffects = new CardData.StatusEffectStacks[]
    {
        SStack("When Deployed Summon Shade Snake", 1)
    };
})
```

---

### Creating a New StatusEffect Class

If you need a custom trigger (e.g., "Trigger When Shade Serpent In Row Attacks"):

```csharp
public class StatusEffectTriggerWhenCertainAllyAttacks : StatusEffectTriggerWhenAllyAttacks
{
    public CardData ally;
    public override bool RunHitEvent(Hit hit)
    {
        Debug.Log($"[Tutorial] {hit.attacker?.name}");
        if (hit.attacker?.name == ally.name)
        {
            return base.RunHitEvent(hit);
        }
        return false;
    }
}
```

- Add the effect:

```csharp
assets.Add(
    new StatusEffectDataBuilder(this)
    .Create<StatusEffectTriggerWhenCertainAllyAttacks>("Trigger When Shade Serpent In Row Attacks")
    .WithCanBeBoosted(false)
    .WithStackable(false)
    .WithText("Trigger when {0} in row attacks")
    .WithTextInsert("<card=mhcdc9.wildfrost.tutorial.shadeSerpent>")
    .WithType("")
    .FreeModify(data =>
        {
           data.isReaction = true;
           data.descColorHex = "F99C61";
           data.affectedBySnow = true;
        })
    .SubscribeToAfterAllBuildEvent<StatusEffectTriggerWhenCertainAllyAttacks>(
        data =>
        {
            data.ally = TryGet<CardData>("shadeSerpent");
        })
);
```

- Update Shade Snake to use the new effect:

```csharp
new CardDataBuilder(this).CreateUnit("shadeSnake", "Shade Snake")
.SetSprites("ShadeSnake.png", "ShadeSnake BG.png")
.SetStats(4, 3, 0)
.WithCardType("Summoned")
.WithValue(50)
.WithFlavour("Hissssssssss")
.SubscribeToAfterAllBuildEvent(data =>
{
    data.startWithEffects = new CardData.StatusEffectStacks[]
    {
        SStack("Trigger When Shade Serpent In Row Attacks",1)
    };
})
```

---

## Unloading

To clean up reward pools on unload:

```csharp
public void UnloadFromClasses()
{
    List<ClassData> tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
    foreach(ClassData tribe in tribes)
    {
        if (tribe == null || tribe.rewardPools == null) { continue; }
        foreach(RewardPool pool in tribe.rewardPools)
        {
            if (pool == null) { continue; }
            pool.list.RemoveAllWhere((item) => item == null || item.ModAdded == this);
        }
    }
}
```

---

## Debugging

- Most errors are null references or typos. Double-check all names and variables.
- Use Unity Explorer and logs to inspect data at runtime.

---

## Next Steps
- Try extending these cards or effects further.
- Experiment with more advanced status effect logic.
- See the [Modding Tools & Techniques](ModdingToolsAndTechniques.md) guide for debugging tips.
- Return to the [index](index.md) for more tutorials and references.
