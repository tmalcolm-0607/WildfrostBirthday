using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_RefreshingWater
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-refreshingwater", "Refreshing Water")
                .SetSprites("items/refreshingwater.png", "bg.png")
    
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1),
                        mod.TStack("Zoomlin", 1)
                    };
                    data.attackEffects = new[] {
                        mod.SStack("Cleanse With Text", 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
