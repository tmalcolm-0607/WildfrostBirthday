// Needed for List<>
using System.Collections.Generic;
using UnityEngine;

// Other common namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Cards
{
    public static class Item_SupplyDrop
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-supplydrop", "Supply Drop")
                .SetSprites("items/supplydrop.png", "bg.png")
                .WithFlavour("A supply drop containing various resources.")
                .WithCardType("Item")
                .WithText("Add 1 Space Sentry and 2 Mini Sentries to your hand.")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                    .SubscribeToAfterAllBuildEvent(data =>
                {

                    // On play, add 4 Space Sentries to hand using the new effect chain
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Add Space Sentry To Hand", 1),
                        mod.SStack("On Card Played Add Mini Sentry To Hand", 1),
                        mod.SStack("On Card Played Add Mini Sentry To Hand", 1),
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}