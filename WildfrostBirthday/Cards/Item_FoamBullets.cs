using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_FoamBullets
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-foambullets", "Foam Bullets")
                .SetSprites("items/foambullets.png", "bg.png")
                .WithFlavour("A pack of foam bullets.")
                .WithCardType("Item")
                .WithValue(25)
                .SetDamage(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        mod.SStack("Hit All Enemies", 1)
                    };
                    data.traits = new List<CardData.TraitStacks>();
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = true;
                    data.canPlayOnFriendly = true;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
