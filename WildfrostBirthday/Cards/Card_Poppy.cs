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
            
            // COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Poppy")
                .SetSprites("companions/poppy0.png", "bg.png")
                .SetStats(11, 2, 4)  // HP, ATK, Counter
                .WithFlavour("Ferocious little guardian who fights back hard.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Hit Apply Demonize To Attacker", 2)
                    };
                    // Set traits
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Smackback"), 1)
                    };
                    // Attach dynamic sprite change script
                    var script = ScriptableObject.CreateInstance<CardScriptChangeMainOnCounter>();
                    script.baseImagePath = "companions/poppy";
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
