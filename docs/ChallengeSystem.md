# ChallengeSystem

## Overview
The `ChallengeSystem` class manages challenges in the game. It tracks active challenges, checks their completion conditions, and handles saving progress.

## Key Properties

### Instance Properties
- **`activeChallenges`**: A list of currently active challenges.
- **`saveRequired`**: A list of challenges that require saving.

## Key Methods

### OnEnable
Initializes active challenges and registers event listeners:
```csharp
public void OnEnable()
{
    List<string> list = SaveSystem.LoadProgressData<List<string>>("completedChallenges", null) ?? new List<string>();
    activeChallenges = new List<ChallengeData>();
    foreach (ChallengeData allChallenge in GetAllChallenges())
    {
        if (list.Contains(allChallenge.name))
        {
            continue;
        }
        bool flag = true;
        ChallengeData[] requires = allChallenge.requires;
        foreach (ChallengeData challengeData in requires)
        {
            if (!list.Contains(challengeData.name))
            {
                flag = false;
                break;
            }
        }
        if (flag)
        {
            activeChallenges.Add(allChallenge);
        }
    }
    foreach (ChallengeData item in activeChallenges.Where((ChallengeData a) => a.listener.checkType == ChallengeListener.CheckType.CustomSystem))
    {
        item.listener.AddCustomSystem(item, this);
    }
    Events.OnStatChanged += StatChanged;
    Events.OnOverallStatsSaved += OverallStatsChanged;
    Events.OnCampaignSaved += CheckSave;
}
```

### OnDisable
Unregisters event listeners:
```csharp
public void OnDisable()
{
    Events.OnStatChanged -= StatChanged;
    Events.OnOverallStatsSaved -= OverallStatsChanged;
    Events.OnCampaignSaved -= CheckSave;
}
```

### StatChanged
Handles stat changes and checks for mid-run challenge completion:
```csharp
public void StatChanged(string stat, string key, int oldValue, int newValue)
{
    for (int num = activeChallenges.Count - 1; num >= 0; num--)
    {
        ChallengeData challengeData = activeChallenges[num];
        ChallengeListener listener = challengeData.listener;
        if (listener.checkType == ChallengeListener.CheckType.MidRun && listener.Check(stat, key))
        {
            listener.Set(challengeData.name, oldValue, newValue);
            if (ChallengeProgressSystem.GetProgress(challengeData.name) >= challengeData.goal)
            {
                activeChallenges.RemoveAt(num);
                saveRequired.Add(challengeData);
            }
        }
    }
}
```

### OverallStatsChanged
Handles end-of-run challenge completion:
```csharp
public void OverallStatsChanged(CampaignStats stats)
{
    bool flag = false;
    for (int num = activeChallenges.Count - 1; num >= 0; num--)
    {
        ChallengeData challengeData = activeChallenges[num];
        ChallengeListener listener = challengeData.listener;
        if (listener.checkType == ChallengeListener.CheckType.EndOfRun && listener.CheckComplete(stats))
        {
            ChallengeProgressSystem.AddProgress(challengeData.name, 1);
            if (ChallengeProgressSystem.GetProgress(challengeData.name) >= challengeData.goal)
            {
                activeChallenges.RemoveAt(num);
                saveRequired.Add(challengeData);
                flag = true;
            }
        }
    }
    if (flag)
    {
        CheckSave();
    }
}
```

### CheckSave
Saves completed challenges:
```csharp
public void CheckSave()
{
    if (saveRequired.Count <= 0)
    {
        return;
    }
    List<string> list = SaveSystem.LoadProgressData<List<string>>("completedChallenges", null) ?? new List<string>();
    List<string> list2 = SaveSystem.LoadProgressData<List<string>>("townNew", null) ?? new List<string>();
    List<string> list3 = SaveSystem.LoadProgressData<List<string>>("unlocked", null) ?? new List<string>();
    foreach (ChallengeData item in saveRequired)
    {
        list.Add(item.name);
        list2.Add(item.reward.name);
        list3.Add(item.reward.name);
        Events.InvokeChallengeCompletedSaved(item);
    }
    SaveSystem.SaveProgressData("completedChallenges", list);
    SaveSystem.SaveProgressData("townNew", list2);
    SaveSystem.SaveProgressData("unlocked", list3);
    saveRequired.Clear();
}
```

### GetAllChallenges
Retrieves all available challenges:
```csharp
public static IEnumerable<ChallengeData> GetAllChallenges()
{
    return from a in AddressableLoader.GetGroup<ChallengeData>("ChallengeData")
        where a.reward.IsActive
        select a;
}
```

## Usage
The `ChallengeSystem` is used to:
- Track active challenges.
- Check for challenge completion during and after runs.
- Save completed challenges and their rewards.

## References
- `ChallengeData`: Represents individual challenges.
- `ChallengeListener`: Handles challenge-specific logic.
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.