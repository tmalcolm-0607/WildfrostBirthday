// No usings needed; all required namespaces are provided by GlobalUsings.cs

using System.Collections.Generic;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Caleb
    {
        public static void Register(WildFamilyMod mod)
        {
            // Helper method to get sprite path with potential random variation
            string getSpritePath(string basePath, string cardId)
            {
                if (cardId == "caleb")
                {
                    int randomNumber = Dead.Random.Range(0, 3);
                    return basePath + randomNumber + ".png";
                }
                return basePath + ".png";
            }
            
            // COMPANION VERSION
            string cardId = "caleb";
            string spritePath = "leaders/caleb";
            
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit("companion-" + cardId, "Caleb")
                .SetSprites("companions/caleb0.png", "bg.png")
                .SetStats(8, 0, 6)  // HP, ATK, Counter
                .WithFlavour("When triggered, gain +1 attack. When attacked, apply 1 overload to attacker.")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with effects
                    data.startWithEffects = new[] {
                        mod.SStack("When Hit Apply Overload To Attacker", 1), // Overburn reduced from 2 to 1
                        mod.SStack("When Trigger Gain Attack To Self", 1) // Gain attack on trigger instead of on hit
                    };
                    // Attach dynamic sprite change script
                    var script = ScriptableObject.CreateInstance<CardScriptChangeMainOnCounter>();
                    script.baseImagePath = "companions/caleb";
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
                        mod.SStack("When Trigger Gain Attack To Self", 1) // Gain attack on trigger instead of on hit
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
