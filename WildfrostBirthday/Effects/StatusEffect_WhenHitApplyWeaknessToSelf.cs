// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_WhenHitApplyWeaknessToSelf
{
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectApplyXWhenHit>("When Hit Apply Weakness To Self")
            .WithText("When hit, gain {0}", SystemLanguage.English)
            .WithTextInsert("<{a}><keyword=weakness>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .WithOffensive(false)        // As an attack effect, this is treated as a buff
            .WithMakesOffensive(false)   // As a starting effect, its entity should target allies
            .WithDoesDamage(false)       // Its entity cannot kill with this effect
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHit>(data =>
            {
                data.effectToApply = mod.TryGet<StatusEffectData>("Weakness");
                data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                data.queue = true;
                data.targetMustBeAlive = true;
            });

        mod.assets.Add(builder);
    }
}