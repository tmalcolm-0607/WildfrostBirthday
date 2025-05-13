// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_FrostMoonApplyFrostburnOnAttack
	{
		public static void Register(WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXOnTurn>("FrostMoon Apply Frostburn On Attack")
				.WithText("On attack, apply 5 Frostburn", SystemLanguage.English)
				.WithText("On attack, apply 5 Frostburn", SystemLanguage.ChineseSimplified)
				.WithText("On attack, apply 5 Frostburn", SystemLanguage.ChineseTraditional)
				.WithText("On attack, apply 5 Frostburn", SystemLanguage.Korean)
				.WithText("On attack, apply 5 Frostburn", SystemLanguage.Japanese)
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
