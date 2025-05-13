using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_BerryCake
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("berry_cake", "Berry Cake")
                .SetSprites("items/berrycake.png", "bg.png")
                .WithFlavour("Increase HP of all allies by 3.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    
                    // Add custom script to increase HP of all allies by 3
                });
                
            mod.assets.Add(builder);
        }
    }
}
