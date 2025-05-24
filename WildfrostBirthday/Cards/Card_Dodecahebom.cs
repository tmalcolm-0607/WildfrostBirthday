using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Dodecahebom
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "dodecahebom";
            string spritePath = "enemies/dodecahebom";

            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Dodecahebom", idleAnim: "GiantAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(874, 0, 4)  // HP, ATK, Counter
                .WithFlavour("A geometrically perfect abomination pulsing with destructive energy.")
                .WithCardType("Miniboss")
                .WithValue(150)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    var startEffects = new List<CardData.StatusEffectStacks> {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("When Hit Apply Weakness To Self", 2),
                        mod.SStack("Weakness", 3)
                    };
                    data.startWithEffects = startEffects.ToArray();

                    // Attack effects
                    data.attackEffects = new[] {
                        mod.SStack("Weakness", 1)
                    };

                    // Set traits - Bombard 2
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Bombard 2", 1)
                    };

                });
                
            mod.assets.Add(enemyBuilder);
        }
    }
}
