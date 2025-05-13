// StatusEffect_Cleanse.cs
// Registers the "Cleanse" effect for the mod.
using System;
using WildfrostBirthday;

namespace WildfrostBirthday
{
    public static class StatusEffect_Cleanse
    {
        /// <summary>
        /// Registers the "Cleanse" effect.
        /// </summary>
        public static void Register(WildFamilyMod mod)
        {
            mod.AddCopiedStatusEffect<StatusEffectInstantCleanse>(
                "Cleanse", "Cleanse With Text",
                data => { },
                text: "{0}",
                textInsert: "<keyword=cleanse>"
            );
        }
    }
}
