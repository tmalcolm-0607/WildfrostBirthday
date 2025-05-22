// Registers the "InstantHeal" status effect for the mod.
// Heals the unit for the specified amount instantly.
using WildfrostBirthday.Helpers;
namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_InstantHeal
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectInstant>("InstantHeal")
                .WithText("Restore {0} health.")
                .WithIcon("status/heal.png")
                .WithIsStatus(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectInstant>(data =>
                {
                    // If the engine requires, implement healing logic here.
                });
            mod.assets.Add(builder);
        }
    }
}