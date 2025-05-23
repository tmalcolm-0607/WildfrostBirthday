using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace WildfrostBirthday.Cards
{
    public static class Item_DetonationStrike
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new CardDataBuilder(mod)
                .CreateItem("item-detonationstrike", "Detonation Strike")
                .SetSprites("items/detonationstrike.png", "bg.png")
                .WithFlavour("powerful strike that requires a shell target.")
                .WithText("Target must be Shelled.")
                .WithCardType("Item")
                .WithValue(60)
                .SetDamage(16)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.traits = new List<CardData.TraitStacks> {
                        mod.TStack("Trash", 1),
                        mod.TStack("Recycle", 2)
                    };
                    
                    // Constraint: can only play on targets with Shell
                    var shellConstraint = ScriptableObject.CreateInstance<TargetConstraintHasStatus>();
                    shellConstraint.status = mod.TryGet<StatusEffectData>("Shell");
                    data.targetConstraints = new[] { shellConstraint };
                    data.canPlayOnHand = false;
                    data.canPlayOnEnemy = true;
                    data.playOnSlot = false;
                });
            mod.assets.Add(builder);
        }
    }
}
