using System.Collections.Generic; // Needed for List<>
// Other common namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Cards
{
    public static class Item_AzulTorch
    {
        public static void Register(WildFamilyMod mod)
        {            // Create the card builder directly
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-azultorch", "Azul Torch")
                .SetSprites("items/azultorch.png", "bg.png")
                .WithFlavour("A torch that applies Overload.")
                .WithCardType("Item")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                .SetDamage(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] { 
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Overload"), 4) 
                    };
                    data.startWithEffects = new CardData.StatusEffectStacks[0];
                    data.traits = new List<CardData.TraitStacks>();
                });
                
            mod.assets.Add(builder);
        }
    }
}
