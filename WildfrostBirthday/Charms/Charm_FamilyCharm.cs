using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_FamilyCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            int bonus = CountMadFamilyCompanions();
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-familycharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/family_charm.png")
                .WithTitle("Family Charm")
                .WithText("Gain +2 Health and +2 Attack for every MadFamily companion in hand, deck, discard, and reserve.")
                .WithTier(3)
                .ChangeDamage(1 * bonus)
                .ChangeHP(2 * bonus);
            mod.assets.Add(builder);
        }

        // Custom script for Family Charm dynamic stat bonus
        // Helper to count MadFamily companions in all player zones at build time
        private static int CountMadFamilyCompanions()
        {
            // TODO: Replace with actual logic to count MadFamily companions in all zones
            // For now, return a default value (e.g., 2)
            return 3;
        }
    }
}
