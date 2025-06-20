using System;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;

namespace WildfrostBirthday.Effects
{
    public class StatusEffect_GainBlingFromHealthLost
    {
        public void Register(WildfrostMod mod)
        {
            new StatusEffectDataBuilder(mod)
                .Create<StatusEffectApplyXWhenHealthLost>("When Health Lost Apply Equal Bling To Self")
                .WithText("Drops {0} equal to <keyword=health> lost", SystemLanguage.English)
                .WithText("损失<keyword=health>时，获得等同数值的{0}", SystemLanguage.ChineseSimplified)
                .WithText("損失<keyword=health>時，獲得等同{0}", SystemLanguage.ChineseTraditional)
                .WithText("<keyword=health>을 상실하면 동일한 양의 {0} 획득", SystemLanguage.Korean)
                .WithText("<keyword=health>を失った時に同じ{0}を得る", SystemLanguage.Japanese)
                .WithTextInsert("<keyword=blings>")
                .WithStackable(false)
                .WithCanBeBoosted(false)
                .WithOffensive(false)        // As an attack effect, this is treated as a buff
                .WithMakesOffensive(false)   // As a starting effect, its entity should target itself
                .WithDoesDamage(false)       // Its entity cannot kill with this effect, eg for Bling Charm
                .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHealthLost>(data =>
                {
                    	data.effectToApply = mod.Get<StatusEffectInstantGainGold>("Gain Gold");
                        data.applyEqualAmount = true; // Apply equal amount of Bling to the entity
                        data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self; // Apply to self

                });
        }
    }
}
