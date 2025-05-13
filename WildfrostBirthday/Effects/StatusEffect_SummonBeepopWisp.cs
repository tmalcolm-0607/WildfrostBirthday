// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_SummonBeepopWisp
    {
        public static void Register(WildfrostBirthday.WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectSummon>("Summon Beepop Wisp")
                .WithText("{0}", SystemLanguage.English)
                .WithText("{0}", SystemLanguage.ChineseSimplified)
                .WithText("{0}", SystemLanguage.ChineseTraditional)
                .WithText("{0}", SystemLanguage.Korean)
                .WithText("{0}", SystemLanguage.Japanese)
                .WithTextInsert("<card=madfamilymod.wildfrost.madhouse.companion-wisp>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectSummon>(data =>
                {
                    data.eventPriority = 99999;
                    data.summonCard = mod.TryGet<CardData>("companion-wisp");
                    data.gainTrait = mod.TryGet<StatusEffectTemporaryTrait>("Temporary Summoned");
                    data.setCardType = mod.TryGet<CardType>("Summoned");
                    data.effectPrefabRef = new UnityEngine.AddressableAssets.AssetReference("SummonCreateCard");
                });
            mod.assets.Add(builder);
        }
    }
}
