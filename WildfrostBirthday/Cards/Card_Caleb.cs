using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Caleb
    {
        public static void Register(WildFamilyMod mod)
        {
            // Companion version
            mod.AddFamilyUnit("caleb", "Caleb", "leaders/caleb", 8, 0, 6, 50, "When attacked, apply 1 overload to attacker. Gain +1 attack on hit.",
                startSStacks: new[] {
                    mod.SStack("When Hit Apply Overload To Attacker", 2),
                    mod.SStack("When Hit Gain Attack To Self (No Ping)", 1)
                }
            );
            // Leader version
            mod.AddFamilyUnit("caleb", "Caleb", "leaders/caleb", 8, 0, 6, 50, "When attacked, apply 1 overload to attacker. Gain +1 attack on hit.",
                startSStacks: new[] {
                    mod.SStack("When Hit Apply Overload To Attacker", 2),
                    mod.SStack("When Hit Gain Attack To Self (No Ping)", 1)
                },
                isLeader: true
            );
        }
    }
}
