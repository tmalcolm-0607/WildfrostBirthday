using System.Collections.Generic; // Needed for List<>
// Other common namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Cards
{
    public static class Item_EnergyCore
    {
        public static void Register(WildFamilyMod mod)
        {            // Create the card builder directly
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-energycore", "Energy Core")
                .SetSprites("items/energycore.png", "bg.png")
                .WithFlavour("A core that radiates energy.")
                .WithCardType("Item")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] { 
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Block"), 1) 
                    };
                    data.startWithEffects = new CardData.StatusEffectStacks[0];
                    data.traits = new List<CardData.TraitStacks>();
                });
                
            mod.assets.Add(builder);
        }
    }
}