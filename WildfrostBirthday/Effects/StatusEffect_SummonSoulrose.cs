// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_SummonSoulrose
    {
        /// <summary>
        /// Registers the "Summon Soulrose" effect.
        /// </summary>
        public static void Register(WildfrostBirthday.WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectSummon>("Summon Soulrose")
                .WithText("Summon {0}", SystemLanguage.English)
                .WithText("召唤{0}", SystemLanguage.ChineseSimplified)
                .WithText("召喚{0}", SystemLanguage.ChineseTraditional)
                .WithText("{0}을(를) 소환", SystemLanguage.Korean)
                .WithText("{0}を召喚する", SystemLanguage.Japanese)
                .WithTextInsert("<card=madfamilymod.wildfrost.madhouse.companion-soulrose>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)      // As an attack effect, this is treated as a buff
                .WithMakesOffensive(false) // As a starting effect, its entity should target allies
                .WithDoesDamage(false)     // Its entity cannot kill with this effect, eg for Bling Charm
                .SubscribeToAfterAllBuildEvent<StatusEffectSummon>(data =>
                {
                    data.eventPriority = 99999;
                    data.summonCard = mod.TryGet<CardData>("companion-soulrose");
                    data.gainTrait = mod.TryGet<StatusEffectTemporaryTrait>("Temporary Summoned");
                    data.setCardType = mod.TryGet<CardType>("Summoned");
                    data.effectPrefabRef = new UnityEngine.AddressableAssets.AssetReference("SummonCreateCard");
                });
            mod.assets.Add(builder);
        }
    }
}