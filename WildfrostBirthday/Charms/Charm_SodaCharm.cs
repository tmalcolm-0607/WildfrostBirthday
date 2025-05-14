


using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_SodaCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-sodacharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/soda_charm.png")
                .WithTitle("Soda Charm")
                .WithText("Gain Barrage, Frenzy x3, Consume. Halve all current effects.")
                .WithTier(3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Barrage", 1),
                        mod.TStack("Consume", 1),
                    };                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("Reduce Effects", 2),
                        mod.SStack("MultiHit", 3)
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
