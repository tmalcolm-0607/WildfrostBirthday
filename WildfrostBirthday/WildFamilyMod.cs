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
        private List<object> assets = new List<object>();

        public override void Load()
        {
            if (!preLoaded)
            {
                CreateFamilyUnits();
                CreateItemCards();
                assets.Add(TribeCopy("Basic", "MadFamily")                   //Snowdweller = "Basic", Shademancer = "Magic", Clunkmaster = "Clunk"
                                   .WithFlag("Images/tribe_madfamily.png")                    //Loads your DrawFlag.png in your Images subfolder of your mod folder
                                   .WithSelectSfxEvent(FMODUnity.RuntimeManager.PathToEventReference("event:/sfx/card/draw_multi"))
                                   .SubscribeToAfterAllBuildEvent(   //New lines start here
                                    (data) =>                         //Other tutorials typically write out delegate here, this is the condensed notation (data is assumed to be ClassData)
                                    {
                                        GameObject gameObject = data.characterPrefab.gameObject.InstantiateKeepName();   //Copy the previous prefab
                                        UnityEngine.Object.DontDestroyOnLoad(gameObject);                                //GameObject may be destroyed if their scene is unloaded. This ensures that will never happen to our gameObject
                                        gameObject.name = "Player (MadFamily)";                                      //For comparison, the snowdweller character is named "Player (Basic)"
                                        data.characterPrefab = gameObject.GetComponent<Character>();                     //Set the characterPrefab to our new prefab
                                        data.id = "MadFamily";
                                        data.leaders = DataList<CardData>("leader-alison", "leader-tony", "leader-cassie", "leader-caleb", "leader-kaylee"); //Used to track win/loss statistics for the tribe. Not displayed anywhere though :/
                                        Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
                                        inventory.deck.list = DataList<CardData>("SnowGlobe", "Sword", "Gearhammer", "Sword", "Gearhammer", "SunlightDrum", "Junkhead", "companion-lulu", "companion-poppy").ToList(); //Some odds and ends
                                        inventory.upgrades.Add(TryGet<CardUpgradeData>("charm-book_charm"));
                                        data.startingInventory = inventory;
                                        //RewardPool unitPool = CreateRewardPool("DrawUnitPool", "Units", DataList<CardData>(
                                        //    "NakedGnome", "GuardianGnome", "Havok",
                                        //    "Gearhead", "Bear", "TheBaker",
                                        //    "Pimento", "Pootie", "Tusk",
                                        //    "Ditto", "Flash", "TinyTyko"));

                                        //RewardPool itemPool = CreateRewardPool("DrawItemPool", "Items", DataList<CardData>(
                                        //    "ShellShield", "StormbearSpirit", "PepperFlag", "SporePack", "Woodhead",
                                        //    "BeepopMask", "Dittostone", "Putty", "Dart", "SharkTooth",
                                        //    "Bumblebee", "Badoo", "Juicepot", "PomDispenser", "LuminShard",
                                        //    "Wrenchy", "Vimifier", "OhNo", "Madness", "Joob"));

                                        //RewardPool charmPool = CreateRewardPool("DrawCharmPool", "Charms", DataList<CardUpgradeData>(
                                        //    "CardUpgradeSuperDraw", "CardUpgradeTrash",
                                        //    "CardUpgradeInk", "CardUpgradeOverload",
                                        //    "CardUpgradeMime", "CardUpgradeShellBecomesSpice",
                                        //    "CardUpgradeAimless"));

                                        //    data.rewardPools = new RewardPool[]
                                        //    {
                                        //        Extensions.GetRewardPool("GeneralUnitPool"),
                                        //        Extensions.GetRewardPool("GeneralItemPool"),
                                        //        Extensions.GetRewardPool("GeneralCharmPool"),
                                        //        Extensions.GetRewardPool("GeneralModifierPool"),
                                        //        Extensions.GetRewardPool("SnowUnitPool"),         //
                                        //        Extensions.GetRewardPool("SnowItemPool"),         //The snow pools are not Snowdwellers, there are general snow units/cards/charms.
                                        //        Extensions.GetRewardPool("SnowCharmPool"),        //
                                        //                                            };
                                    })
                                   );
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
            AddCopiedStatusEffect<StatusEffectInstantCleanse>(
                "Cleanse", "Cleanse With Text",
                data =>
                {
                },
                text: "{0}",
                textInsert: "<keyword=cleanse>"
            );

            // === 1. Register Base Effects First ===
            AddStatusEffect<StatusEffectApplyXWhenDestroyed>(
                "When Destroyed Add Health To Allies",
                "When destroyed, add 1 to all allies (Max)",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Increase Max Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                },
                type: "Increase Max Health",
                canBeBoosted: true
            );

            AddCopiedStatusEffect<StatusEffectApplyXWhenUnitIsKilled>(
                "When Enemy Is Killed Apply Shell To Attacker",
                "When Enemy Is Killed Apply Health To Attacker",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Increase Max Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                text: "When an enemy is killed, apply <{a}><keyword=health> to the attacker",
                textInsert: "<keyword=health>"
            );

            AddStatusEffect<StatusEffectApplyXOnKill>(
                "On Kill Heal To Self",
                "Restore 2 on kill",
                data =>
                {
                    data.effectToApply = Get<StatusEffectData>("Increase Health");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: true
            );

            // === 2. Register Soulrose BEFORE referencing it ===
            AddFamilyUnit(
                "soulrose", "Soulrose", "companions/soulrose",
                1, 0, 0, 0,
                "When destroyed, add +1 health to all allies",
                startSStacks: new[] { SStack("When Destroyed Add Health To Allies", 1) }
            );
            AddFamilyUnit(
                "wisp", "Wisp", "companions/wisp",
                5, 4, 6, 0,
                "When an enemy is killed, apply 4 health to the attacker"
            ).SubscribeToAfterAllBuildEvent(data =>
            {
                data.startWithEffects = new[] {
                    SStack( "When Enemy Is Killed Apply Health To Attacker", 4)
                };
            });

            // === 3. Register Copied Effects (can now reference Soulrose) ===
            AddCopiedStatusEffect<StatusEffectSummon>(
                "Summon Beepop", "On Turn Summon Soulrose",
                data =>
                {
                    data.summonCard = TryGet<CardData>("companion-soulrose");
                },
                text: "{0}",
                textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-soulrose>"
            );        
            AddCopiedStatusEffect<StatusEffectSummon>(
                "Summon Fallow", "Summon Soulrose",
                data =>
                {
                    data.summonCard = TryGet<CardData>("companion-soulrose");
                }
            );
            AddCopiedStatusEffect<StatusEffectSummon>(
                "Summon Beepop", "Summon Wisp", data =>
                {
                    data.summonCard = TryGet<CardData>("companion-wisp");
                },
                text: "{0}",
                textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-wisp>"
            );

            AddCopiedStatusEffect<StatusEffectInstantSummon>(
                "Instant Summon Fallow", "Instant Summon Soulrose",
                data =>
                {
                    data.targetSummon = TryGet<StatusEffectData>("Summon Soulrose") as StatusEffectSummon;
                }
            );

            AddCopiedStatusEffect<StatusEffectApplyXWhenDeployed>(
                "When Deployed Summon Wowee", "When Deployed Summon Soulrose",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Instant Summon Soulrose");
                },
                text: "Summon",
                textInsert: "<card=madfamilymod.wildfrost.madhouse.companion-soulrose>"
            );

            // === 3. Register Copied Effects (can now reference Soulrose) ===
            AddCopiedStatusEffect<StatusEffectApplyXOnTurn
>(
                "On Turn Add Attack To Allies", "On Turn Add Attack To Self",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Increase Attack");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                }
            );





            // === 4. Create Units that use those effects ===
            AddFamilyUnit("alison", "Alison", "leaders/alison", 9, 3, 3, 50, "Restore 2 HP on kill", attackSStacks: new[]
            {
                SStack("On Kill Heal To Self", 2)
            }
            );

            AddFamilyUnit("alison", "Alison", "leaders/alison", 9, 3, 3, 50, "Restore 2 HP on kill", attackSStacks: new[]
           {
                SStack("On Kill Heal To Self", 2)
            }, isLeader: true
           );

            AddFamilyUnit("tony", "Tony", "leaders/tony", 8, 2, 4, 50, "Summon Soulrose")
               .SubscribeToAfterAllBuildEvent(data =>
               {
                   data.startWithEffects = new[] {
                    SStack("When Deployed Summon Soulrose", 1),
                    SStack("On Turn Summon Soulrose", 1)
                   };
               });

            AddFamilyUnit("tony", "Tony", "leaders/tony", 8, 2, 4, 50, "Summon Soulrose", isLeader: true)
              .SubscribeToAfterAllBuildEvent(data =>
              {
                  data.startWithEffects = new[] {
                    SStack("When Deployed Summon Soulrose", 1),
                    SStack("On Turn Summon Soulrose", 1)
                  };
              });
            AddStatusEffect<StatusEffectApplyXOnKill>(
                "When Hit Apply Overload To Attacker",
                "When Hit Apply Overload",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Overload");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                canBeBoosted: true
            );

            AddStatusEffect<StatusEffectApplyXOnKill>(
                "When Hit Gain Attack To Self (No Ping)",
                "Gain 1 Attack On Hit",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Increase Attack");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: true
            );


            AddFamilyUnit("caleb", "Caleb", "leaders/caleb", 8, 0, 6, 50, "When attacked, apply 1 overload to attacker. Gain +1 attack on hit.", startSStacks: new[]
                {
                    SStack("When Hit Apply Overload To Attacker", 2),
                    SStack("When Hit Gain Attack To Self (No Ping)", 1)
                });

            AddFamilyUnit("caleb", "Caleb", "leaders/caleb", 8, 0, 6, 50, "When attacked, apply 1 overload to attacker. Gain +1 attack on hit.", startSStacks: new[]
            {
                    SStack("When Hit Apply Overload To Attacker", 2),
                    SStack("When Hit Gain Attack To Self (No Ping)", 1)
                }, isLeader: true);

            // === Add Custom "While Active" Effect for Kaylee ===
            AddStatusEffect<StatusEffectApplyXOnTurn>(
                "On Turn Apply Teeth To Allies",
                "While Active Teeth To Allies (Kaylee)",
                 data =>
                 {
                     data.effectToApply = TryGet<StatusEffectData>("Teeth");
                     data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                 },
                canBeBoosted: true
            );

            AddStatusEffect<StatusEffectApplyXOnTurn>(
               "On Turn Apply Teeth To Self",
               "While Active Teeth To Allies (Kaylee)",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Teeth");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: true
                );


            AddStatusEffect<StatusEffectApplyXWhenDeployed>(
               "When Deployed Apply Teeth To Self",
               "Teeth",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Teeth");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
               canBeBoosted: true
           );

            AddFamilyUnit("kaylee", "Kaylee", "leaders/kaylee", 7, 4, 7, 50, "Sharp-witted and sharper-fanged, Kaylee boosts all allies' bite.", startSStacks: new[]
                        {
                            SStack("When Deployed Apply Teeth To Self", 4), // Kaylee starts with 4 teeth
                             SStack("On Turn Apply Teeth To Allies", 2), // Applies +3 teeth to all allies
                        }
            );
            AddFamilyUnit("kaylee", "Kaylee", "leaders/kaylee", 7, 4, 7, 50, "Sharp-witted and sharper-fanged, Kaylee boosts all allies' bite.", startSStacks: new[]
                        {
                            SStack("When Deployed Apply Teeth To Self", 4), // Kaylee starts with 4 teeth
                             SStack("On Turn Apply Teeth To Allies", 2), // Applies +3 teeth to all allies
                        }
            , isLeader: true);
            // === Status Effect: Apply Ink On Hit ===
            AddStatusEffect<StatusEffectApplyXOnTurn>(
                "On Turn Apply Ink To RandomEnemy",
                "On hit, apply 2 Ink to the target",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Null"); // "Null" = Ink
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                },
                canBeBoosted: true
            );

            // === Cassie Unit ===
            AddFamilyUnit("cassie", "Cassie", "leaders/cassie",
                            5, 1, 3, 50,
                            "Joyful and chaotic, Cassie bounces through battle with ink and impulse.",
                            startSStacks: new[]
                            {
                                SStack("MultiHit", 2), // Frenzy x2
                                SStack("On Turn Apply Ink To RandomEnemy", 2)
                            }
                        )
                        .SetTraits(new CardData.TraitStacks[]
                        {
                            new CardData.TraitStacks(TryGet<TraitData>("Aimless"), 1)
                        });

            AddFamilyUnit("cassie", "Cassie", "leaders/cassie",
                5, 1, 3, 50,
                "Joyful and chaotic, Cassie bounces through battle with ink and impulse.",
                startSStacks: new[]
                {
                                SStack("MultiHit", 2), // Frenzy x2
                                SStack("On Turn Apply Ink To RandomEnemy", 2)
                }
            , isLeader: true)
            .SetTraits(new CardData.TraitStacks[]
            {
                            new CardData.TraitStacks(TryGet<TraitData>("Aimless"), 1)
            });

            // === Lulu Effect: Trigger When Ally Is Hit Apply 2 Snow ===
            AddStatusEffect<StatusEffectApplyXWhenAllyIsHit>(
                "When Ally is Hit Apply Frost To Attacker",
                "When ally is hit, apply 2 Frost to the attacker",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Frost");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                canBeBoosted: true
            );

            // === Lulu Unit ===
            AddFamilyUnit("lulu", "Lulu", "companions/lulu", 6, 2, 3, 50,
                "Lulu defends her family with snowy retaliation.",
                startSStacks: new[] { SStack("When Ally is Hit Apply Frost To Attacker", 2) }
            );

            // === Poppy Effect: Smackback Apply 1 Demonize ===
            AddStatusEffect<StatusEffectApplyXWhenHit>(
                "When Hit Apply Demonize To Attacker",
                "When hit, apply 1 Demonize to the attacker",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Demonize");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                canBeBoosted: true
            );

            // === Poppy Unit ===
            AddFamilyUnit("poppy", "Poppy", "companions/poppy", 11, 2, 4, 50,
                "Ferocious little guardian who fights back hard.",
                startSStacks: new[] { SStack("When Hit Apply Demonize To Attacker", 2) }
            )
            .SetTraits(new CardData.TraitStacks[]
            {
                new CardData.TraitStacks(TryGet<TraitData>("Smackback"), 1)
            });

            // 1. Register the base status effect FIRST — BEFORE charm creation
            AddStatusEffect<StatusEffectApplyXWhenAllyIsHit>(
                "When Ally is Hit Apply Frost To Attacker",
                "When an ally is hit, apply 1 frost to them",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Frost");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                },
                canBeBoosted: true
            );

            AddStatusEffect<StatusEffectApplyXOnTurn>(
                "Collect Bling On Trigger",
                "Gain 1 Bling when triggered",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Gain Gold");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                },
                canBeBoosted: false
            );

            // Effect 1: Gain +2 Counter (when deployed)
            AddStatusEffect<StatusEffectApplyXWhenDeployed>(
                "FrostMoon Increase Max Counter",
                "When deployed, gain +2 counter",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Increase Max Counter");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                }
            );

            // Effect 2: Apply 5 Frost on Attack to Target
            AddStatusEffect<StatusEffectApplyXOnTurn>(
                "FrostMoon Apply Frostburn On Attack",
                "On attack, apply 5 Frostburn",
                data =>
                {
                    data.effectToApply = TryGet<StatusEffectData>("Frost");
                    data.applyToFlags = StatusEffectApplyX.ApplyToFlags.FrontEnemy;
                }
            );


            // 2. THEN create the charm
            if (!IsAlreadyRegistered<CardUpgradeData>("charm-pug_charm"))
            {
                var pugCharm = AddCharm("pug_charm", "Pug Charm", "When an ally is hit, apply 1 frost to them", "GeneralCharmPool", "charms/pug_charm", 2)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.effects = new StatusEffectStacks[]
                        {
                SStack("When Ally is Hit Apply Frost To Attacker", 1)
                        };
                    });
            }

            var godlenVialCharm = AddCharm("golden_vial", "Golden Vial Charm", "Gain 1 Bling when triggered", "GeneralCharmPool", "charms/golden_vial_charm", 2).SubscribeToAfterAllBuildEvent(data =>
            {
                data.effects = new StatusEffectStacks[]
                {
                                  SStack("Collect Bling On Trigger", 1)
                };
            });

            // Final Charm
            var frostMoonCharm = AddCharm("frost_moon", "Frost Moon Charm", "Gain +2 Counter and apply 5 Frostburn on attack", "GeneralCharmPool", "charms/frost_moon_charm", 3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new StatusEffectStacks[]
                    {
                        SStack("FrostMoon Increase Max Counter", 2),
                        SStack("FrostMoon Apply Frostburn On Attack", 5)
                    };
                });

            // Adding TargetConstraintIsItem to charms with Consume trait
            var sodaCharm = AddCharm("soda_charm", "Soda Charm", "Gain Barrage, Frenzy x3, Consume. Halve all current effects.", "GeneralCharmPool", "charms/soda_charm", 3)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.giveTraits = new TraitStacks[]
                    {
                        TStack("Barrage", 1),
                        TStack("Consume", 1),
                    };

                    data.effects = new StatusEffectStacks[]
                    {
                        SStack("Reduce Effects", 2),
                        SStack("MultiHit", 3)
                    };

                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
                    };
                });

            var pizzaCharm = AddCharm("pizza_charm", "Pizza Charm", "Hits all enemies. Consume.", "GeneralCharmPool", "charms/pizza_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.giveTraits = new TraitStacks[]
                    {
                        TStack("Barrage", 1),
                        TStack("Consume", 1)
                    };

                    data.targetConstraints = new TargetConstraint[]
                    {
                        ScriptableObject.CreateInstance<TargetConstraintIsItem>()
                    };
                });

            // Restoring plantCharm, bookCharm, and duckCharm
            var plantCharm = AddCharm("plant_charm", "Plant Charm", "Gain +1 Attack after attacking", "GeneralCharmPool", "charms/plant_charm", 2)
    .SubscribeToAfterAllBuildEvent(data =>
    {
        data.effects = new StatusEffectStacks[]
        {
            SStack("On Turn Add Attack To Self", 1)
        };
    }).WithText("On Turn Add {0} Attack To Self");

            var bookCharm = AddCharm("book_charm", "Book Charm", "Draw 1 on deploy and each turn", "GeneralCharmPool", "charms/book_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new StatusEffectStacks[]
                    {

                    };
                    data.giveTraits = new TraitStacks[]
                    {
            TStack("Draw", 1)
                    };
                });

            var duckCharm = AddCharm("duck_charm", "Duck Charm", "Gain Frenzy, Aimless, and set Attack to 1", "GeneralCharmPool", "charms/duck_charm", 2)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.effects = new StatusEffectStacks[]
                    {
            SStack("When Hit Add Frenzy To Self", 1),
            SStack("Set Attack", 1),
            SStack("MultiHit", 1)
                    };

                    data.giveTraits = new TraitStacks[]
                    {
            TStack("Aimless", 1)
                    };
                });
        }



        private CardDataBuilder AddFamilyUnit(
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
        }
        private CardUpgradeDataBuilder AddCharm(
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
            });

            assets.Add(builder);
            return builder;
        }


        private StatusEffectDataBuilder AddStatusEffect<T>(string id, string text, Action<T> modify, string type = null, bool canBeBoosted = false, string textInsert = null) where T : StatusEffectData
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

        private StatusEffectDataBuilder AddInstantStatusEffect<T>(string id, string text, Action<T> modify, string type = null, bool canBeBoosted = false, string textInsert = null) where T : StatusEffectData
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

        private StatusEffectDataBuilder AddCopiedStatusEffect<T>(string from, string to, Action<T> modify, string? text = null, string? textInsert = null) where T : StatusEffectData
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
        private ClassDataBuilder TribeCopy(string oldName, string newName) => DataCopy<ClassData, ClassDataBuilder>(oldName, newName);

        private T[] DataList<T>(params string[] names) where T : DataFile => names.Select((s) => TryGet<T>(s)).ToArray();

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

        private CardDataBuilder AddItemCard(
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
            AddItemCard(
                "Snow_pillow", "Snow Pillow", "items/snowpillow",
                "A pillow made of snow.", 50,
                attackSStacks: new[] {
                    SStack("Heal", 6),
                    SStack("Snow", 1)
                    }
            );
            AddItemCard(
                "refreshing_water", "Refreshing Water", "items/refreshingwater",
                "A bottle of refreshing water.", 40,
                traitSStacks: new List<CardData.TraitStacks> {
                        TStack("Consume", 1),
                        TStack("Zoomlin", 1)
                }
            ).SubscribeToAfterAllBuildEvent(data =>
            {
                data.attackEffects = new[] {
                    SStack("Cleanse With Text", 1)
                };
            });

            AddItemCard(
                "wisp_mask", "Wisp Mask", "items/wispmask",
                "A mask with the ability to summon wisps.", 60,
                traitSStacks: new List<CardData.TraitStacks> {
                        TStack("Consume", 1),
                        TStack("Zoomlin", 1)
                }
            ).SubscribeToAfterAllBuildEvent(data =>
            {
                data.startWithEffects = new[] {
                    SStack("Summon Wisp", 1)
                };
                data.canPlayOnHand = false;
                data.canPlayOnEnemy = false;
                data.playOnSlot = true;

            }).SetDamage(null);
        }
    }

 
            AddItemCard(
                "cheese_crackers", "Cheese Crackers", "items/cheese_crackers",
                "A pack of cheese crackers.", 10,
                startSStacks: new[] {
                    SStack("MultiHit", 2)
                },
                           traitSStacks: new List<CardData.TraitStacks>
                           {
                               TStack("Aimless", 1)
                           }
            ).SubscribeToAfterAllBuildEvent(data =>
    {
        data.attackEffects = new CardData.StatusEffectStacks[]
        {
            new CardData.StatusEffectStacks(Get<StatusEffectData>("Increase Attack"), 1),
        };
        ;
    });
            AddItemCard(
                     "foam_bullets", "Foam Bullets", "items/foam_bullets",
                     "A pack of foam bullets.", 10,
                     startSStacks: new[] {
                         SStack("Hit All Enemies", 1)
                     },
                     traitSStacks: new List<CardData.TraitStacks>
                     {
                         TStack("Noomlin", 1)
                     }
                 );
        }
    }
}

