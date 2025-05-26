// Needed for List<>
using System.Collections.Generic;
using UnityEngine;

// Other common namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Cards
{
    public static class Item_LuminFragment
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-luminfragment", "Lumin Fragment")
                .SetSprites("items/lumin_fragment.png", "bg.png")
                .WithFlavour("A fragment of pure luminice, radiating with energy.")
                .WithCardType("Item")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new CardData.StatusEffectStacks[]
		        {
		            new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Instant Gain Lumin"), 1)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Consume", 1)
                    };
                });
            mod.assets.Add(builder);
        }
    }
}
