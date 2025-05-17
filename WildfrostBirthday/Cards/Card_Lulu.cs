using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Lulu
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-lulu";
             string getSpritePath(string basePath, string cardId)
            {
                if (cardId == "companion-lulu")
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
                .CreateUnit(cardId, "Lulu")
                .SetSprites(getSpritePath("companions/lulu", cardId), "bg.png")
                .SetStats(6, 2, 3)  // HP, ATK, Counter
                .WithFlavour("Lulu defends her family with snowy retaliation.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Ally is Hit Apply Frost To Attacker", 2)
                    };
                    // Attach dynamic sprite change script
                    var script = ScriptableObject.CreateInstance<CardScriptChangeMainOnCounter>();
                    script.baseImagePath = "companions/lulu";
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
