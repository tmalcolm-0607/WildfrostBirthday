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
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        mod.SStack("Hit All Enemies", 1)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Noomlin", 1)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = true;
                });
            mod.assets.Add(builder);
        }
    }
}
