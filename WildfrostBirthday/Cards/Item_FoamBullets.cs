using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_FoamBullets
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddItemCard(
                "foam_bullets", "Foam Bullets", "items/foam_bullets",
                "A pack of foam bullets.", 10,
                startSStacks: new[] {
                    mod.SStack("Hit All Enemies", 1)
                },
                traitSStacks: new List<CardData.TraitStacks>
                {
                    mod.TStack("Noomlin", 1)
                }
            );
        }
    }
}
