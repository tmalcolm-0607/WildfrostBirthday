// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_OnTurnAddAttackToSelf
	{
		public static void Register(WildfrostBirthday.WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXOnTurn>("On Turn Add Attack To Self")
				.WithText("Add {0} to self", SystemLanguage.English)
				.WithText("给自己增加{0}", SystemLanguage.ChineseSimplified)
				.WithText("給自己加{0}", SystemLanguage.ChineseTraditional)
				.WithText("자신에게 {0} 추가", SystemLanguage.Korean)
				.WithText("自分に{0}を追加する", SystemLanguage.Japanese)
				.WithTextInsert("<+{a}><keyword=attack>")
				.WithStackable(true)
				.WithCanBeBoosted(true)
				.WithOffensive(false)
				.WithMakesOffensive(false)
				.WithDoesDamage(false)
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectInstantIncreaseAttack>("Increase Attack");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
					data.waitForAnimationEnd = true;
				});
			mod.assets.Add(builder);
		}
	}
}
