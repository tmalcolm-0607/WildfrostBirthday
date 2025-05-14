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
                    // On play, randomize MultiHit between 1 and 6
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Randomize MultiHit", 6)                        
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
