// Registers the Rejuvenation status icon in the Pokefrost/Overshroom style
using UnityEngine;
using WildfrostBirthday.Keywords;

namespace WildfrostBirthday.Helpers
{
    public static class StatusIconRegistration
    {        public static void RegisterRejuvenationIcon(WildFamilyMod mod)
        {
            Debug.Log("[StatusIconRegistration] Starting Rejuvenation icon registration");
            
            // Load the sprite from the mod's assets/images/status directory
            var spritePath = mod.ImagePath("status/rejuvenation.png");
            Debug.Log($"[StatusIconRegistration] Looking for sprite at: {spritePath}");
            
            var sprite = spritePath.ToSprite();
            if (sprite == null)
            {
                Debug.LogWarning($"[StatusIconRegistration] Sprite not found at {spritePath}, using fallback");
                // Try to use a fallback sprite (health icon)
                if (CardManager.cardIcons.ContainsKey("health"))
                {
                    sprite = CardManager.cardIcons["health"].GetComponent<UnityEngine.UI.Image>().sprite;
                    Debug.Log("[StatusIconRegistration] Using health icon as fallback");
                }
            }
              // Get the keyword - it should be available now that we build it properly
            KeywordData? keyword = null;
            
            try
            {
                keyword = mod.TryGet<KeywordData>("rejuvenation");
                Debug.Log($"[StatusIconRegistration] Found keyword via TryGet: {keyword?.name}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[StatusIconRegistration] Failed to get rejuvenation keyword: {ex.Message}");
            }
            
            if (sprite != null && keyword != null)
            {
                // Register the icon with the same type as the keyword's iconName
                // (Pokefrost: this.CreateIcon("OvershroomIcon", ADD.ASprite("overshroomicon"), "overshroom", "shroom", ...))
                try
                {
                    var iconGameObject = mod.CreateIcon(
                        name: "rejuvenationicon",
                        sprite: sprite,
                        type: "rejuvenation",
                        copyTextFrom: "health", // Use health as a template for text overlay (more appropriate than shroom)
                        textColor: Color.black, // Use standard black text color like other effects
                        keys: new KeywordData[] { keyword },
                        posX: -1
                    );
                    
                    Debug.Log("[StatusIconRegistration] Successfully created Rejuvenation icon");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"[StatusIconRegistration] Failed to create icon: {ex.Message}\n{ex.StackTrace}");
                }
            }
            else
            {
                Debug.LogError($"[StatusIconRegistration] Cannot create icon - sprite: {sprite != null}, keyword: {keyword != null}");
            }
        }
    }
}
