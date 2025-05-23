
using Deadpan.Enums.Engine.Components.Modding;
using Deadpan.Enums.Engine.Components;
using System.Collections.Generic;
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

            // Create the battle data
            var builder = new BattleDataBuilder(mod)
                .Create("battle_volatile_amoeboms")
                .WithTitle("Volatile Amoeboms")
                .WithWaveCounter(4) // 4-turn initial countdown
                .WithGenerationScript(script);

            mod.assets.Add(builder);
        }
    }

    /// <summary>
    /// Custom wave data for storing our waves
    /// </summary>
    public class VolatileAmoebomWaveData : BattleWaveManager.WaveData 
    {
        private BattleWaveManager.Wave wave;

        public VolatileAmoebomWaveData(BattleWaveManager.Wave wave)
        {
            this.wave = wave;
            this.counter = wave.counter;
            this.isBossWave = wave.isBossWave;
        }

        public override void InsertCard(int index, CardData card)
        {
            if (wave.units == null)
                wave.units = new List<CardData>();
            wave.units.Insert(index, card);
        }

        public override void AddCard(CardData card)
        {
            if (wave.units == null)
                wave.units = new List<CardData>();
            wave.units.Add(card);
        }

        public override CardData PeekCardData(int index) => wave.units[index];
        public override string GetCardName(int index) => wave.units[index].name;
        public override CardData GetCardData(int index) => wave.units[index];
        public override bool AddUpgradeToCard(int index, CardUpgradeData upgrade) => false; // Not needed for our use case
    }

    public class VolatileAmoebomsBattleScript : BattleGenerationScriptWaves
    {
        public override SaveCollection<BattleWaveManager.WaveData> Run(BattleData battleData, int points)
        {
            var waves = new SaveCollection<BattleWaveManager.WaveData>();

            // Wave 1: Sulfur Bom + Colossal Amoebom
            var wave1 = new BattleWaveManager.Wave();
            wave1.counter = 4;
            wave1.isBossWave = false;
            wave1.units = new List<CardData>();
            wave1.units.Add(AddressableLoader.Get<CardData>("CardData", "sulfur_bom")); 
            wave1.units.Add(AddressableLoader.Get<CardData>("CardData", "colossal_amoebom"));
            waves.Add(new VolatileAmoebomWaveData(wave1));

            // Wave 2: Triple Sulfur Boms
            var wave2 = new BattleWaveManager.Wave();
            wave2.counter = 3;
            wave2.isBossWave = false;
            wave2.units = new List<CardData>();
            wave2.units.Add(AddressableLoader.Get<CardData>("CardData", "sulfur_bom"));
            wave2.units.Add(AddressableLoader.Get<CardData>("CardData", "sulfur_bom"));
            wave2.units.Add(AddressableLoader.Get<CardData>("CardData", "sulfur_bom"));
            waves.Add(new VolatileAmoebomWaveData(wave2));

            // Wave 3 (Boss): Colossal Amoebom + Sulfur Bom + Dodecahebom 
            var wave3 = new BattleWaveManager.Wave();
            wave3.counter = 4;
            wave3.isBossWave = true;
            wave3.units = new List<CardData>();
            wave3.units.Add(AddressableLoader.Get<CardData>("CardData", "colossal_amoebom"));
            wave3.units.Add(AddressableLoader.Get<CardData>("CardData", "sulfur_bom")); 
            wave3.units.Add(AddressableLoader.Get<CardData>("CardData", "dodecahebom"));
            waves.Add(new VolatileAmoebomWaveData(wave3));

            return waves;
        }
    }
}