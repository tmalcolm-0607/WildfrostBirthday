using UnityEngine;

namespace WildfrostBirthday.Effects
{
    // This effect is no longer being used directly - we're using the base game's Increase Max Health effect instead
    // Keeping this file for documentation purposes
    public static class StatusEffect_DynamicFamilyCharmBonusHealth
    {
        public static void Register(WildFamilyMod mod)
        {
            // Just create a marker status effect that shows in the UI but doesn't have any effect
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectData>("DynamicFamilyCharmBonusHealth")
                .WithText("Gain +1 Health for every MadFamily unit in your deck or reserve.", SystemLanguage.English)
                .WithTextInsert("<+{a}><keyword=health>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false);
            
            mod.assets.Add(builder);
            Debug.Log("[FamilyCharm] DynamicFamilyCharmBonusHealth marker effect added to mod assets");
        }
    }
}
