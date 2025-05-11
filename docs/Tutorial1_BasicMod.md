# Tutorial 1: Making a Basic Mod (Gameplay Modifier)

*By Michael Coopman, edited by Phan*

---

## Overview
This tutorial will show how to make a mod, test it, and publish it. The example mod will be a modifier system. You may ask, "Why not start with a mod for a unit, item, or a charm?" Well, that is because:

1. Modifier systems are easier to code.
2. It may not be obvious that these are easier to code.
3. The effects of these systems are more apparent than a single unit, item, or mod can be.

The goal is to show how much power your mod has over the gameplay experience. If you just want the example code, the complete tutorial mod code will be available here.

---

## Basic Mod Project Setup
Your code should look like this, with minor variations:

```csharp
public Tutorial1(string modDirectory) : base(modDirectory)
{
}

public override string GUID => "mhcdc9.wildfrost.tutorial"; // [creator name].[game name].[mod name] is standard convention. LOWERCASE!
public override string[] Depends => new string[0]; // The GUID of other mods that your mod requires. This tutorial has none.
public override string Title => "The 1st Tutorial of Many (or a Few)";
public override string Description => "The goal of this tutorial is to create a modifier system (think daily voyage bell) and make it a mod.";
```

---

## Card Data Editing
Let's see if we can do something simple: can we set Booshu's hp and attack to 99? There's an easy way to do this with `Get<CardData>(string name)`, but this will change the master copy of Booshu, which is bad practice. Instead, whenever a clone of Booshu is created, we set the clone's hp and attack to 99. How do we do this? Cue the `Events` class.

---

## Using Events
The `Events` class holds all the general events tracked during the game. Any class or object may attach their own event handler method to this event. The event for this tutorial is `OnCardDataCreated`, which is invoked whenever a master copy card is cloned. We want to hook our method on `Load()` and unhook on `Unload()`:

```csharp
protected override void Load()
{
    base.Load();
    Events.OnCardDataCreated += BigBooshu;
}

protected override void Unload()
{
    base.Unload();
    Events.OnCardDataCreated -= BigBooshu;
}
```

Now, to make the method `BigBooshu`:

```csharp
private void BigBooshu(CardData cardData)
{
    Debug.Log("[Tutorial1] New CardData Created: " + cardData.name);
    if (cardData.name == "BerryPet") // Booshu's internal name
    {
        cardData.hp = 99;
        cardData.damage = 99;
        cardData.value = 99 * 36;
        Debug.Log("[Tutorial1] Booshu!");
    }
}
```

---

## Debug.Log
You may notice there are two `Debug.Log` lines. These will only be seen if you have the Console mod. It's good practice to put a unique [tag] in the front to easily find your debug lines. You can also use `WriteLine`, `WriteWarn`, and `WriteError` if your mod framework provides them.

---

## Building/Testing the Mod
Press Ctrl+B to build the project. This creates a `.dll` in your `obj/debug` folder. Place the `.dll` in `[GameRoot]/Modded/Wildfrost_Data/StreamingAssets/Mods`. Optionally, add an `icon.png` for your mod.

Run the game and turn on your mod. If you go to the Pet House, Booshu's stats are unchanged (the master copy is not used directly). Start a new run to see the result.

---

## Give All Enemies "Apply Haze"
Let's make enemies harder by adding haze to their attack effects. Hook another method to `OnCardDataCreated`:

```csharp
private void ScaryEnemies(CardData cardData)
{
    switch (cardData.cardType.name)
    {
        case "Miniboss":
        case "Boss":
        case "BossSmall":
        case "Enemy":
            cardData.attackEffects = CardData.StatusEffectStacks.Stack(cardData.attackEffects, new CardData.StatusEffectStacks[]
            {
                new CardData.StatusEffectStacks(Get<StatusEffectData>("Haze"), 1)
            });
            cardData.value *= 2;
        break;
    }
}
```

Don't forget to hook this method onto `OnCardDataCreated` in `Load` and `Unload`.

---

## Publishing Your Mod
On your mod page, use the Steam button to publish or update your mod. It is highly recommended to use the Mod Uploader to add tags for discoverability. Once you publish, do not change the GUID.

---

## Other Notable Stuff

### Some Other Events
- `OnCardDataCreated`: Invoked when a card is cloned (used in this tutorial)
- `OnEntityCreated`: Invoked when an entity of a CardData is created
- `OnSceneLoaded`: Invoked when a scene is loaded
- `OnEntityOffered`: Invoked when a card is presented as a choice
- `OnEntityChosen`: Invoked when a card is chosen
- `OnBattleEnd`: Invoked when the battle is over
- `PostBattle`: Invoked when you return to the campaign map
- `OnCampaignEnd`: Invoked when the run is over

### Vanilla ModifierSystem Classes
Search "ModifierSystem" in the object browser. Notable ones:
- AddFrenzyToBossesModifierSystem
- BombskullClunkersModifierSystem
- BoostAllEffectsModifierSystem
- BoostArea2EnemyDamageModifierSystem
- DeadweightAfterBossModifierSystem
- DoubleGoblingGoldModifierSystem
- PermadeathModifierSystem
- MoreCardRewardsModifierSystem
- RecallChargeRedrawBellModifierSystem
- RecallBellStartChargedModifierSystem

---

## Last Updated
May 11, 2025
