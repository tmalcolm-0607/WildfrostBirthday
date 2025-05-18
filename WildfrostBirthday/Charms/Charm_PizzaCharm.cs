

using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_PizzaCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-pizzacharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/pizza_charm.png")
                .WithTitle("Pizza Charm")
                .WithText("Change this card's targeting mode. Consume.")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Consume", 1)
                    };
                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
                    };
                    // Add the status effect that changes target mode (like Nova)
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        new CardData.StatusEffectStacks(mod.Get<StatusEffectData>("Hit All Enemies"), 1)
                    };
                });

            mod.assets.Add(builder);
        }
    }
}
