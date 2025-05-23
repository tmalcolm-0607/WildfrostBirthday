using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_RejuvenationRune
    {        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-rejuvenationrune", "Rejuvenation Rune")
                .SetSprites("items/rejuvenation_rune.png", "bg.png")
                .WithFlavour("A mystical rune that grants healing power.")
                .WithCardType("Item")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] {
                    mod.SStack("Rejuvenation", 3)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = true;
                    data.canPlayOnFriendly = true;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}