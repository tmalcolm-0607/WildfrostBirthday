﻿// MadFamily Tribe Mod - Wildfrost
using WildfrostBirthday.Helpers;
using WildfrostBirthday.Patches;

namespace WildfrostBirthday
{

    public class WildFamilyMod : WildfrostMod
    {
        // Persistent UI root for mod icons (Pokefrost-style)
        public static GameObject? UIRoot;

        public WildFamilyMod(string modDirectory) : base(modDirectory) 
        {
            Instance = this;
            // Apply Harmony patches for debugging and functionality
            HarmonyInstance.PatchAll(typeof(StatusEffectPopupDebugPatch));
        }
        public static WildFamilyMod? Instance;

        public override string GUID => "madfamilymod.wildfrost.madhouse";
        public override string[] Depends => new string[] { };
        public override string Title => "MadHouse Family Tribe";
        public override string Description => "Mod made by the MadHouse Family for us to play with our family in game. Made for our Mother who is the greatest of all time";

        private bool preLoaded = false;
public List<object> assets = new List<object>();

        public override void Load()

        {            if (!preLoaded)
            {
                // Create persistent UI root for icons if not already present
                if (UIRoot == null)
                {
                    UIRoot = new GameObject("WildfrostBirthdayUI");
                    GameObject.DontDestroyOnLoad(UIRoot);
                    UIRoot.SetActive(false);
                }
                
                // IMPORTANT: Register components in the correct order
                // Keywords must be registered FIRST, then status effects, then icons
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllKeywords(this);
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllStatusEffects(this);
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllCards(this);
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllItems(this);
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllCharms(this);
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllBattles(this);
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllTribes(this);
                
                // Register campaign node types (not included in RegisterAllComponents)
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllCampaignNodeTypes(this);
                
                // Register the Rejuvenation status icon AFTER keywords and status effects are registered
                
                
                preLoaded = true;
            }

            base.Load();

            WildfrostBirthday.Helpers.StatusIconRegistration.RegisterRejuvenationIcon(this);
            Events.OnEntityCreated += FixImage;
            GameMode gameMode = TryGet<GameMode>("GameModeNormal"); //GameModeNormal is the standard game mode. 
            gameMode.classes = gameMode.classes.Append(TryGet<ClassData>("MadFamily")).ToArray();

            // Integrate our battle into the game mode
            IntegrateBattleIntoGameMode(gameMode);
        }

        public override void Unload()
        {
            base.Unload();

            GameMode gameMode = TryGet<GameMode>("GameModeNormal");
            gameMode.classes = RemoveNulls(gameMode.classes); //Without this, a non-restarted game would crash on tribe selection            

            UnloadFromClasses();
        }

        //Credits to Hopeful for this method
        public override List<T> AddAssets<T, Y>()
        {
            if (assets.OfType<T>().Any())
                Debug.LogWarning($"[{Title}] adding {typeof(Y).Name}s: {assets.OfType<T>().Count()}");
            return assets.OfType<T>().ToList();
        }

        public void UnloadFromClasses()
        {
            List<ClassData> tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
            foreach (ClassData tribe in tribes)
            {
                if (tribe == null || tribe.rewardPools == null) { continue; } //This isn't even a tribe; skip it.

                foreach (RewardPool pool in tribe.rewardPools)
                {
                    if (pool == null) { continue; }
                    ; //This isn't even a reward pool; skip it.

                    pool.list.RemoveAllWhere((item) => item == null || item.ModAdded == this); //Find and remove everything that needs to be removed.
                }
            }
        }

        public T TryGet<T>(string name) where T : DataFile
        {
            T? data;
            if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
                data = Get<StatusEffectData>(name) as T;
            else if (typeof(KeywordData).IsAssignableFrom(typeof(T)))
                data = (AddressableLoader.Get<KeywordData>("KeywordData", Extensions.PrefixGUID(name, this).ToLower()) ?? Get<KeywordData>(name.ToLower())) as T;
            else
                data = Get<T>(name);

            if (data == null)
                throw new Exception($"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{name}] or [{Extensions.PrefixGUID(name, this)}]");
            return data;
        }

        public CardData.StatusEffectStacks SStack(string name, int amount) => new CardData.StatusEffectStacks(TryGet<StatusEffectData>(name), amount);

        public CardData.TraitStacks TStack(string name, int amount) => new CardData.TraitStacks(TryGet<TraitData>(name), amount);

        public StatusEffectDataBuilder StatusCopy(string oldName, string newName)
        {
            StatusEffectData data = TryGet<StatusEffectData>(oldName).InstantiateKeepName();
            data.name = newName;
            data.targetConstraints = new TargetConstraint[0];
            var builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>();
            builder.Mod = this;
            return builder;
        }

