// Registers the "On Card Played Deal Random Damage To Target (1-6)" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_OnCardPlayedDealRandomDamageToTarget
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnCardPlayed>("On Card Played Deal Random Damage To Target (1-6)")
                .WithText("Deal {0} random damage (1-6) to the target when played", SystemLanguage.English)
                .WithTextInsert("<random 1-6><keyword=damage>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(true)
                .WithMakesOffensive(true)
                .WithDoesDamage(true)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCardPlayed>(data =>
                {
                    data.dealDamage = true;
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                    // Custom scriptableAmount: random 1-6
                    data.scriptableAmount = new ScriptableAmountRandomRange(1, 6);
                });
            mod.assets.Add(builder);
        }
    }
    // Helper: ScriptableAmount that returns a random value in a range (inclusive)
    public class ScriptableAmountRandomRange : ScriptableAmount
    {
        private readonly int min;
        private readonly int max;
        public ScriptableAmountRandomRange(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
        public override int Get(Entity entity)
        {
            return UnityEngine.Random.Range(min, max + 1); // inclusive
        }
    }
}
