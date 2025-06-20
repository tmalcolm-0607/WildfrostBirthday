using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Etherea
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-etherea";
            string spritePath = "companions/etherea";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Etherea", idleAnim: "FloatAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(6, null, 6)  // HP, ATK, Counter
                .WithFlavour("An enchanted Blue Willow, harnessing the powers of Overload, no enemies are safe from her reach.")
                .WithCardType("Friendly")
                .WithText("Apply 2 <keyword=overload> to a random enemy.")
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    
                    data.startWithEffects = new[]
                    {
                        mod.SStack("On Turn Apply Overload To RandomEnemy", 2),
                        mod.SStack("MultiHit", 2)
                    };
                    
                    
                });
            mod.assets.Add(companionBuilder);
        }
    }
}