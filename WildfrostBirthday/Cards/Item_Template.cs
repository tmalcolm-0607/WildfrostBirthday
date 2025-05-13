// Template file for items using the direct builder pattern

using System.Collections.Generic;
using UnityEngine;
using Dead;

namespace WildfrostBirthday.Cards
{
    public static class Item_Template
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the card builder directly
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-template", "Template Item")
                .SetSprites("items/template.png", "bg.png")
                .WithFlavour("This is a template item.")
                .WithCardType("Item")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                .SetDamage(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Attack effects - apply when the item is used to attack
                    data.attackEffects = new CardData.StatusEffectStacks[] { 
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect1"), 2),
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect2"), 3)
                    };
                    
                    // Start with effects - permanently active
                    data.startWithEffects = new CardData.StatusEffectStacks[] {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect3"), 1)
                    };
                    
                    // Traits
                    data.traits = new List<CardData.TraitStacks> {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Trait1"), 1),
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Trait2"), 2)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
