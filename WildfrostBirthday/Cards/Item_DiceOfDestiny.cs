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
                .WithFlavour("MultiHit x2, randomize attacks between 1–6.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        mod.SStack("MultiHit", 2)
                    };
                });
                
            mod.assets.Add(builder);
            // Add custom script for randomizing attacks if needed
        }
    }
}
