using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Cards
{
    public static class Card_Cassie
    {
        public static void Register(WildFamilyMod mod)
        {
            // Companion version
            mod.AddFamilyUnit("cassie", "Cassie", "leaders/cassie", 5, 1, 3, 50, "Joyful and chaotic, Cassie bounces through battle with ink and impulse.",
                startSStacks: new[] {
                    mod.SStack("MultiHit", 2),
                    mod.SStack("On Turn Apply Ink To RandomEnemy", 2)
                }
            ).SetTraits(new CardData.TraitStacks[] {
                new CardData.TraitStacks(mod.TryGet<TraitData>("Aimless"), 1)
            });
            // Leader version
            mod.AddFamilyUnit("cassie", "Cassie", "leaders/cassie", 5, 1, 3, 50, "Joyful and chaotic, Cassie bounces through battle with ink and impulse.",
                startSStacks: new[] {
                    mod.SStack("MultiHit", 2),
                    mod.SStack("On Turn Apply Ink To RandomEnemy", 2)
                },
                isLeader: true
            ).SetTraits(new CardData.TraitStacks[] {
                new CardData.TraitStacks(mod.TryGet<TraitData>("Aimless"), 1)
            });
        }
    }
}
