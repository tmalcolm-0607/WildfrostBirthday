// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_WhenDeployedApplyTeethToSelf
	{
		public static void Register(WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXWhenDeployed>("When Deployed Apply Teeth To Self")
				.WithText("When deployed, gain {0}", SystemLanguage.English)
				.WithText("部署后，获得{0}", SystemLanguage.ChineseSimplified)
				.WithText("部署後，獲得{0}", SystemLanguage.ChineseTraditional)
				.WithText("배치 시, {0} 획득", SystemLanguage.Korean)
				.WithText("配置時に{0}を得る", SystemLanguage.Japanese)
				.WithTextInsert("<{a}><keyword=teeth>")
				.WithStackable(true)
				.WithCanBeBoosted(true)
				.WithOffensive(false)        // As an attack effect, this is treated as a buff
				.WithMakesOffensive(false)   // As a starting effect, its entity should target allies
				.WithDoesDamage(false)       // Its entity cannot kill with this effect, eg for Bling Charm
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectSpikes>("Teeth");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
				});
			mod.assets.Add(builder);
		}
	}
}
