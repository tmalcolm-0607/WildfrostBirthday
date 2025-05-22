// WildfrostBirthday/Cards/Card_Quock.cs
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Cards
{
    public static class Card_Quock
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateUnit("Quock", "Quock")
                .SetSprites("companions/quock.png", "companions/quock_bg.png") // Adjust sprite paths as needed
                .SetStats(3, 2, 2) // HP, ATK, Counter
                .WithCardType("Companion")
                .WithFlavour("A feisty companion with a quick counter and a resistance to snow.")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("ImmuneToSnow", 1)
                    };
                data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Smackback"), 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}