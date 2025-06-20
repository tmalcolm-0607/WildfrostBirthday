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
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(20, null, 10)  // HP, ATK, Counter
                .WithCardType("Friendly")
                .WithText("Adds some Lumin Fragments to your hand.")
                .WithFlavour("A radiant companion who grants an object of power amplifying ice crystals if she manages to successfully trigger without being hit.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] 
                    {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("When Hit Apply Null To Self", 10),
                        mod.SStack("On Card Played Add LuminFragment To Hand", 1)
                    };
                });

            mod.assets.Add(companionBuilder);
        }
    }
}