// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_CollectBlingOnTrigger
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnTurn>("Collect Bling On Trigger")
                .WithText("Gain 1 Bling when triggered", SystemLanguage.English)
                .WithText("Gain 1 Bling when triggered", SystemLanguage.ChineseSimplified)
                .WithText("Gain 1 Bling when triggered", SystemLanguage.ChineseTraditional)
                .WithText("Gain 1 Bling when triggered", SystemLanguage.Korean)
                .WithText("Gain 1 Bling when triggered", SystemLanguage.Japanese)
                .WithTextInsert("<+1><keyword=bling>")
                .WithStackable(true)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectInstantGainGold>("Gain Gold");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                });
            mod.assets.Add(builder);
        }
    }
}
