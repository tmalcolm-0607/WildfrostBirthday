using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_FamilyCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-familycharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/family_charm.png")
                .WithTitle("Family Charm")
                .WithText("Gain +2 Health and +2 Attack for every MadFamily leader or companion in hand, deck, discard, and reserve.")
                .WithTier(3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Attach the dynamic effect
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        new CardData.StatusEffectStacks(
                            mod.TryGet<StatusEffectData>("DynamicFamilyCharmBonus"), 1)
                    };
                });
            mod.assets.Add(builder);
        }

        // Dynamic bonus logic is now handled by StatusEffect_DynamicFamilyCharmBonus
    }
}
