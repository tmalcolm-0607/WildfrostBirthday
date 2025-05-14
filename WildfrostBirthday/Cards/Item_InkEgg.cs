// Needed for List<>
using System.Collections.Generic;

// Other common namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Cards
{
    public static class Item_InkEgg
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-inkegg", "Ink Egg")
                .SetSprites("items/inkegg.png", "bg.png")
                .WithFlavour("An egg that applies 7 Ink.")
                .WithCardType("Item")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                .SetDamage(1)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = new[] {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Null"), 7)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1),
                        mod.TStack("Zoomlin", 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
