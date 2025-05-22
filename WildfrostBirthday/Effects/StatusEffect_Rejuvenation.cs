// Registers the "Rejuvenation" status effect for the mod.
// Restores health every turn, counts down by 1 each turn. Positioned like Snow.
using WildfrostBirthday.Helpers;
namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_Rejuvenation
    {
        public static void Register(WildFamilyMod mod)
        {
            // Use StatusEffectApplyXOnTurn with a custom effect that restores health to self when triggered
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnTurn>("Rejuvenation")
                .WithText("Restores {0} health every turn.")
                .WithIcon("status/rejuvenation.png")
                .WithIconGroupName("counter")
                .WithKeyword("rejuvenation")
                .WithTextInsert("<+{a}><keyword=health>")
                .WithIsStatus(true)
                .WithCanBeBoosted(false)
                .WithStackable(true) // Most status effects are stackable
                .WithOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
                {
                    // Debug logging to help diagnose loading issues
                    System.Diagnostics.Debug.WriteLine("[Rejuvenation] Registering status effect...");
                    data.dealDamage = false;
                    if (typeof(ScriptableAmount).Assembly.GetType("ScriptableAmountConstant") is { } constantType)
                    {
                        var amount = (ScriptableAmount)System.Activator.CreateInstance(constantType);
                        constantType.GetField("value").SetValue(amount, 1); // Restore 1 health per turn
                        data.scriptableAmount = amount;
                        System.Diagnostics.Debug.WriteLine($"[Rejuvenation] ScriptableAmountConstant found, set to 1");
                    }
                    else
                    {
                        data.scriptableAmount = null; // fallback, will restore 1 health if not set
                        System.Diagnostics.Debug.WriteLine("[Rejuvenation] ScriptableAmountConstant not found, using default");
                    }
                    // Use StatusEffectInstantHeal for healing, as per modding docs
                    var healEffect = mod.TryGet<StatusEffectData>("Heal");
                    if (healEffect == null)
                    {
                        System.Diagnostics.Debug.WriteLine("[Rejuvenation] Could not find StatusEffectData 'InstantHeal'. Status will not heal!");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[Rejuvenation] Found StatusEffectData 'InstantHeal'.");
                    }
                    data.effectToApply = healEffect;
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                    data.waitForAnimationEnd = true;
                });
            mod.assets.Add(builder);
        }
    }
}
