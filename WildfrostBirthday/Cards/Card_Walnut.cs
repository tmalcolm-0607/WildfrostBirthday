using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Walnut
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "walnut";
            string spritePath = "enemies/walnut";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Walnut", idleAnim: "PulseAnimationProfile", bloodProfile: "BloodProfileHusk")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(1, 3, 3)  // HP, ATK, Counter
                .WithFlavour("A tiny little acorn just adorably and aggressively fights for Apricot.")
                .WithCardType("Enemy")
                .WithValue(100)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects - Start with Shell 2
                    var startEffects = new List<CardData.StatusEffectStacks> {
                        mod.SStack("Shell", 2),
                        mod.SStack("On Turn Apply Shell To Self", 1),

                    };
                    data.startWithEffects = startEffects.ToArray();
                    
                });
                
            mod.assets.Add(enemyBuilder);
        }
    }
}