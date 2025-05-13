

using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_PizzaCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var pizzaCharm = mod.AddCharm("pizza_charm", "Pizza Charm", "Hits all enemies. Consume.", "GeneralCharmPool", "charms/pizza_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Barrage", 1),
                        mod.TStack("Consume", 1)
                    };

                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
                    };
                });
        }
    }
}
