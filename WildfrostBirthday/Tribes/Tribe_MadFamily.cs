using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Tribes
{
    /// <summary>
    /// Modular registration logic for the MadFamily tribe.
    /// This class follows the best-practices pattern: one tribe per file, minimal entry point, and use of only approved/documented helpers.
    /// See docs/TribeLogicOverview.md and docs/Tutorial5_CreatingATribe.md for rationale and migration details.
    /// </summary>
    public static class Tribe_MadFamily
    {
        /// <summary>
        /// Registers the MadFamily tribe and all associated assets (leaders, inventory, reward pools).
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void Register(WildFamilyMod mod)
        {
            // Create the tribe asset using the builder pattern and subscribe to post-build customization.
            var builder = new ClassDataBuilder(mod)
                .Create("MadFamily")
                .WithFlag("Images/tribe_madfamily.png")
                .WithSelectSfxEvent(FMODUnity.RuntimeManager.PathToEventReference("event:/sfx/card/draw_multi"));
            builder.SubscribeToAfterAllBuildEvent(data =>
            {
                // Set tribe ID and prepare character prefab (see Tutorial5_CreatingATribe.md)
                data.id = "MadFamily";
                GameObject gameObject = mod.TryGet<ClassData>("Basic").characterPrefab.gameObject.InstantiateKeepName();
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
                gameObject.name = "Player (MadFamily)";
                data.characterPrefab = gameObject.GetComponent<Character>();

                // Set leaders (must be registered before tribe)
                data.leaders = new CardData[]
                {
                    mod.TryGet<CardData>("leader-alison"),
                    mod.TryGet<CardData>("leader-tony"),
                    mod.TryGet<CardData>("leader-cassie"),
                    mod.TryGet<CardData>("leader-caleb"),
                    mod.TryGet<CardData>("leader-kaylee"),
                };

                // Create starting inventory (see Tutorial5_CreatingATribe.md#starting-inventory)
                Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
                inventory.deck = new CardDataList
                {
                    mod.TryGet<CardData>("SnowGlobe"),
                    mod.TryGet<CardData>("Sword"),
                    mod.TryGet<CardData>("Gearhammer"),
                    mod.TryGet<CardData>("Sword"),
                    mod.TryGet<CardData>("Gearhammer"),
                    mod.TryGet<CardData>("SunlightDrum"),
                    mod.TryGet<CardData>("Junkhead"),
                    mod.TryGet<CardData>("companion-lulu"),
                    mod.TryGet<CardData>("companion-poppy"),
                };
                inventory.reserve = new CardDataList{};
                inventory.upgrades = new List<CardUpgradeData>
                {
                    mod.TryGet<CardUpgradeData>("charm-bookcharm"),
                        };
                        inventory.goldOwed = 0;
                        data.startingInventory = inventory;
                        
                        // Create reward pools
                        data.rewardPools = CreateRewardPools(mod);
                    });
                

            // Add the tribe to the mod assets
            mod.assets.Add(builder);
        }
        
        // Helper method to create the reward pools
        private static RewardPool[] CreateRewardPools(WildFamilyMod mod)
        {
            List<RewardPool> pools = new List<RewardPool>();
            
            // Items pool
            var itemsPool = ScriptableObject.CreateInstance<RewardPool>();
            itemsPool.type = "Items";
            itemsPool.copies = 1;
            itemsPool.list = new List<DataFile>
            {
                mod.TryGet<CardData>("Mimik"),
                mod.TryGet<CardData>("Blingo"),
                mod.TryGet<CardData>("MegaMimik"),
                mod.TryGet<CardData>("ZoomlinNest"),
                mod.TryGet<CardData>("SunlightDrum"),
                mod.TryGet<CardData>("Demonheart"),
                mod.TryGet<CardData>("PinkberryJuice"),
                mod.TryGet<CardData>("BerryBlade"),
                mod.TryGet<CardData>("BerryBasket"),
                mod.TryGet<CardData>("BlazeTea"),
                mod.TryGet<CardData>("Hooker"),
                mod.TryGet<CardData>("MoltenDip"),
                mod.TryGet<CardData>("NoomlinBiscuit"),
                mod.TryGet<CardData>("ZoomlinWafers"),
                mod.TryGet<CardData>("item-snowpillow"),
                mod.TryGet<CardData>("item-refreshingwater"),
                mod.TryGet<CardData>("item-wispmask"),
                mod.TryGet<CardData>("item-azulbook"),
                mod.TryGet<CardData>("item-azultorch"),
                mod.TryGet<CardData>("item-berrycake"),
                mod.TryGet<CardData>("item-blazeberry"),
                mod.TryGet<CardData>("item-cheesecrackers"),
                mod.TryGet<CardData>("item-diceofdestiny"),
                mod.TryGet<CardData>("item-dynamoroller"),
                mod.TryGet<CardData>("item-foambullets"),
                mod.TryGet<CardData>("item-foamrocket"),
                mod.TryGet<CardData>("item-hazetacks"),
                mod.TryGet<CardData>("item-inkegg"),
                mod.TryGet<CardData>("item-junkpile"),
                mod.TryGet<CardData>("item-shellbom"),
                mod.TryGet<CardData>("companion-cfrs"),
            };
            itemsPool.isGeneralPool = true;
            pools.Add(itemsPool);
            
            // Units pool
            var unitsPool = ScriptableObject.CreateInstance<RewardPool>();
            unitsPool.type = "Units";
            unitsPool.copies = 1;
            unitsPool.list = new List<DataFile>
            {
                mod.TryGet<CardData>("Foxee"),
                mod.TryGet<CardData>("Blunky"),
                mod.TryGet<CardData>("Dimona"),
                mod.TryGet<CardData>("Bombom"),
                mod.TryGet<CardData>("BigBerry"),
                mod.TryGet<CardData>("Bonnie"),
                mod.TryGet<CardData>("Klutz"),
                mod.TryGet<CardData>("Noggin"),
                mod.TryGet<CardData>("LuminCat"),
                mod.TryGet<CardData>("MagmaBlacksmith"),
                mod.TryGet<CardData>("Blue"),
            };
            unitsPool.isGeneralPool = true;
            pools.Add(unitsPool);
            
            // Charms pool
            var charmsPool = ScriptableObject.CreateInstance<RewardPool>();
            charmsPool.type = "Charms";
            charmsPool.copies = 1;
            charmsPool.list = new List<DataFile>
            {
                mod.TryGet<CardUpgradeData>("CardUpgradeBalanced"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBarrage"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBattle"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBling"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBombskull"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBoost"),
                mod.TryGet<CardUpgradeData>("CardUpgradeCake"),
                mod.TryGet<CardUpgradeData>("CardUpgradeCloudberry"),
                mod.TryGet<CardUpgradeData>("CardUpgradeFrenzyConsume"),
                mod.TryGet<CardUpgradeData>("CardUpgradeFrosthand"),
                mod.TryGet<CardUpgradeData>("CardUpgradeHeart"),
                mod.TryGet<CardUpgradeData>("CardUpgradeHook"),
                mod.TryGet<CardUpgradeData>("CardUpgradeNoomlin"),
                mod.TryGet<CardUpgradeData>("CardUpgradePunchfist"),
                mod.TryGet<CardUpgradeData>("CardUpgradeSun"),
                mod.TryGet<CardUpgradeData>("CardUpgradeWildcard"),
                mod.TryGet<CardUpgradeData>("CardUpgradeDraw"),
                mod.TryGet<CardUpgradeData>("CardUpgradePig"),
                mod.TryGet<CardUpgradeData>("CardUpgradeDemonize"),
                mod.TryGet<CardUpgradeData>("CardUpgradeFury"),
                mod.TryGet<CardUpgradeData>("CardUpgradeSnowImmune"),
                mod.TryGet<CardUpgradeData>("CardUpgradeAttackIncreaseCounter"),
                mod.TryGet<CardUpgradeData>("CardUpgradeAttackRemoveEffects"),
                mod.TryGet<CardUpgradeData>("CardUpgradeAttackConsume"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBlock"),
                mod.TryGet<CardUpgradeData>("CardUpgradeRemoveCharmLimit"),
                mod.TryGet<CardUpgradeData>("CardUpgradeFrenzyReduceAttack"),
                mod.TryGet<CardUpgradeData>("CardUpgradeConsumeAddHealth"),
                mod.TryGet<CardUpgradeData>("CardUpgradeAttackAndHealth"),
                mod.TryGet<CardUpgradeData>("CardUpgradeGreed"),
                mod.TryGet<CardUpgradeData>("CardUpgradeCritical"),
                mod.TryGet<CardUpgradeData>("CardUpgradeSpark"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBlue"),
                mod.TryGet<CardUpgradeData>("CardUpgradeBootleg"),
                mod.TryGet<CardUpgradeData>("CardUpgradeFlameberry"),
                mod.TryGet<CardUpgradeData>("CardUpgradeGlass"),
                mod.TryGet<CardUpgradeData>("CardUpgradeHeartmist"),
                mod.TryGet<CardUpgradeData>("CardUpgradeHunger"),
                mod.TryGet<CardUpgradeData>("CardUpgradeMuncher"),
                mod.TryGet<CardUpgradeData>("CardUpgradePlink"),
                mod.TryGet<CardUpgradeData>("CardUpgradeShadeClay"),
                mod.TryGet<CardUpgradeData>("charm-pugcharm"),
                mod.TryGet<CardUpgradeData>("charm-goldenvialcharm"),
                mod.TryGet<CardUpgradeData>("charm-frostmooncharm"),
                mod.TryGet<CardUpgradeData>("charm-sodacharm"),
                mod.TryGet<CardUpgradeData>("charm-pizzacharm"),
                mod.TryGet<CardUpgradeData>("charm-plantcharm"),
                mod.TryGet<CardUpgradeData>("charm-bookcharm"),
                mod.TryGet<CardUpgradeData>("charm-duckcharm"),
            };
            charmsPool.isGeneralPool = true;
            pools.Add(charmsPool);
            
            // Snow items pool
            var snowItemsPool = ScriptableObject.CreateInstance<RewardPool>();
            snowItemsPool.type = "Items";
            snowItemsPool.copies = 1;
            snowItemsPool.list = new List<DataFile>
            {
                mod.TryGet<CardData>("Snowcake"),
                mod.TryGet<CardData>("SnowGlobe"),
            };
            snowItemsPool.isGeneralPool = true;
            pools.Add(snowItemsPool);
            
            // Snow units pool
            var snowUnitsPool = ScriptableObject.CreateInstance<RewardPool>();
            snowUnitsPool.type = "Units";
            snowUnitsPool.copies = 1;
            snowUnitsPool.list = new List<DataFile>
            {
                mod.TryGet<CardData>("Snobble"),
                mod.TryGet<CardData>("Snoffel"),
            };
            snowUnitsPool.isGeneralPool = true;
            pools.Add(snowUnitsPool);
            
            // Snow charms pool
            var snowCharmsPool = ScriptableObject.CreateInstance<RewardPool>();
            snowCharmsPool.type = "Charms";
            snowCharmsPool.copies = 1;
            snowCharmsPool.list = new List<DataFile>
            {
                mod.TryGet<CardUpgradeData>("CardUpgradeSnowball"),
            };
            snowCharmsPool.isGeneralPool = true;
            pools.Add(snowCharmsPool);
            
            // Additional items pool
            var additionalItemsPool = ScriptableObject.CreateInstance<RewardPool>();
            additionalItemsPool.type = "Items";
            additionalItemsPool.copies = 1;
            additionalItemsPool.list = new List<DataFile>
            {
                mod.TryGet<CardData>("MobileCampfire"),
                mod.TryGet<CardData>("Snowcracker"),
                mod.TryGet<CardData>("ScrapPile"),
            };
            additionalItemsPool.isGeneralPool = false;
            pools.Add(additionalItemsPool);
            
            // Additional units pool
            var additionalUnitsPool = ScriptableObject.CreateInstance<RewardPool>();
            additionalUnitsPool.type = "Units";
            additionalUnitsPool.copies = 1;
            additionalUnitsPool.list = new List<DataFile>
            {
                mod.TryGet<CardData>("companion-alison"),
                mod.TryGet<CardData>("companion-tony"),
                mod.TryGet<CardData>("companion-cassie"),
                mod.TryGet<CardData>("companion-caleb"),
                mod.TryGet<CardData>("companion-kaylee"),
                mod.TryGet<CardData>("companion-ivy"),
                mod.TryGet<CardData>("companion-bwob"),
                mod.TryGet<CardData>("companion-chonk"),
                

            };
            additionalUnitsPool.isGeneralPool = true;
            pools.Add(additionalUnitsPool);
            
            // Additional charms pool
            var additionalCharmsPool = ScriptableObject.CreateInstance<RewardPool>();
            additionalCharmsPool.type = "Charms";
            additionalCharmsPool.copies = 1;
            additionalCharmsPool.list = new List<DataFile>
            {
                mod.TryGet<CardUpgradeData>("CardUpgradeSpice"),
                mod.TryGet<CardUpgradeData>("CardUpgradeShroom"),
                mod.TryGet<CardUpgradeData>("CardUpgradeAcorn"),
                mod.TryGet<CardUpgradeData>("CardUpgradeShellOnKill"),
                mod.TryGet<CardUpgradeData>("CardUpgradeShellBecomesSpice"),
                mod.TryGet<CardUpgradeData>("CardUpgradeShroomReduceHealth"),
                mod.TryGet<CardUpgradeData>("CardUpgradeScrap"),
                mod.TryGet<CardUpgradeData>("CardUpgradeHeartburn"),
            };
            additionalCharmsPool.isGeneralPool = false;
            pools.Add(additionalCharmsPool);
            
            // Modifiers pool
            var modifiersPool = ScriptableObject.CreateInstance<RewardPool>();
            modifiersPool.type = "Modifiers";
            modifiersPool.copies = 1;
            modifiersPool.list = new List<DataFile>
            {
                mod.TryGet<GameModifierData>("BlessingCompanions"),
                mod.TryGet<GameModifierData>("BlessingHand"),
                mod.TryGet<GameModifierData>("BlessingHealth"),
                mod.TryGet<GameModifierData>("BlessingRedrawBell"),
                mod.TryGet<GameModifierData>("BlessingTime"),
                mod.TryGet<GameModifierData>("BlessingRecall"),
                mod.TryGet<GameModifierData>("BlessingCharge"),
                mod.TryGet<GameModifierData>("BlessingConsume"),
                mod.TryGet<GameModifierData>("BlessingNoomlin"),
                mod.TryGet<GameModifierData>("BlessingStrength"),
                mod.TryGet<GameModifierData>("BlessingInfinity"),
            };
            modifiersPool.isGeneralPool = true;
            pools.Add(modifiersPool);
            
            return pools.ToArray();
        }
    }
}
