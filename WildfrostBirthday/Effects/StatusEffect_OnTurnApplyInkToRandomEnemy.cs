// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
	public static class StatusEffect_OnTurnApplyInkToRandomEnemy
	{
		public static void Register(WildFamilyMod mod)
		{
			var builder = new StatusEffectDataBuilder(mod)
				.Create<StatusEffectApplyXOnTurn>("On Turn Apply Ink To RandomEnemy")
				.WithText("Apply {0} to a random enemy", SystemLanguage.English)
				.WithText("对一名随机敌人施加{0}", SystemLanguage.ChineseSimplified)
				.WithText("對隨機敵人施加{0}", SystemLanguage.ChineseTraditional)
				.WithText("무작위의 적에게 {0} 부여", SystemLanguage.Korean)
				.WithText("ランダムな敵に{0}を与える", SystemLanguage.Japanese)
				.WithTextInsert("<{a}><keyword=null>")
				.WithStackable(true)
				.WithCanBeBoosted(true)
				.WithOffensive(false)
				.WithMakesOffensive(false)
				.WithDoesDamage(false)
				.SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnTurn>(data =>
				{
					data.effectToApply = mod.TryGet<StatusEffectNull>("Null");
					data.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomEnemy;
					data.waitForAnimationEnd = true;
				});
			mod.assets.Add(builder);
		}
	}
}
