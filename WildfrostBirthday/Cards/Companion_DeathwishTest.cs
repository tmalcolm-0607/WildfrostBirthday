// WildfrostBirthday/Cards/Companion_DeathwishTest.cs
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Companion_DeathwishTest
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateUnit("deathwish", "Deathwish")
                .SetSprites("companions/deathwish.png", "companions/deathwish_bg.png") // You can adjust or provide your own sprite paths
                .SetStats(30, 0, 0) // HP, ATK, Counter
                .WithCardType("Companion")
                .WithFlavour("A test unit for the Deathwish status effect.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("Deathwish", 3)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