        private CardDataBuilder CardCopy(string oldName, string newName) => DataCopy<CardData, CardDataBuilder>(oldName, newName);
        public ClassDataBuilder TribeCopy(string oldName, string newName) => DataCopy<ClassData, ClassDataBuilder>(oldName, newName);

        public T[] DataList<T>(params string[] names) where T : DataFile => names.Select((s) => TryGet<T>(s)).ToArray();

        private T DataCopy<Y, T>(string oldName, string newName) where Y : DataFile where T : DataFileBuilder<Y, T>, new()
        {
            Y data = Get<Y>(oldName).InstantiateKeepName();
            data.name = GUID + "." + newName;
            T builder = data.Edit<Y, T>();
            builder.Mod = this;
            return builder;
        }

        internal T[] RemoveNulls<T>(T[] data) where T : DataFile
        {
            List<T> list = data.ToList();
            list.RemoveAll(x => x == null || x.ModAdded == this);
            return list.ToArray();
        }

        public class CardScriptChangeBackground : CardScript
        {
            public string imagePath = string.Empty;
            public override void Run(CardData target)
            {
                target.backgroundSprite = imagePath.ToSprite(); //Change the background image of the charmbearer.
            }
        }

        public class CardScriptChangeMain : CardScript
        {
            public string imagePath = string.Empty;
           
        }

        internal CardScript GiveUpgrade(string name = "Crown") //Give a crown
        {
            CardScriptGiveUpgrade script = ScriptableObject.CreateInstance<CardScriptGiveUpgrade>(); //This is the standard way of creating a ScriptableObject
            script.name = $"Give {name}";                               //Name only appears in the Unity Inspector. It has no other relevance beyond that.
            script.upgradeData = TryGet<CardUpgradeData>(name);
            return script;
        }

        internal CardScript AddRandomHealth(int min, int max) //Boost health by a random amount
        {
            CardScriptAddRandomHealth health = ScriptableObject.CreateInstance<CardScriptAddRandomHealth>();
            health.name = "Random Health";
            health.healthRange = new Vector2Int(min, max);
            return health;
        }

        internal CardScript AddRandomDamage(int min, int max) //Boost damage by a ranom amount
        {
            CardScriptAddRandomDamage damage = ScriptableObject.CreateInstance<CardScriptAddRandomDamage>();
            damage.name = "Give Damage";
            damage.damageRange = new Vector2Int(min, max);
            return damage;
        }

        internal CardScript AddRandomCounter(int min, int max) //Increase counter by a random amount
        {
            CardScriptAddRandomCounter counter = ScriptableObject.CreateInstance<CardScriptAddRandomCounter>();
            counter.name = "Give Counter";
            counter.counterRange = new Vector2Int(min, max);
            return counter;
        }

        private RewardPool CreateRewardPool(string name, string type, DataFile[] list)
        {
            RewardPool pool = ScriptableObject.CreateInstance<RewardPool>();
            pool.name = name;
            pool.type = type;            //The usual types are Units, Items, Charms, and Modifiers.
            pool.list = list.ToList();
            return pool;
        }
        private void FixImage(Entity entity)
        {
            if (entity.display is Card card && !card.hasScriptableImage) //These cards should use the static image
            {
                card.mainImage.gameObject.SetActive(true);               //And this line turns them on
            }
        }

public CardDataBuilder AddItemCard(
            string id, string displayName, string spritePath,
            string flavor, int blingValue,
            CardData.StatusEffectStacks[]? startSStacks = null,
            CardData.StatusEffectStacks[]? attackSStacks = null,
           List<CardData.TraitStacks>? traitSStacks = null)
        {
            string cardId = "item-" + id;
            string fullSprite = spritePath + ".png";
            string fullBg = spritePath + "_bg.png";

            var builder = new CardDataBuilder(this)
                .CreateItem(cardId, displayName)
                .SetSprites(fullSprite, fullBg)
                .WithFlavour(flavor)
                .WithCardType("Item")
                .WithValue(blingValue)
                .AddPool("GeneralItemPool")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.startWithEffects = startSStacks ?? new StatusEffectStacks[0];
                    data.attackEffects = attackSStacks ?? new StatusEffectStacks[0];
                    data.traits = traitSStacks ?? new List<TraitStacks>();
                });

