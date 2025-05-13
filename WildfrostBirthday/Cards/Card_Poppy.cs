using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Poppy
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddFamilyUnit("poppy", "Poppy", "companions/poppy", 11, 2, 4, 50,
                "Ferocious little guardian who fights back hard.",
                startSStacks: new[] { mod.SStack("When Hit Apply Demonize To Attacker", 2) }
            ).SetTraits(new CardData.TraitStacks[] {
                new CardData.TraitStacks(mod.TryGet<TraitData>("Smackback"), 1)
            });
        }
    }
}
