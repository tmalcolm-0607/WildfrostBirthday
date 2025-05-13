# Charms

This folder will contain modular charm registration logic for the Wildfrost MadFamily Tribe mod. Each charm should be defined in its own file/class with a static Register(WildFamilyMod mod) method, following the same pattern as status effects.

- Place one charm per file for maintainability.
- Update the main mod file to call each charm's Register method.
- See docs/EnhancedCharmCreation.md for best practices.
