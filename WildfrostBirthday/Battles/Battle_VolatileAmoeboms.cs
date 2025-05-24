using Deadpan.Enums.Engine.Components.Modding;
using Deadpan.Enums.Engine.Components;
using System.Collections.Generic;
using System;
using UnityEngine;

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
            this.wave = wave;
        }

        public override void InsertCard(int index, CardData card)
        {
            if (wave.units == null) wave.units = new List<CardData>();
            wave.units.Insert(index, card);
        }

        public override void AddCard(CardData card)
        {
            if (wave.units == null) wave.units = new List<CardData>();
            wave.units.Add(card);
        }

        public override CardData PeekCardData(int index) => wave.units[index];
        public override string GetCardName(int index) => wave.units[index].name;
        public override CardData GetCardData(int index) => wave.units[index];
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
            if (mod == null)
            {
                Debug.LogError("VolatileAmoebomsBattleScript not initialized with mod!");
                return new SaveCollection<BattleWaveManager.WaveData>();
            }

            Debug.Log($"[VolatileAmoeboms] Attempting to create battle with points: {points}");
            Debug.Log($"[VolatileAmoeboms] Mod GUID: {mod.GUID}");

            var waves = new SaveCollection<BattleWaveManager.WaveData>();

            try
            {
                // Get required cards
                CardData sulfurBom = mod.TryGet<CardData>("sulfur_bom");
                CardData colossalAmoebom = mod.TryGet<CardData>("colossal_amoebom");
                CardData dodecahebom = mod.TryGet<CardData>("dodecahebom");                // Wave 1: Sulfur Bom + Colossal Amoebom
                var wave1 = new BattleWaveManager.Wave();
                wave1.counter = 4;
                wave1.isBossWave = false;
                wave1.units = new List<CardData> { sulfurBom, colossalAmoebom };
                waves.Add(new SimpleWaveData(wave1));

                // Wave 2: Triple Sulfur Boms
                var wave2 = new BattleWaveManager.Wave();
                wave2.counter = 4;
                wave2.isBossWave = false;
                wave2.units = new List<CardData> { sulfurBom, sulfurBom, sulfurBom };
                waves.Add(new SimpleWaveData(wave2));

                // Wave 3: Boss Wave - Colossal Amoebom + Sulfur Bom + Dodecahebom
                var wave3 = new BattleWaveManager.Wave();
                wave3.counter = 4;
                wave3.isBossWave = true;
                wave3.units = new List<CardData> { colossalAmoebom, sulfurBom, dodecahebom };
                waves.Add(new SimpleWaveData(wave3));

                return waves;
            }
            catch (Exception e)
            {
                Debug.LogError($"[VolatileAmoeboms] Failed to create battle: {e.Message}");
                return waves;
            }
        }
    }
}
