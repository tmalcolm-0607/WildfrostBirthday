
using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_GoldenVialCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var goldenVialCharm = mod.AddCharm("golden_vial", "Golden Vial Charm", "Gain 1 Bling when triggered", "GeneralCharmPool", "charms/golden_vial_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("Collect Bling On Trigger", 1)
                    };
                });
        }
    }
}
