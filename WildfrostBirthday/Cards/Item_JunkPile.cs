using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_JunkPile
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("junk_pile", "Junk Pile")
                .SetSprites("items/junkpile.png", "bg.png")
                .WithFlavour("A pile of junk.")
                .WithCardType("Item")
                .WithValue(45)
                .SubscribeToAfterAllBuildEvent(data =>
                {                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Trash", 10),
                        mod.TStack("Consume", 1),
                        mod.TStack("Zoomlin", 1)
                    };
                });
                
            mod.assets.Add(builder);
        }
    }
}
