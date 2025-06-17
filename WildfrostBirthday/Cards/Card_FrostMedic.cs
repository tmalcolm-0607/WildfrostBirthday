using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_FrostMedic
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "frostmedic";
            string spritePath = "enemies/frostmedic";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Frost Medic", idleAnim: "PulseAnimationProfile", bloodProfile: "BloodProfilePinkWisp")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(10, 1, 6)  // HP, ATK, Counter
                .WithFlavour("A Frosty Medic who heals allies with the magic of the frost.")
                .WithCardType("Enemy")
                .WithValue(100)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects - Start with Shell 2
                    var startWithEffects = new List<CardData.StatusEffectStacks> {
                        mod.SStack("On Turn Heal & Cleanse Allies", 3),
                    };
                    data.startWithEffects = startWithEffects.ToArray();

                });
                
            mod.assets.Add(enemyBuilder);
        }
    }
}