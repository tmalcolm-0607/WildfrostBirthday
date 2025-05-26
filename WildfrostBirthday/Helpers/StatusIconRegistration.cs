// Registers the Rejuvenation status icon in the Pokefrost/Overshroom style
using UnityEngine;
using WildfrostBirthday.Keywords;

namespace WildfrostBirthday.Helpers
{
    public static class StatusIconRegistration
    {
        public static void RegisterRejuvenationIcon(WildFamilyMod mod)
        {
            // Load the sprite from the mod's assets/images/status directory
            var sprite = mod.ImagePath("status/rejuvenation.png").ToSprite();
            var keyword = mod.Get<KeywordData>("rejuvenation");
            // Register the icon with the same type as the keyword's iconName
            // (Pokefrost: this.CreateIcon("OvershroomIcon", ADD.ASprite("overshroomicon"), "overshroom", "shroom", ...))
            mod.CreateIcon(
                name: "rejuvenationicon",
                sprite: sprite,
                type: "rejuvenation",
                copyTextFrom: "shroom", // Use shroom as a template for text overlay
                textColor: Color.black,
                keys: new KeywordData[] { keyword },
                posX: -1
            );
        }
    }
}
