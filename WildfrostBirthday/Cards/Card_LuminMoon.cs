using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_LuminMoon
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-lumin_moon";
            string spritePath = "companions/lumin_moon";
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Lumin Moon")
                .SetSprites(spritePath + ".png", "bg.png") // Adjust sprite paths as needed
                .SetStats(20, null, 6) // HP, ATK, Counter
                .WithCardType("Friendly")
                .WithText("Add a Lumin Fragment to your hand.")
                .WithFlavour("A radiant companion who grants an object of power amplifying ice crystals if she manages to successfully trigger without being hit.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Use the helper methods to get status effect stacks and trait stacks
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("When Hit Apply Null To Self", 6),
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("On Card Played Add LuminFragment To Hand"), 1),

                    };
                    
        
                });
            mod.assets.Add(companionBuilder);
        }
    }
}