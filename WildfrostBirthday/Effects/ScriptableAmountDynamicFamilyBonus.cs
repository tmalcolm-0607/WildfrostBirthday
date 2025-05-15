using System.Linq;
using UnityEngine;

namespace WildfrostBirthday.Effects
{
    // Calculates the number of MadFamily leaders/companions in all player zones
    [CreateAssetMenu(menuName = "ScriptableAmounts/DynamicFamilyBonus", fileName = "ScriptableAmountDynamicFamilyBonus")]
    public class ScriptableAmountDynamicFamilyBonus : ScriptableAmount
    {
        public override int Get(Entity entity)
        {
            // Try to access all player zones (deck, reserve, etc.)
            var deck = References.PlayerData.inventory.deck.list;
            var reserve = References.PlayerData.inventory.reserve.list;
            // If hand/discard are available, add them here
            var all = deck.Concat(reserve);
            // Count MadFamily units by ID prefix (customize as needed)
            return all.Count(card => card != null && (card.id.ToString().Contains("companion-") || card.id.ToString().Contains("leader-")));
        }
    }
}
