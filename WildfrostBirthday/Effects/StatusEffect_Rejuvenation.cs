// Registers the "Rejuvenation" status effect for the mod.
// Behaves like Shroom but heals instead of damaging
using System;
using System.Reflection;
using UnityEngine;
using Dead;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_Rejuvenation
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new StatusEffectDataBuilder(mod)                .Create<StatusEffectShroom>("Rejuvenation")
                .WithText("Restores {0} health at the end of turn.")
                .WithIcon("status/rejuvenation.png")
                .WithIconGroupName("health")
                .WithIsStatus(true)
                .WithCanBeBoosted(false)
                .WithStackable(true)
                .WithOffensive(false)       // Not a negative status like Shroom
                .WithMakesOffensive(false)  // Doesn't change targeting behavior
                .WithDoesDamage(false)      // Doesn't do damage, it heals
                .WithVisible(true)
                .SubscribeToAfterAllBuildEvent<StatusEffectShroom>(data =>
                {                    UnityEngine.Debug.Log("[Rejuvenation] Registering status effect...");
                    data.applyFormatKey = Extensions.GetLocalizedString("Card Text", "Apply X");
                    data.removeOnDiscard = true;
                    
                    // Use reflection to set the healInsteadOfDamage property if it exists
                    // This is safer than direct access in case the property doesn't exist
                    PropertyInfo healProperty = typeof(StatusEffectShroom).GetProperty("healInsteadOfDamage", 
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    
                    if (healProperty != null)
                    {
                        healProperty.SetValue(data, true);
                        UnityEngine.Debug.Log("[Rejuvenation] Set healInsteadOfDamage to true");
                    }
                    else 
                    {
                        FieldInfo healField = typeof(StatusEffectShroom).GetField("healInsteadOfDamage", 
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        
                        if (healField != null)
                        {
                            healField.SetValue(data, true);
                            UnityEngine.Debug.Log("[Rejuvenation] Set healInsteadOfDamage field to true");
                        }
                        else
                        {
                            UnityEngine.Debug.LogWarning("[Rejuvenation] Could not find healInsteadOfDamage property or field");
                        }
                    }
                    
                    // Set target constraints to ensure it can be applied properly
                    data.targetConstraints = new TargetConstraint[]
                    {
                        new Scriptable<TargetConstraintCanBeHit>()
                    };
                });
            mod.assets.Add(builder);
        }
    }
      // Helper class to create scriptable objects - same as used by Shroom
    public class Scriptable<T> where T : ScriptableObject, new()
    {
        readonly Action<T>? modifier;
        public Scriptable() { }
        public Scriptable(Action<T> modifier) { this.modifier = modifier; }
        public static implicit operator T(Scriptable<T> scriptable)
        {
            T result = ScriptableObject.CreateInstance<T>();
            scriptable.modifier?.Invoke(result);
            return result;
        }
    }
}
