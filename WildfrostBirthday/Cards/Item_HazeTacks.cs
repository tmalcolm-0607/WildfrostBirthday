using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_HazeTacks
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("haze_tacks", "Haze Tacks")
                .SetSprites("items/hazetacks.png", "bg.png")
                .WithFlavour("Apply 3 Teeth and 2 Haze.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] {
                        mod.SStack("Teeth", 3),
                        mod.SStack("Haze", 2)
                    };
                      data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Barrage", 1),
                        mod.TStack("Consume", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
