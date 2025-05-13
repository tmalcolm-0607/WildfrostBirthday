
using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_PugCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            if (!mod.IsAlreadyRegistered<CardUpgradeData>("charm-pug_charm"))
            {
                var pugCharm = mod.AddCharm("pug_charm", "Pug Charm", "When an ally is hit, apply 1 frost to them", "GeneralCharmPool", "charms/pug_charm", 2)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.effects = new CardData.StatusEffectStacks[]
                        {
                            mod.SStack("When Ally is Hit Apply Frost To Attacker", 1)
                        };
                    });
            }
        }
    }
}
