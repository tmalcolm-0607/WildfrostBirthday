// No usings needed; all required namespaces are provided by GlobalUsings.cs

using System.Collections.Generic;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Alison
    {
        public static void Register(WildFamilyMod mod)
        {
            // Helper method to get sprite path with potential random variation
            string getSpritePath(string basePath, string cardId)
            {
                if (cardId == "alison")
                {
                    int randomNumber = Dead.Random.Range(0, 3);
                    return basePath + randomNumber + ".png";
                }
                return basePath + ".png";
            }
            
            // COMPANION VERSION
            string cardId = "alison";
            string spritePath = "leaders/alison";
            
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Alison")
                .SetSprites(getSpritePath(spritePath, cardId), "bg.png")
                .SetStats(9, 3, 3)  // HP, ATK, Counter
                .WithFlavour("Restore 2 HP on kill")
                .WithCardType("Friendly")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Attack effects
                    data.attackEffects = new[] {
                        mod.SStack("On Kill Heal To Self", 2)
                    };
                });

            mod.assets.Add(companionBuilder);

            // LEADER VERSION
            var leaderBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Alison")
                .SetSprites(getSpritePath(spritePath, cardId), "bg.png")
                .SetStats(9, 3, 3)  // HP, ATK, Counter
                .WithFlavour("Restore 2 HP on kill")
                .WithCardType("Leader")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Attack effects
                    data.attackEffects = new[] {
                        mod.SStack("On Kill Heal To Self", 2)
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
