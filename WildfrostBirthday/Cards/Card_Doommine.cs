using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_DoomMine
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-doommine";
            string spritePath = "companions/doommine";

            // CLUNKER COMPANION VERSION
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Doom Mine")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(null, null, 0)  // Scrap HP, ATK, Counter
                .WithFlavour("A deadly explosive from the top secret Versions of the KPSF.")
                .WithCardType("Clunker")
                .WithText("When Destroyed, deal 20 damage to the attacker.")
                .WithValue(50)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add traits for scrap HP if needed
                    data.startWithEffects = new[]
                    {
                        mod.SStack("Scrap", 3),
                        mod.SStack("When Destroyed Apply Damage To Attacker", 20),
                    };
                    
                });
            mod.assets.Add(companionBuilder);
        }
    }
}