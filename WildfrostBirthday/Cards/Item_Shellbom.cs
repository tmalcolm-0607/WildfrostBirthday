using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_Shellbom
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-shellbom", "Shellbom")
                .SetSprites("items/shellbom.png", "bg.png")
                .WithFlavour("A bomb that fortifies and weakens at once.")
                .SetDamage(0)
                .WithCardType("Item")
                .WithValue(60)
               .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] {
                        mod.SStack("Shell", 8),
                        mod.SStack("Weakness", 1)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = true;
                    data.playOnSlot = false;
                });
                   
            mod.assets.Add(builder);
        }
    }
}
