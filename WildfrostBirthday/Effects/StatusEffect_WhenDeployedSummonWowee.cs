using System;

namespace WildfrostBirthday.Effects
{
    public static class StatusEffect_WhenDeployedSummonWowee
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddCopiedStatusEffect<StatusEffectApplyXWhenDeployed>(
                "When Deployed Summon Wowee", "When Deployed Summon Soulrose",
                data =>
                {
                    data.effectToApply = mod.TryGet<StatusEffectData>("Instant Summon Soulrose");
                },
                text: "Summon",
                textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-soulrose>"
            );
        }
    }
}
