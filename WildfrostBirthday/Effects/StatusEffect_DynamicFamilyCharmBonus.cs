using UnityEngine;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_DynamicFamilyCharmBonus
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the scriptable amount that will calculate the bonus
            var scriptableAmount = ScriptableObject.CreateInstance<ScriptableAmountDynamicFamilyBonus>();
            Debug.Log("[FamilyCharm] Created ScriptableAmountDynamicFamilyBonus instance");
            
            // Create the attack effect
            var attackBuilder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectInstantIncreaseAttack>("DynamicFamilyCharmBonus")
                .WithText("Gain +1 Attack for every MadFamily unit in your deck or reserve.", SystemLanguage.English)
                .WithTextInsert("<+{a}><keyword=attack>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .SubscribeToAfterAllBuildEvent<StatusEffectInstantIncreaseAttack>(data =>
                {
                    Debug.Log("[FamilyCharm] Configuring DynamicFamilyCharmBonus for Attack");
                    data.scriptableAmount = scriptableAmount;
                });
            mod.assets.Add(attackBuilder);
            
            // Create the health effect
            // Using a regular StatusEffectData and will reference the base game's "Increase Max Health" in Charm_FamilyCharm
            var healthBuilder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectData>("DynamicFamilyCharmBonusHealth")
                .WithText("Gain +1 Health for every MadFamily unit in your deck or reserve.", SystemLanguage.English)
                .WithTextInsert("<+{a}><keyword=health>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false);
            mod.assets.Add(healthBuilder);
            
            // Additional debug log to confirm the status effects were added to assets
            Debug.Log("[FamilyCharm] DynamicFamilyCharmBonus effects (attack and health) added to mod assets");
        }
    }
}
