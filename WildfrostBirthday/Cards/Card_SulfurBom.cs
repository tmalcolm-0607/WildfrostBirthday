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
                .CreateUnit(cardId, "Sulfur Bom", idleAnim: "FloatAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(13, 0, 5)  // HP, ATK, Counter
                .WithFlavour("A volatile blob that floats menacingly.")
                .WithCardType("Enemy")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    var startEffects = new List<CardData.StatusEffectStacks>
                    {
                        mod.SStack("Weakness", 1),    // Starts with 1 Weakness
                        mod.SStack("MultiHit", 2)     // Has 2 MultiHit
                    };
                    data.startWithEffects = startEffects.ToArray();
                    var AttackEffects = new List<CardData.StatusEffectStacks>
                    {
                        mod.SStack("Weakness", 1),    // Starts with 1 Weakness
                    
                    };
                    data.attackEffects = AttackEffects.ToArray();
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
