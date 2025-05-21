// WildfrostBirthday/Cards/Card_DeathwishTest.cs
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_RejuvenationTest
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateUnit("RejuvenationTest", "RejuvenationTest")
                .SetSprites("companions/rejuvenation.png", "companions/rejuvenation_bg.png") // Adjust sprite paths as needed
                .SetStats(30, 0, 0) // HP, ATK, Counter
                .WithCardType("Companion")
                .WithFlavour("A test unit for the Rejuvenation status effect.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("Rejuvenation", 3)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
