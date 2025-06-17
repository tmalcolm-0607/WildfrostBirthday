using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_DiceOfDestiny
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-diceofdestiny", "Dice of Destiny")
                .SetSprites("items/diceofdestiny.png", "bg.png")
                .WithFlavour("A mystical die that channels chaos into power.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(0) // No damage, this is an item
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // On play, deal random damage between 1 and 6
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Deal Random Damage To Target (1-6)", 1)
                    };
                    data.canPlayOnEnemy = true;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
