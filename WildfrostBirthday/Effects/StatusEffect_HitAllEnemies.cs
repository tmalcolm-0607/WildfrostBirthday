using UnityEngine;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_HitAllEnemies
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the status effect using the StatusEffectDataBuilder
            var builder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyX>("HitAllEnemies")
                .WithText("Hits all enemies", SystemLanguage.English)
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(true)
                .WithMakesOffensive(true)
                .WithDoesDamage(true)
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyX>(data =>
                {
                    data.dealDamage = true;
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
                });

            // Add the status effect to the mod's assets
            mod.assets.Add(builder);
            
            // Log that the effect was created
            Debug.Log("[WildfrostBirthday] HitAllEnemies status effect registered");
        }
    }
}