// StatusEffect_OnKillHealToSelf.cs
// Registers the "On Kill Heal To Self" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_OnKillHealToSelf
{
	/// <summary>
	/// Registers the "On Kill Heal To Self" effect.
	/// </summary>
	public static void Register(WildfrostBirthday.WildFamilyMod mod)
	{
		var builder = new StatusEffectDataBuilder(mod)
			.Create<StatusEffectApplyXOnKill>("On Kill Heal To Self")
			.WithText("Restore {0} on kill", SystemLanguage.English)
			.WithText("击杀时，恢复{0}", SystemLanguage.ChineseSimplified)
			.WithText("擊殺時，回復{0}", SystemLanguage.ChineseTraditional)
			.WithText("적을 죽이면 {0} 회복", SystemLanguage.Korean)
			.WithText("敵を倒した時に{0}回復する", SystemLanguage.Japanese)
			.WithTextInsert("<{a}><keyword=health>")
			.WithStackable(true)
			.WithCanBeBoosted(true)
			.WithOffensive(false)      // As an attack effect, this is treated as a buff
			.WithMakesOffensive(false) // As a starting effect, its entity should target allies
			.WithDoesDamage(false)     // Its entity cannot kill with this effect, eg for Bling Charm
			.SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnKill>(data =>
			{
				data.eventPriority = -1;
				data.effectToApply = mod.TryGet<StatusEffectInstantHeal>("Heal (No Ping)");
				data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
				data.waitForAnimationEnd = true;
				data.queue = true;
			});
		mod.assets.Add(builder);
	}
}
