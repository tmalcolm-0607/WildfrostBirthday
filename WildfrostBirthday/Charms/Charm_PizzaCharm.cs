

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
                .WithText("Hits all enemies. Consume.")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Barrage", 1),
                        mod.TStack("Consume", 1)
                    };
                    
                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
