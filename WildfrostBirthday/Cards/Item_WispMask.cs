using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_WispMask
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("wisp_mask", "Wisp Mask")
                .SetSprites("items/wispmask.png", "bg.png")
                .WithFlavour("A mask with the ability to summon wisps.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1),
                        mod.TStack("Zoomlin", 1)
                    };
                    
                    data.startWithEffects = new[] {
                        mod.SStack("Summon Wisp", 1)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = true;
                });
                
            mod.assets.Add(builder);
        }
    }
}
