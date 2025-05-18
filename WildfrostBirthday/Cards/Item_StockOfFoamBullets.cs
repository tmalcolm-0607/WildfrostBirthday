using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_StockOfFoamBullets
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-stockoffoambullets", "Stock of Foam Bullets")
                .SetSprites("items/foambullets.png", "bg.png")
                .WithText("Add 4 Foam Bullets to your hand.")
                .WithCardType("Item")
                .WithValue(30)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                     
                    };
                    // On play, add 4 Foam Bullets to hand using the new effect chain
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Add Foam Bullets To Hand", 1),
                        mod.SStack("On Card Played Add Foam Bullets To Hand", 1),
                        mod.SStack("On Card Played Add Foam Bullets To Hand", 1),
                        mod.SStack("On Card Played Add Foam Bullets To Hand", 1)

                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
