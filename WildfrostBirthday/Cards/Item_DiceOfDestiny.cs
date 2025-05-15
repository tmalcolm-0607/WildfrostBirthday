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
                .WithFlavour("Deal 1–6 random damage.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // On play, deal random damage between 1 and 6
                    data.attackEffects = new[] {
                        mod.SStack("On Card Played Deal Random Damage To Target (1-6)", 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
