using UnityEngine;
using System.IO;
using System.Reflection;

namespace WildfrostBirthday.Helpers
{
    // CardScript that changes the main sprite based on the counter value (0-3)
    public class CardScriptChangeMainOnCounter : CardScript
    {
        public string baseImagePath = ""; // e.g. "images/companions/poppy"
        public int lastCounter = -1;

        // This script should be called manually from a counter change event or similar
        public override void Run(CardData card)
        { 
            if (card == null)
            {
                UnityEngine.Debug.LogError("[CardScriptChangeMainOnCounter] Card is null");
                return;
            }

            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(location))
            {
                UnityEngine.Debug.LogError("[CardScriptChangeMainOnCounter] Could not get assembly location");
                return;
            }

            UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] Previous mainsprite name: {(card.mainSprite != null ? card.mainSprite.name : "null")} Counter: {lastCounter} : {card.counter}");
            int counter = card.counter;
            if (counter != lastCounter)
            {
                int clamped = Mathf.Clamp(counter, 0, 3);
                string fullPath = $"{baseImagePath}{clamped}.png";
                UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] Changing sprite for {card.name} to {fullPath}");
                
                string path = Path.Combine(location, fullPath);
                if (!File.Exists(path))
                {
                    UnityEngine.Debug.LogError($"[CardScriptChangeMainOnCounter] Sprite file does not exist: {path}");
                    return;
                }
                
                try
                {
                    Sprite newSprite = path.ToSprite();
                    if (newSprite == null)
                    {
                        UnityEngine.Debug.LogError($"[CardScriptChangeMainOnCounter] Failed to load sprite from path: {path}");
                        return;
                    }
                    card.mainSprite = newSprite;
                    UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] New mainsprite name: {(card.mainSprite != null ? card.mainSprite.name : "null")}");
                    lastCounter = counter;
                }
                catch (System.Exception ex)
                {
                    UnityEngine.Debug.LogError($"[CardScriptChangeMainOnCounter] Error loading sprite: {ex.Message}");
                }
            }
        }
    }
}
