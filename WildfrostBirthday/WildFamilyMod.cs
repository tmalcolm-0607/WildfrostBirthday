// MadFamily Tribe Mod - Wildfrost
using Dead;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using static CardData;


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
                CreateFamilyUnits();
                CreateItemCards();
                WildfrostBirthday.Tribes.Tribe_MadFamily.Register(this);
                preLoaded = true;
            }

            base.Load();

            Events.OnEntityCreated += FixImage;
            GameMode gameMode = TryGet<GameMode>("GameModeNormal"); //GameModeNormal is the standard game mode. 
            gameMode.classes = gameMode.classes.Append(TryGet<ClassData>("MadFamily")).ToArray();
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

        private void CreateFamilyUnits()
        {
            // Register base Cleanse effect if not present

            // Register modularized effects (all modular status effects used by cards/charms/items)
            StatusEffect_Cleanse.Register(this);
            StatusEffect_WhenDestroyedAddHealthToAllies.Register(this);
            StatusEffect_WhenEnemyIsKilledApplyHealthToAttacker.Register(this);
            StatusEffect_OnKillHealToSelf.Register(this);
            Effects.StatusEffect_CollectBlingOnTrigger.Register(this);
            Effects.StatusEffect_FrostMoonApplyFrostburnOnAttack.Register(this);
            Effects.StatusEffect_FrostMoonIncreaseMaxCounter.Register(this);
            Effects.StatusEffect_OnTurnApplyTeethToAllies.Register(this);
            Effects.StatusEffect_OnTurnApplyTeethToSelf.Register(this);
            Effects.StatusEffect_WhenAllyIsHitApplyFrostToAttacker.Register(this);
            Effects.StatusEffect_WhenDeployedApplyTeethToSelf.Register(this);
            Effects.StatusEffect_WhenDeployedSummonWowee.Register(this);
            Effects.StatusEffect_WhenHitApplyDemonizeToAttacker.Register(this);
            Effects.StatusEffect_WhenHitApplyOverloadToAttacker.Register(this);
            Effects.StatusEffect_WhenHitGainAttackToSelfNoPing.Register(this);
            Effects.StatusEffect_SummonBeepop.Register(this);
            Effects.StatusEffect_SummonFallow.Register(this);
            Effects.StatusEffect_SummonBeepopWisp.Register(this);
            Effects.StatusEffect_InstantSummonFallow.Register(this);
            Effects.StatusEffect_OnTurnAddAttackToAllies.Register(this);
            Effects.StatusEffect_OnTurnApplyInkToRandomEnemy.Register(this);
            StatusEffect_OnTurnSummonSoulrose.Register(this);
            StatusEffect_SummonSoulrose.Register(this);
            StatusEffect_SummonWisp.Register(this);
            StatusEffect_WhenDeployedSummonSoulrose.Register(this);

            // Register all modular cards/units
            Cards.Card_Soulrose.Register(this);
            Cards.Card_Wisp.Register(this);
            Cards.Card_Alison.Register(this);
            Cards.Card_Tony.Register(this);
            Cards.Card_Caleb.Register(this);
            Cards.Card_Kaylee.Register(this);
            Cards.Card_Cassie.Register(this);
            Cards.Card_Lulu.Register(this);
            Cards.Card_Poppy.Register(this);

            // Register all modular charms
            Charms.Charm_PugCharm.Register(this);
            Charms.Charm_GoldenVialCharm.Register(this);
            Charms.Charm_FrostMoonCharm.Register(this);
            Charms.Charm_SodaCharm.Register(this);
            Charms.Charm_PizzaCharm.Register(this);
            Charms.Charm_PlantCharm.Register(this);
            Charms.Charm_BookCharm.Register(this);
            Charms.Charm_DuckCharm.Register(this);
        }



    public CardDataBuilder AddFamilyUnit(
            string id, string displayName, string spritePath,
            int hp, int atk, int counter, int blingvalue, string flavor,
            CardData.StatusEffectStacks[]? attackSStacks = null,
            CardData.StatusEffectStacks[]? startSStacks = null,
            bool isLeader = false)
        {

            if (id=="alison" || id=="caleb" || id=="lulu" || id=="poppy")
            {
                int randomNumber = Dead.Random.Range(0,3);
                spritePath = spritePath + randomNumber.ToString();
                 
            }
            string cardId = (isLeader ? "leader-" : "companion-") + id;
            string fullSprite = spritePath + ".png";
            string fullBg = "bg.png";
            string poolName = isLeader ? "LeaderPool" : "GeneralUnitPool";

            var builder = new CardDataBuilder(this)
                .CreateUnit(cardId, displayName)
                .SetSprites(fullSprite, fullBg)
                .SetStats(hp, atk, counter)
                .WithFlavour(flavor)
                .WithCardType(isLeader ? "Leader" : "Friendly")
                .WithValue(blingvalue)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.attackEffects = attackSStacks ?? new StatusEffectStacks[0];
                    data.startWithEffects = startSStacks ?? new StatusEffectStacks[0];
                })
                .FreeModify(data =>
                {
                    if (isLeader)
                    {
                        data.createScripts = new CardScript[]
                        {
                    GiveUpgrade(),
                    AddRandomHealth(-1, 3),
                    AddRandomDamage(0, 2),
                    AddRandomCounter(-1, 1)
                        };
                    }
                });

            assets.Add(builder);
            return builder;
        }        public CardUpgradeDataBuilder AddCharm(
            string id, 
            string title, 
            string cardText, 
            string charmPool, 
            string spritePath, 
            int tier, 
            StatusEffectStacks[]? effects = null,
            TargetConstraint[]? constraints = null,
            TraitStacks[]? traits = null)
        {
            string cardId = "charm-" + id;

            var builder = new CardUpgradeDataBuilder(this)
                .Create(cardId)
                .AddPool(charmPool)
                .WithType(CardUpgradeData.Type.Charm)
                .WithImage(spritePath + ".png")
                .WithTitle(title)
                .WithText(cardText)
                .WithTier(tier);

            builder.SubscribeToAfterAllBuildEvent(data =>
            builder.SubscribeToAfterAllBuildEvent(data =>
            {
                if (effects != null)
                    data.effects = effects;


                if (constraints != null)
                    data.targetConstraints = constraints;


                if (traits != null)
                    data.giveTraits = traits;

                    CardScriptChangeMain script = ScriptableObject.CreateInstance<CardScriptChangeMain>(); // new line: creates the card script                    
                    data.scripts = new CardScript[1] { script }; // new line: attaches the script to the charm.
            }));

            assets.Add(builder);
            return builder;
        }


        public StatusEffectDataBuilder AddStatusEffect<T>(string id, string text, Action<T> modify, string? type = null, bool canBeBoosted = false, string? textInsert = null) where T : StatusEffectData
        {
            var builder = new StatusEffectDataBuilder(this)
                .Create<T>(id)
                .WithText(text);

            if (!string.IsNullOrEmpty(type)) builder.WithType(type);
            if (canBeBoosted) builder.WithCanBeBoosted(true);
            if (!string.IsNullOrEmpty(textInsert)) builder.WithTextInsert(textInsert);

            builder.SubscribeToAfterAllBuildEvent(data => modify((T)data));
            assets.Add(builder);
            return builder;
        }

        public StatusEffectDataBuilder AddCopiedStatusEffect<T>(string from, string to, Action<T> modify, string? text = null, string? textInsert = null) where T : StatusEffectData
        {
            var builder = StatusCopy(from, to);
            if (!string.IsNullOrEmpty(text)) builder.WithText(text);
            if (!string.IsNullOrEmpty(textInsert)) builder.WithTextInsert(textInsert);
            builder.SubscribeToAfterAllBuildEvent(data => modify((T)data));
            assets.Add(builder);
            return builder;
        }

        public bool IsAlreadyRegistered<T>(string id) where T : DataFile
        {
            try
            {
                var fullId = Extensions.PrefixGUID(id, this).ToLower();
                var asset = AddressableLoader.Get<T>(typeof(T).Name, fullId) ?? Get<T>(id);
                return asset != null;
            }
            catch
            {
                return false;
            }
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

        // Example usage of AddItemCard
        private void CreateItemCards()
        {
            Cards.Item_SnowPillow.Register(this);
            Cards.Item_RefreshingWater.Register(this);
            Cards.Item_WispMask.Register(this);
            Cards.Item_CheeseCrackers.Register(this);
            Cards.Item_FoamBullets.Register(this);
        }
    }
}

            

