// Template file for charms using the direct builder pattern
// No usings needed; all required namespaces are provided by GlobalUsings.cs

namespace WildfrostBirthday.Charms
{
    public static class Charm_Template
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the charm builder directly
            var builder = new CardUpgradeDataBuilder(mod)
                .Create("charm-template")
                .AddPool("GeneralCharmPool")  // Or specific pool
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage("charms/template.png")
                .WithTitle("Template Charm")
                .WithText("A template charm that shows how to implement charms.")
                .WithTier(1)  // Tier from 1 to 3
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    // Status effects
                    data.effects = new CardData.StatusEffectStacks[] {
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect1"), 2),
                        new CardData.StatusEffectStacks(mod.TryGet<StatusEffectData>("Effect2"), 3)
                    };
                    
                    // Target constraints
                    data.targetConstraints = new TargetConstraint[] {
                        // Add constraints here if needed
                    };
                    
                    // Traits
                    data.giveTraits = new CardData.TraitStacks[] {
                        new CardData.TraitStacks(mod.TryGet<TraitData>("Trait1"), 1)
                    };
                    
                    // Add custom script (example, replace with actual script type if needed)
                    // CardScriptChangeMain script = ScriptableObject.CreateInstance<CardScriptChangeMain>();
                    // data.scripts = new CardScript[] { script };
                });
                
            mod.assets.Add(builder);
        }
    }
}
