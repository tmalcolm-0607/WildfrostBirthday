# Refactoring Guide for WildFamilyMod

## Item Refactoring

### Original Pattern:
`csharp
mod.AddItemCard(
    \
id\, \Name\, \items/sprite\,
    \Description\, value,
    attackSStacks: new[] {
        mod.SStack(\Effect1\, amount1),
        mod.SStack(\Effect2\, amount2)
    }
);
`

### New Pattern:
`csharp
var builder = new CardDataBuilder(mod)
    .CreateItem(\id\, \Name\)
    .SetSprites(\items/sprite.png\, \bg.png\)
    .WithFlavour(\Description\)
    .WithCardType(\Item\) 
    .WithValue(value)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.attackEffects = new CardData.StatusEffectStacks[] {
            mod.SStack(\Effect1\, amount1),
            mod.SStack(\Effect2\, amount2)
        };
        
        // Any other data fields should be here
    });
    
mod.assets.Add(builder);
`

## Charm Refactoring

### Original Pattern:
`csharp
mod.AddCharm(\id\, \Name\, \Description\, \Pool\, \charms/sprite\, tier)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.effects = new CardData.StatusEffectStacks[] {
            mod.SStack(\Effect\, amount)
        };
    });
`

### New Pattern:
`csharp
var builder = new CardUpgradeDataBuilder(mod)
    .Create(\id\)
    .AddPool(\Pool\) 
    .WithType(CardUpgradeData.Type.Charm)
    .WithImage(\charms/sprite.png\)
    .WithTitle(\Name\)
    .WithText(\Description\)
    .WithTier(tier)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.effects = new CardData.StatusEffectStacks[] {
            mod.SStack(\Effect\, amount)
        };
        
        // Any other data fields should be here
    });
    
mod.assets.Add(builder);
`

## Important Notes:
1. Always add \.png\ to sprite paths
2. Always add builder to mod.assets with mod.assets.Add(builder)
3. For traits, use 
ew List<CardData.TraitStacks> instead of arrays
4. Make sure all required imports are included

