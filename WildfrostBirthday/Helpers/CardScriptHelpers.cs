// Helper class for card script generation to avoid duplicating code across card files

using UnityEngine;

namespace WildfrostBirthday.Helpers
{
    public static class CardScriptHelpers
    {
        public static CardScript GetGiveUpgradeScript(WildFamilyMod mod, string name = "Crown")
        {
            var script = ScriptableObject.CreateInstance<CardScriptGiveUpgrade>();
            script.name = $"Give {name}";
            script.upgradeData = mod.TryGet<CardUpgradeData>(name);
            return script;
        }

        public static CardScript GetRandomHealthScript(int min, int max)
        {
            var health = ScriptableObject.CreateInstance<CardScriptAddRandomHealth>();
            health.name = "Random Health";
            health.healthRange = new Vector2Int(min, max);
            return health;
        }

        public static CardScript GetRandomDamageScript(int min, int max)
        {
            var damage = ScriptableObject.CreateInstance<CardScriptAddRandomDamage>();
            damage.name = "Give Damage";
            damage.damageRange = new Vector2Int(min, max);
            return damage;
        }

        public static CardScript GetRandomCounterScript(int min, int max)
        {
            var counter = ScriptableObject.CreateInstance<CardScriptAddRandomCounter>();
            counter.name = "Give Counter";
            counter.counterRange = new Vector2Int(min, max);
            return counter;
        }
        
        public class Scriptable<T> where T : ScriptableObject, new()
        {
            const string credit = "Credit to Hopeful";
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
    }
}
