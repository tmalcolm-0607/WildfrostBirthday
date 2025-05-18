// StatusEffect_SummonWisp.cs
// Registers the "Summon Wisp" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

public static class StatusEffect_SummonWisp
{
	/// <summary>
	/// Registers the "Summon Wisp" effect.
	/// </summary>
	public static void Register(WildfrostBirthday.WildFamilyMod mod)
	{
		var builder = new StatusEffectDataBuilder(mod)
			.Create<StatusEffectSummon>("Summon Wisp")
			.WithText("{0}", SystemLanguage.English)
			.WithText("{0}", SystemLanguage.ChineseSimplified)
			.WithText("{0}", SystemLanguage.ChineseTraditional)
			.WithText("{0}", SystemLanguage.Korean)
			.WithText("{0}", SystemLanguage.Japanese)
			.WithTextInsert("Summon <card=madfamilymod.wildfrost.madhouse.companion-wisp>")
			.WithStackable(false)
			.WithCanBeBoosted(false)
			.WithOffensive(false)
			.WithMakesOffensive(false)
			.WithDoesDamage(false)
			.SubscribeToAfterAllBuildEvent<StatusEffectSummon>(data =>
			{
				data.eventPriority = 99999;
				data.summonCard = mod.TryGet<CardData>("madfamilymod.wildfrost.madhouse.companion-wisp");
				data.gainTrait = mod.TryGet<StatusEffectTemporaryTrait>("Temporary Summoned");
				data.setCardType = mod.TryGet<CardType>("Summoned");
				data.effectPrefabRef = new UnityEngine.AddressableAssets.AssetReference("SummonCreateCard");
			});
		mod.assets.Add(builder);
	}
}
