// Helper class for reward pool operations

using System.Collections.Generic;
using UnityEngine;

namespace WildfrostBirthday.Helpers
{
    public static class RewardPoolHelpers
    {
        /// <summary>
        /// Creates a reward pool with the given name, type, and list of items
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="name">The name of the reward pool</param>
        /// <param name="type">The type of reward pool (typically Units, Items, Charms, or Modifiers)</param>
        /// <param name="list">The array of DataFile objects in the reward pool</param>
        /// <returns>A RewardPool object</returns>
        public static RewardPool CreateRewardPool(this WildFamilyMod mod, string name, string type, DataFile[] list)
        {
            RewardPool pool = ScriptableObject.CreateInstance<RewardPool>();
            pool.name = name;
            pool.type = type;
            pool.list = list.ToList();
            return pool;
        }
        
        /// <summary>
        /// Removes all reward pool items that were added by this mod from all tribes
        /// </summary>
        /// <param name="mod">The mod instance</param>
        public static void UnloadFromClasses(this WildFamilyMod mod)
        {
            List<ClassData> tribes = AddressableLoader.GetGroup<ClassData>("ClassData");
            foreach (ClassData tribe in tribes)
            {
                if (tribe == null || tribe.rewardPools == null) 
                { 
                    continue; 
                }

                foreach (RewardPool pool in tribe.rewardPools)
                {
                    if (pool == null) 
                    { 
                        continue; 
                    }

                    pool.list.RemoveAllWhere((item) => item == null || item.ModAdded == mod);
                }
            }
        }
        
        /// <summary>
        /// Fixes card images that should use the static image rather than a scriptable one
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="entity">The entity to fix the image for</param>
        public static void FixImage(this WildFamilyMod mod, Entity entity)
        {
            if (entity.display is Card card && !card.hasScriptableImage) 
            {
                card.mainImage.gameObject.SetActive(true);
            }
        }
    }
}
