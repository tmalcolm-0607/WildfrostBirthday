using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_BerryCake
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-berrycake", "Berry Cake")
                .SetSprites("items/berrycake.png", "bg.png")
                .WithFlavour("Increase HP of all allies by 3.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    // Use on-play effect to increase max health of all allies by 3
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Increase Max Health To Allies", 3),
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = true;
                });
            mod.assets.Add(builder);
        }
    }
}
