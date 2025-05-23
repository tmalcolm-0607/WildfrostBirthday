# Refactoring Summary

## What Was Done

### Removed Helper Methods
1. Removed obsolete helper methods from WildFamilyMod.cs:
   - GiveUpgrade
   - AddRandomHealth
   - AddRandomDamage
   - AddRandomCounter
   - AddItemCard
   - AddCharm
   - AddFamilyUnit
   - AddStatusEffect
   - AddCopiedStatusEffect

### Added New Files
1. Created CardScriptHelpers.cs in Helpers namespace to replace card script helper methods
2. Created StackHelpers.cs in Helpers namespace for stack-related methods (SStack, TStack)
3. Created DataCopyHelpers.cs in Helpers namespace for data copying methods (StatusCopy, CardCopy, TribeCopy)
4. Created DataUtilities.cs in Helpers namespace for data utility methods (DataList, RemoveNulls)
5. Created RewardPoolHelpers.cs in Helpers namespace for reward pool operations
6. Created RefactoringHelpers.md with documentation of the helper classes

### Refactored Files
1. Refactored all Card_*.cs files to use the direct builder pattern
2. Refactored all Item_*.cs files to use the direct builder pattern:
   - Item_BerryCake.cs
   - Item_BlazeBerry.cs
   - Item_CheeseCrackers.cs
   - Item_DiceOfDestiny.cs
   - Item_DynamoRoller.cs
   - Item_FoamBullets.cs
   - Item_FoamRocket.cs
   - Item_HazeTacks.cs
   - Item_JunkPile.cs
   - Item_PlywoodSheet.cs
   - Item_RefreshingWater.cs
   - Item_SoulroseMask.cs
   - Item_TackSpray.cs
   - Item_WispMask.cs

3. Refactored all Charm_*.cs files to use the direct builder pattern:
   - Charm_DuckCharm.cs
   - Charm_FrostMoonCharm.cs
   - Charm_GoldenVialCharm.cs
   - Charm_PizzaCharm.cs
   - Charm_PlantCharm.cs
   - Charm_PugCharm.cs
   - Charm_SodaCharm.cs

4. Fixed namespace issues in Effects files:
   - StatusEffect_OnTurnApplyTeethToAllies.cs
   - StatusEffect_SummonBeepop.cs
   - StatusEffect_SummonFallow.cs
   - StatusEffect_SummonBeepopWisp.cs
   - StatusEffect_OnTurnAddAttackToAllies.cs

## Benefits of Refactoring
1. More consistent code that follows modern patterns
2. Direct use of builder pattern makes code easier to understand
3. Centralized helper methods into proper utility classes
4. Eliminated redundant helper methods
5. Better organized project structure

## Helper Methods Refactoring
We moved the following helper methods from WildFamilyMod.cs to specialized utility classes:

1. Stack helpers:
   - SStack: Moved to StackHelpers.cs as an extension method
   - TStack: Moved to StackHelpers.cs as an extension method

2. Copy helpers:
   - StatusCopy: Moved to DataCopyHelpers.cs as an extension method
   - CardCopy: Moved to DataCopyHelpers.cs as an extension method
   - TribeCopy: Moved to DataCopyHelpers.cs as an extension method
   - DataCopy: Moved to DataCopyHelpers.cs as an extension method

3. Data helpers:
   - DataList: Moved to DataUtilities.cs as an extension method
   - RemoveNulls: Moved to DataUtilities.cs as an extension method

4. Other utilities:
   - CreateRewardPool: Moved to RewardPoolHelpers.cs as an extension method
   - UnloadFromClasses: Moved to RewardPoolHelpers.cs as an extension method
   - FixImage: Moved to RewardPoolHelpers.cs as an extension method

## Next Steps
1. Continue to test the mod in-game to verify functionality works as expected
2. Consider adding XML documentation to utility classes (started, but can be expanded)
3. Consider further refactoring to improve code consistency
4. Update any card, charm, and item files that might not be using the proper builder pattern
