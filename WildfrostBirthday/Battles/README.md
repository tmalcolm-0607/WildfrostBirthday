# Battles

This folder contains modular battle registration logic for the Wildfrost MadFamily Tribe mod. Each battle should be defined in its own file/class with a static Register(WildFamilyMod mod) method, following the same pattern as other components.

- Place one battle per file for maintainability.
- Name files with Battle_ prefix for automatic registration.
- Add XML documentation comments to explain wave composition.
