using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_DynamoRoller
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-dynamoroller", "Dynamo Roller")
                .SetSprites("items/dynamoroller.png", "bg.png")
                .WithFlavour("A roller that hits all enemies.")
                .WithCardType("Item")
                .WithValue(55)
                .SetDamage(10)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Recycle", 3)
                    };
                });
                
            mod.assets.Add(builder);
            // Add custom logic for Barrage if needed
        }
    }
}
