
using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_PlantCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-plantcharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/plant_charm.png")
                .WithTitle("Plant Charm")
                .WithText("On Turn Add +1 Attack To Self")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("On Turn Add Attack To Self", 1)
                    };
                    // If TargetConstraintHasAttack is available, set the constraint here
                    #if WILDFROST_CONSTRAINTS_AVAILABLE
                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintHasAttack>()
                    };
                    #endif
                });

            mod.assets.Add(builder);
        }
    }
}
