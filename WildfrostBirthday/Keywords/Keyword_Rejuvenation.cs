using System;
using UnityEngine;

namespace WildfrostBirthday.Keywords
{    public static class Keyword_Rejuvenation
    {
        public static void Register(WildFamilyMod mod)
        {
            // Register the icon for the keyword (Pokefrost style)
            // This assumes the icon is at assets/images/status/rejuvenation.png
            var iconPath = "status/rejuvenation.png";
            var iconName = "rejuvenation";
            // If your mod has a helper for registering icons, call it here (Pokefrost uses CreateIcon)
            // Example: mod.CreateIcon("RejuvenationIcon", ...)

            var builder = new KeywordDataBuilder(mod)
                .Create("rejuvenation")
                .WithTitle("Rejuvenation")
                .WithDescription("Restore {a} health at the end of turn.")
                .WithShowName(false)
                .WithShowIcon(true)
                .WithIconName(iconName)
                .WithCanStack(true);

            mod.assets.Add(builder);
            Debug.Log($"[WildfrostBirthday] Registered 'Rejuvenation' keyword with icon '{iconName}' and path '{iconPath}'");
        }
    }
}
