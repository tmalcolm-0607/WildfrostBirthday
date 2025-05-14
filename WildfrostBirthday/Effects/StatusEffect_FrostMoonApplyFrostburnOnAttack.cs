// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_FrostMoonApplyFrostOnAttack
	{
		public static void Register(WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXOnTurn>("FrostMoon Apply Frost On Attack")
				.WithText("On attack, apply 5 Frost", SystemLanguage.English)
				.WithText("On attack, apply 5 Frost", SystemLanguage.ChineseSimplified)
				.WithText("On attack, apply 5 Frost", SystemLanguage.ChineseTraditional)
				.WithText("On attack, apply 5 Frost", SystemLanguage.Korean)
				.WithText("On attack, apply 5 Frost", SystemLanguage.Japanese)
				.WithStackable(true)
				.WithCanBeBoosted(false)
				.WithOffensive(false)
				.WithMakesOffensive(false)
				.WithDoesDamage(false)
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectFrost>("Frost");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.FrontEnemy;
				});
			mod.assets.Add(builder);
		}
	}
}
