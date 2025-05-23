# Keywords

This folder contains keyword definitions for the Wildfrost MadFamily Tribe mod. Each keyword should be defined in its own file/class with a static `Register(WildFamilyMod mod)` method, following the same pattern as status effects and cards.

- Place one keyword per file for maintainability
- Keywords will be automatically registered through the ComponentRegistration system
- Keyword internal names must be lowercase
- Display names (title) should use proper capitalization

## Current Keywords:
- **Rejuvenation**: A healing effect that restores health at the end of turn, similar to how Shroom inflicts damage but in reverse
