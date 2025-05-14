# Project Tracking

## Overview
This document tracks the progress, tasks, and references for the Wildfrost MadFamily Tribe Mod project. It serves as a central hub for managing the mod's development and documentation.

## Sections
- **[Home Page](index.md)**: Landing page and introduction to the documentation.
- **[CardUpgradeData Documentation](CardUpgradeData.md)**: Detailed documentation of the `CardUpgradeData` class.
- **[StatusEffectData Documentation](StatusEffectData.md)**: Documentation of the `StatusEffectData` class and how status effects are used in the mod.
- **[CardData Documentation](CardData.md)**: Comprehensive documentation of the `CardData` class, a core component of the game.
- **[AddFamilyUnit Guide](AddFamilyUnit.md)**: Guide to using the `AddFamilyUnit` method to create custom units.
- **[Charm Creation Guide](CharmCreation.md)**: Guide to creating and balancing charms using the `AddCharm` method.
- **[Enhanced Charm Creation Guide](EnhancedCharmCreation.md)**: Advanced guide for creating charms using target constraints.
- **[Implementing Status Effects Guide](ImplementingStatusEffects.md)**: Guide for creating and implementing new status effects.
- **[Status Effect Examples](StatusEffectExamples.md)**: Practical examples of different types of status effects used in the mod.
- **[Modding Tools & Techniques](ModdingToolsAndTechniques.md)**: Essential tools, in-game exploration, and commands for modding and debugging.
- **Backlog**: Pending tasks and ideas for future development.
- **Tasks**: Current tasks being worked on.
- **References**: Links to external and internal resources.

## Backlog
- Document the `TraitData` class and how traits are used in the mod.
- Create documentation for `CardData` class, which is central to the mod's functionality.
- Document the core workflow of adding new cards and units to the game.
- Create examples of implementing different types of status effects.
- Create a guide on how constraints work (`TargetConstraint` and derived classes).
- Create visual diagrams showing the relationships between different classes.
- Develop a testing plan for validating charm functionality.
- Document best practices for balancing new charms and units.

- Create a guide for using the debugging tools for testing the mod.
- Diagram the workflow for adding new content to the mod.
- Document the `TraitData` class and its usages in the mod.
- Create a guide for modifying and extending existing game assets.
- Finalize and document a comprehensive testing plan for all features, including edge cases and troubleshooting steps. (See docs/TestingPlan.md, added 2025-05-14)
- Validate all migrated features and document issues as they are found. (Ongoing)

## Recent Progress (2025-05-14)
- Tribe/unit logic for MadFamily audited and confirmed modular (see WildfrostBirthday/Tribes/Tribe_MadFamily.cs).
- Tribe registration, structure, and helpers fully documented in docs/TribeLogicOverview.md.
- All tribe migration/refactor tasks marked complete in tasks.md.

## Updates
- Expanded `CardUpgradeData.md` with examples, interactions, and related links.
- Added examples for creating and assigning upgrades.
- Documented interactions with `CardData`, `TargetConstraint`, and `CardScript`.
- Created comprehensive documentation for `StatusEffectData.md`.
- Created detailed documentation for `CardData.md`, including inner classes and usage examples.
- Created a detailed guide for the `AddFamilyUnit` method with examples and best practices.

## Recent Progress (2025-05-14)
- Tribe/unit logic for MadFamily audited and confirmed modular (see WildfrostBirthday/Tribes/Tribe_MadFamily.cs).
- Tribe registration, structure, and helpers fully documented in docs/TribeLogicOverview.md.
- All tribe migration/refactor tasks marked complete in tasks.md.
- Cards, charms, and effects audited for modular structure and helper usage.
- Modularization/documentation pattern added to CardLogicOverview.md, CharmLogicOverview.md, and EffectLogicOverview.md.
- All relevant migration tasks marked complete in tasks.md.

## References
- **Wildfrost Modding Wiki**: [Link Placeholder]
- **Decompiled Game DLL**: Located in `Reference/Assembly-CSharp/`.
- **Completed Mod Project**: Located in `WildfrostMods-master/`.
- **Tutorials**: Located in `WildfrostModTutorials-master/`.

## Notes
- Ensure all documentation is accurate and up-to-date.
- Use Markdown links to connect related documents and resources.
- Regularly review and update this tracking document as the project progresses.
