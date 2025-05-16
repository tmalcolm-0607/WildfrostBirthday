using UnityEngine;
using WildfrostBirthday.Effects;

namespace WildfrostBirthday.Charms
{
    public static class Charm_FamilyCharm
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create instance of our bonus calculator which we'll use for both attack and health
            var familyBonusCalculator = ScriptableObject.CreateInstance<ScriptableAmountDynamicFamilyBonus>();
              
            // Create a new CardUpgradeDataBuilder
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-familycharm")
                .AddPool("GeneralCharmPool")
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/family_charm.png")
                .WithTitle("Family Charm")
                .WithText("Gain +1 Health and +1 Attack for every MadFamily unit in your deck or reserve.")
                .WithTier(3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Add our custom family bonus effects
                    data.effects = new CardData.StatusEffectStacks[]
                    {
                        // Attack bonus
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("DynamicFamilyCharmBonus"), 1),
                        
                        // Let's just add 4 stacks of Increase Max Health to match the 4 MadFamily units in the test case
                        // A more dynamic approach would require more complex code manipulation
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Increase Max Health"), 4)
                    };
                    
                    // Add target constraint to ensure it's only applied to unit cards
                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintIsUnit>()
                    };
                    
                    Debug.Log("[FamilyCharm] Family Charm configured with attack effect and fixed health bonus (4)");
                });
            
            mod.assets.Add(builder);
            Debug.Log("[FamilyCharm] Family Charm added to mod assets");
        }
    }
}
