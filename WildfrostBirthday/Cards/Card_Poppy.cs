using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Poppy
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-poppy";
             string getSpritePath(string basePath, string cardId)
            {
                if (cardId == "companion-poppy")
                {
                    int randomNumber = Dead.Random.Range(0, 3);
                    UnityEngine.Debug.Log("[FamilyCharm] Random number for sprite path: " + basePath + randomNumber + ".png");
                    return basePath + randomNumber + ".png";
                }
                UnityEngine.Debug.Log("[FamilyCharm] Card ID not recognized, using default sprite path: " + basePath + ".png");
                return basePath + ".png";
            }
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Poppy")
                .SetSprites(getSpritePath("companions/poppy", cardId), "bg.png")
                .SetStats(11, 4, 4)  // HP, ATK, Counter
                .WithFlavour("Ferocious little guardian who fights back hard.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.attackEffects = new[] {
                        mod.SStack("Demonize", 1)
                    };
                    // Set traits
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Smackback"), 1)
                    };                    // Attach dynamic sprite change script
                    var script = ScriptableObject.CreateInstance<CardScriptChangeMainOnCounter>();
                    script.baseImagePath = "images/companions/poppy";
                    UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] Setting Poppy companion sprite path to {script.baseImagePath}");
                    if (data.createScripts != null)
                    {
                        var scripts = new List<CardScript>(data.createScripts) { script };
                        data.createScripts = scripts.ToArray();
                    }
                    else
                    {
                        data.createScripts = new CardScript[] { script };
                    }
                });
                
            mod.assets.Add(companionBuilder);
        }
    }
}
