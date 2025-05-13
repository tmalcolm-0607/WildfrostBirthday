// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenHitApplyOverloadToAttacker
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXWhenHit>("When Hit Apply Overload To Attacker")
                .WithText("When hit, apply {0} to the attacker", SystemLanguage.English)
                .WithText("受到攻击时，对攻击者施加{0}", SystemLanguage.ChineseSimplified)
                .WithText("受到攻擊時，對攻擊者施加{0}", SystemLanguage.ChineseTraditional)
                .WithText("피격 시, 공격자에게 {0} 부여", SystemLanguage.Korean)
                .WithText("攻撃された時、攻撃者に{0}を与える", SystemLanguage.Japanese)
                .WithTextInsert("<{a}><keyword=overload>")
                .WithStackable(true)
                .WithCanBeBoosted(true)
                .WithOffensive(false)        // As an attack effect, this is treated as a buff
                .WithMakesOffensive(false)   // As a starting effect, its entity should target allies
                .WithDoesDamage(false)       // Its entity cannot kill with this effect, eg for Bling Charm
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHit>(data =>
                {
                    data.desc = "When hit, apply <{0}><keyword=overload> to the attacker";
                    data.hiddenKeywords = new KeywordData[]
                    {
                        mod.TryGet<KeywordData>("Hit"),
                    };
                    data.effectToApply = mod.TryGet<StatusEffectOverload>("Overload");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                    data.targetMustBeAlive = false;
                });
            
            mod.assets.Add(builder);
        }
    }
}
