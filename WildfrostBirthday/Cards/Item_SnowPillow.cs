using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_SnowPillow
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("Snow_pillow", "Snow Pillow")
                .SetSprites("items/snowpillow.png", "bg.png")
                .WithFlavour("A pillow made of snow.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new CardData.StatusEffectStacks[] {
                        mod.SStack("Heal", 6),
                        mod.SStack("Snow", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
