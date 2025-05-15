using UnityEngine;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_DynamicFamilyCharmBonus
    {
        public static void Register(WildFamilyMod mod)
        {
            var scriptableAmount = ScriptableObject.CreateInstance<ScriptableAmountDynamicFamilyBonus>();
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectInstantIncreaseAttack>("DynamicFamilyCharmBonus")
                .WithText("Gain +2 Health and +2 Attack for every MadFamily leader or companion in your deck or reserve.", SystemLanguage.English)
                .WithTextInsert("<+{a}><keyword=attack>, <+{a}><keyword=health>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectInstantIncreaseAttack>(data =>
                {
                    data.scriptableAmount = scriptableAmount;
                    // If there's a way to also boost health, add it here (or create a second effect for health)
                });
            mod.assets.Add(builder);
        }
    }
}
