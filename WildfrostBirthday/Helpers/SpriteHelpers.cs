using System;
using System.IO;
using UnityEngine;
using System.Reflection;

namespace WildfrostBirthday.Helpers
{
    public static class SpriteHelpers
    {
        /// <summary>
        /// Safely loads a sprite from a path, with detailed error reporting
        /// </summary>
        /// <param name="basePath">The base path to the sprite</param>
        /// <param name="fileName">The filename including extension</param>
        /// <returns>The loaded Sprite, or null if the sprite couldn't be loaded</returns>
        public static Sprite SafeLoadSprite(string basePath, string fileName)
        {
            try
            {
                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.IsNullOrEmpty(location))
                {
                    UnityEngine.Debug.LogError($"[SafeLoadSprite] Could not get assembly location");
                    return null;
                }

                string fullPath = Path.Combine(location, basePath, fileName);
                UnityEngine.Debug.Log($"[SafeLoadSprite] Loading sprite from: {fullPath}");
                
                if (!File.Exists(fullPath))
                {
                    UnityEngine.Debug.LogError($"[SafeLoadSprite] Sprite file does not exist: {fullPath}");
                    return null;
                }
                
                Sprite sprite = fullPath.ToSprite();
                if (sprite == null)
                {
                    UnityEngine.Debug.LogError($"[SafeLoadSprite] Failed to load sprite from path: {fullPath}");
                    return null;
                }
                
                return sprite;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[SafeLoadSprite] Error loading sprite: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Checks if a sprite file exists at the given path
        /// </summary>
        /// <param name="basePath">The base path to the sprite</param>
        /// <param name="fileName">The filename including extension</param>
        /// <returns>True if the sprite file exists, false otherwise</returns>
        public static bool SpriteExists(string basePath, string fileName)
        {
            try
            {
                string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.IsNullOrEmpty(location))
                {
                    UnityEngine.Debug.LogError($"[SpriteExists] Could not get assembly location");
                    return false;
                }

                string fullPath = Path.Combine(location, basePath, fileName);
                bool exists = File.Exists(fullPath);
                
                UnityEngine.Debug.Log($"[SpriteExists] Checking sprite at: {fullPath}, Exists: {exists}");
                return exists;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[SpriteExists] Error checking sprite: {ex.Message}");
                return false;
            }
        }
    }
}
