
using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_PlantCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var plantCharm = mod.AddCharm("plant_charm", "Plant Charm", "Gain +1 Attack after attacking", "GeneralCharmPool", "charms/plant_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("On Turn Add Attack To Self", 1)
                    };
                })
                .WithText("On Turn Add {0} Attack To Self");
        }
    }
}
