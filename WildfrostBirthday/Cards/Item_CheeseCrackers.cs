using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_CheeseCrackers
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddItemCard(
                "cheese_crackers", "Cheese Crackers", "items/cheese_crackers",
                "A pack of cheese crackers.", 10,
                startSStacks: new[] {
                    mod.SStack("MultiHit", 2)
                },
                traitSStacks: new List<CardData.TraitStacks>
                {
                    mod.TStack("Aimless", 1)
                }
            ).SubscribeToAfterAllBuildEvent(data =>
            {
                data.attackEffects = new CardData.StatusEffectStacks[]
                {
                    new CardData.StatusEffectStacks(mod.Get<StatusEffectData>("Increase Attack"), 1),
                };
            });
        }
    }
}
