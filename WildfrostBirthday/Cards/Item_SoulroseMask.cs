using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_SoulroseMask
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-soulrosemask", "Soulrose Mask")
                .SetSprites("items/soulrosemask.png", "bg.png")
                .WithFlavour("A mask that summons a Soulrose.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(null)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                   
                    
                    data.startWithEffects = new[] {
                        mod.SStack("Summon Soulrose", 1)
                    };                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = true;
                });
                
            mod.assets.Add(builder);
        }
    }
}
