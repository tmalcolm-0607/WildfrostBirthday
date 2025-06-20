using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Goldengale
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-goldengale";
            string spritePath = "companions/goldengale";
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Goldengale")
                .SetSprites(spritePath + ".png", "bg.png") // Adjust sprite paths as needed
                .SetStats(15, 0, 4) // HP, ATK, Counter
                .WithCardType("Friendly")                
                .WithFlavour("A shiny gilded girl who mesmerizes her foes with her dazzling gilded presence.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Use the helper methods to get status effect stacks and trait stacks
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("When Hit Apply Gold To Attacker (No Ping)", 2),
                        mod.SStack("Weakness", 1)
                    };
                    
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Greed", 1),
                        
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}