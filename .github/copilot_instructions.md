#
## AI Agent Best Practices

When using AI agents (such as GitHub Copilot or similar tools) to generate code or documentation:

1. **Propose, Review, and Validate:**
   - Propose changes with clear rationale and context.
   - Review generated code and documentation for accuracy, clarity, and adherence to project rules.
   - Validate that changes do not introduce regressions or inconsistencies.

2. **Avoid Assumptions:**
   - Do not assume intent or requirements—prefer explicitness and request clarification if needed.
   - Cross-reference with existing documentation, code, and project plans before making changes.

3. **Traceability:**
   - Document the reasoning behind significant changes, especially for refactors or migrations.
   - Reference related files, issues, or discussions in commit messages and documentation.

4. **Consistency and Formatting:**
   - Ensure generated code and docs match the project's formatting and naming conventions.
   - Use consistent Markdown structure, code block formatting, and internal linking.

5. **Documentation-First:**
   - Update or create documentation alongside code changes.
   - Add usage examples, references, and links to related resources for all new helpers/utilities.

6. **Testing and Validation:**
   - Propose or update test plans for new features or refactors.
   - Document edge cases, known issues, and validation steps.

7. **Iterative Improvement:**
   - Prefer small, incremental changes with clear review points.
   - Regularly review and refine both code and documentation for maintainability.

8. **Collaboration:**
   - Communicate with team members about major changes, rationale, and alternatives considered.
   - Use tracking files (e.g., `tracking.md`) to log progress and decisions.

> For more details, see the [General Rules](#general-rules) and [Documentation Tasks](#documentation-tasks) sections above.
# Copilot Instructions for Wildfrost MadFamily Tribe Mod

## Purpose
This document outlines the rules and tasks for improving and creating documentation for the Wildfrost MadFamily Tribe Mod project. It serves as a guide for maintaining consistency, accuracy, and thoroughness in all documentation efforts.

## General Rules
1. **Accuracy**:
   - Ensure all information is correct and up-to-date.
   - Cross-reference with the Wildfrost Modding Wiki, decompiled game DLLs, and other provided resources.

2. **Modding Scope**:
   - Never modify files in the `Reference/Assembly-CSharp/` or any decompiled game code folders. All changes must be made in the mod project only.
   - Extend or patch game classes using Harmony or provided modding APIs, not by editing base game code.
   - Build helpers and extension methods in the mod to reduce duplication and centralize logic.
   - Before adding new helpers or utilities, scan the mod project for similar/duplicate code and refactor if possible.
   - Keep mod code modular: separate features (tribes, cards, charms, effects, map nodes) into their own folders and files.
   - Use clear, descriptive naming for all new files, classes, and methods.
   - Document all new helpers/utilities with usage examples and references.
   - Prefer composition and extension over inheritance when possible.
   - Avoid static state unless necessary for performance or singleton patterns.
   - Use partial classes only for very large features that require splitting across files.
   - Always keep the mod's entry point (e.g., `ModEntry.cs` or `WildFamilyMod.cs`) minimal—register features, don't implement them there.
   - Regularly review and refactor methods/definitions for clarity, duplication, and maintainability.
   - When migrating or refactoring, review documentation, tutorials, and reference DLLs for best practices and up-to-date patterns.
   - For each migration/refactor, discuss and document the rationale and best practices in the documentation and tracking files.
   - Use AI agent best practices: propose, review, and validate changes; avoid assumptions; prefer explicitness and traceability; document decisions and alternatives considered.

2. **Consistency**:
   - Use consistent formatting across all Markdown documents.
   - Follow the structure of existing documentation (e.g., `CardUpgradeData.md`).

3. **Clarity**:
   - Write in clear, concise language.
   - Avoid technical jargon unless necessary, and provide explanations for complex terms.

4. **Linking**:
   - Use Markdown links to connect related documents and resources.
   - Include file paths and references to relevant code files.

5. **Versioning**:
   - Update the `tracking.md` document with changes and new tasks.
   - Add a "Last Updated" section to each document.

## Documentation Tasks
1. **Expand Existing Documentation**:
   - Review and verify the accuracy of existing documents (e.g., `CardUpgradeData.md`).
   - Add missing details, such as examples and interactions with other classes.

2. **Create New Documentation**:
   - Document key classes, methods, and systems in the project.
   - Include usage examples, references, and links to related resources.

3. **Investigate Usages**:
   - Identify and document where key classes and methods are used in the project.
   - Provide context for their role and interactions within the mod.

4. **Centralize Tracking**:
   - Maintain the `tracking.md` document as a central hub for progress and tasks.
   - Regularly update it with new tasks, completed work, and references.

5. **Testing and Validation**:
   - Develop a testing plan for validating the mod's functionality.
   - Document edge cases and potential issues.

## Formatting Guidelines
1. **Headers**:
   - Use `#` for the main title, `##` for sections, and `###` for subsections.

2. **Code Blocks**:
   - Use triple backticks (```) for code snippets.
   - Specify the language for syntax highlighting (e.g., ` ```csharp `).

3. **Lists**:
   - Use `-` for unordered lists and numbers for ordered lists.

4. **Links**:
   - Use `[Link Text](URL)` for external links.
   - Use `[Document Name](Relative Path)` for internal links.

## Example Workflow
1. **Investigate a Class**:
   - Locate the class file in the project.
   - Review its definition, fields, methods, and usages.
   - Document the class in a new or existing Markdown file.

2. **Update Documentation**:
   - Verify the accuracy of existing information.
   - Add new sections, such as examples or related classes.
   - Link to relevant resources and files.

3. **Track Progress**:
   - Update `tracking.md` with the completed task.
   - Add new tasks or backlog items as needed.

## Notes
- Use the `docs/` folder for all documentation files.
- Collaborate with team members to ensure comprehensive coverage.
- Regularly review and refine documentation to maintain quality.
