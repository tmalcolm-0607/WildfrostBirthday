using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_FreezingTreat
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-freezingtreat", "Freezing Treat")
                .SetSprites("items/freezingtreat.png", "bg.png")
                .WithFlavour("A treat that chills and protects.")
                .WithCardType("Item")
                .WithValue(40)
                .SetDamage(4)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    data.attackEffects = new[] {
                        mod.SStack("Block", 2),
                        mod.SStack("Snow", 2)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = true;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
