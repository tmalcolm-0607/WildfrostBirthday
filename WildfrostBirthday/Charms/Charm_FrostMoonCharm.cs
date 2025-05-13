
using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_FrostMoonCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var frostMoonCharm = mod.AddCharm("frost_moon", "Frost Moon Charm", "Gain +2 Counter and apply 5 Frost on attack", "GeneralCharmPool", "charms/frost_moon_charm", 3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("FrostMoon Increase Max Counter", 2),
                        mod.SStack("FrostMoon Apply Frost On Attack", 5)
                    };
                });
        }
    }
}
