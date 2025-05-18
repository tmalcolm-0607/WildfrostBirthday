using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Ivy
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-ivy";
            string spritePath = "companions/ivy";
            
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Ivy")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(12, 1, 3)  // HP, ATK, Counter
                .WithFlavour("Applies 2 Shroom to any enemy who attacks her.")
                .WithCardType("Friendly")
                .WithValue(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Pigheaded", 1)
                    };
                    data.startWithEffects = new[] {
                        mod.SStack("When Hit Apply Shroom To Attacker", 2)
                    };
                });
            mod.assets.Add(companionBuilder);
        }
    }
}
