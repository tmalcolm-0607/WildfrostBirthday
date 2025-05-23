using System;
using UnityEngine;

namespace WildfrostBirthday.Keywords
{
    public static class Keyword_Rejuvenation
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the keyword data
            var builder = new KeywordDataBuilder(mod)
                .Create("rejuvenation") // Internal name must be lowercase
                .WithTitle("Rejuvenation") // Display name with proper capitalization
                .WithDescription("Restores {0} health at the end of turn.") // Explanation with {0} for stack count                .WithTitleColour(new Color(0.35f, 0.75f, 0.35f)) // Green color for healing
                .WithBodyColour(new Color(0.2f, 0.5f, 0.3f)) // Darker green for body text
                .WithShowName(true) // Show the name in the tooltip
                .WithCanStack(true); // Can stack like Shroom does

            // Add the keyword to the mod's assets
            mod.assets.Add(builder);
            
            Debug.Log($"[WildfrostBirthday] Registered 'Rejuvenation' keyword");
        }
    }
}
