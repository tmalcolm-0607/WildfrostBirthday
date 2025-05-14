# Asset Helpers

Provides utility methods for asset management and retrieval.

## Extension Methods

- **IsAlreadyRegistered<T>**: Checks if an asset with the given ID is already registered.
- **TryGet<T>**: Safely gets an asset of the specified type by name, throwing an exception if not found.

These methods were previously in the WildFamilyMod class but have been moved to their own helper class for better organization and reuse.

```csharp
// Example usage
public class MyComponent 
{
    public static void Register(WildFamilyMod mod)
    {
        // Check if an asset is already registered
        if (!mod.IsAlreadyRegistered<CardData>("my-card"))
        {
            // Try to get an existing asset
            var effect = mod.TryGet<StatusEffectData>("Spice");
            
            // Create a new asset builder
            var builder = new CardDataBuilder(mod)
                // ...configuration...
            
            mod.assets.Add(builder);
        }
    }
}
```

These helpers make it easier to work with Wildfrost's asset system when creating and retrieving mod components.
