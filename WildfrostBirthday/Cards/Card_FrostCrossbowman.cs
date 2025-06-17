using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_FrostCrossbowman
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "frostcrossbowman";
            string spritePath = "enemies/frostcrossbowman";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Frost Crossbowman", idleAnim: "PulseAnimationProfile", bloodProfile: "BloodProfilePinkWisp")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(8, 2, 4)  // HP, ATK, Counter
                .WithFlavour("A Frosty Crossbowman who shoots icy bolts at enemies from a distance, dealing low damage but applying good amounts of Frost.")
                .WithCardType("Enemy")
                .WithValue(100)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects - Start with Shell 2
                    var attackEffects = new List<CardData.StatusEffectStacks> {
                        mod.SStack("Frost", 2),
                    };
                    data.startWithEffects = attackEffects.ToArray();
                    var traits = new List<CardData.TraitStacks>
                    {
                        mod.TStack("Longshot", 1) // Frost Crossbowman hits the furthest enemy
                    };
                });
                
            mod.assets.Add(enemyBuilder);
        }
    }
}