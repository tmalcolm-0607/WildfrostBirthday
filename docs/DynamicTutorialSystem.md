# DynamicTutorialSystem

## Overview
The `DynamicTutorialSystem` class manages dynamic tutorials in the game. It provides functionality for showing, hiding, and tracking tutorial prompts based on player actions and game state.

## Key Properties

### Serialized Fields
- **`redrawTutorial`**: A `Tutorial` object for the redraw tutorial.
- **`moveTutorial`**: A `Tutorial` object for the move tutorial.
- **`recallTutorial`**: A `Tutorial` object for the recall tutorial.
- **`aimlessTutorial`**: A `Tutorial` object for the aimless tutorial.
- **`reactionTutorial`**: A `Tutorial` object for the reaction tutorial.
- **`tutorials`**: An array of all `Tutorial` objects managed by the system.

## Tutorial Class
The `Tutorial` class represents an individual tutorial and includes the following properties and methods:

### Properties
- **`onlyShowOnce`**: A boolean indicating if the tutorial should only be shown once.
- **`turnsRequired`**: The number of turns required before the tutorial is shown.
- **`resetOffset`**: The offset for resetting the tutorial counter.
- **`saveString`**: The key used for saving the tutorial's progress.
- **`stringRef`**: A localized string for the tutorial prompt.
- **`promptAnchor`**: The anchor position for the tutorial prompt.
- **`promptPosition`**: The position of the tutorial prompt.
- **`promptWidth`**: The width of the tutorial prompt.
- **`promptEmote`**: The emote type for the tutorial prompt.
- **`flipEmote`**: The flip direction for the emote.
- **`current`**: The current progress of the tutorial.
- **`currentBool`**: A boolean representing the current state of the tutorial.
- **`shown`**: A boolean indicating if the tutorial is currently shown.
- **`actionDoneThisTurn`**: A boolean indicating if the required action was performed this turn.

### Methods
- **`ResetCount`**: Resets the tutorial counter and hides the prompt.
- **`Load`**: Loads the tutorial's progress from the save system.
- **`Save`**: Saves the tutorial's progress to the save system.
- **`CheckIncreaseCount`**: Increases the tutorial counter if the required action was not performed this turn.
- **`Check`**: Checks if the tutorial conditions are met.
- **`Show`**: Displays the tutorial prompt.
- **`Hide`**: Hides the tutorial prompt.

## Usage
The `DynamicTutorialSystem` is used to:
- Manage dynamic tutorials that adapt to player actions.
- Show and hide tutorial prompts based on game state.
- Save and load tutorial progress.

## References
- `PromptSystem`: Used for creating and managing tutorial prompts.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.