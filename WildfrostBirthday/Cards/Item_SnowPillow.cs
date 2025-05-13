using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_SnowPillow
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.AddItemCard(
                "Snow_pillow", "Snow Pillow", "items/snowpillow",
                "A pillow made of snow.", 50,
                attackSStacks: new[] {
                    mod.SStack("Heal", 6),
                    mod.SStack("Snow", 1)
                }
            );
        }
    }
}
