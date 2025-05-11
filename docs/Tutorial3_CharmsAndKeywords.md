# Tutorial 3: Making Charms and Keywords

*Michael edited this page on Feb 13 · 13 revisions*
*By Michael Coopman*

---

## Contents
- [Overview](#overview)
- [Setup](#setup)
- [Helper Methods](#helper-methods)
- [Keywords](#keywords)
- [Card Upgrades (Charms)](#card-upgrades-charms)
- [Status Effects](#status-effects)
- [Extras: Card Scripts](#extras-card-scripts)
- [Unloading](#unloading)
- [Final Remarks](#final-remarks)

---

## Overview
Welcome back modders! The goal of this tutorial is to create a charm and keyword using their respective builders. Like with the previous tutorial, this boils down to debugging a new status effect. We will make the glacial charm: any time the charmbearer applies snow/frost, they also apply equal amounts of frost/snow. This charm is not my original idea; I remember discussion of such a charm happening months ago, but forgot by whom (contact to gain credit). As before, the complete code for this tutorial can be found here. With that out of the way, let's get started.

---

## Setup
The set-up will be very similar to the previous tutorial: override `Load()` and `Unload()`, and load them all in an overridden `AddAssets` (see below). The data types being loaded in this tutorial are `KeywordData`, `CardUpgradeData`, and `StatusEffectData`.

```csharp
// Thanks to Hopeful for this method
public override List<T> AddAssets<T, Y>()
{
    if (assets.OfType<T>().Any())
        Debug.LogWarning($"[{Title}] adding {typeof(Y).Name}s: {assets.OfType<T>().Count()}");
    return assets.OfType<T>().ToList();
}
```

---

## Helper Methods
This tutorial will use two helper methods: `TryGet` and `SStack`. Both have been defined and explained in Tutorial 2. They are written below for your convenience. In addition, a third helper method is included, `TStack`, but not used.

```csharp
public T TryGet<T>(string name) where T : DataFile
{
    T data;
    if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
        data = base.Get<StatusEffectData>(name) as T;
    else if (typeof(KeywordData).IsAssignableFrom(typeof(T)))
        data = base.Get<KeywordData>(name.ToLower()) as T;
    else
        data = base.Get<T>(name);

    if (data == null)
        throw new Exception($"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{name}] or [{Extensions.PrefixGUID(name, this)}]");

    return data;
}

public CardData.StatusEffectStacks SStack(string name, int amount) => new CardData.StatusEffectStacks(TryGet<StatusEffectData>(name), amount);

// TStack is not used in this tutorial, but may be useful for your own projects. Similar to SStack.
public CardData.TraitStacks TStack(string name, int amount) => new CardData.TraitStacks(TryGet<TraitData>(name), amount); 
```

---

## Keywords
The `KeywordData` only holds the name, description, and how to display those things. We will be able to see this keyword in action once we make the charm. So, the implementation is straightforward. Then, write some code that builds your keyword. **IMPORTANT:** keyword names are forced to be lowercase, so our name will be `glacial` instead of `Glacial`.

This is part of the `CreateModAssets()` or similar. This code must run before `base.Load()` in `Load()`.

```csharp
assets.Add(
    new KeywordDataBuilder(this)
        .Create("glacial") // the internal name for the upgrade.
        .WithTitle("Glacial") // the in-game name for the upgrade.
        .WithTitleColour(new Color(0.85f, 0.44f, 0.85f)) // set purple on the title of the keyword pop-up
        .WithShowName(true) // shows name in Keyword box (as opposed to a nonexistant icon).
        .WithDescription("Apply equal <keyword=snow> or <keyword=frost> when the other is applied|Does not cause infinites!") // this is body|note.
        .WithNoteColour(new Color(0.85f, 0.44f, 0.85f)) // set teal
        .WithBodyColour(new Color(0.2f,0.5f,0.5f)) // set body color
        .WithCanStack(false) // the keyword does not show its stack number.
);
```

For extra flair, you can also add something like `PanelSprite("Images/picture.png")` and `PanelColor(Color)` to the mix without any problem. Yeah, that's all there is to discuss about keywords.

---

## Card Upgrades (Charms)
The `CardUpgradeData` contains information on changing simple attributes (hp, damage, counter) as well as adding new ones (traits, status effects, attack effects). We will settle for just adding a starting effect, but might as well test a few properties to make sure everything is working here. (We could make glacial as a trait. Traits are best used for attaching multiple effects and/or dealing with contradictory effects like aimless and barrage. In this scenario, the choice is irrelevant, so... path of least resistance. See the TraitDataBuilder page for more details on making traits).

```csharp
assets.Add(
    new CardUpgradeDataBuilder(this)                                     
        .Create("CardUpgradeGlacial") // usually named as CardUpgradeGlacial
        .AddPool("GeneralCharmPool") // adds the upgrade to the general pool
        .WithType(CardUpgradeData.Type.Charm) // sets the upgrade to a charm (other choices are crowns and tokens)
        .WithImage("GlacialCharm.png") // sets the image file path to "GlacialCharm.png". See below.
        .WithTitle("Glacial Charm") // sets in-game name as Glacial Charm
        .WithText($"Gain <keyword={Extensions.PrefixGUID("glacial",this)}>") // allows me to skip the GUID. The Text class does not.
        // if you did not heed the advice from before, the keyword name must be lowercase, so use .ToLower() to fix that.
        // if you are having trouble, find your keyword via the Unity Explorer and verify its name. 
        .WithTier(2) // sets cost in shops
        .ChangeHP(1) // not in final version. Adds 1 to HP or Sets HP to 1.
        .WithSetHP(true) // not in final version. Combined with the lines above, sets HP to 1.
        .SetAttackEffects(SStack("Snow",1),SStack("Frost",1)) // not in final version. Adds 1 Snow and Frost.
        // SStack is a shorthand function for making StatusEffctStacks defined in the previous tutorial.
);
```

For the sprite of the charm, the image should have a height of about 180 pixels, and a width of similar size (90-100%). The dimensions are important to get right because the game uses three different scalings for these charms (charms in journal scales to a box, charms in backpack scale based on width, charms as a boss reward do not scale at all). Now, let's build the code and see the results using via command line (Gain Upgrade [Charm Name]).

---

## Status Effects
Now to address the major problem with this charm: an endless loop. As much fun as it is to endlessly apply snow, it is meaningless if we cannot stop the loop. So, when we apply the additional effect, we need a way to mark that additional effect so that we do not proc on it. Here is one viable way to implement this.

```csharp
public class StatusEffectMeldXandY : StatusEffectData
{
    public string statusType1; // usually snow.
    public string statusType2; // usually frost.
    public StatusEffectData effectToApply; // usually Snow.
    public StatusEffectData effectToApply2; // usually Frost.

    protected override void Init()
    {
        base.OnApplyStatus += Run; // after a status is applied, Run will be called if RunApplyStatusEvent returns true.
    }

    private IEnumerator Run(StatusEffectApply apply)
    {
        if (apply.effectData.type == statusType1)
        {
            return StatusEffectSystem.Apply(apply.target, target, effectToApply2, apply.count); // apply equal frost
        }
        if (apply.effectData.type == statusType2)
        {
            return StatusEffectSystem.Apply(apply.target, target, effectToApply, apply.count); // apply equal snow
        }
        Debug.Log("[Tutorial] Unreachable Code?");
        return null; 
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply) // system calls this every time a status is about to be applied.
    {
        if ( (target?.enabled != null && apply.applier == target)                                
            && (apply.effectData?.type == statusType1 || apply.effectData?.type == statusType2)  
            && !(apply.effectData == effectToApply || apply.effectData == effectToApply2) )      
        {
            return (apply.count > 0); // call the Run method (if relevant).
        }
        return false; // don't call the Run method.
    }
}
```

We will extend the class `StatusEffectData`. In a past draft of this tutorial, `StatusEffectApplyX` was used as the base class, but it turns out we are not using any characteristics of that class. Generally, `StatusEffectApplyX` or its derived classes are great to extend because of their helper methods/variables `applyToFlags` and the established framework.

Our class behaves similarly to `StatusEffectApplyXWhenYAppliedTo` except we have two X's (the snow and frost statuses) and two Y's (the types "snow" and "frost"). Before any status is applied any target, our glacial status will check: (1) whether the applier is this card, (2) whether the effect is what we care about, and (3) the effect is not marked. If all of this is satisfied, `RunApplyStatusEvent` gives the green light to invoke `OnApplyStatus`, which activates the Run method. Great. So, what does the implementation look like?

```csharp
assets.Add(
    new StatusEffectDataBuilder(this)
        .Create<StatusEffectMeldXandY>("Apply Equal Snow And Frost") // internal name
        .WithCanBeBoosted(false) // e.g. Sun Vase will not double this.
        .WithText($"<keyword={Extensions.PrefixGUID("glacial",this)}>") // shows glacial in the description.
        .WithType("") // sets its type to one with no SFX/VFX. 
        .FreeModify<StatusEffectMeldXandY>(data => // to edit variables from StatusEffectMeldXandY, FreeModify is used.
        {
            data.statusType1 = "snow"; // must be lowercase
            data.statusType2 = "frost"; // must be lowercase
            data.effectToApply = TryGet<StatusEffectData>("Snow").InstantiateKeepName(); // this is one of the effect that this will apply
            data.effectToApply2 = TryGet<StatusEffectData>("Frost").InstantiateKeepName(); // this is the other effect that our new effect will apply
            data.eventPriority = 1; // this effect activates before most things.
        })
);
```

There are two important things here. The first is that we clone the snow and frost statuses so that we can distinguish our snow and frost from any other. The second is that giving the status an `eventPriority` of 1 means it triggers before most status effects. This way we can get the amount before any modifications occurs (like snow resistance). After editing our card upgrade a bit with these lines, we are ready to build and try it out.

These lines replace all the "Not in final version" lines of our `CardUpgradeDataBuilder`.

```csharp
.SubscribeToAfterAllBuildEvent(data => // once all of the mod assets are loaded into the game, then the delegate will be called.
{
    data.effects = new CardData.StatusEffectStacks[1] { SStack("Apply Equal Snow And Frost", 1) };
})
```

---

## Extras: Card Scripts
I feel like I need to go over card scripts here as well. Card scripts are one-time scripts that are run on cards when the charm/crown is applied. In theory, we could rework the design to replace snow/frost effects to snow & frost, but that is remarkably tedious (see CardScriptSwapEffectsBasedOn). So, we will do something more simple.

```csharp
public class CardScriptChangeBackground : CardScript
{
    public string imagePath;
    public override void Run(CardData target)
    {
        target.backgroundSprite = imagePath.ToSprite(); // change the background image of the charmbearer.
    }
}

.SubscribeToAfterAllBuildEvent(data =>
{
    data.effects = new CardData.StatusEffectStacks[1] { SStack("Apply Equal Snow And Frost", 1) };
    CardScriptChangeBackground script = ScriptableObject.CreateInstance<CardScriptChangeBackground>(); // new line: creates the card script
    script.imagePath = this.ImagePath("Frostail BG.png"); // new line: sets the image path
    data.scripts = new CardScript[1] { script }; // new line: attaches the script to the charm.
})
```

We will change their background sprites to some purple. Ultimately, the thing to note is that writing card scripts is just writing code that executes once. And with that, we have our new charm!

---

## Unloading
Like the previous tutorial, have the `UnloadFromClasses()` on hand to clean out the charm reward pool.

Call this method in your `Unload` method.

```csharp
public void UnloadFromClasses()
{
    List<ClassData> tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
    foreach (ClassData tribe in tribes)
    {
        if (tribe == null || tribe.rewardPools == null) { continue; } // this isn't even a tribe; skip it.

        foreach (RewardPool pool in tribe.rewardPools)
        {
            if (pool == null) { continue; } // this isn't even a reward pool; skip it.

            pool.list.RemoveAllWhere((item) => item == null || item.ModAdded == this); // find and remove everything that needs to be removed.
        }
    }
}
```

To test this method is running correctly. Build the solution, run the game, and find your way to the GeneralCharmPool via inspect ClassData Magic. You should see the glacial charm there. Unload your mod, "Update displayed values", and verify that the charm has disappeared and was not replaced with a null or destroyed.

---

## Final Remarks
With this tutorial complete, you should be ready to make your own cards, if you have not done so already. It may also be time to go back to Basic Project Setup and publicize your assembly.

These tutorials are meant to be a solid starting point. Feel free to start trying out stuff beyond what's written here. If you have an idea that you are not sure where to start, just ask it in the #Mod-development channel on the Wildfrost Discord and see what happens!

---

[Back to Modding Tutorials](index.md)