            assets.Add(builder);
            return builder;
        }       private void IntegrateBattleIntoGameMode(GameMode gameMode)
        {
            // Get our battle data
            var apricotBattle = TryGet<BattleData>("battle_apricot");
            if (apricotBattle == null)
            {
                Debug.LogError($"[{Title}] Could not find Apricot battle data");
                return;
            }
            // Get the campaign populator for the game mode
            var populator = gameMode.populator;
            if (populator == null || populator.tiers == null || populator.tiers.Length < 3)
            {
                Debug.LogError($"[{Title}] Game mode does not have enough tiers");
                return;
            }
            // Add the battle to tier 2's pool to make it available throughout the game
            var tier2 = populator.tiers[2];
            tier2.battlePool = new BattleData[] { apricotBattle };
            Debug.Log($"[{Title}] Successfully added Apricot to tier 2");            // Get our battle data
            var frostknightBattle = TryGet<BattleData>("battle_frost_knight");
            if (frostknightBattle == null)
            {
                Debug.LogError($"[{Title}] Could not find Frost Knight battle data");
                return;
            }
            
            // Add the battle to tier 5's pool to make it available throughout the game
            var tier5 = populator.tiers[5];
            tier5.battlePool = new BattleData[] { frostknightBattle };
            Debug.Log($"[{Title}] Successfully added Frost Knight to tier 5");

            // Get our battle data for Volatile Amoeboms
            var amoebomsBattle = TryGet<BattleData>("battle_volatile_amoeboms");
            if (amoebomsBattle == null)
            {
                Debug.LogError($"[{Title}] Could not find Volatile Amoeboms battle data");
                return;
            }

            // Get the campaign populator for the game mode
            var amoebomsPopulator = gameMode.populator;
            if (amoebomsPopulator == null || amoebomsPopulator.tiers == null || amoebomsPopulator.tiers.Length < 7)
            {
                Debug.LogError($"[{Title}] Game mode does not have enough tiers");
                return;
            }
            // Add the battle to tier 6's pool to make it available throughout the game
            var tier6 = amoebomsPopulator.tiers[6];
            tier6.battlePool = new BattleData[] { amoebomsBattle };
            Debug.Log($"[{Title}] Successfully added Volatile Amoeboms to tier 6");
        }        
        /// <summary>
        /// Registers a custom status icon for use with status effects and keywords (Pokefrost/Overshroom style).
        /// </summary>
        /// <param name="name">The internal name for the icon GameObject (e.g. "RejuvenationIcon").</param>
        /// <param name="sprite">The Unity sprite to use for the icon.</param>
        /// <param name="type">The icon type string (should match the iconName used in KeywordData, e.g. "rejuvenationicon").</param>
        /// <param name="copyTextFrom">The icon type to copy text overlay settings from (e.g. "shroom").</param>
        /// <param name="textColor">The color for the icon's text overlay.</param>
        /// <param name="keys">Any keywords to associate with this icon (for tooltip popups).</param>
        /// <param name="posX">Horizontal offset for the icon (default 1 for Pokefrost compatibility).</param>
        public GameObject CreateIcon(string name, UnityEngine.Sprite sprite, string type, string copyTextFrom, UnityEngine.Color textColor, KeywordData[] keys, int posX = 1)
        {
            GameObject gameObject = new GameObject(name);
            // Parent to persistent UI root for mod icons
            if (WildFamilyMod.UIRoot != null)
                gameObject.transform.SetParent(WildFamilyMod.UIRoot.transform);
            gameObject.SetActive(false);
            StatusIcon icon = gameObject.AddComponent<StatusIconExt>();
            var cardIcons = CardManager.cardIcons;
            if (!string.IsNullOrEmpty(copyTextFrom) && cardIcons.ContainsKey(copyTextFrom))
            {
                var text = cardIcons[copyTextFrom].GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.InstantiateKeepName();
                text.transform.SetParent(gameObject.transform);
                icon.textElement = text.GetComponent<TMPro.TextMeshProUGUI>();
                icon.textColour = textColor;
                icon.textColourAboveMax = textColor;
                icon.textColourBelowMax = textColor;
            }
            icon.onCreate = new UnityEngine.Events.UnityEvent();
            icon.onDestroy = new UnityEngine.Events.UnityEvent();
            icon.onValueDown = new UnityEventStatStat();
            icon.onValueUp = new UnityEventStatStat();
            icon.afterUpdate = new UnityEngine.Events.UnityEvent();
            var image = gameObject.AddComponent<UnityEngine.UI.Image>();
            image.sprite = sprite;
            var cardHover = gameObject.AddComponent<CardHover>();
            cardHover.enabled = false;
            cardHover.IsMaster = false;
            var cardPopUp = gameObject.AddComponent<CardPopUpTarget>();
            cardPopUp.keywords = keys;
            cardPopUp.posX = posX;
            cardHover.pop = cardPopUp;
            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.zero;
            rectTransform.sizeDelta *= 0.01f;
            gameObject.SetActive(true);
            icon.type = type;
            cardIcons[type] = gameObject;
            return gameObject;
        }
    }
}
