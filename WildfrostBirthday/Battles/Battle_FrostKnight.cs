using Deadpan.Enums.Engine.Components.Modding;
using Deadpan.Enums.Engine.Components;
using System.Collections.Generic;
using System;
using UnityEngine;
using Dead;
using WildfrostBirthday.Helpers;

namespace WildfrostBirthday.Battles
{    /// <summary>
    /// A battle encounter featuring Frost Knight and their allies
    /// Wave 1: Frost Knight + Grink + Frostinger
    /// Wave 2: Frost Spearman + Frostinger + Mimik
    /// Wave 3: Mini Forge + Frost Crossbowman + Spuncher
    /// </summary>
    public static class Battle_Frost_Knight
    {
        public static void Register(WildFamilyMod mod)
        {
            var builder = new BattleDataBuilder(mod)
                .Create("battle_frost_knight")
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    data.title = "Frost Knight";
                    data.waveCounter = 5;
                    data.pools = new BattleWavePoolData[]
                    {
                        // Wave 1: Frost Knight + Grink + Frostinger
                        new Scriptable<BattleWavePoolData>(bwpd =>
                        {
                            bwpd.weight = 1;
                            bwpd.forcePulls = 1;
                            bwpd.maxPulls = 1;
                            bwpd.waves = new BattleWavePoolData.Wave[]
                            {
                                new BattleWavePoolData.Wave()
                                {
                                    units = new List<CardData>
                                    {
                                        mod.TryGet<CardData>("frost_knight"),
                                        mod.TryGet<CardData>("Grink"),
                                        mod.TryGet<CardData>("frostmedic"),

                                    },
                                    value = 125,
                                    positionPriority = 0,
                                    fixedOrder = true,
                                    maxSize = 3,
                                }
                            };
                        }),

                        // Wave 2: Two hazelnuts and two walnuts
                        new Scriptable<BattleWavePoolData>(bwpd =>
                        {
                            bwpd.weight = 1;
                            bwpd.forcePulls = 1;
                            bwpd.maxPulls = 1;
                            bwpd.waves = new BattleWavePoolData.Wave[]
                            {
                                new BattleWavePoolData.Wave()
                                {
                                    units = new List<CardData>
                                    {
                                        
                                        mod.TryGet<CardData>("frostspearman"),
                                        mod.TryGet<CardData>("Frostinger"),
                                        mod.TryGet<CardData>("Mimik"),
                                    },
                                    value = 125,
                                    positionPriority = 1,
                                    fixedOrder = false,
                                    maxSize = 3,
                                }
                            };
                        }),
                        
                        // Wave 3: two walnuts and two hazelnuts
                        new Scriptable<BattleWavePoolData>(bwpd =>
                        {
                            bwpd.weight = 1;
                            bwpd.forcePulls = 1;
                            bwpd.maxPulls = 1;
                            bwpd.waves = new BattleWavePoolData.Wave[]
                            {
                                new BattleWavePoolData.Wave()
                                {
                                    units = new List<CardData>
                                    {
                                        mod.TryGet<CardData>("MiniForge"),
                                        mod.TryGet<CardData>("frostcrossbowman"),
                                        mod.TryGet<CardData>("Spuncher"),
                                        
                                    },
                                    value = 0,
                                    positionPriority = 9,
                                    fixedOrder = false,
                                    maxSize = 3,
                                }
                            };
                        })
                    };

                  

                    // Set up generation and setup scripts
                    data.generationScript = new Scriptable<BattleGenerationScriptWaves>();
                    data.setUpScript = new Scriptable<ScriptBattleSetUp>();

                    // Set battle sprite
                    data.sprite = "battles/frost_knight".ToSprite();

                    // Set localized name reference
                    data.nameRef = Extensions.GetLocalizedString("UI Text", "map_battle_frost_knight");
                });

            mod.assets.Add(builder);
            Debug.Log($"[Frost Knight] Registered battle data: battle_frost_knight");
        }
    }
}