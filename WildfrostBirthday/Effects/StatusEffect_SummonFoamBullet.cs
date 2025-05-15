// Registers the "Summon FoamBullet" effect for the mod.
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_SummonFoamBullet
    {
        /// <summary>
        /// Registers the "Summon FoamBullet" effect.
        /// </summary>
        public static void Register(WildfrostBirthday.WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectSummon>("Summon FoamBullet")
                .WithText("Summon {0}", SystemLanguage.English)
                .WithTextInsert("<card=FoamBullet>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectSummon>(data =>
                {
                    data.eventPriority = 99999;
                    data.summonCard = mod.TryGet<CardData>("item-foambullets");
                    data.gainTrait = mod.TryGet<StatusEffectTemporaryTrait>("Temporary Summoned");
                    data.setCardType = mod.TryGet<CardType>("Item");
                    data.effectPrefabRef = new UnityEngine.AddressableAssets.AssetReference("SummonCreateCard");
                });
            mod.assets.Add(builder);
        }
    }
}
