using System.Collections.Generic; // Needed for List<>
using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_TurboDrive
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-turbo_drive", "Turbo Drive")
                .SetSprites("items/turbo_drive.png", "bg.png")
                .WithFlavour("A device that Overclocks the target, at the cost of their health.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(4)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] {
                        mod.SStack("Reduce Counter", 5),
                        
                    };
                      data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Recycle", 2),
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}