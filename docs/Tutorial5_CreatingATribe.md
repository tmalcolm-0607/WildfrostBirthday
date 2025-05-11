# Tutorial 5: Creating a Tribe

*Phan edited this page on Mar 9 · 14 revisions*
*By Michael Coopman*

---

## Contents
- [Overview: The Draw Tribe](#overview-the-draw-tribe)
- [Setup](#setup)
- [Helper Methods I](#helper-methods-i)
- [Creating a Tribe](#creating-a-tribe)
- [Adding Tribes to Tribe Selection](#adding-tribes-to-tribe-selection)
- [Character Prefab](#character-prefab)
- [Leaders](#leaders)
- [Helper Methods II](#helper-methods-ii)
- [Inspect Flags](#inspect-flags)
- [2nd Practice Test](#2nd-practice-test)
- [Creating Tribe-specific Cards and Charms](#creating-tribe-specific-cards-and-charms)
- [Starting Inventory](#starting-inventory)
- [Reward Pools](#reward-pools)
- [Extras: Tribe Hut Display](#extras-tribe-hut-display)
- [Conclusion](#conclusion)

---

## Overview: The Draw Tribe
Welcome welcome! This tutorial will teach you how to create a custom tribe in the game. Making a custom tribe is a large undertaking, typically involving ~12 companions, ~20 items/clunkers, ~6 charms. This tutorial will not go into detail about creating such objects, so it is recommended to have a good grasp on Tutorials 0-3. There are also some patching class used here so reading Tutorial 4 would be nice. In addition, we will assume you have publicized the assembly by now. There are some parts about cards and card upgrades that will be covered here as it pertains to tribes (i.e. leaders and reward pools). In general, the hardest part of creating a tribe is the card design and the status effects. So, the goal for this tutorial is to have a more comprehensive overview of ClassData and ClassDataBuilder. As always, the full code can be found here. Well, let's get started!

This tribe will be a mix of the "best parts" of the other three base tribes. There will be a focus on drawing cards.

---

## Setup
We will follow a setup similar to Tutorials 2 and 3. The Load method will call the CreateModAssets method for each unique asset type. This tutorial will only use three of them: CardData, CardUpgradeData, and ClassData. Assuming you have publicized your assembly, a better version of AddAssets is provided below.

```csharp
public override List<T> AddAssets<T, Y>()
{
    if (assets.OfType<T>().Any())
        Debug.LogWarning($"[{Title}] adding {typeof(Y).Name}s: {assets.OfType<T>().Select(a => a._data.name).Join()}");
    return assets.OfType<T>().ToList();
}
```

---

## Helper Methods I
The helper methods from Tutorial 2 will be used again (TryGet, TStack, SStack and StatusCopy). To keep this tutorial from taking ages to make, we will define a copy method for cards and tribes, shown below. TribeCopy is used to quickly get to a workable tribe, but by the end of this tutorial, we will replace almost all of the assets from our copied tribe.

```csharp
private CardDataBuilder CardCopy(string oldName, string newName) => DataCopy<CardData, CardDataBuilder>(oldName, newName);
private ClassDataBuilder TribeCopy(string oldName, string newName) => DataCopy<ClassData,ClassDataBuilder>(oldName, newName);

private T DataCopy<Y, T>(string oldName, string newName) where Y : DataFile where T : DataFileBuilder<Y, T>, new()
{
    Y data = Get<Y>(oldName).InstantiateKeepName();
    data.name = GUID + "." + newName;
    T builder = data.Edit<Y, T>();
    builder.Mod = this;
    return builder;
}
```

The next method is one used for unloading. Whenever a mod unloads, all DataFile instances are erased from the game; however, their spot in arrays and lists are never scrubbed. This leaves many null references in a couple places. For reward pools, we have UnloadFromClasses from prior tutorials. For more specific spots, we can use RemoveNulls to clean arrays whenever we unload. Use UnloadFromClasses to remove nulls from all the reward pools, and RemoveNulls for things like trait overrides and the tribe list for the standard game mode.

```csharp
internal T[] RemoveNulls<T>(T[] data) where T : DataFile
{
    List<T> list = data.ToList();
    list.RemoveAll(x => x == null || x.ModAdded == this);
    return list.ToArray();
}
```

Finally, a tribe involves making many lists of DataFile's with each one using TryGet multiple times. So, DataList is made to make the code look as clean and readable as possible.

```csharp
private T[] DataList<T>(params string[] names) where T : DataFile => names.Select((s) => TryGet<T>(s)).ToArray();
```

---

## Creating a Tribe
Since we have TribeCopy, we can make a barebones tribe very quickly. Something like this will give us a snowdweller tribe with a unique flag.

```csharp
private void CreateModAssets()
{
    assets.Add(TribeCopy("Basic", "Draw")
        .WithFlag("Images/DrawFlag.png")
        .WithSelectSfxEvent(FMODUnity.RuntimeManager.PathToEventReference("event:/sfx/card/draw_multi"))
    );
    preLoaded = true;
}
```

---

## Adding Tribes to Tribe Selection
Although the ClassDataBuilder builds our tribe and AddAssets loads it into the game, nothing in the game would bother finding it. This is why we need to manually add (and remove) our tribe to the tribe selection. In addition, create a static class field named instance that tracks of the instance of your mod (this will be useful for calling TryGet or finding the ModDirectory in other classes you make). The code will be a part of the Load and Unload methods, shown below.

```csharp
public static Tutorial5 instance;

public override void Load()
{
    instance = this;
    if (!preLoaded) { CreateModAssets(); }
    base.Load();
    GameMode gameMode = TryGet<GameMode>("GameModeNormal");
    gameMode.classes = gameMode.classes.Append(TryGet<ClassData>("Draw")).ToArray();
}

public override void Unload()
{
    base.Unload();
    GameMode gameMode = TryGet<GameMode>("GameModeNormal");
    gameMode.classes = RemoveNulls(gameMode.classes);
    UnloadFromClasses();
}
```

---

## Character Prefab
Now that we managed to get the tribe displayed in the game, the next order of business is distinguishing it from the other tribes. The first way is to change its character name. The CharacterPrefab is a template that holds all of the non-inventory data for the tribe. This ends up being References.Player when the game begins. There are things that could be changed here, such as hand size and companion limit, but we will settle on just changing the name. The characterPrefab also holds many fields that look important but are never read, so we will ignore them.

```csharp
assets.Add(TribeCopy("Basic", "Draw")
    .WithFlag("Images/DrawFlag.png")
    .WithSelectSfxEvent(FMODUnity.RuntimeManager.PathToEventReference("event:/sfx/card/draw_multi"))
    .SubscribeToAfterAllBuildEvent(
        (data) => {
            GameObject gameObject = data.characterPrefab.gameObject.InstantiateKeepName();
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            gameObject.name = "Player (Tutorial.Draw)";
            data.characterPrefab = gameObject.GetComponent<Character>();
            data.id = "Tutorial.Draw";
        })
);
```

---

## Leaders
Making leaders for a tribe works similarly with other cards: use CardDataBuilder and set the card type to "Leader". They do come with slight nuances:
- leaders come with slightly randomized stats (createScripts)
- leaders are assumed to have randomized appearances (ScriptableCardImages)

Helper methods for leader scripts:

```csharp
internal CardScript GiveUpgrade(string name = "Crown")
{
    CardScriptGiveUpgrade script = ScriptableObject.CreateInstance<CardScriptGiveUpgrade>();
    script.name = $"Give {name}";
    script.upgradeData = TryGet<CardUpgradeData>(name);
    return script;
}

internal CardScript AddRandomHealth(int min, int max)
{
    CardScriptAddRandomHealth health = ScriptableObject.CreateInstance<CardScriptAddRandomHealth>();
    health.name = "Random Health";
    health.healthRange = new Vector2Int(min,max);
    return health;
}

internal CardScript AddRandomDamage(int min, int max)
{
    CardScriptAddRandomDamage damage = ScriptableObject.CreateInstance<CardScriptAddRandomDamage>();
    damage.name = "Give Damage";
    damage.damageRange = new Vector2Int(min, max);
    return damage;
}

internal CardScript AddRandomCounter(int min, int max)
{
    CardScriptAddRandomCounter counter = ScriptableObject.CreateInstance<CardScriptAddRandomCounter>();
    counter.name = "Give Counter";
    counter.counterRange = new Vector2Int(min, max);
    return counter;
}
```

---

## Inspect Flags
To mark inspected cards with their flag, patch `References.Classes` to use the AddressableLoader list instead:

```csharp
[HarmonyPatch(typeof(References), nameof(References.Classes), MethodType.Getter)]
static class FixClassesGetter
{
    static void Postfix(ref ClassData[] __result) => __result = AddressableLoader.GetGroup<ClassData>("ClassData").ToArray();
}
```

---

## 2nd Practice Test
Set the draw tribe's leaders to the cards you just created. Example:

```csharp
data.leaders = DataList<CardData>("needleLeader", "muncherLeader", "Leader1_heal_on_kill");
```

---

## Creating Tribe-specific Cards and Charms
The procedure is very similar to that in Tutorials 2 and 3. The only change is that you should not put tribe-specific cards and charms in other pools.

---

## Starting Inventory
Each tribe has a unique starting deck. Use `SubscribeToAfterAllBuildEvent` and create an `Inventory` instance:

```csharp
Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
inventory.deck.list = DataList<CardData>("superMuncher", "SnowGlobe", "Sword", "Gearhammer", "Dart", "EnergyDart", "SunlightDrum", "Junkhead", "IceDice").ToList();
inventory.upgrades.Add(TryGet<CardUpgradeData>("CardUpgradeCritical"));
data.startingInventory = inventory;
```

---

## Reward Pools
Helper method:
```csharp
private RewardPool CreateRewardPool(string name, string type, DataFile[] list)
{
    RewardPool pool = ScriptableObject.CreateInstance<RewardPool>();
    pool.name = name;
    pool.type = type;
    pool.list = list.ToList();
    return pool;
}
```

Example:
```csharp
RewardPool unitPool = CreateRewardPool("DrawUnitPool", "Units", DataList<CardData>( 
    "NakedGnome", "GuardianGnome", "Havok",
    "Gearhead", "Bear", "TheBaker",
    "Pimento", "Pootie", "Tusk",
    "Ditto", "Flash", "TinyTyko"));

RewardPool itemPool = CreateRewardPool("DrawItemPool", "Items", DataList<CardData>(
    "ShellShield", "StormbearSpirit", "PepperFlag", "SporePack", "Woodhead",
    "BeepopMask", "Dittostone", "Putty", "Dart", "SharkTooth",
    "Bumblebee", "Badoo", "Juicepot", "PomDispenser", "LuminShard",
    "Wrenchy", "Vimifier", "OhNo", "Madness", "Joob"));

RewardPool charmPool = CreateRewardPool("DrawCharmPool", "Charms", DataList<CardUpgradeData>(
    "CardUpgradeSuperDraw", "CardUpgradeTrash",
    "CardUpgradeInk", "CardUpgradeOverload",
    "CardUpgradeMime", "CardUpgradeShellBecomesSpice",
    "CardUpgradeAimless"));

data.rewardPools = new RewardPool[]
{
    unitPool,
    itemPool,
    charmPool,
    Extensions.GetRewardPool("GeneralUnitPool"),
    Extensions.GetRewardPool("GeneralItemPool"),
    Extensions.GetRewardPool("GeneralCharmPool"),
    Extensions.GetRewardPool("GeneralModifierPool"),
    Extensions.GetRewardPool("SnowUnitPool"),
    Extensions.GetRewardPool("SnowItemPool"),
    Extensions.GetRewardPool("SnowCharmPool"),
};
```

---

## Extras: Tribe Hut Display
### Adding Flags to the Tribe Hut Interior
Patch `TribeHutSequence.SetupFlags` to add your tribe flag:

```csharp
[HarmonyPatch(typeof(TribeHutSequence), "SetupFlags")]
class PatchTribeHut
{
    static string TribeName = "Draw";
    static void Postfix(TribeHutSequence __instance)
    {
        GameObject gameObject = GameObject.Instantiate(__instance.flags[0].gameObject);
        gameObject.transform.SetParent(__instance.flags[0].gameObject.transform.parent, false);
        TribeFlagDisplay flagDisplay = gameObject.GetComponent<TribeFlagDisplay>();
        ClassData tribe = Tutorial5.instance.TryGet<ClassData>(TribeName);
        flagDisplay.flagSprite = tribe.flag;
        __instance.flags = __instance.flags.Append(flagDisplay).ToArray();
        flagDisplay.SetAvailable();
        flagDisplay.SetUnlocked();
        // ... see full tutorial for display setup
    }
}
```

### Creating a Tribe Hut Display
Use the Unity Explorer to find and clone display objects, then set up the display as needed. Use localized strings for title and description.

---

## Conclusion
We have gone through a detailed outline of how to make a tribe. Though the process of making a tribe is long, everything should seem familiar by now, with the exception of the last two code blocks. The most important thing is that you feel that modding is a fun/rewarding experience!

---

[Back to Modding Tutorials](index.md)
