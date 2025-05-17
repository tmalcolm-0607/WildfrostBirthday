// No usings needed; all required namespaces are provided by GlobalUsings.cs

using System.Collections.Generic;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Caleb
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "caleb";
            // Helper method to get sprite path with potential random variation
            string getSpritePath(string basePath, string cardId)
            {
                if (cardId == "caleb")
                {
                    int randomNumber = Dead.Random.Range(0, 3);
                    UnityEngine.Debug.Log("[getSpritePath] Random number for sprite path: " + basePath + randomNumber + ".png");
                    return basePath + randomNumber + ".png";
                }
                UnityEngine.Debug.Log("[getSpritePath] Card ID not recognized, using default sprite path: " + basePath + ".png");
                return basePath + ".png";
            }
            
            // COMPANION VERSION
            string spritePath = "leaders/caleb";
            
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit("companion-" + cardId, "Caleb")
                .SetSprites(getSpritePath("companions/caleb", cardId), "bg.png")
                .SetStats(8, 0, 6)  // HP, ATK, Counter
                .WithFlavour("When triggered, gain +1 attack. When attacked, apply 1 overload to attacker.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Hit Apply Overload To Attacker", 1), // Overburn reduced from 2 to 1
                        mod.SStack("On Turn Apply Attack To Self", 1) // Gain attack at the start of each turn
                    };                    // Attach dynamic sprite change script
                    var script = ScriptableObject.CreateInstance<CardScriptChangeMainOnCounter>();
                    // Add "images/" prefix to match the expected path format
                    script.baseImagePath = "images/companions/caleb";
                    UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] Setting Caleb companion sprite path to {script.baseImagePath}");
                    if (data.createScripts != null)
                    {
                        var scripts = new List<CardScript>(data.createScripts) { script };
                        data.createScripts = scripts.ToArray();
                    }
                    else
                    {
                        data.createScripts = new CardScript[] { script };
                    }
                });
            mod.assets.Add(companionBuilder);
            
            // LEADER VERSION
            var leaderBuilder = new CardDataBuilder(mod)
                .CreateUnit("leader-" + cardId, "Caleb")
                .SetSprites(getSpritePath(spritePath, cardId), "bg.png")
                .SetStats(8, 0, 6)  // HP, ATK, Counter
                .WithFlavour("When triggered, gain +1 attack. When attacked, apply 1 overload to attacker.")
                .WithCardType("Leader")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Hit Apply Overload To Attacker", 1), // Overburn reduced from 2 to 1
                        mod.SStack("On Turn Apply Attack To Self", 1) // Gain attack at the start of each turn
                    };
                    // Leader-specific scripts
                    data.createScripts = new CardScript[]
                    {
                        CardScriptHelpers.GetGiveUpgradeScript(mod),
                        CardScriptHelpers.GetRandomHealthScript(-1, 3),
                        CardScriptHelpers.GetRandomDamageScript(0, 2),
                        CardScriptHelpers.GetRandomCounterScript(-1, 1)
                    };
                });
            mod.assets.Add(leaderBuilder);
        }
    }
}
