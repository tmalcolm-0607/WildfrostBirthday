
using System.Collections.Generic; // Needed for List<>
// Other common namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Cards
{
    public static class Item_AzulBook
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-azulbook", "Azul Book")
                .SetSprites("items/azulbook.png", "bg.png")
                .WithFlavour("A book that applies Overload.")
                .WithCardType("Item")
                .WithValue(45)
                .AddPool("GeneralItemPool")
                .SetDamage(0)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = new[] {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Overload"), 1)
                    };
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Barrage", 1)
                    };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = false;
                    data.playOnSlot = true;
                });
            mod.assets.Add(builder);
        }
    }
}