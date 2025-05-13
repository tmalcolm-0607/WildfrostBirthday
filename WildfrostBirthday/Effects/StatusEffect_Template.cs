// Template file for status effects using the direct builder pattern
// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_Template
{
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        // Create the status effect builder directly
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectApplyXOnKill>("Template Effect")
            .WithText("When a unit is killed, apply 2 Frostburn")
            .WithType("Attack")  // Optional type
            .WithCanBeBoosted(true)  // Can be boosted (optional)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnKill>(data =>
            {
                // Configure the effect's behavior
                data.effectToApply = mod.TryGet<StatusEffectData>("Frostburn");
                data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                // Add target constraints if needed
                data.targetConstraints = new TargetConstraint[] {
                    // Add constraints here if needed
                };
            });
        mod.assets.Add(builder);
    }
}
