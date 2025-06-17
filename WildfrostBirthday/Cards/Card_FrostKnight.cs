using System;
using System.Collections.Generic;
using UnityEngine;
using WildfrostBirthday.Helpers;


namespace WildfrostBirthday.Cards
{

    public static class Card_FrostKnight
    {        
        public static void Register(WildFamilyMod mod)
        {
            // First, register the phase transition status effect
            var phaseBuilder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectNextPhase>("FrostBossPhase2")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .WithType("nextphase")
                .SubscribeToAfterAllBuildEvent<StatusEffectNextPhase>(data =>
                {
                    data.preventDeath = true;
                    data.nextPhase = mod.TryGet<CardData>("frost_knight_2");
                    // Let the game handle the default transformation animation
                    data.animation = null;
                });

            mod.assets.Add(phaseBuilder);
            string cardId = "frost_knight";
            string spritePath = "enemies/frost_knight";

            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "The Frost Knight", idleAnim: "GiantAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(40, 7, 6)  // HP: 40, ATK: 7, Counter: 6
                .WithFlavour("An ancient warrior encased in the armor of the frost itself, wielding a violet sword to lethally numb those who oppose him.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Start with ImmuneToSnow 1
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("FrostBossPhase2", 1)
                    };

                    // Attack effect: Apply 7 Frost
                    data.attackEffects = new[] {
                        mod.SStack("Frost", 7)
                    };
                });            mod.assets.Add(builder);
        }
    }
}
