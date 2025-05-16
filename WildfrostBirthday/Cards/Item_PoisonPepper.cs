using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_PoisonPepper
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-poisonpepper", "Poison Pepper")
                .SetSprites("items/poisonpepper.png", "bg.png")
                .WithFlavour("A spicy pepper laced with toxins. Applies 2 Shroom and 10 Spice.")
                .WithCardType("Item")
                .WithValue(40)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    data.attackEffects = new[] {
                        mod.SStack("Shroom", 2),
                        mod.SStack("Spice", 10)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = true;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
