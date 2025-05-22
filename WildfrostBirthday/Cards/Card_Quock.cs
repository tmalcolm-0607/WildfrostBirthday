using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Quock
    {
        public static void Register(WildFamilyMod mod)
        {
            string cardId = "companion-quock";
            string spritePath = "companions/quock";
            var companionBuilder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "quock")
                .SetSprites(spritePath + ".png", spritePath + "_bg.png") // Adjust sprite paths as needed
                .SetStats(3, 2, 2) // HP, ATK, Counter
                .WithCardType("Companion")
                .WithFlavour("A feisty companion with a quick counter and a resistance to snow.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Defensive: Only add non-null status effects and traits
                    var immuneToSnow = mod.SStack("ImmuneToSnow", 1);
                    data.startWithEffects = immuneToSnow != null ? new CardData.StatusEffectStacks[] { immuneToSnow } : new CardData.StatusEffectStacks[0];

                    var smackbackTrait = mod.TryGet<TraitData>("Smackback");
                    data.traits = smackbackTrait != null ? new List<CardData.TraitStacks> { new CardData.TraitStacks(smackbackTrait, 1) } : new List<CardData.TraitStacks>();
                });
            mod.assets.Add(companionBuilder);
        }
    }
}