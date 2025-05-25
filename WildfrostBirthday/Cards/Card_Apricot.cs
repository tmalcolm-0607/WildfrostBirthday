using System;
using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Card_Apricot
    {
        public static void Register(WildFamilyMod mod)
        {
            // First, register the phase transition status effect
            var phaseBuilder = new StatusEffectDataBuilder(mod)
                .Create<StatusEffectNextPhase>("ShellBossPhase2")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)
                .WithMakesOffensive(false)
                .WithDoesDamage(false)
                .WithType("nextphase")
                .SubscribeToAfterAllBuildEvent<StatusEffectNextPhase>(data =>
                {
                    data.preventDeath = true;
                    data.nextPhase = mod.TryGet<CardData>("apricot_2");
                    // Let the game handle the default transformation animation
                    data.animation = null;
                });

            mod.assets.Add(phaseBuilder);

            string cardId = "apricot";
            string spritePath = "enemies/apricot";

            var builder = new CardDataBuilder(mod)
                .CreateUnit(cardId, "Apricot", idleAnim: "GiantAnimationProfile")
                .SetSprites(spritePath + ".png", "bg.png")
                .SetStats(5, 5, 6)  // HP: 5, ATK: 5, Counter: 6
                .WithFlavour("Apricot remains sealed inside her prison, awaiting release. In the meantime, she harrasses her foes with extra damage equivalent to how hard the shell prison remains.")
                .WithCardType("Boss")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Starting effects
                    data.startWithEffects = new[] {
                        mod.SStack("ImmuneToSnow", 1),
                        mod.SStack("Shell", 10),
                        mod.SStack("ShellBossPhase2", 1),
                        mod.SStack("Bonus Damage Equal To Shell", 1)
                    };

                    // Set traits - Backline
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Backline", 1)
                    };
                });            mod.assets.Add(builder);
        }
    }
}
