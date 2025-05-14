
using System;
using UnityEngine;

namespace WildfrostBirthday.Charms
{
    public static class Charm_PugCharm
    {
        public static void Register(WildFamilyMod mod)
        {            
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-pugcharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/pug_charm.png")
                .WithTitle("Pug Charm")
                .WithText("When an ally is hit, apply 1 frost to them")
                .WithTier(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("When Ally is Hit Apply Frost To Attacker", 1)
                    };
                });
                
            mod.assets.Add(builder);           
        }
    }
}
