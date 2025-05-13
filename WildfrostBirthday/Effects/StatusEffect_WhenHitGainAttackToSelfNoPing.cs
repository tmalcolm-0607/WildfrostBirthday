// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenHitGainAttackToSelfNoPing
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXWhenHit>("When Hit Gain Attack To Self (No Ping)")
                .WithText("When hit, gain {0}", SystemLanguage.English)
                .WithText("受到攻击时，获得{0}", SystemLanguage.ChineseSimplified)
                .WithText("受到攻擊時，獲得{0}", SystemLanguage.ChineseTraditional)
                .WithText("피격 시, {0} 획득", SystemLanguage.Korean)
                .WithText("攻撃された時に{0}を得る", SystemLanguage.Japanese)
                .WithTextInsert("<+{a}><keyword=attack>")
                .WithStackable(true)
                .WithCanBeBoosted(true)
                .WithOffensive(false)        // As an attack effect, this is treated as a buff
                .WithMakesOffensive(false)   // As a starting effect, its entity should target allies
                .WithDoesDamage(false)       // Its entity cannot kill with this effect, eg for Bling Charm
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHit>(data =>
                {
                    data.desc = "When hit, gain <+{0}><keyword=attack>";
                    data.hiddenKeywords = new KeywordData[]
                    {
                        mod.TryGet<KeywordData>("Hit"),
                    };
                    data.effectToApply = mod.TryGet<StatusEffectInstantIncreaseAttack>("Increase Attack");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                    data.doPing = false;
                });
            
            mod.assets.Add(builder);
        }
    }
}
