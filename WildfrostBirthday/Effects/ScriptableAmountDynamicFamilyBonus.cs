using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace WildfrostBirthday.Effects
{
    // Calculates the number of MadFamily leaders/companions in all player zones
    [CreateAssetMenu(menuName = "ScriptableAmounts/DynamicFamilyBonus", fileName = "ScriptableAmountDynamicFamilyBonus")]
    public class ScriptableAmountDynamicFamilyBonus : ScriptableAmount
    {
        // Known MadFamily base game units (to be detected regardless of id pattern)
        private readonly string[] knownMadFamilyUnits = new[] {
            "Foxee",
            "Blunky",
            "Dimona",
            "Bombom",
            "BigBerry",
            "Bonnie",
            "Klutz",
            "Noggin",
            "LuminCat"
            // Add any other known Mad Family units here
        };

        public override int Get(Entity entity)
        {
            // Try to access all player zones (deck, reserve, etc.)
            var deck = References.PlayerData?.inventory?.deck?.list ?? new List<CardData>();
            var reserve = References.PlayerData?.inventory?.reserve?.list ?? new List<CardData>();
            // If hand/discard are available, add them here
            var all = deck.Concat(reserve).ToList();
            
            // Debug logging of all unit IDs to see what's available
            Debug.Log("[FamilyCharm] === Looking for MadFamily units in inventory ===");
            foreach (var card in all)
            {
                if (card != null)
                {
                    Debug.Log($"[FamilyCharm] Card ID: {card.name}");
                }
            }
            
            // Detect Mad Family members by pattern and known units list
            int unitCount = 0;
            try {
                // Look for companion/leader keywords to ensure we only count units (not items/spells)
                string[] unitTypeKeywords = new[] {
                    "companion",
                    "leader",
                    "Companion",
                    "Leader"
                };

                // Look for MadFamily-specific patterns
                string[] familyKeywords = new[] {
                    "madfamily",
                    "mad-family",
                    "mad_family",
                    "family"
                };
                
                foreach (var card in all)
                {
                    if (card != null)
                    {
                        string cardName = card.name.ToString();
                        string cardNameLower = cardName.ToLower();
                        bool isCompanionOrLeader = false;
                        bool isMadFamily = false;
                        
                        // First check if it's a companion or leader
                        foreach (string keyword in unitTypeKeywords)
                        {
                            if (cardNameLower.Contains(keyword.ToLower()))
                            {
                                isCompanionOrLeader = true;
                                break;
                            }
                        }
                        
                        // If it's a unit, check if it's a MadFamily unit
                        if (isCompanionOrLeader)
                        {
                            // Check against known MadFamily unit list
                            foreach (string knownUnit in knownMadFamilyUnits)
                            {
                                if (cardName.Contains(knownUnit))
                                {
                                    isMadFamily = true;
                                    Debug.Log($"[FamilyCharm] Found known MadFamily unit: {cardName}");
                                    break;
                                }
                            }
                            
                            // If not found in known list, check for MadFamily keywords
                            if (!isMadFamily)
                            {
                                foreach (string keyword in familyKeywords)
                                {
                                    if (cardNameLower.Contains(keyword))
                                    {
                                        isMadFamily = true;
                                        Debug.Log($"[FamilyCharm] Found MadFamily unit by keyword: {cardName} (matched '{keyword}')");
                                        break;
                                    }
                                }
                            }
                            
                            // Count this unit if it's confirmed MadFamily
                            if (isMadFamily)
                            {
                                unitCount++;
                            }
                        }
                    }
                }
                
                Debug.Log($"[FamilyCharm] Total MadFamily companions/leaders found: {unitCount}");
            }
            catch (System.Exception ex) {
                Debug.Log($"[FamilyCharm] Error checking for MadFamily units: {ex.Message}");
            }
            
            // If no units found, count all cards for debugging
            if (unitCount == 0)
            {
                try {
                    int totalCards = all.Count(card => card != null);
                    Debug.Log($"[FamilyCharm] No MadFamily units found. Total cards in inventory: {totalCards}");
                }
                catch (System.Exception ex) {
                    Debug.Log($"[FamilyCharm] Error counting total cards: {ex.Message}");
                }
            }
            
            // Apply a bonus of 1 per unit (changed from 2)
            int bonus = unitCount;
            
            Debug.Log($"[FamilyCharm] ScriptableAmountDynamicFamilyBonus.Get called for entity: {entity?.ToString() ?? "null"}, units: {unitCount}, bonus calculated: {bonus}");
            Debug.Log($"[FamilyCharm] Deck count: {deck.Count}, Reserve count: {reserve.Count}, MadFamily units found: {unitCount}, final bonus: {bonus}");
            
            return bonus;
        }
    }
}
