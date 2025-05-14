using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_TackSpray
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-tackspray", "Tack Spray")
                .SetSprites("items/tackspray.png", "bg.png")
                .WithFlavour("A spray that hits all enemies.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(1)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        mod.SStack("Hit All Enemies", 1)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = true;
                });
            mod.assets.Add(builder);
        }
    }
}
