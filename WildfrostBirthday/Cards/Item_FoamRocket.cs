using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_FoamRocket
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-foamrocket", "Foam Rocket")
                .SetSprites("items/foamrocket.png", "bg.png")
                .WithFlavour("A rocket with a soft impact.")
                .WithCardType("Item")
                .WithValue(45)
                .SetDamage(2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Noomlin", 1)
                    };                    
                });
                
            mod.assets.Add(builder);
        }
    }
}
