// WildfrostBirthday/Cards/Card_CFRS.cs
using WildfrostBirthday.Helpers;
using System.Collections.Generic;

namespace WildfrostBirthday.Cards
{
    public static class Card_CFRS
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateUnit("C.F.R.S.", "C.F.R.S.")
                .SetSprites("clunkers/cfrs.png", "clunkers/cfrs_bg.png") // Adjust sprite paths as needed
                .SetStats(2, 1, 2) // Scrap, ATK, Counter
                .WithCardType("Clunker")
                .AddPool("FriendlyClunkerPool")
                .WithFlavour("A clunker equipped with Krunker's artillery.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Bombard 1"), 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}