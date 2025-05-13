// Helper class for data collection operations

using System.Collections.Generic;
using System.Linq;

namespace WildfrostBirthday.Helpers
{
    public static class DataUtilities
    {
        /// <summary>
        /// Creates an array of DataFile objects from their names
        /// </summary>
        /// <typeparam name="T">The type of DataFile to retrieve</typeparam>
        /// <param name="mod">The mod instance</param>
        /// <param name="names">The names of the DataFile objects to retrieve</param>
        /// <returns>An array of retrieved DataFile objects</returns>
        public static T[] DataList<T>(this WildFamilyMod mod, params string[] names) where T : DataFile 
            => names.Select((s) => mod.TryGet<T>(s)).ToArray();

        /// <summary>
        /// Removes null and mod-added DataFile objects from an array
        /// </summary>
        /// <typeparam name="T">The type of DataFile in the array</typeparam>
        /// <param name="mod">The mod instance</param>
        /// <param name="data">The array of DataFile objects to clean</param>
        /// <returns>A new array with null and mod-added objects removed</returns>
        public static T[] RemoveNulls<T>(this WildFamilyMod mod, T[] data) where T : DataFile
        {
            List<T> list = data.ToList();
            list.RemoveAll(x => x == null || x.ModAdded == mod);
            return list.ToArray();
        }
    }
}
