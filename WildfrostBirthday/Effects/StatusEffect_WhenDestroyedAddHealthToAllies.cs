// StatusEffect_WhenDestroyedAddHealthToAllies.cs
// Registers the "When Destroyed Add Health To Allies" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_WhenDestroyedAddHealthToAllies
{
    /// <summary>
    /// Registers the "When Destroyed Add Health To Allies" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectApplyXWhenDestroyed>("When Destroyed Add Health To Allies")
            .WithText("When destroyed, add {0} to all allies", SystemLanguage.English)
            .WithText("被摧毁时，使所有友军增加{0}", SystemLanguage.ChineseSimplified)
            .WithText("被摧毀時，使所有隊友增加{0}", SystemLanguage.ChineseTraditional)
            .WithText("파괴 시, 모든 아군에게 {0} 추가", SystemLanguage.Korean)
            .WithText("壊れた時にすべての味方に{0}を与える", SystemLanguage.Japanese)
            .WithTextInsert("<+{a}><keyword=health>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .WithOffensive(false)        // As an attack effect, this is treated as a buff
            .WithMakesOffensive(false)   // As a starting effect, its entity should target allies
            .WithDoesDamage(false)       // Its entity cannot kill with this effect, eg for Bling Charm
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(data =>
            {
                data.eventPriority = 99;
                data.effectToApply = mod.TryGet<StatusEffectInstantIncreaseMaxHealth>("Increase Max Health");
                data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                data.doPing = false;
                data.targetMustBeAlive = false;
            });
        mod.assets.Add(builder);
    }
}
