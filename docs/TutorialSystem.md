# TutorialSystem

## Overview
The `TutorialSystem` class manages the tutorial flow in the game. It tracks tutorial progress, handles scene changes, and dynamically adds tutorial components based on the current context.

## Key Properties

### Serialized Fields
- **`redrawBellHelpKey`**: A localized string for the redraw bell tutorial prompt.
- **`redrawBellHelpEmote`**: The emote type for the redraw bell tutorial prompt.
- **`redrawBellHelpButtonKey`**: A localized string for the redraw bell tutorial button.
- **`redrawBellHelpSprite`**: A sprite for the redraw bell tutorial.
- **`data`**: A `Data` struct containing tutorial progress information (e.g., battle and event numbers).

## Key Methods

### OnEnable
Registers event listeners for tutorial-related events:
```csharp
public void OnEnable()
{
    Events.OnSceneChanged += SceneChanged;
    Events.OnCampaignSaved += CampaignSaved;
    Events.OnCampaignLoaded += CampaignLoaded;
}
```

### OnDisable
Unregisters event listeners:
```csharp
public void OnDisable()
{
    Events.OnSceneChanged -= SceneChanged;
    Events.OnCampaignSaved -= CampaignSaved;
    Events.OnCampaignLoaded -= CampaignLoaded;
}
```

### SceneChanged
Handles scene changes and adds appropriate tutorial components:
```csharp
public void SceneChanged(Scene scene)
{
    string text = scene.name;
    if (!(text == "Battle"))
    {
        if (text == "Event")
        {
            switch (data.eventNumber)
            {
            case 0:
                base.gameObject.AddComponent<TutorialCompanionSystem>();
                break;
            case 2:
                base.gameObject.AddComponent<TutorialItemSystem2>();
                break;
            }
            data.eventNumber++;
        }
        return;
    }
    switch (data.battleNumber)
    {
    case 0:
        base.gameObject.AddComponent<TutorialBattleSystem1>();
        SaveSystem.SaveProgressData("tutorialProgress", 1);
        Events.InvokeTutorialProgress(1);
        break;
    case 1:
        base.gameObject.AddComponent<TutorialBattleSystem2>();
        break;
    case 2:
        base.gameObject.AddComponent<TutorialBattleSystem3>();
        DynamicTutorialSystem dynamicTutorialSystem = UnityEngine.Object.FindObjectOfType<DynamicTutorialSystem>();
        if ((object)dynamicTutorialSystem != null)
        {
            dynamicTutorialSystem.enabled = true;
        }
        SaveSystem.SaveProgressData("tutorialProgress", 2);
        Events.InvokeTutorialProgress(2);
        break;
    }
    data.battleNumber++;
}
```

### CampaignSaved
Saves the tutorial progress:
```csharp
public void CampaignSaved()
{
    SaveSystem.SaveCampaignData(Campaign.Data.GameMode, "tutorialData", data);
}
```

### CampaignLoaded
Loads the tutorial progress:
```csharp
public void CampaignLoaded()
{
    data = SaveSystem.LoadCampaignData(Campaign.Data.GameMode, "tutorialData", default(Data));
}
```

## Usage
The `TutorialSystem` is used to:
- Track and manage tutorial progress.
- Dynamically add tutorial components based on the current scene and progress.
- Save and load tutorial data.

## References
- `TutorialBattleSystem1`, `TutorialBattleSystem2`, `TutorialBattleSystem3`: Components added during specific tutorial battles.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.