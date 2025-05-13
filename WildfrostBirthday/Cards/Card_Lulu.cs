using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Lulu
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddFamilyUnit("lulu", "Lulu", "companions/lulu", 6, 2, 3, 50,
                "Lulu defends her family with snowy retaliation.",
                startSStacks: new[] { mod.SStack("When Ally is Hit Apply Frost To Attacker", 2) }
            );
        }
    }
}
