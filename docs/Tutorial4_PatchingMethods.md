# Tutorial 4: Patching Methods

*Phan edited this page on Feb 10 · 7 revisions*
*By Michael Coopman*

---

## Overview & Background
Welcome back! In this tutorial, we will showcase some examples of modifying methods in the base game of Wildfrost.

The modding framework of Wildfrost is done using the Harmony library, which allows for "patching, replacing, and decorating .NET methods during runtime". While a Harmony instance is active (always in our case), method calls can be rerouted to the Harmony instance's modified methods. This is a very powerful tool to use. The Harmony library has its own documentation and several helpful sections about patching: [Harmony Docs](https://harmony.pardeike.net/). This tutorial will not try to cover patching at the level of detail the Harmony docs will. Instead, we show some applicable scenarios to use some of the techniques from the doc alongside helpful explanations. The only prerequisites to this tutorial is the Basic Project Setup and Tutorial 0. It is highly recommended to publicize your assembly at this point as there will modify some private variables. This tutorial does not publicize the assembly, but it requires Traverse-ing some hoops. If you ever get really stuck, the link to the completed code can be found here.

---

## Prefixes
### Skipping a method altogether with Prefix
Example:
```csharp
[HarmonyPatch(typeof(CheckAchievements), nameof(CheckAchievements.Start))]
class PatchAchievements { static bool Prefix() => false; }
```
This disables the `Start` method in `CheckAchievements`. The patch class and method can be placed anywhere in your mod assembly.

### Modifying parameters before a method runs
Example:
```csharp
[HarmonyPatch(typeof(NoTargetTextSystem), nameof(NoTargetTextSystem._Run), new Type[]
{
    typeof(Entity),
    typeof(NoTargetType),
    typeof(object[]),
})]
class PatchNoTargetDance
{
    internal static int count = 0;
    static void Prefix(NoTargetTextSystem __instance, ref Vector2 ___shakeDurationRange, ref Vector2 ___shakeAmount)
    {
        count++;
        Debug.Log($"[Tutorial] Prefix: {count}");
        if (count == 3)
        {
            ___shakeDurationRange = new Vector2(0.15f, 0.2f);
            ___shakeAmount = new Vector3(0.75f, 0f, 0f);
        }
    }
}
```

You can use `__instance` for the calling object, and `___fieldName` for private fields. Use `ref` to modify them.

---

## Replacing a method with your own
You can replace the method's result by setting `ref IEnumerator __result` in your prefix and returning `false`.

---

## Postfix
Postfixes run after the original method. You can use them to read or modify the result.

Example:
```csharp
[HarmonyPatch(typeof(Dead.Random), nameof(Dead.Random.Range), new Type[] { typeof(int), typeof(int) })]
class PatchRandom
{
    static void Postfix(int __result, int minInclusive, int maxInclusive)
    {
        Debug.Log($"[Tutorial] [{minInclusive}, {maxInclusive}] -> {__result}");
    }
}
```

You can also overwrite the result by returning a value from your postfix.

---

## IEnumerators and Postfix
You can wrap or interlace actions with the original IEnumerator:
```csharp
static IEnumerator Postfix(IEnumerator __result)
{
    int i = 0;
    while(__result.MoveNext())
    {
        Debug.Log($"Action {i}!");
        yield return __result.Current;
    }
}
```

---

## Loose Ends
- **Passthrough:** Use `__state` to pass info from prefix to postfix.
- **Transpilers:** Modify IL code directly.
- **Finalizers:** Add try-catch logic to methods.
- **Reverse Patching:** Use source code as a method before other patches.

---

[Back to Modding Tutorials](index.md)
