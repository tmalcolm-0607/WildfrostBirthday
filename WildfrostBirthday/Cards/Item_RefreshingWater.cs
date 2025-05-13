using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_RefreshingWater
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddItemCard(
                "refreshing_water", "Refreshing Water", "items/refreshingwater",
                "A bottle of refreshing water.", 40,
                traitSStacks: new List<CardData.TraitStacks> {
                    mod.TStack("Consume", 1),
                    mod.TStack("Zoomlin", 1)
                }
            ).SubscribeToAfterAllBuildEvent(data =>
            {
                data.attackEffects = new[] {
                    mod.SStack("Cleanse With Text", 1)
                };
            });
        }
    }
}
