
using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_FrostMoonCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("frost_moon")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/frost_moon_charm.png")
                .WithTitle("Frost Moon Charm")
                .WithText("Gain +2 Counter and apply 5 Frost on attack")
                .WithTier(3)                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("FrostMoon Increase Max Counter", 2),
                        mod.SStack("FrostMoon Apply Frost On Attack", 5)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
