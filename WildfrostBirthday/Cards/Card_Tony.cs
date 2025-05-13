using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Tony
    {
        public static void Register(WildFamilyMod mod)
        {
            // Companion version
            mod.AddFamilyUnit("tony", "Tony", "leaders/tony", 8, 2, 4, 50, "Summon Soulrose")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        mod.SStack("When Deployed Summon Soulrose", 1),
                        mod.SStack("On Turn Summon Soulrose", 1)
                    };
                });
            // Leader version
            mod.AddFamilyUnit("tony", "Tony", "leaders/tony", 8, 2, 4, 50, "Summon Soulrose", isLeader: true)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        mod.SStack("When Deployed Summon Soulrose", 1),
                        mod.SStack("On Turn Summon Soulrose", 1)
                    };
                });
        }
    }
}
