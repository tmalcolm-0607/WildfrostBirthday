// Helper methods for asset management and retrieval
using Dead;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WildfrostBirthday.Helpers
{
    /// <summary>
    /// Utility class for asset management and retrieval
    /// </summary>
    public static class AssetHelpers
    {
        /// <summary>
        /// Checks if an asset with the given ID is already registered
        /// </summary>
        /// <typeparam name="T">The type of DataFile to check</typeparam>
        /// <param name="mod">The mod instance</param>
        /// <param name="id">The ID of the asset to check</param>
        /// <returns>True if the asset is already registered, false otherwise</returns>
        public static bool IsAlreadyRegistered<T>(this WildFamilyMod mod, string id) where T : DataFile
        {
            try
            {
                var fullId = Extensions.PrefixGUID(id, mod).ToLower();
                var asset = AddressableLoader.Get<T>(typeof(T).Name, fullId) ?? mod.Get<T>(id);
                return asset != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Safely gets an asset of the specified type by name, throwing an exception if not found
        /// </summary>
        /// <typeparam name="T">The type of DataFile to retrieve</typeparam>
        /// <param name="mod">The mod instance</param>
        /// <param name="name">The name of the asset to retrieve</param>
        /// <returns>The retrieved asset</returns>
        /// <exception cref="Exception">Thrown when the asset cannot be found</exception>
        public static T TryGet<T>(this WildFamilyMod mod, string name) where T : DataFile
        {
            T? data;
            if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
                data = mod.Get<StatusEffectData>(name) as T;
            else if (typeof(KeywordData).IsAssignableFrom(typeof(T)))
                data = (AddressableLoader.Get<KeywordData>("KeywordData", Extensions.PrefixGUID(name, mod).ToLower()) ?? mod.Get<KeywordData>(name.ToLower())) as T;
            else
                data = mod.Get<T>(name);

            if (data == null)
                throw new Exception($"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{name}] or [{Extensions.PrefixGUID(name, mod)}]");
            return data;
        }
    }
}
