// Registers the "On Card Played Increase Max Health To Allies" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnCardPlayedIncreaseMaxHealthToAllies
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnCardPlayed>("On Card Played Increase Max Health To Allies")
                .WithText("Increase max health of all allies by {0}", SystemLanguage.English)
                .WithTextInsert("<+{a}><keyword=health>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Increase Max Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                });
            mod.assets.Add(builder);
        }
    }
}
