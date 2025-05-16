using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_CheeseCrackers
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-cheesecrackers", "Cheese Crackers")
                .SetSprites("items/cheesecrackers.png", "bg.png")
                .WithFlavour("A pack of cheese crackers.")
                .WithCardType("Item")
                .WithValue(70)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        mod.SStack("MultiHit", 2)
                    };
                    
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Aimless", 1),
                        mod.TStack("Consume", 1)
                    };
                    data.attackEffects = new CardData.StatusEffectStacks[] {
                        new CardData.StatusEffectStacks(mod.Get<StatusEffectData>("Increase Attack"), 1),
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
