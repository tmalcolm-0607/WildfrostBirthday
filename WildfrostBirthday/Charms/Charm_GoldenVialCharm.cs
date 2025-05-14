
using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_GoldenVialCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-goldenvialcharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/golden_vial_charm.png")
                .WithTitle("Golden Vial Charm")
                .WithText("Gain 1 Bling when triggered")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("Collect Bling On Trigger", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
