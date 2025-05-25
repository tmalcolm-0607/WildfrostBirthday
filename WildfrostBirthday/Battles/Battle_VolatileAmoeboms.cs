using Deadpan.Enums.Engine.Components.Modding;
using Deadpan.Enums.Engine.Components;
using System.Collections.Generic;
using System;
using UnityEngine;
using Dead;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace WildfrostBirthday.Battles
{
    /// <summary>
    /// Helper class for creating scriptable objects with initialization
    /// </summary>
    public class Scriptable<T> where T : ScriptableObject, new()
    {
        readonly Action<T> modifier;
        public Scriptable() { }
        public Scriptable(Action<T> modifier) { this.modifier = modifier; }
        public static implicit operator T(Scriptable<T> scriptable)
        {
            T result = ScriptableObject.CreateInstance<T>();
            scriptable.modifier?.Invoke(result);
            return result;
        }
    }
    /// <summary>
    /// A battle encounter featuring three waves:
    /// Wave 1: Sulfur Bom + Colossal Amoebom
    /// Wave 2: Triple Sulfur Boms
    /// Wave 3 (Boss): Colossal Amoebom + Sulfur Bom + Dodecahebom
    /// </summary>
    /// 
    
    public static class Battle_VolatileAmoeboms 
    {    public static void Register(WildFamilyMod mod)
    {
        // Create string table entry for the battle name
        string battleId = "volatile_amoeboms";
        string displayName = "The Volatile Amoeboms";
        
        var builder = new BattleDataBuilder(mod)
            .Create(battleId)
            .WithTitle("Battle!")
            .SubscribeToAfterAllBuildEvent(data =>
            {
                data.title = "Battle!";
                data.waveCounter = 4;
                data.pools = new BattleWavePoolData[]
                {
                    // Wave 1: Sulfur Bom + Colossal Amoebom
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
                                    mod.TryGet<CardData>("sulfur_bom"),
                                    mod.TryGet<CardData>("colossal_amoebom")
                                },
                                value = 125,
                                positionPriority = 0,
                                fixedOrder = false,
                                maxSize = 2,
                            }
                        };
                    }),
                    
                    // Wave 2: Triple Sulfur Boms
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
                                    mod.TryGet<CardData>("sulfur_bom"),
                                    mod.TryGet<CardData>("sulfur_bom"),
                                    mod.TryGet<CardData>("sulfur_bom")
                                },
                                value = 125,
                                positionPriority = 1,
                                fixedOrder = true,
                                maxSize = 3,
                            }
                        };
                    }),
                    
                    // Wave 3 (Boss): Colossal Amoebom + Sulfur Bom + Dodecahebom
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
                                    mod.TryGet<CardData>("colossal_amoebom"),
                                    mod.TryGet<CardData>("sulfur_bom"),
                                    mod.TryGet<CardData>("dodecahebom")
                                },
                                value = 0,
                                positionPriority = 9,
                                fixedOrder = true,
                                maxSize = 3,
                            }
                        };
                    })
                };

                // Set up gold giver pool (optional)
                data.goldGiverPool = new CardData[]
                {
                    mod.TryGet<CardData>("Gobling")
                };

                // Set up generation and setup scripts
                data.generationScript = new Scriptable<BattleGenerationScriptWaves>();
                data.setUpScript = new Scriptable<ScriptBattleSetUp>();                // Set the sprite for both battle and map icon
                data.sprite = "battles/volatile_amoeboms".ToSprite();

                // Set the display name for the battle
                var localizedString = new LocalizedString("Battles", "volatile_amoeboms");
                data.nameRef = localizedString;

                // Add the localization entry
                var collection = LocalizationHelper.GetCollection("Battles", new LocaleIdentifier(SystemLanguage.English));
                collection.SetString("volatile_amoeboms", displayName);
            });

        mod.assets.Add(builder);
        Debug.Log($"[VolatileAmoeboms] Registered battle data: battle_volatile_amoeboms");
    }
    }
}