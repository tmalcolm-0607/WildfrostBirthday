using UnityEngine;

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
            int counter = card.counter;
            if (counter != lastCounter)
            {
                int clamped = Mathf.Clamp(counter, 0, 3);
                string path = $"{baseImagePath}{clamped}.png";
                card.mainSprite = path.ToSprite();
                lastCounter = counter;
            }
        }
    }
}
