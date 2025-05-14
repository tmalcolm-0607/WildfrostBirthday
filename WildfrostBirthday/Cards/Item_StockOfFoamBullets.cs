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
                .WithFlavour("Add 4 Foam Bullets to your hand.")
                .WithCardType("Item")
                .WithValue(30)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    // On play, add 4 Foam Bullets to hand
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Add Foam Bullets To Hand", 4)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = true;
                });
            mod.assets.Add(builder);
        }
    }
}
