// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_WhenDestroyedApplyWeaknessToAll
{
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectApplyXWhenDestroyed>("When Destroyed Apply Weakness To Allies & Enemies")
            .WithText("When destroyed, apply {0} to all allies and enemies", SystemLanguage.English)
            .WithTextInsert("<{a}><keyword=weakness>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .WithOffensive(true)         // This is considered an offensive effect
            .WithMakesOffensive(false)   // But doesn't make the card target enemies
            .WithDoesDamage(false)       // Not direct damage
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(data =>
            {
                data.eventPriority = 99; // High priority to ensure it triggers before death
                data.effectToApply = mod.TryGet<StatusEffectData>("Weakness");
                data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies | StatusEffectApplyX.ApplyToFlags.Enemies;

                data.doPing = true;      // Show visual effect
                data.targetMustBeAlive = true;
            });

        mod.assets.Add(builder);
    }
}