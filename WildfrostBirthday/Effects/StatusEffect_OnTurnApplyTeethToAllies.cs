// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_OnTurnApplyTeethToAllies
	{
		public static void Register(WildfrostBirthday.WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXOnTurn>("On Turn Apply Teeth To Allies")
				.WithText("Apply {0} to all allies", SystemLanguage.English)
				.WithText("对所有友军施加{0}", SystemLanguage.ChineseSimplified)
				.WithText("對所有隊友施加{0}", SystemLanguage.ChineseTraditional)
				.WithText("모든 아군에게 {0} 부여", SystemLanguage.Korean)
				.WithText("すべての味方に{0}を与える", SystemLanguage.Japanese)
				.WithTextInsert("<{a}><keyword=teeth>")
				.WithStackable(true)
				.WithCanBeBoosted(true)
				.WithOffensive(false)
				.WithMakesOffensive(false)
				.WithDoesDamage(false)
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectSpikes>("Teeth");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
					data.waitForAnimationEnd = true;
				});
			mod.assets.Add(builder);
		}
	}
}
// Removed duplicate/invalid trailing builder chain
