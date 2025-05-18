// Registers the "On Card Played Add Foam Bullets To Hand" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnCardPlayedAddFoamBulletsToHand
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnCardPlayed>("On Card Played Add Foam Bullets To Hand")
           
                // Removed .WithTextInsert() because item tags are not supported
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Instant Summon FoamBullet In Hand");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                });
            mod.assets.Add(builder);
        }
    }
}
