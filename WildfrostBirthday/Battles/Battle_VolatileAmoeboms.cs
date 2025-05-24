using Deadpan.Enums.Engine.Components.Modding;
using Deadpan.Enums.Engine.Components;
using System.Collections.Generic;
using System;
using UnityEngine;
using Dead;

namespace WildfrostBirthday.Battles
{
    /// <summary>
    /// A battle encounter featuring three waves:
    /// Wave 1: Sulfur Bom + Colossal Amoebom
    /// Wave 2: Triple Sulfur Boms
    /// Wave 3 (Boss): Colossal Amoebom + Sulfur Bom + Dodecahebom
    /// </summary>
    public static class Battle_VolatileAmoeboms 
    {
        public static void Register(WildFamilyMod mod)
        {
            // Create the battle generation script for the waves
            var script = ScriptableObject.CreateInstance<VolatileAmoebomsBattleScript>();
            script.Initialize(mod);

            // Create the battle data
            var builder = new BattleDataBuilder(mod)
                .Create("battle_volatile_amoeboms")
                .WithTitle("Volatile Amoeboms")
                .WithWaveCounter(4) // 4-turn initial countdown
                .WithGenerationScript(script)
                .WithSprite("battles/volatile_amoeboms.png");

            mod.assets.Add(builder);
            Debug.Log($"[VolatileAmoeboms] Registered battle data: battle_volatile_amoeboms");
        }
    }

    /// <summary>
    /// Simple wave data implementation that wraps a Wave
    /// </summary>
    public class SimpleWaveData : BattleWaveManager.WaveData 
    {
        private BattleWaveManager.Wave wave;

        public SimpleWaveData(BattleWaveManager.Wave wave)
        {
            this.wave = wave ?? new BattleWaveManager.Wave();
            
            // Ensure units is always initialized
            if (this.wave.units == null)
            {
                this.wave.units = new List<CardData>();
            }
        }

        public override void InsertCard(int index, CardData card)
        {
            if (card == null)
            {
                Debug.LogWarning("[SimpleWaveData] Attempted to insert null card");
                return;
            }
            
            if (wave.units == null) 
            {
                wave.units = new List<CardData>();
            }
            
            // Ensure index is valid
            if (index < 0 || index > wave.units.Count)
            {
                Debug.LogWarning($"[SimpleWaveData] Invalid index {index}, adding card to end instead");
                wave.units.Add(card);
            }
            else
            {
                wave.units.Insert(index, card);
            }
        }

        public override void AddCard(CardData card)
        {
            if (card == null)
            {
                Debug.LogWarning("[SimpleWaveData] Attempted to add null card");
                return;
            }
            
            if (wave.units == null)
            {
                wave.units = new List<CardData>();
            }
            
            wave.units.Add(card);
        }        public override CardData PeekCardData(int index)
        {
            if (wave.units == null || index < 0 || index >= wave.units.Count)
            {
                Debug.LogWarning($"[SimpleWaveData] Cannot peek card at index {index}, units collection is null or index out of range");
                return ScriptableObject.CreateInstance<CardData>(); // Return a dummy card instead of null
            }
            return wave.units[index];
        }
        
        public override string GetCardName(int index)
        {
            if (wave.units == null || index < 0 || index >= wave.units.Count)
            {
                Debug.LogWarning($"[SimpleWaveData] Cannot get card name at index {index}, units collection is null or index out of range");
                return "Unknown";
            }
            
            var card = wave.units[index];
            return card != null ? card.name : "Null Card";
        }
          public override CardData GetCardData(int index)
        {
            if (wave.units == null || index < 0 || index >= wave.units.Count)
            {
                Debug.LogWarning($"[SimpleWaveData] Cannot get card data at index {index}, units collection is null or index out of range");
                return ScriptableObject.CreateInstance<CardData>(); // Return a dummy card instead of null
            }
            return wave.units[index];
        }
        
        public override bool AddUpgradeToCard(int index, CardUpgradeData upgrade) => false;
    }

    public class VolatileAmoebomsBattleScript : BattleGenerationScriptWaves
    {
        private WildFamilyMod? mod;

        public void Initialize(WildFamilyMod mod)
        {
            this.mod = mod;
        }

        public override SaveCollection<BattleWaveManager.WaveData> Run(BattleData battleData, int points)
        {
            // Create a proper initialized collection - this should never be null
            var waves = new SaveCollection<BattleWaveManager.WaveData>();
            
            if (mod == null)
            {
                Debug.LogError("[VolatileAmoeboms] VolatileAmoebomsBattleScript not initialized with mod!");
                return waves;
            }

            Debug.Log($"[VolatileAmoeboms] Attempting to create battle with points: {points}");
            Debug.Log($"[VolatileAmoeboms] Mod GUID: {mod.GUID}");

            try
            {
                // Get required cards
                CardData sulfurBom = mod.TryGet<CardData>("sulfur_bom");
                if (sulfurBom == null)
                {
                    Debug.LogError("[VolatileAmoeboms] Could not find SulfurBom card!");
                    return waves;
                }
                
                CardData colossalAmoebom = mod.TryGet<CardData>("colossal_amoebom");
                if (colossalAmoebom == null)
                {
                    Debug.LogError("[VolatileAmoeboms] Could not find ColossalAmoebom card!");
                    return waves;
                }
                
                CardData dodecahebom = mod.TryGet<CardData>("dodecahebom");
                if (dodecahebom == null)
                {
                    Debug.LogError("[VolatileAmoeboms] Could not find Dodecahebom card!");
                    return waves;
                }
                
                Debug.Log("[VolatileAmoeboms] All required cards found, building waves");

                // Wave 1: Sulfur Bom + Colossal Amoebom
                var wave1 = new BattleWaveManager.Wave();
                wave1.counter = 4;
                wave1.isBossWave = false;
                wave1.units = new List<CardData> { sulfurBom, colossalAmoebom };
                waves.Add(new SimpleWaveData(wave1));
                Debug.Log("[VolatileAmoeboms] Added Wave 1");

                // Wave 2: Triple Sulfur Boms
                var wave2 = new BattleWaveManager.Wave();
                wave2.counter = 4;
                wave2.isBossWave = false;
                wave2.units = new List<CardData> { sulfurBom, sulfurBom, sulfurBom };
                waves.Add(new SimpleWaveData(wave2));
                Debug.Log("[VolatileAmoeboms] Added Wave 2");

                // Wave 3: Boss Wave - Colossal Amoebom + Sulfur Bom + Dodecahebom
                var wave3 = new BattleWaveManager.Wave();
                wave3.counter = 4;
                wave3.isBossWave = true;
                wave3.units = new List<CardData> { colossalAmoebom, sulfurBom, dodecahebom };
                waves.Add(new SimpleWaveData(wave3));
                Debug.Log("[VolatileAmoeboms] Added Wave 3 (Boss Wave)");
                
                // Validate our waves collection before returning
                if (waves.Count == 0)
                {
                    Debug.LogError("[VolatileAmoeboms] No waves were added to the collection!");
                }
                else
                {
                    Debug.Log($"[VolatileAmoeboms] Successfully created battle with {waves.Count} waves");
                }

                return waves;
            }
            catch (Exception e)
            {
                Debug.LogError($"[VolatileAmoeboms] Failed to create battle: {e.Message}");
                Debug.LogError($"[VolatileAmoeboms] Stack trace: {e.StackTrace}");
                return waves;
            }
        }
    }
}
