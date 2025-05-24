using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_ColossalAmoebom
    {
        public static void Register(WildFamilyMod mod)
        {            string cardId = "colossal_amoebom";
            string spritePath = "enemies/colossal_amoebom";
            
            var enemyBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Colossal Amoebom", idleAnim: "GoopAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(555, 2, 13)  // HP, ATK, Counter
                .WithFlavour("A massive, ominous slime with six piercing yellow eyes.")
                .WithCardType("Enemy")
                .WithValue(100)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects - 4 weakness and gain 3 weakness when hit
                    var startEffects = new List<CardData.StatusEffectStacks> {
                        mod.SStack("Weakness", 4),
                        mod.SStack("When Hit Apply Weakness To Self", 3),
                        mod.SStack("When Destroyed Apply Weakness To Allies & Enemies", 2),
                        mod.SStack("Hit All Enemies", 1)
                    };
                    data.startWithEffects = startEffects.ToArray();
                    
                });
                
            mod.assets.Add(enemyBuilder);
        }
    }
}