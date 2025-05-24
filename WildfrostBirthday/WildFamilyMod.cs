﻿// MadFamily Tribe Mod - Wildfrost
namespace WildfrostBirthday
{

    public class WildFamilyMod : WildfrostMod
    {

        public WildFamilyMod(string modDirectory) : base(modDirectory) => Instance = this;
        public static WildFamilyMod? Instance;

        public override string GUID => "madfamilymod.wildfrost.madhouse";
        public override string[] Depends => new string[] { };
        public override string Title => "MadHouse Family Tribe";
        public override string Description => "Mod made by the MadHouse Family for us to play with our family in game. Made for our Mother who is the greatest of all time";

        private bool preLoaded = false;
public List<object> assets = new List<object>();

        public override void Load()
        {

            if (!preLoaded)
            {
                // Automatically register all components (status effects, cards, charms, items, tribes)
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllComponents(this);
                // Register battles and campaign nodes
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllBattles(this);
                WildfrostBirthday.Helpers.ComponentRegistration.RegisterAllCampaignNodeTypes(this);
                preLoaded = true;
            }

            base.Load();

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
        }

        private void IntegrateBattleIntoGameMode(GameMode gameMode)
        {
            // Get our battle data
            var battle = TryGet<BattleData>("battle_volatile_amoeboms");
            if (battle == null)
            {
                Debug.LogError($"[{Title}] Could not find Volatile Amoeboms battle data");
                return;
            }            // Get the campaign populator for the game mode
            var populator = gameMode.populator;
            if (populator == null || populator.tiers == null || populator.tiers.Length < 1)
            {
                Debug.LogError($"[{Title}] Game mode does not have enough tiers");
                return;
            }            // Add the battle to tier 0 (alongside Snowbo Squad and Pengoons)
            var tier0 = populator.tiers[0];
            if (tier0.battlePool == null)
                tier0.battlePool = new BattleData[0];

            // Create new array with our battle added
            tier0.battlePool = tier0.battlePool.Concat(new[] { battle }).ToArray();
        }
    }
}

