// Registers the "On Card Played Add LuminShard To Hand" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnCardPlayedAddLuminFragmentToHand
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnCardPlayed>("On Card Played Add LuminFragment To Hand")

                .WithTextInsert("Add Lumin Fragment to Hand")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Instant Summon LuminFragment In Hand");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Hand;
                });
            mod.assets.Add(builder);
        }
    }
}
