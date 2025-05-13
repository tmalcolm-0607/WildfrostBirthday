using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_PlywoodSheet
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("plywood_sheet", "Plywood Sheet")
                .SetSprites("items/plywood.png", "bg.png")
                .WithFlavour("Add 3 Junk to your hand.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                      // Add custom script to add 3 Junk to hand
                });
                
            mod.assets.Add(builder);
        }
    }
}
