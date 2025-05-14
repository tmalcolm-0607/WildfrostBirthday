// StatusEffect_WhenEnemyIsKilledApplyHealthToAttacker.cs
// Registers the "When Enemy Is Killed Apply Health To Attacker" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_WhenEnemyIsKilledApplyHealthToAttacker
{
    /// <summary>
    /// Registers the "When Enemy Is Killed Apply Health To Attacker" effect.
    /// </summary>
    public static void Register(WildfrostBirthday.WildFamilyMod mod)
    {
        var builder = new StatusEffectDataBuilder(mod)
            .Create<StatusEffectApplyXWhenUnitIsKilled>("When Enemy Is Killed Apply Health To Attacker")
            .WithText("When an enemy is killed, apply {0} to the attacker", SystemLanguage.English)
            .WithText("一名敌人被击杀时，对攻击者施加{0}", SystemLanguage.ChineseSimplified)
            .WithText("敵人被擊殺時，對攻擊者施加{0}", SystemLanguage.ChineseTraditional)
            .WithText("적이 죽으면, 공격자에게 {0} 부여", SystemLanguage.Korean)
            .WithText("敵が倒された時、攻撃者に{0}を与える", SystemLanguage.Japanese)
            .WithTextInsert("<{a}><keyword=health>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .WithOffensive(false)        // As an attack effect, this is treated as a buff
            .WithMakesOffensive(false)   // As a starting effect, its entity should target allies
            .WithDoesDamage(false)       // Its entity cannot kill with this effect, eg for Bling Charm
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenUnitIsKilled>(data =>
            {
                data.desc = "When an enemy is killed, apply <{0}><keyword=health> to the attacker";
                data.effectToApply = mod.TryGet<StatusEffectData>("Increase Max Health");
                data.applyConstraints = new TargetConstraint[]
                {
                    ScriptableObject.CreateInstance<TargetConstraintOnBoard>(),
                    ScriptableObject.CreateInstance<TargetConstraintIsAlive>(),
                };
                data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                data.queue = true;
                data.ally = false;
                data.enemy = true;
            });
        mod.assets.Add(builder);
    }
}