using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_PlywoodSheet
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-plywoodsheet", "Plywood Sheet")
                .SetSprites("items/plywood.png", "bg.png")
                .WithFlavour("Add 3 Scrap to your hand.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    // Add 3 Scrap to hand on play
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Add Scrap To Hand", 3)
                    };
                    data.canPlayOnHand = true;
                    data.canPlayOnEnemy = false;
                    data.canPlayOnFriendly = true; 
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
