using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_SulfurBom
    {
        public static void Register(WildFamilyMod mod)
        {            string cardId = "sulfur_bom";
            string spritePath = "enemies/sulfur_bom";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Sulfur Bom", idleAnim: "FloatSquishAnimationProfile")
                .SetSprites("enemies/sulfur_bom.png", "enemies/sulfur_bom_bg.png")
                .SetStats(21, 0, 3)  // HP, ATK, Counter
                .WithFlavour("A volatile blob that floats menacingly.")
                .WithCardType("Enemy")
                .WithValue(10)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    var startEffects = new List<CardData.StatusEffectStacks>
                    {
                        mod.SStack("Weakness", 3),    // Starts with 3 Weakness
                        mod.SStack("MultiHit", 2)     // Has 2 MultiHit
                    };
                    data.startWithEffects = startEffects.ToArray();
                    var attackEffects = new List<CardData.StatusEffectStacks>
                    {
                        mod.SStack("Weakness", 1),    // Starts with 1 Weakness

                    };
                    data.attackEffects = attackEffects.ToArray();
                    // Set traits - Aimless
                    data.traits = new List<CardData.TraitStacks>
                    {
                        mod.TStack("Aimless", 1)
                    };


                });
            mod.assets.Add(enemyBuilder);
        }
    }
}
