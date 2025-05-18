using UnityEngine;
using System.Linq;

// Custom CardScript for Duck Charm: set attack to 1, add MultiHit X (original attack)
public class CardScriptDuckCharmMultiHit : CardScript
{
    public override void Run(CardData card)
    {
        int originalAttack = card.damage > 0 ? card.damage -1 : 0;
        Debug.Log($"[CardScriptDuckCharmMultiHit] Running for card: {card.name}, original attack: {originalAttack}");
        if (originalAttack > 1)
        {
            card.damage = 1;
            int multiHitCount = originalAttack > 1 ? originalAttack - 1 : 0;
            Debug.Log($"[CardScriptDuckCharmMultiHit] Setting MultiHit count to: {multiHitCount}");
            // Check if the card has existing effects
            if (card.startWithEffects != null)
            {
                // Update existing MultiHit effect if present
                for (int i = 0; i < card.startWithEffects.Length; i++)
                {
                    if (card.startWithEffects[i].data.name == "MultiHit")
                    {
                        card.startWithEffects[i] = new CardData.StatusEffectStacks(card.startWithEffects[i].data, multiHitCount);
                        return;
                    }
                }
            }
            else
            {
                // Initialize attackEffects if null
                card.startWithEffects = new CardData.StatusEffectStacks[0];
            }

            // Add MultiHit effect if not present
            var multiHitEffect = Resources.FindObjectsOfTypeAll<StatusEffectData>().FirstOrDefault(x => x.name == "MultiHit");
            if (multiHitEffect != null)
            {
                card.startWithEffects = card.startWithEffects.Append(new CardData.StatusEffectStacks(multiHitEffect, multiHitCount)).ToArray();
            }
        }
    }
}