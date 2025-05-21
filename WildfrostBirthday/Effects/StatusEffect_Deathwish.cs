// Registers the "Deathwish" status effect for the mod.
// When the counter reaches 0, the unit takes 999 damage (guaranteed death). Positioned like Snow.
using WildfrostBirthday.Helpers;
namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_Deathwish
    {
        public static void Register(WildFamilyMod mod)
        {
            // Fallback: Use StatusEffectApplyXOnTurn with a custom effect that applies 999 damage to self when triggered
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXOnTurn>("Deathwish")
                .WithText("When counter reaches 0, take fatal damage.")
                .WithType("Snow") // Position and icon group like Snow
                .WithIcon("status/deathwish.png")
                .WithIconGroupName("counter")
                .WithKeyword("deathwish")
                .WithIsStatus(true)
                .WithCanBeBoosted(false)
                .WithStackable(false)
                .WithOffensive(true)
                .WithDoesDamage(true)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
                {
                    data.dealDamage = true;
                    // Use a built-in ScriptableAmountConstant if available, else fallback to null (should default to 1)
                    if (typeof(ScriptableAmount).Assembly.GetType("ScriptableAmountConstant") is { } constantType)
                    {
                        var amount = (ScriptableAmount)System.Activator.CreateInstance(constantType);
                        constantType.GetField("value").SetValue(amount, 999);
                        data.scriptableAmount = amount;
                    }
                    else
                    {
                        data.scriptableAmount = null; // fallback, will deal 1 damage if not set
                    }
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                    data.waitForAnimationEnd = true;
                });
            mod.assets.Add(builder);
        }
    }
}
