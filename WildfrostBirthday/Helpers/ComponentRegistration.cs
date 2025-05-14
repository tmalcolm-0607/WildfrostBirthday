// Component registration utilities
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WildfrostBirthday.Helpers
{
    /// <summary>
    /// Utility class for automatically registering mod components
    /// </summary>
    public static class ComponentRegistration
    {
        private static Dictionary<string, List<Type>> _componentTypeCache = new Dictionary<string, List<Type>>();
        private static bool _initialized = false;

        /// <summary>
        /// Initializes the component type cache by scanning the assembly for registrable components
        /// </summary>
        private static void InitializeComponentCache()
        {
            if (_initialized)
                return;

            _componentTypeCache.Clear();
            
            // Get all types in our assembly
            Assembly assembly = typeof(ComponentRegistration).Assembly;
            Type[] types = assembly.GetTypes();

            // Find all static classes with the Register method
            foreach (Type type in types)
            {
                // Skip non-static classes (static classes are abstract and sealed)
                if (!type.IsAbstract || !type.IsSealed)
                    continue;                // Check if the class has a Register method with WildFamilyMod parameter
                MethodInfo? registerMethod = type.GetMethod("Register", 
                    BindingFlags.Public | BindingFlags.Static, 
                    null, 
                    new Type[] { typeof(WildFamilyMod) }, 
                    null);

                if (registerMethod == null)
                    continue;                // Categorize by type based on name prefix
                string typeName = type.Name;
                string? category = null;

                if (typeName.StartsWith("StatusEffect_"))
                    category = "StatusEffect";
                else if (typeName.StartsWith("Card_"))
                    category = "Card";
                else if (typeName.StartsWith("Item_"))
                    category = "Item";
                else if (typeName.StartsWith("Charm_"))
                    category = "Charm";
                else if (typeName.StartsWith("Tribe_"))
                    category = "Tribe";

                // Skip if we couldn't determine category
                if (category == null)
                    continue;

                // Add to appropriate list
                if (!_componentTypeCache.ContainsKey(category))
                    _componentTypeCache[category] = new List<Type>();

                _componentTypeCache[category].Add(type);
            }

            _initialized = true;
        }

        /// <summary>
        /// Registers all components of a specific category
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="category">The component category</param>
        private static void RegisterComponentsByCategory(WildFamilyMod mod, string category)
        {
            InitializeComponentCache();
            
            if (!_componentTypeCache.ContainsKey(category))
                return;

            foreach (Type type in _componentTypeCache[category])
            {
                try
                {
                    MethodInfo registerMethod = type.GetMethod("Register", 
                        BindingFlags.Public | BindingFlags.Static, 
                        null, 
                        new Type[] { typeof(WildFamilyMod) }, 
                        null);
                    
                    if (registerMethod != null)
                    {
                        // Skip Template classes as they're just examples
                        if (type.Name.Contains("Template"))
                            continue;
                            
                        UnityEngine.Debug.Log($"[{mod.Title}] Auto-registering {category}: {type.Name}");
                        registerMethod.Invoke(null, new object[] { mod });
                    }
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"[{mod.Title}] Error registering {type.Name}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Registers all status effects
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void RegisterAllStatusEffects(this WildFamilyMod mod)
        {
            RegisterComponentsByCategory(mod, "StatusEffect");
        }

        /// <summary>
        /// Registers all cards
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void RegisterAllCards(this WildFamilyMod mod)
        {
            RegisterComponentsByCategory(mod, "Card");
        }

        /// <summary>
        /// Registers all items
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void RegisterAllItems(this WildFamilyMod mod)
        {
            RegisterComponentsByCategory(mod, "Item");
        }

        /// <summary>
        /// Registers all charms
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void RegisterAllCharms(this WildFamilyMod mod)
        {
            RegisterComponentsByCategory(mod, "Charm");
        }

        /// <summary>
        /// Registers all tribes
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void RegisterAllTribes(this WildFamilyMod mod)
        {
            RegisterComponentsByCategory(mod, "Tribe");
        }

        /// <summary>
        /// Registers all components of all types
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void RegisterAllComponents(this WildFamilyMod mod)
        {
            UnityEngine.Debug.Log($"[{mod.Title}] Registering all components");

            mod.RegisterAllStatusEffects();
            mod.RegisterAllCards();
            mod.RegisterAllItems();
            mod.RegisterAllCharms();            
            mod.RegisterAllTribes();
        }
    }
}
