
using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_BookCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var bookCharm = mod.AddCharm("book_charm", "Book Charm", "Draw 1 on deploy and each turn", "GeneralCharmPool", "charms/book_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        // No status effects
                    };
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Draw", 1)
                    };
                });
        }
    }
}
