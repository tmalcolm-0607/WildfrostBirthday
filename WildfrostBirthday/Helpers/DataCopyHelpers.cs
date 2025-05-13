// Helper class for copying data between objects

using UnityEngine;

namespace WildfrostBirthday.Helpers
{
    public static class DataCopyHelpers
    {
        /// <summary>
        /// Creates a copy of a StatusEffectData with a new name
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="oldName">The name of the status effect to copy</param>
        /// <param name="newName">The new name for the copied status effect</param>
        /// <returns>A StatusEffectDataBuilder for the copied status effect</returns>
        public static StatusEffectDataBuilder StatusCopy(this WildFamilyMod mod, string oldName, string newName)
        {
            StatusEffectData data = mod.TryGet<StatusEffectData>(oldName).InstantiateKeepName();
            data.name = newName;
            data.targetConstraints = new TargetConstraint[0];
            var builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>();
            builder.Mod = mod;
            return builder;
        }
        
        /// <summary>
        /// Creates a copy of a CardData with a new name
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="oldName">The name of the card to copy</param>
        /// <param name="newName">The new name for the copied card</param>
        /// <returns>A CardDataBuilder for the copied card</returns>
        public static CardDataBuilder CardCopy(this WildFamilyMod mod, string oldName, string newName) 
            => DataCopy<CardData, CardDataBuilder>(mod, oldName, newName);
        
        /// <summary>
        /// Creates a copy of a ClassData (Tribe) with a new name
        /// </summary>
        /// <param name="mod">The mod instance</param>
        /// <param name="oldName">The name of the tribe to copy</param>
        /// <param name="newName">The new name for the copied tribe</param>
        /// <returns>A ClassDataBuilder for the copied tribe</returns>
        public static ClassDataBuilder TribeCopy(this WildFamilyMod mod, string oldName, string newName) 
            => DataCopy<ClassData, ClassDataBuilder>(mod, oldName, newName);

        /// <summary>
        /// Generic method to create a copy of any DataFile with a new name
        /// </summary>
        /// <typeparam name="Y">The type of DataFile to copy</typeparam>
        /// <typeparam name="T">The type of DataFileBuilder to create</typeparam>
        /// <param name="mod">The mod instance</param>
        /// <param name="oldName">The name of the data to copy</param>
        /// <param name="newName">The new name for the copied data</param>
        /// <returns>A DataFileBuilder for the copied data</returns>
        public static T DataCopy<Y, T>(this WildFamilyMod mod, string oldName, string newName) 
            where Y : DataFile 
            where T : DataFileBuilder<Y, T>, new()
        {
            Y data = mod.Get<Y>(oldName).InstantiateKeepName();
            data.name = mod.GUID + "." + newName;
            T builder = data.Edit<Y, T>();
            builder.Mod = mod;
            return builder;
        }
    }
}
