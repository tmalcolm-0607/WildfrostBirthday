// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_FrostMoonIncreaseMaxCounter
	{
		public static void Register(WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXWhenDeployed>("FrostMoon Increase Max Counter")
				.WithText("When deployed, gain +2 counter", SystemLanguage.English)
				.WithText("When deployed, gain +2 counter", SystemLanguage.ChineseSimplified)
				.WithText("When deployed, gain +2 counter", SystemLanguage.ChineseTraditional)
				.WithText("When deployed, gain +2 counter", SystemLanguage.Korean)
				.WithText("When deployed, gain +2 counter", SystemLanguage.Japanese)
				.WithStackable(true)
				.WithCanBeBoosted(false)
				.WithOffensive(false)      // As an attack effect, this is treated as a buff
				.WithMakesOffensive(false) // As a starting effect, its entity should target allies
				.WithDoesDamage(false)     // Its entity cannot kill with this effect, eg for Bling Charm
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectData>("Increase Max Counter");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
				});
			mod.assets.Add(builder);
		}
	}
}
