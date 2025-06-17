using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_FrostSpearman
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "frostspearman";
            string spritePath = "enemies/frostspearman";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Frost Spearman", idleAnim: "PulseAnimationProfile", bloodProfile: "BloodProfilePinkWisp")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(10, 4, 3)  // HP, ATK, Counter
                .WithFlavour("A Frosty Spearman who jabs at enemies with its icy spear, dealing good damage and applying Frost.")
                .WithCardType("Enemy")
                .WithValue(100)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects - Start with Shell 2
                    data.attackEffects = new[] {
                        mod.SStack("Frost", 3)
                    };
                    
                });
                
            mod.assets.Add(enemyBuilder);
        }
    }
}
