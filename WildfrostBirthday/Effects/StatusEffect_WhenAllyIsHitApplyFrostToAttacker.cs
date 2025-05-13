// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenAllyIsHitApplyFrostToAttacker
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXWhenAllyIsHit>("When Ally is Hit Apply Frost To Attacker")
                .WithText("When an ally is hit, apply {0} to the attacker", SystemLanguage.English)
                .WithText("一名友军受到攻击时，对攻击者施加{0}", SystemLanguage.ChineseSimplified)
                .WithText("隊友受到攻擊時，對攻擊者施加{0}", SystemLanguage.ChineseTraditional)
                .WithText("아군 피격 시, 공격자에게 {0} 부여", SystemLanguage.Korean)
                .WithText("味方が攻撃を受けた時に攻撃者に{0}を与える", SystemLanguage.Japanese)
                .WithTextInsert("<{a}><keyword=frost>")
                .WithStackable(true)
                .WithCanBeBoosted(true)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenAllyIsHit>(data =>
                {
                    data.hiddenKeywords = new KeywordData[]
                    {
                        mod.TryGet<KeywordData>("Hit"),
                    };
                    data.effectToApply = mod.TryGet<StatusEffectFrost>("Frost");
                    data.applyConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintOnBoard>(),
                    };
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                });
            mod.assets.Add(builder);
        }
    }
}