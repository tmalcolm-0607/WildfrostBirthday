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
            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] Previous mainsprite name:{location} {(card.mainSprite != null ? card.mainSprite.name : "null")} {lastCounter} : {card.counter}");
                int counter = card.counter;
            if (counter != lastCounter)
            {
                int clamped = Mathf.Clamp(counter, 0, 3);
                UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] Previous mainsprite name:{location} {(card.mainSprite != null ? card.mainSprite.name : "null")}");
                UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] Changing sprite for {card.name} to {baseImagePath}{clamped}.png");
                string path = Path.Combine(location, $"{baseImagePath}{clamped}.png");
                card.mainSprite = path.ToSprite();
                UnityEngine.Debug.Log($"[CardScriptChangeMainOnCounter] New mainsprite name: {(card.mainSprite != null ? card.mainSprite.name : "null")}");
                lastCounter = counter;
            }
        }
    }
}
