using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Kaylee
    {
        public static void Register(WildFamilyMod mod)
        {
            // Companion version
            mod.AddFamilyUnit("kaylee", "Kaylee", "leaders/kaylee", 7, 4, 7, 50, "Sharp-witted and sharper-fanged, Kaylee boosts all allies' bite.",
                startSStacks: new[] {
                    mod.SStack("When Deployed Apply Teeth To Self", 4),
                    mod.SStack("On Turn Apply Teeth To Allies", 2)
                }
            );
            // Leader version
            mod.AddFamilyUnit("kaylee", "Kaylee", "leaders/kaylee", 7, 4, 7, 50, "Sharp-witted and sharper-fanged, Kaylee boosts all allies' bite.",
                startSStacks: new[] {
                    mod.SStack("When Deployed Apply Teeth To Self", 4),
                    mod.SStack("On Turn Apply Teeth To Allies", 2)
                },
                isLeader: true
            );
        }
    }
}
