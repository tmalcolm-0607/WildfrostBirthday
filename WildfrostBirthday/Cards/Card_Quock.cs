using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Quock
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-quock";
            string spritePath = "companions/quock";
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Quock")
                .SetSprites(spritePath + ".png", "bg.png") // Adjust sprite paths as needed
                .SetStats(3, 2, 2) // HP, ATK, Counter
                .WithCardType("Friendly")                .WithFlavour("A feisty companion with a quick counter and a resistance to snow.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Use the helper methods to get status effect stacks and trait stacks
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1)
                    };
                    
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Smackback", 1),
                        mod.TStack("Barrage", 1)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}