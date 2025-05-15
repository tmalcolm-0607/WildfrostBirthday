// Registers the "Instant Summon FoamBullet In Hand" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_InstantSummonFoamBullet
    {
        /// <summary>
        /// Registers the "Instant Summon FoamBullet In Hand" effect.
        /// </summary>
        public static void Register(WildfrostBirthday.WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectInstantSummon>("Instant Summon FoamBullet In Hand")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectInstantSummon>(data =>
                {
                    data.eventPriority = 99999;
                    data.targetSummon = mod.TryGet<StatusEffectSummon>("Summon FoamBullet");
                });
            mod.assets.Add(builder);
        }
    }
}
