
using System;
using UnityEngine;
using WildfrostBirthday;

namespace WildfrostBirthday.Charms
{
    public static class Charm_DuckCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            var duckCharm = mod.AddCharm("duck_charm", "Duck Charm", "Gain Frenzy, Aimless, and set Attack to 1", "GeneralCharmPool", "charms/duck_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        mod.SStack("When Hit Add Frenzy To Self", 1),
                        mod.SStack("Set Attack", 1),
                        mod.SStack("MultiHit", 1)
                    };
                    data.giveTraits = new CardData.TraitStacks[]
                    {
                        mod.TStack("Aimless", 1)
                    };
                });
        }
    }
}
