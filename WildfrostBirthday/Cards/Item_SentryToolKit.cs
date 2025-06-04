// Needed for List<>
using System.Collections.Generic;
using UnityEngine;

// Other common namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Cards
{
    public static class Item_SentryToolKit
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-sentrytoolkit", "Sentry Toolkit")
                .SetSprites("items/sentrytoolkit.png", "bg.png")
                .WithFlavour("A toolkit containing the parts to craft a sentry turret.")
                .WithCardType("Item")
                .WithText("Add 1 Space Sentry to your hand.")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                    .SubscribeToAfterAllBuildEvent(data =>
                {
                    
                    // On play, add 4 Foam Bullets to hand using the new effect chain
                    data.startWithEffects = new[] {
                        mod.SStack("On Card Played Add Space Sentry To Hand", 1)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Recycle", 2)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
