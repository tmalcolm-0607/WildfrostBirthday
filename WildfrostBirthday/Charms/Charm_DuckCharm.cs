
using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_DuckCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-duckcharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/duck_charm.png")
                .WithTitle("Duck Charm")
                .WithText("Gain Frenzy, Aimless, and set Attack to 1")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("When Hit Add Frenzy To Self", 1),
                        mod.SStack("Set Attack", 1),
                        mod.SStack("MultiHit", 1)
                    };
                      data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Aimless", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
