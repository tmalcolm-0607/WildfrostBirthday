using System;
using UnityEngine;

namespace WildfrostBirthday.Keywords
{    public static class Keyword_Rejuvenation
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the keyword data
            var builder = new KeywordDataBuilder(mod)
                .Create("rejuvenation") // Internal name must be lowercase
                .WithTitle("Rejuvenation") // Display name with proper capitalization
                .WithDescription("Restore health at the end of turn")
                .WithShowName(true) // Show the name in the tooltip
                .WithCanStack(true); // Can stack like other status effects

            // Add the keyword to the mod's assets
            mod.assets.Add(builder);
            
            Debug.Log($"[WildfrostBirthday] Registered 'Rejuvenation' keyword");
        }
    }
}
