// Registers the "Instant Summon SpaceSentry In Hand" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_InstantSummonSpaceSentry
    {
        /// <summary>
        /// Registers the "Instant Summon SpaceSentry In Hand" effect.
        /// </summary>
        public static void Register(WildfrostBirthday.WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectInstantSummon>("Instant Summon SpaceSentry In Hand")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummon>(data =>
                {
                    data.eventPriority = 99999;
                    data.targetSummon = mod.TryGet<StatusEffectSummon>("Summon SpaceSentry");
                });
            mod.assets.Add(builder);
        }
    }
}