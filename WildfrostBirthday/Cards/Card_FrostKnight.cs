using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;


namespace WildfrostBirthday.Cards
{
    public static class Card_FrostKnight
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "frost_knight";
            string spritePath = "enemies/frost_knight";

            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "The Frost Knight")
                .SetSprites(spritePath + "frost_knight.png", "bg.png")
                .SetStats(40, 10, 6)  // HP: 40, ATK: 10, Counter: 6
                .WithFlavour("An ancient warrior encased in glacial armor, commanding the very essence of winter itself.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with ImmuneToSnow 1
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1)
                    };

                    // Attack effect: Apply 10 Frost
                    data.attackEffects = new[] {
                        mod.SStack("Frost", 10)
                    };
                });

            mod.assets.Add(builder);
        }
    }
}
