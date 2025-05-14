
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Charms
{
    public static class Charm_BookCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-bookcharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/book_charm.png")
                .WithTitle("Book Charm")
                .WithText("Draw 1 on deploy and each turn")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        // No status effects
                    };
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Draw", 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
