# Tribes

This folder will contain modular tribe registration logic for the Wildfrost MadFamily Tribe mod. Each tribe should be defined in its own file/class with a static Register(WildFamilyMod mod) method, following the same pattern as status effects and charms.

- Place one tribe per file for maintainability.
- Update the main mod file to call each tribe's Register method.
- See docs/TribeLogicOverview.md for best practices.
