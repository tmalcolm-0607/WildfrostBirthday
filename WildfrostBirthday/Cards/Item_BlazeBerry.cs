using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_BlazeBerry
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-blazeberry", "Blaze Berry")
                .SetSprites("items/blazeberry.png", "bg.png")
                .WithFlavour("Reduce Max HP by 4 and MultiHit.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] {
                        mod.SStack("MultiHit", 1),
                        mod.SStack("On Card Played Reduce Max Health", 4)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
