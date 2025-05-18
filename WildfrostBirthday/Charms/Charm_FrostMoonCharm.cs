
using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_FrostMoonCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-frostmooncharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/frost_moon_charm.png")
                .WithTitle("Frost Moon Charm")
                .WithText("Gain +2 Counter and apply 5 Frost on attack")
                .WithTier(3)                .SubscribeToAfterAllBuildEvent(data =>
                {
                  data.attackEffects = new CardData.StatusEffectStacks[]
                {
                new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Frost"), 5),
            };
            data.effects = new CardData.StatusEffectStacks[]
            {
                mod.SStack("Increase Max Counter", 2)
            };
            data.targetConstraints = new TargetConstraint[]
            {
                ScriptableObject.CreateInstance<TargetConstraintIsUnit>()
            };
        });

            mod.assets.Add(builder);
        }
    }
}
