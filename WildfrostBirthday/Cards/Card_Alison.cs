using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Alison
    {
        public static void Register(WildFamilyMod mod)
        {
            // Companion version
            mod.AddFamilyUnit("alison", "Alison", "leaders/alison", 9, 3, 3, 50, "Restore 2 HP on kill",
                attackSStacks: new[] { mod.SStack("On Kill Heal To Self", 2) }
            );
            // Leader version
            mod.AddFamilyUnit("alison", "Alison", "leaders/alison", 9, 3, 3, 50, "Restore 2 HP on kill",
                attackSStacks: new[] { mod.SStack("On Kill Heal To Self", 2) },
                isLeader: true
            );
        }
    }
}
