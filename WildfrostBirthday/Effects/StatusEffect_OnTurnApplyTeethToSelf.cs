// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_OnTurnApplyTeethToSelf
	{
		public static void Register(WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXOnTurn>("On Turn Apply Teeth To Self")
				.WithText("Gain {0}", SystemLanguage.English)
				.WithText("获得{0}", SystemLanguage.ChineseSimplified)
				.WithText("獲得{0}", SystemLanguage.ChineseTraditional)
				.WithText("{0} 획득", SystemLanguage.Korean)
				.WithText("{0}を得る", SystemLanguage.Japanese)
				.WithTextInsert("<{a}><keyword=teeth>")
				.WithStackable(true)
				.WithCanBeBoosted(true)
				.WithOffensive(false)
				.WithMakesOffensive(false)
				.WithDoesDamage(false)
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectSpikes>("Teeth");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
					data.waitForAnimationEnd = true;
				});
			mod.assets.Add(builder);
		}
	}
}
