# Tutorial: Making Status Icons

*Phan edited this page on Mar 22 · 4 revisions*
*By : @Hopeless_phan ( Fury )*

---

## Contents
- [Hello reader!](#hello-reader)
- [1. Prior Setup](#1-prior-setup)
- [2. Designing the icon](#2-designing-the-icon)
- [3. Using StatusIconBuilders](#3-using-statusiconbuilders)
  - [3.1 Create Mod Assets](#31-create-mod-assets)
  - [3.2 Create the Keyword and StatusEffect](#32-create-the-keyword-and-statuseffect)
  - [3.3 The Status Icon Builder](#33-the-status-icon-builder)
  - [3.4 Visual/Sound effects](#34-visualsound-effects)
- [4. Testing](#4-testing)

---

## Hello reader!
I've made a straightforward way to make status icons using a custom databuilder, relying on the VFX Tools mod [Link].

You can test a simple mod that does this on the workshop: [Link], or see [the example code].

---

## 1. Prior Setup
Steps to follow if you haven't used VFX Tools or made icons before.

---

## 2. Designing the icon
**TLDR:** Make sure there's at least 10% transparent padding around your icon! Template PSDs provided.

---

## 3. Using StatusIconBuilders

### 3.1 Create Mod Assets
Status Icons generally involve 3 new mod assets: a keyword, a status effect, and the actual icon. When multiple assets are involved for one thing, it's good practice to organise each group of assets in their own code.

Suppose we're making a new icon called "Amber". We could make a `CreateAmberIcon()` method to call in `CreateModAssets()` like this:

```csharp
public void CreateModAssets()
{
    CreateAmberIcon();
    // ... other asset creation
}

public void CreateAmberIcon()
{
    // Everything Amber-related can go in here now
}
```

I'll also be putting this Amber icon (Amber) into a subfolder "Icons" of "Images" in the mod directory. This way I can access it later via `ImagePath("Icons/amber.png")`.

---

### 3.2 Create the Keyword and StatusEffect
Nothing special is really needed for these, so you should just make them as usual. These two examples will be barebones compared to what you can really do!

> **Warning:**
> It's worth checking here that your mod's GUID should be all lowercase! If not, the game may not display the keyword popup for your icon.

```csharp
public void CreateAmberIcon()
{
    assets.Add(
        new KeywordDataBuilder(this)
            .Create("amber") // no spaces, all lowercase !!
            .WithTitle("Amber") // what shows up when hovering the icon
            .WithDescription(@"""
                Freeze!
                |Fossilised tree sap, which we call amber, waited for millions of years...
            """));

    assets.Add(
        new StatusEffectDataBuilder(this)
            .Create<StatusEffectSnow>("amber effect") // StatusEffect class to make the effect
            .Subscribe_WithStatusIcon("amber icon") // whatever you want to name the icon builder
    );
}
```

> **Note:**
> Note the addition of `Subscribe_WithStatusIcon("amber icon")` here. Replace `amber icon` with whatever you want to name the icon's builder.

---

### 3.3 The Status Icon Builder
As mentioned in the previous section, the example icon builder will have the name "amber icon". If you use a different name here, make sure to change it in the effect builder's `Subscribe_WithStatusIcon` too!

Now you'll have to choose a `statusType` that others won't accidentally use -- This can be part of your mod's GUID. Since my mod GUIDs start with `hope`, I choose to use `hope.amber` but you're not me. Use something else! >:O

In `CreateAmberIcon()`,

```csharp
assets.Add(
    new StatusIconBuilder(this)
        .Create(name: "amber icon", // used in StatusEffectDataBuilder.Subscribe_WithStatusIcon()
                statusType: "hope.amber", // [creator name].[icon name]
                ImagePath("Icons/hope.amber.png")) // the image I want to use in [my mod directory]/Images/Icons
        // name == status type, but VFX mod will try to adjust it
        .WithIconGroupName(StatusIconBuilder.IconGroups.counter) // counter icons
        // Most can skip these two altogether
        .WithTextColour(new Color(0.1f, 0f, 0f))
        .WithTextShadow(new Color(1f, 1f, 0f))
        .WithTextboxSprite() // uses the main sprite for the textbox
        //.WithTextboxSprite(ImagePath("Icons/amber.png")) // slightly slower, but lets you use other (lower-res) textbox sprites
        .WithKeywords(iconKeywordOrNull:"amber") // "amber" will be adjusted to show the icon's textbox sprite
);
```

Note that most icons would use the same textbox sprite as the actual sprite, so only `.WithTextboxSprite()` is needed. For certain icons, it makes more sense to use two different sprites. Take this modded status Cat for example:

*Pictured: Salvo Kitty from Absent Avalanche (https://steamcommunity.com/sharedfiles/filedetails/?id=3347694881)*

---

### 3.4 Visual/Sound effects
Note that VFX Tools mod lets you import gifs and audio files. This lets you choose what visual effect/sound to play when this effect is applied, by adding:

```csharp
.WithApplyVFX(ImagePath("your gif.gif")) // replace with your own GIF or APNG filepath, in the Images folder
.WithApplySFX(ImagePath("your sound.mp3")) // replace with your own MP3/WAV/OGG etc filepath, in the Images folder
```

---

## 4. Testing
Using the Another Console mod [Link], you should quickly test that you can do the following commands:

- `add status <statusEffect name>` to add the status to a card. If you specified any WithApplyVFX or WithApplySFX, it should also play.
- `add attackeffect <statusEffect name>` to add "Apply 1Amber" to the card. This card should then be able to apply the status to the target it triggered against -- if you specified any WithApplyVFX or WithApplySFX, it should also play on this target.

> **Note:**
> Modded effects cannot be used with `.SetAttackEffect()` or `.SetStartWithEffect` as normal; this code runs before any of our effects exist... This is why we have to directly set `data.attackEffects` and `data.startWithEffects`.

Once you confirm they exist, move on to putting them in a card's databuilder. This can look like:

```csharp
new CardDataBuilder(this)
    .CreateUnit(name:"BerryPet", englishTitle:"Booshu")
    .SetStats(4, 3, 4)
    .WithValue(25) // Drop (25/36) = 0 gold as an enemy
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.attackEffects = new CardData.StatusEffectStacks[]
        {
            SStack("amber effect", 1), // "Apply 1 Amber"
        };
        data.startWithEffects = new CardData.StatusEffectStacks[]
        {
            SStack("On Turn Heal Allies", 3),
            SStack("amber effect", 1),
        };
    })
```

> **Note:**
> Try hovering over the icon (or card, if you set the attack effect) to see whether your keyword pops up. If not, check that your mod GUID is lowercase!

---

[Back to Modding Tutorials](index.md)
