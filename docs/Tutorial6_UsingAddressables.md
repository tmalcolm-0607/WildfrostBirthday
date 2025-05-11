# Tutorial 6: Using Addressables

*Phan edited this page last week · 5 revisions*
*By : @Hopeless_phan ( Fury )*

---

## Contents
- [0. Why use Addressables?](#0-why-use-addressables)
- [1. Prior Setup](#1-prior-setup)
  - [1.1 Installing Unity v2021.3.16f1](#11-installing-unity-v20211316f1)
  - [1.2 Download Unity Hub](#12-download-unity-hub)
- [2. Unity Project Setup](#2-unity-project-setup)
  - [2.1 Enable Sprite Packing](#21-enable-sprite-packing)
  - [2.2 Required Unity Packages](#22-required-unity-packages)
  - [2.3 Create Addressables Settings](#23-create-addressables-settings)
  - [2.4 Create Addressables Profile for your Mod](#24-create-addressables-profile-for-your-mod)
- [3. Addressables for real](#3-addressables-for-real)
  - [3.1 Create Sprites](#31-create-sprites)
  - [3.2 Create Sprite Atlases](#32-create-sprite-atlases)
  - [3.3 Building/Loading the AssetBundle](#33-buildingloading-the-assetbundle)
- [4. Extras](#4-extras)
  - [4.1 Using Sprite Atlases](#41-using-sprite-atlases)
  - [4.2 Harmony Suppressor's code](#42-harmony-suppressors-code)
  - [4.3 Creating CardData/StatusEffects in Unity (without DataBuilders)](#43-creating-carddatastatuseffects-in-unity-without-databuilders)
- [5. Resources / References](#5-resources--references)

---

## 0. Why use Addressables?
Addressables can initialize assets asynchronously in the background and store Unity-type objects like Sprites and CardData. This means multiple mods can initialize at the same time without long delays. However, the first build can be slow. This tutorial focuses on using Addressables for sprites and atlases, not on asynchronous loading.

---

## 1. Prior Setup
### 1.1 Installing Unity v2021.3.16f1
Install Unity 2021.3.16f1. Using the wrong version may cause issues with exported AssetBundles.

### 1.2 Download Unity Hub
Get a free Unity license by signing into Unity Hub. Locate your Unity installation and agree to the terms.

---

## 2. Unity Project Setup
Create a new 3D project in Unity Hub. It may take a few minutes to import files.

### 2.1 Enable Sprite Packing
Go to Edit > Project Settings > Editor > Sprite Packer and set it to Always Enabled.

### 2.2 Required Unity Packages
Install the following packages from Window > Package Manager:
- Addressables
- 2D Sprite (optional, but recommended)

### 2.3 Create Addressables Settings
Open Window > Asset Management > Addressables > Groups and select Create Addressables Settings. Note the Build Path for later.

### 2.4 Create Addressables Profile for your Mod
In your mod code, define:
```csharp
public static string CatalogFolder => Path.Combine(instance.ModDirectory, "Windows");
public static string CatalogPath => Path.Combine(CatalogFolder, "catalog.json");
```
Create a new variable in Addressables Profiles for your mod's catalog folder. Set the BuildPath and LoadPath accordingly.

---

## 3. Addressables for real
### 3.1 Create Sprites
Import your sprite files into a folder in your Unity project. Set Texture Type to Sprite and Sprite Mode to Single.

### 3.2 Create Sprite Atlases
If you installed 2D Sprite, create Sprite Atlases to pack your sprites. Set Max Texture Size to 8192, disable Allow Rotation and Tight Packing, and add your sprite folders. Mark the atlas as Addressable.

> **Warning:** Remove original sprites from the addressable group after adding them to an atlas to avoid large bundle sizes.

### 3.3 Building/Loading the AssetBundle
Build the addressables (Build > New Build > Default Build Script). Copy the output folder (e.g., Windows) into your mod directory. In your mod's `Load()` method, load the catalog:
```csharp
if (!Addressables.ResourceLocators.Any(r => r is ResourceLocationMap map && map.LocatorId == CatalogPath))
    Addressables.LoadContentCatalogAsync(CatalogPath).WaitForCompletion();
```
To load assets:
```csharp
Cards = GetAsset<SpriteAtlas>("Assets/{GUID}/Cards.spriteatlas");
```
Or asynchronously:
```csharp
UI = Addressables.LoadAssetAsync<SpriteAtlas>("Assets/hope.wildfrost.mymod/UI.spriteatlas");
```

---

## 4. Extras
### 4.1 Using Sprite Atlases
Get a sprite from an atlas:
```csharp
sprite = atlas.GetSprite(nameWithoutExtension);
```
Example extension method:
```csharp
public static CardDataBuilder SetAddressableSprites(this CardDataBuilder builder, string mainSpriteName, string backgroundSpriteName)
{
    Sprite mainSprite = MyMod.Cards.GetSprite(mainSpriteName.Replace(".png", ""));
    Sprite backgroundSprite = MyMod.Cards.GetSprite(backgroundSpriteName.Replace(".png", ""));
    return builder.SetSprites(mainSprite, backgroundSprite);
}
```

### 4.2 Harmony Suppressor's code
To speed up load times, use Harmony Suppressor or include its patch:
```csharp
[HarmonyPatch(typeof(WildfrostMod.DebugLoggerTextWriter), nameof(WildfrostMod.DebugLoggerTextWriter.WriteLine))]
class PatchHarmony
{
    static bool Prefix() { Postfix(); return false; }
    static void Postfix() => HarmonyLib.Tools.Logger.ChannelFilter = HarmonyLib.Tools.Logger.LogChannel.Warn | HarmonyLib.Tools.Logger.LogChannel.Error;
}
```
In your mod constructor:
```csharp
public MyMod(string modDirectory) : base(modDirectory)
{
    HarmonyInstance.PatchAll(typeof(PatchHarmony));
}
```

### 4.3 Creating CardData/StatusEffects in Unity (without DataBuilders)
You can use Unity's CreateAssetMenu to create CardData and StatusEffects as ScriptableObjects, set them as addressable, and load them asynchronously. See Unity's documentation for more details.

---

## 5. Resources / References
- [Unity's Addressables documentation](https://docs.unity3d.com/Packages/com.unity.addressables@1.19/manual/index.html)
- [Introduction to Scriptable Objects](https://learn.unity.com/tutorial/introduction-to-scriptable-objects)
- [ThunderKit for Risk of Rain 2 modding](https://github.com/ThunderKit)

---

[Back to Modding Tutorials](index.md)
