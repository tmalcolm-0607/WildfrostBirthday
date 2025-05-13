


using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_SodaCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var sodaCharm = mod.AddCharm("soda_charm", "Soda Charm", "Gain Barrage, Frenzy x3, Consume. Halve all current effects.", "GeneralCharmPool", "charms/soda_charm", 3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Barrage", 1),
                        mod.TStack("Consume", 1),
                    };

                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("Reduce Effects", 2),
                        mod.SStack("MultiHit", 3)
                    };

                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
                    };
                });
        }
    }
}
