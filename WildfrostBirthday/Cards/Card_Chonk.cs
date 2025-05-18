using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Chonk
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-chonk";
            string spritePath = "companions/chonk";
            
            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Chonk")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, 3, 3)  // Scrap HP, ATK, Counter
                .WithFlavour("A clunker with a big heart and a bigger frame.")
                .WithCardType("Clunker")
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 2)
                    };
                    data.attackEffects = new[]
                    {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Weakness"), 2)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}
