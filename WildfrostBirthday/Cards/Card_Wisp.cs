using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Wisp
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddFamilyUnit("wisp", "Wisp", "companions/wisp", 5, 4, 6, 0,
                "When an enemy is killed, apply 4 health to the attacker"
            ).SubscribeToAfterAllBuildEvent(data =>
            {
                data.startWithEffects = new[] {
                    mod.SStack("When Enemy Is Killed Apply Health To Attacker", 4)
                };
            });
        }
    }
}
