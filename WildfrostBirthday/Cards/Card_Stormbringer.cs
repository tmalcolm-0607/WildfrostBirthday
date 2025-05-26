using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Stormbringer
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "stormbringer";
            string spritePath = "enemies/stormbringer";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Stormbringer", idleAnim: "SwayAnimationProfile")
                .SetSprites("enemies/stormbringer.png", "enemies/stormbringer_bg.png")
                .SetStats(33, 0, 6)  // HP, ATK, Counter
                .WithFlavour("A volatile blob that floats menacingly.")
                .WithCardType("Enemy")
                .WithValue(10)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    var startEffects = new List<CardData.StatusEffectStacks>
                    {
                        mod.SStack("Weakness", 3),    // Starts with 3 Weakness
                        mod.SStack("MultiHit", 3)     // Has 3 MultiHit
                    };
                    data.startWithEffects = startEffects.ToArray();

                   
                    // Set traits - Aimless
                    data.traits = new List<CardData.TraitStacks>
                    {
                        mod.TStack("Barrage", 1)
                    };


                });
            mod.assets.Add(enemyBuilder);
        }
    }
}