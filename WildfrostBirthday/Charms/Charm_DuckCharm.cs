
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
                .WithText("Set Attack to 1, gain MultiHit equal to original attack, and Aimless.")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Remove all attack, set to 1, add MultiHit X (original attack)
                    var duckScript = ScriptableObject.CreateInstance<CardScriptDuckCharmMultiHit>();
                    data.scripts = new CardScript[] { duckScript };
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Aimless", 1)
                    };
                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintDoesAttack>()
                    };
                });

            mod.assets.Add(builder);
        }
    }
}
