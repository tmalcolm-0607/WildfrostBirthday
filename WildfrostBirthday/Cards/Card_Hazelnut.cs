using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Hazelnut
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "hazelnut";
            string spritePath = "enemies/hazelnut";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Hazelnut", idleAnim: "PulseAnimationProfile", bloodProfile: "BloodProfileHusk")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(1, 2, 4)  // HP, ATK, Counter
                .WithFlavour("A tiny little acorn who throws small pebbles at enemies to protect their leader, Apricot.")
                .WithCardType("Enemy")
                .WithValue(100)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects - Start with Shell 2
                    var startEffects = new List<CardData.StatusEffectStacks> {
                        mod.SStack("Shell", 2),

                    };
                    data.startWithEffects = startEffects.ToArray();
                    data.traits = new List<CardData.TraitStacks>
                    {
                        mod.TStack("Longshot", 1) // Hazelnut hits the furthest enemy
                    };
                });
                
            mod.assets.Add(enemyBuilder);
        }
    }
}