using System;
using Dead;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenDeployedSummonWowee
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXWhenDeployed>("When Deployed Summon Wowee")
                .WithText("When deployed, summon {0}", SystemLanguage.English)
                .WithText("部署后，召唤{0}", SystemLanguage.ChineseSimplified)
                .WithText("部署後，召喚{0}", SystemLanguage.ChineseTraditional)
                .WithText("배치 시, {0} 소환", SystemLanguage.Korean)
                .WithText("配置時に{0}を召喚する", SystemLanguage.Japanese)
                .WithTextInsert("<card=Fallow>")
                .WithStackable(true)
                .WithCanBeBoosted(false)
                .WithOffensive(false)        // As an attack effect, this is treated as a buff
                .WithMakesOffensive(false)   // As a starting effect, its entity should target allies
                .WithDoesDamage(false)       // Its entity cannot kill with this effect, eg for Bling Charm
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(data =>
                {
                    data.eventPriority = 99999;
                    data.effectToApply = mod.TryGet<StatusEffectInstantSummon>("Instant Summon Fallow");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                    data.waitForAnimationEnd = true;
                    data.queue = true;
                });
            mod.assets.Add(builder);
        }
    }
}