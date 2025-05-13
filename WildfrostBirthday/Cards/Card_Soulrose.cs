using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Soulrose
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddFamilyUnit("soulrose", "Soulrose", "companions/soulrose", 1, 0, 0, 0,
                "When destroyed, add +1 health to all allies",
                startSStacks: new[] { mod.SStack("When Destroyed Add Health To Allies", 1) }
            );
        }
    }
}
