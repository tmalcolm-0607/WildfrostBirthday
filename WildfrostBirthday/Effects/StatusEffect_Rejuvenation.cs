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
                .WithText("Restores health every turn.")
                .WithIcon("status/rejuvenation.png")
                .WithIconGroupName("counter")
                .WithKeyword("rejuvenation")
                .WithTextInsert("<{a}><keyword=health>")
                .WithIsStatus(true)
                .WithCanBeBoosted(false)
                .WithStackable(false)
                .WithOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
                {
                    data.dealDamage = false;
                    // Use a built-in ScriptableAmountConstant if available, else fallback to null (should default to 1)
                    if (typeof(ScriptableAmount).Assembly.GetType("ScriptableAmountConstant") is { } constantType)
                    {
                        var amount = (ScriptableAmount)System.Activator.CreateInstance(constantType);
                        constantType.GetField("value").SetValue(amount, 1); // Restore 1 health per turn
                        data.scriptableAmount = amount;
                    }
                    else
                    {
                        data.scriptableAmount = null; // fallback, will restore 1 health if not set
                    }
                    data.effectToApply = mod.TryGet<StatusEffectData>("Increase Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                    data.waitForAnimationEnd = true;
                });
            mod.assets.Add(builder);
        }
    }
}
