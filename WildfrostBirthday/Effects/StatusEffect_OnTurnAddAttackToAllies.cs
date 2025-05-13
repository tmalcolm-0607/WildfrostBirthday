// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_OnTurnAddAttackToAllies
	{
		public static void Register(WildfrostBirthday.WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXOnTurn>("On Turn Add Attack To Allies")
				.WithText("Add {0} to all allies", SystemLanguage.English)
				.WithText("给所有友军增加{0}", SystemLanguage.ChineseSimplified)
				.WithText("給所有隊友加{0}", SystemLanguage.ChineseTraditional)
				.WithText("모든 아군에게 {0} 추가", SystemLanguage.Korean)
				.WithText("すべての味方に{0}を追加する", SystemLanguage.Japanese)
				.WithTextInsert("<+{a}><keyword=attack>")
				.WithStackable(true)
				.WithCanBeBoosted(true)
				.WithOffensive(false)
				.WithMakesOffensive(false)
				.WithDoesDamage(false)
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectInstantIncreaseAttack>("Increase Attack");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
					data.waitForAnimationEnd = true;
				});
			mod.assets.Add(builder);
		}
	}
}
