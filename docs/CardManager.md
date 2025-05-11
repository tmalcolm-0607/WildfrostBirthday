# CardManager

## Overview
The `CardManager` class is responsible for managing card-related operations, including pooling, instantiation, and returning cards to the pool. It also handles loading card icons and managing card-related events.

## Key Properties

### Static Properties
- **`cardIcons`**: A dictionary mapping card icon types to their corresponding `GameObject` instances.
- **`startPos`**: A `Vector3` representing the default starting position for cards.
- **`cardPools`**: A dictionary of `ObjectPool<Card>` objects for managing card pooling.
- **`init`**: A boolean indicating whether the `CardManager` has been initialized.

### Constants
- **`SCALE`**: The default scale for cards.
- **`HOVER_SCALE`**: The scale for cards when hovered.

## Key Methods

### Start
Initializes the card pools and loads card icons:
```csharp
public IEnumerator Start()
{
    Transform t = base.transform;
    List<CardType> list = AddressableLoader.GetGroup<CardType>("CardType");
    foreach (CardType cardType in list)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(cardType.prefabRef);
        yield return handle;
        GameObject prefab = handle.Result;
        for (int i = 0; i < 3; i++)
        {
            cardPools.Add($"{cardType.name}{i}", new ObjectPool<Card>(() => Object.Instantiate(prefab, startPos, quaternion.identity, t).GetComponent<Card>(), delegate(Card card)
            {
                card.OnGetFromPool();
                card.entity.OnGetFromPool();
                card.transform.position = startPos;
                card.transform.localRotation = Quaternion.identity;
                card.transform.localScale = Vector3.one;
                card.gameObject.SetActive(value: true);
            }, delegate(Card card)
            {
                card.transform.SetParent(t);
                card.OnReturnToPool();
                card.entity.OnReturnToPool();
                Events.InvokeCardPooled(card);
                card.gameObject.SetActive(value: false);
            }, delegate(Card card)
            {
                Object.Destroy(card.gameObject);
            }, collectionCheck: false, 10, 20));
        }
    }
    LoadCardIcons();
    init = true;
}
```

### LoadCardIcons
Loads card icon prefabs and populates the `cardIcons` dictionary:
```csharp
public void LoadCardIcons()
{
    if (cardIconLoadHandle.IsValid())
    {
        Addressables.Release(cardIconLoadHandle);
    }
    Debug.Log("CardManager Loading Card Icon Prefabs");
    cardIconLoadHandle = Addressables.LoadAssetsAsync<GameObject>("CardIcons", null);
    foreach (GameObject item in cardIconLoadHandle.WaitForCompletion())
    {
        if (item != null)
        {
            StatusIcon component = item.GetComponent<StatusIcon>();
            if ((object)component != null)
            {
                cardIcons[component.type] = item;
            }
        }
    }
    Debug.Log($"{cardIcons.Count} icons loaded");
}
```

### Get
Retrieves a card from the pool and initializes it:
```csharp
public static Card Get(CardData data, CardController controller, Character owner, bool inPlay, bool isPlayerCard)
{
    int num = (isPlayerCard ? CardFramesSystem.GetFrameLevel(data.name) : 0);
    Card card = cardPools[$"{data.cardType.name}{num}"].Get();
    card.frameLevel = num;
    card.entity.data = data;
    card.entity.inPlay = inPlay;
    card.hover.controller = controller;
    card.entity.owner = owner;
    card.frameSetter.Load(num);
    Events.InvokeEntityCreated(card.entity);
    return card;
}
```

### ReturnToPool
Returns a card to the pool:
```csharp
public static bool ReturnToPool(Entity entity, Card card)
{
    if (GameManager.End || entity.inCardPool)
    {
        return false;
    }
    if (!entity.returnToPool)
    {
        Object.Destroy(entity.gameObject);
    }
    return true;
}
```

## Usage
The `CardManager` is used to:
- Manage card pooling for efficient memory usage.
- Load and manage card icons.
- Handle card-related events and interactions.

## References
- `docs/data`: Contains datasets for game data, cards, and references commonly used in modding.