using HarmonyLib;
using UnityEngine;

namespace WildfrostBirthday.Patches
{
    /// <summary>
    /// Debug patch to help diagnose the CardPopUpTarget NullReferenceException
    /// This will help identify what's missing when hovering over status effect icons
    /// </summary>
    [HarmonyPatch(typeof(CardPopUpTarget), "Pop")]
    public static class StatusEffectPopupDebugPatch
    {
        static bool Prefix(CardPopUpTarget __instance)
        {
            try
            {
                // Check if the CardPopUpTarget has the necessary references
                if (__instance == null)
                {
                    Debug.LogError("[StatusEffectPopupDebug] CardPopUpTarget instance is null!");
                    return false;
                }

                // If it's a status effect icon, let's validate its data
                if (__instance.name.Contains("Rejuvenation") || __instance.name.Contains("rejuvenation"))
                {
                    Debug.Log($"[StatusEffectPopupDebug] Attempting to show popup for: {__instance.name}");
                      // Try to get the status effect component if it exists
                    var statusIcon = __instance.GetComponent<StatusIcon>();
                    if (statusIcon != null)
                    {
                        Debug.Log($"[StatusEffectPopupDebug] StatusIcon found - type: {statusIcon.type}");
                        
                        // Try to access the status effect data through reflection or available properties
                        var statusEffect = statusIcon.GetComponent<StatusEffect>();
                        if (statusEffect != null)
                        {
                            Debug.Log($"[StatusEffectPopupDebug] StatusEffect found - name: {statusEffect.name}");
                        }
                        else
                        {
                            Debug.Log("[StatusEffectPopupDebug] No StatusEffect component found");
                        }
                    }
                    else
                    {
                        Debug.Log("[StatusEffectPopupDebug] No StatusIcon component found");
                    }
                }

                return true; // Continue with original method
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[StatusEffectPopupDebug] Exception in debug patch: {ex.Message}\n{ex.StackTrace}");
                return true; // Still try to continue
            }
        }

        static void Postfix(CardPopUpTarget __instance)
        {
            // Log successful popup creation for Rejuvenation
            if (__instance.name.Contains("Rejuvenation") || __instance.name.Contains("rejuvenation"))
            {
                Debug.Log($"[StatusEffectPopupDebug] Successfully created popup for: {__instance.name}");
            }
        }
    }
}
