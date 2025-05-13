using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_TackSpray
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("tack_spray", "Tack Spray")
                .SetSprites("items/tackspray.png", "bg.png")
                .WithFlavour("A spray that hits all enemies.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(1)
                .SubscribeToAfterAllBuildEvent(data =>
                {                    data.startWithEffects = new[] {
                        mod.SStack("Hit All Enemies", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
