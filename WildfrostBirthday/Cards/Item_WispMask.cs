using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_WispMask
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddItemCard(
                "wisp_mask", "Wisp Mask", "items/wispmask",
                "A mask with the ability to summon wisps.", 60,
                traitSStacks: new List<CardData.TraitStacks> {
                    mod.TStack("Consume", 1),
                    mod.TStack("Zoomlin", 1)
                }
            ).SubscribeToAfterAllBuildEvent(data =>
            {
                data.startWithEffects = new[] {
                    mod.SStack("Summon Wisp", 1)
                };
                data.canPlayOnHand = false;
                data.canPlayOnEnemy = false;
                data.playOnSlot = true;
            }).SetDamage(null);
        }
    }
}
