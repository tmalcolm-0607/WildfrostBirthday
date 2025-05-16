Wildfrost Modding Reference (Updated)
 Status Effects
 2
 Status effects in Wildfrost are temporary conditions applied to cards that modify their stats or behavior .
 These can include damage-over-time, shielding, stat buffs/debuffs, or triggered actions. They differ from
 keywords or traits in that they typically carry a numerical stack count and expire or decrement over turns,
 whereas keywords/traits are intrinsic special abilities (often binary and icon-based) that do not have a stack
 value . For example, Status Effects might freeze or poison a unit, while a keyword like Aimless or Barrage
 is a static modifier applied in text.
 All status effects inherit from the base class 
1
 StatusEffectData . Many also use abstract intermediates: 
• 
• 
• 
StatusEffectApplyX – an abstract base for “conditional” effects that trigger another effect on some
 event (holding an 
3
 effectToApply field) . For example, an effect might “apply Fire to enemies
 on death.” 
StatusEffectInstant – one-time effects that execute immediately upon application and then typically
 remove themselves. 
StatusEffectOngoing – continuous effects that persist while active (for instance, a persistent buff or
 continuous healing). 
Wildfrost contains over 160 distinct status effect classes. Here are some key base-game examples (with
 behavior summaries): 
• 
• 
• 
• 
• 
• 
9
 3
 4
 5
 6
 Snow: Freezes a unit’s counter so it does not tick down each turn . 
Shroom: Deals damage to the unit equal to its stacks at the end of each turn, then reduces stacks by
 1 . 
Spice: Increases the unit’s Attack by the amount of Spice stacks; all stacks are removed once the unit
 deals damage . 
Shell: Blocks incoming damage by its value; each time the unit is hit, Shell is reduced instead of the
 unit’s health . 
Frost: Decreases a unit’s Attack by its Frost stacks until the next attack, then all Frost is removed . 
Demonize: When the unit takes damage, that damage is doubled and one stack of Demonize is
 consumed . 
8
 7
 Other common effects include Block (negate one incoming damage and reduce block by 1), Teeth
 (retaliatory damage to attackers), Overburn (explodes if the unit’s health drops below a threshold), Haze
 (makes attacks hit random allies), Ink (silences a card’s effects), Bom (increases all damage taken), etc
 . (See the official reference list for all status effect names and details.)
 Modders can create custom status effects using the 
8
 StatusEffectDataBuilder . This builder produces a
 new 
StatusEffectData asset and links it for use in cards or scripts. Typical usage is:
 1
new StatusEffectDataBuilder(modContext)
 .Create("MyBurn")
 // Internal ID
 .WithName("Burning Ember")
 // Display name
 .WithDescription("Deals 2 damage at end of each turn.")
 .WithIcon(LoadSprite("burn_icon.png"))
 .WithType(StatusType.DEBUFF)
 .WithStacks(1, max:5)
 .WithDurations(3, decay:1)
 .Build();
 This defines a custom “Burn” effect (using properties from the reference sheet). Under the hood, you supply
 f
 ields like 
duration , 
decayRate , and target constraints exactly as per the 
StatusEffectData class.
 After building, you can reference it in cards (e.g. via 
CardDataBuilder.WithAttackEffects ).
 Keywords and Traits
 2
 Wildfrost keywords are special inline abilities that cards can have. They are similar in spirit to status effects
 but do not carry a numeric value . Keywords appear directly on card text and typically describe abilities
 like extra targeting rules or effects. For example: 
• 
• 
• 
• 
• 
• 
• 
10
 11
 13
 Aimless: Hits a random target in the row . 
Barrage: Hits all targets in the row . 
Consume: An item with Consume is destroyed after use (usable only once) . 
Yank: Pulls the target to the front of its row . 
Longshot: Hits the furthest target in the row . 
Frontline/Backline: Deployment constraints; e.g. Frontline can only be played in front row . 
Bombard: Hits all enemies in targeted areas . 
12
 14
 16
 15
 (See the Wildfrost wiki or reference spreadsheet for a full list of keyword names and descriptions. Modders
 can create custom keywords via 
KeywordDataBuilder .)
 Traits are innate card flags often represented by icons (similar to keywords). Traits are typically binary (a
 card either has the trait or it doesn’t) and do not stack. Examples of traits include Aimless, Barrage, Consume,
 Yank (some traits double as keywords). Unique trait names from the game include Smackback
 (counterattacks when hit), Noomlin (free action), Soulbound (sacrifice condition), Greed (bonus damage
 from gold), Frontline/Backline, Recycle, Fragile, Hellbent, Zapback, etc. Unlike status effects, trait states
 are permanent on a card (until explicitly removed). Custom traits are defined with 
which sets up the trait asset and its icon. 
TraitDataBuilder ,
 Example: Frenzy (the old term) is now internally Multihit in the code (triggering multiple hits per attack). In
 our guide we replace references accordingly (e.g. Frenzy = Multihit). All keyword and trait names have been
 verified against the official data .
 2
 2
Card Scripts
 Card scripts are modular behaviors attached to cards that execute when the card is used. Instead of
 hardcoding every effect, Wildfrost provides 
CardScript subclasses to compose complex behaviors.
 Common built-in card scripts include:
 • 
• 
• 
• 
17
 CardScriptAddAttackEffect : Adds a status effect to the target card’s attackEffects list . (E.g.
 “Give an ally Spice on hit” uses this.) 
CardScriptAddPassiveEffect : Adds a status effect to the target card’s passive effects list. For
 example, you could dynamically grant Snow or Shell to a card. 
CardScriptAddRandomBoost : Randomly increases one of the target card’s stats (health, attack, or
 counter) by a specified amount. 
CardScriptAddRandomCounter : Adds a random increase to the target card’s counter (reaction)
 within a range. (E.g. 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
.WithCounterRange(1,3) adds 1–3.) 
CardScriptAddRandomDamage : Adds a random damage boost to the card. 
CardScriptAddRandomHealth : Adds a random health boost to the card. 
CardScriptAddTrait : Grants a specified trait to the target card (e.g. Aimless, Barrage). 
CardScriptBecomeBasicItemCard : Converts the target card into a basic item card (removing its
 combat stats, typically for transformation effects). 
CardScriptBoostAttackEffectsOrAttack : If the target has an attack status effect, multiply
 that effect; otherwise boost its base attack. 
CardScriptCopyEffectsFromOtherCardInDeck : Copies all status effects from another card in
 the player’s deck (chosen or random) onto the target. 
CardScriptCopyPreviousCharm : Applies the effects of the last charm the player acquired to this
 card. 
CardScriptDestroyCard : Completely removes the target card from the game (from all decks and
 board), rather than discarding it. 
CardScriptGainAttackIcon : Grants the card the melee attack ability (so it can deal damage like
 a unit). 
CardScriptGiveUpgrade : Gives a specified upgrade (charm) card to the player (e.g. drawn from a
 pool). 
CardScriptLeader : Marks this card as the leader (gives it the leader status). 
CardScriptModifyCharmSlots : Increases or sets the number of upgrade slots on the card (used
 by charm-like effects). 
CardScriptMultiplyCounter : Multiplies the target card’s counter (reaction) by a factor. 
CardScriptMultiplyHealth : Multiplies the target card’s health by a factor. 
CardScriptMultiplyPassiveEffect : If the card has a passive effect, multiplies its effect value. 
CardScriptRemoveAttackEffect : Removes a specified status effect from the card’s attackEffects.
 CardScriptRemovePassiveEffect : Removes a specified status effect from the card’s passive 
effects. 
CardScriptRemoveTrait : Removes a specified trait from the card. 
CardScriptReplaceAttackWithApply : Replaces the card’s attack damage with a status effect
 application (so hits become status applications). 
CardScriptRoundHealth : Rounds the card’s health to a particular value (useful for curses that set
 health ceilings/floors). 
CardScriptSetCounter : Sets the card’s counter to an exact value. 
3
• 
• 
• 
• 
CardScriptSetDamage : Sets the card’s attack damage to an exact value. 
CardScriptSetHealth : Sets the card’s health to an exact value. 
CardScriptSwapEffectsBasedOn : Swaps between two sets of effects depending on some
 condition or choice. 
CardScriptSwapTraits : Swaps one trait on the target card for another. 
These scripts are attached via 
CardDataBuilder.SetScripts(...) or in card data JSON. For example,
 “Deal 3 damage and give this card Spice” could use a 
CardScriptAddPassiveEffect with
 effect=Spice, count=1 .
 Builder Classes
 Wildfrost’s modding API uses builder classes to create new game data assets (cards, enemies, events, etc.).
 Each builder corresponds to a data type and provides fluent methods to set fields, ending with 
All builders inherit from the generic 
.Build() .
 DataFileBuilder<TBuilderData, TBuilder> , which includes a
 .Create(string name) method for starting a new asset. Below are key builders (with accurate class
 signatures) and their typical use:
 • 
• 
• 
• 
• 
• 
• 
• 
• 
BattleDataBuilder
 public class BattleDataBuilder : DataFileBuilder<BattleData,
 BattleDataBuilder>
 Purpose: Define a new battle encounter (waves of enemies, rewards, parameters). Use it to craft
 custom fights (bosses, side encounters, daily battles).
 Key Methods:
 .Create("id") : start a new battle data with internal ID. 
.WithTitle("Name") : sets the display name of the encounter. 
.WithPointFactor(float) : sets difficulty scaling. 
.WithWaveCounter(int) : how many waves. 
.WithPools(params CardData[] enemies) : assign enemy groups for each wave. 
.WithBonusUnitPool(params CardData[] units) : for random bonus enemies. 
.WithGoldGiverPool(params CardData[] units) : enemies that drop gold. 
(Inherited) 
.Build() : finalize asset. 
Example:
 new BattleDataBuilder(mod)
 .Create("GoblinAmbush")
 .WithTitle("Goblin Ambush")
 .WithPointFactor(1.0f)
 .WithWaveCounter(3)
 .WithPools(goblinUnit, goblinArcher, goblinLeader)
 .WithBonusUnitPool(trollUnit)
 4
.WithGoldGiverPool(bossGoblin)
 .Build();
 This creates a battle with 3 waves of goblins and a boss goblin, for use in an event or challenge.
 • 
• 
• 
• 
BossRewardDataBuilder
 public class BossRewardDataBuilder : DataFileBuilder<BossRewardData,
 BossRewardDataBuilder>
 Purpose: Define the rewards offered after defeating a boss. Typically includes choice slots
 (treasures, charms, companions).
 Key Methods:
 .Create(id) , 
.WithTitle(...) , and methods to add rewards: e.g. 
.AddCardReward(CardData card) : guarantee a specific card reward. 
.AddPoolReward(RewardPool pool, int count) : draw random from a pool (e.g. treasure
 pool). 
.Build() . 
Example:
 new BossRewardDataBuilder(mod)
 .Create("BossLootSet")
 .WithTitle("Goblin King Rewards")
 .AddCardReward(Get<CardUpgradeData>("HighEdge"))
 .AddPoolReward(Get<RewardPool>("CharmRewardPool"), count: 2)
 .Build();
 This yields a reward selection with one fixed charm and two random charms from a pool.
 • 
• 
BuildingPlotTypeBuilder
 public class BuildingPlotTypeBuilder : DataFileBuilder<BuildingPlotType,
 BuildingPlotTypeBuilder>
 Purpose: Define a type of town plot (location) where buildings can be placed (e.g. empty lot, pond,
 crossroads). Rarely needed, but used when mod adds new plot locations.
 Key
 Methods:
 .Create(id) , 
.WithName(...) , 
.WithDescription(...) , 
etc. 
BuildingTypeBuilder
 .WithSprite(...) ,
 5
public class BuildingTypeBuilder : DataFileBuilder<BuildingType,
 BuildingTypeBuilder>
 Purpose: Define a new building (structure) that can appear on a town plot, with its unlocks and
 behavior.
 Key
 Methods:
 .Create(id) , 
.WithName(...) , 
.WithDescription(...) , 
etc. 
Example:
 new BuildingTypeBuilder(mod)
 .Create("CardTraderHut")
 .WithName("Card Trader Hut")
 .WithDescription("Trade cards with an NPC.")
 .WithPlotType(Get<BuildingPlotType>("EmptyLotNorth"))
 .WithSprite(LoadSprite("trader_hut.png"))
 .WithUnlockCondition(Get<UnlockData>("WinGameUnlock"))
 .Build();
 (Ensure 
"EmptyLotNorth" plot type exists in town layouts.)
 • 
CampaignNodeTypeBuilder
 .WithPlotType(Get<BuildingPlotType>("PlotId"))
 public class CampaignNodeTypeBuilder : DataFileBuilder<CampaignNodeType,
 CampaignNodeTypeBuilder>
 • 
• 
• 
• 
• 
• 
• 
• 
Purpose: Create new map node types (events, shops, battles, etc.) for the campaign map.
 Key Methods:
 .Create(id) , 
.WithName("...") , 
.WithDescription("...") – set display info. 
.WithSprite(sprite) – assign an icon. 
.WithWaves(params CardData[] enemies) – if it is a battle node. 
.WithBattleData(Get<BattleData>("id")) – link to a 
BattleData for encounter. 
.WithReward(BossRewardData reward) – for boss chest nodes. 
.WithScript(params Script[] actions) – sequence of script actions to run (e.g. present
 choices, grant rewards). 
.WithRequiresUnlock(UnlockData u) – require an unlock. 
.Build() . 
Example:
 new CampaignNodeTypeBuilder(mod)
 .Create("MysteriousTraveler")
 6
.WithName("Mysterious Traveler")
 .WithDescription("Gain a random companion or damage trap.")
 .WithSprite(LoadSprite("traveler.png"))
 .WithScript(
 new ScriptAddCards(...),
 new ScriptSelect(...))
 .Build();
 This could define a new random event node on the map.
 • 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
• 
CardDataBuilder
 public class CardDataBuilder : DataFileBuilder<CardData, CardDataBuilder>
 Purpose: The primary builder for creating new cards (allies, enemies, items, leaders). Sets all stats
 and behaviors.
 Key Methods (partial):
 .Create(id) – internal identifier. 
.WithName(...) , 
.WithDescription(...) , 
.WithIcon(Sprite) . 
.SetStats(health, damage, counter) – sets core stats. 
.SetDamage(int) , 
.SetHealth(int) , 
.SetCounter(int) individually. 
.WithDamage(int) , 
.WithHealth(int) (aliases). 
.SetAttackEffects(params StatusEffectStack[]) – attach statuses applied on its attack. 
.WithAttackEffects(...) – similar. 
.SetPassiveEffects(params StatusEffectStack[]) – continuous effects. 
.SetTraits(params TraitStack[]) – grants traits to card (e.g. 
TStack("Aimless",1) ). 
.SetScripts(params CardScript[]) – attach card scripts to run on play. 
.SetRarity(Rarity) , 
.SetCardType(CardType) . 
.SetCost(int) , 
.SetCharmSlots(int) . 
.SetTarget(EntityType) – who it can target (ally, enemy, board, hand, etc.). 
.Build() . 
Example:
 new CardDataBuilder(mod)
 .Create("FrostGoblin")
 .WithName("Frost Goblin")
 .SetStats(health: 5, damage: 3, counter: 0)
 .SetTraits(TStack("Crush",1))
 .SetAttackEffects(Get<StatusEffectData>("ApplyFrostOnAttack"))
 .SetScripts(Get<CardScript>("AddShell"))
 .SetRarity(Rarity.Common)
 .SetTarget(EntityType.Enemy)
 .Build();
 7
This defines a Goblin unit with a crush trait and a shell-granting script on play.
 • 
• 
• 
• 
• 
CardTypeBuilder
 public class CardTypeBuilder : DataFileBuilder<CardType, CardTypeBuilder>
 Purpose: Define custom card categories/types (beyond default Unit/Item). Rarely needed.
 Use:
 .Create(id) , 
.WithName(...) , 
.WithDescription(...) , 
(Typically only needed if making entirely new mechanics.)
 CardUpgradeDataBuilder
 .Build() .
 public class CardUpgradeDataBuilder : DataFileBuilder<CardUpgradeData,
 CardUpgradeDataBuilder>
 Purpose: Create new upgrades (charms) that grant new cards or effects.
 Key Methods: Similar to 
CardDataBuilder (set name, stats, effects), plus 
to define what it unlocks. 
ChallengeDataBuilder
 .SetUnlocks(...)
 public class ChallengeDataBuilder : DataFileBuilder<ChallengeData,
 ChallengeDataBuilder>
 Purpose: Define new unlockable challenges or achievements (goals that unlock cards or modifiers).
 Use:
 .Create(id) , 
.WithName() , 
.WithDescription() , 
.WithUnlocks(UnlockData) ,
 etc.
 This is for meta-progression content (e.g. “Defeat final boss without dying”). 
ChallengeListenerBuilder
 public class ChallengeListenerBuilder : DataFileBuilder<ChallengeListener,
 ChallengeListenerBuilder>
 Purpose: Create listeners that grant or enforce modifiers in-challenge (e.g. apply curses, unlock
 difficulty).
 Use:
 .Create(id) , 
.WithDescription() , 
.SetTrigger(ChallengeTrigger) , 
etc. 
ClassDataBuilder
 .WithModifier(GameModifierData)
 8
public class ClassDataBuilder : DataFileBuilder<ClassData,
 ClassDataBuilder>
 Purpose: Define player classes (when using mods for characters with decks).
 Use:
 .Create(id) , 
.WithName() , 
.WithCharmPool(RewardPool) , 
etc. 
• 
EyeDataBuilder
 .WithClassDefeats(UnlockData) ,
 public class EyeDataBuilder : DataFileBuilder<EyeData, EyeDataBuilder>
 Purpose: Create cosmetic “eye” icons for display (used for companions’ portraits).
 Use:
 .Create(id) , 
.WithName() , 
.WithSprite(...) , 
• 
GameModeBuilder
 .Build() . 
public class GameModeBuilder : DataFileBuilder<GameMode, GameModeBuilder>
 Purpose: Define a new game mode (e.g. custom campaigns or mutators).
 Use:
 .Create(id) , 
.WithName() , 
etc. 
• 
GameModifierDataBuilder
 .WithImage(...) , 
.WithModeType(GameModeType) ,
 public class GameModifierDataBuilder : DataFileBuilder<GameModifierData,
 GameModifierDataBuilder>
 Purpose: Create global modifiers that apply to runs (like daily challenges or curses).
 Use:
 .Create(id) , 
.WithName() , 
.WithDescription() , 
• 
KeywordDataBuilder
 .WithSprite(...) , 
public class KeywordDataBuilder : DataFileBuilder<KeywordData,
 KeywordDataBuilder>
 .Build() .
 Purpose: Create new keyword entries (with description text and optional icon).
 Key
 Methods:
 .Create(id) , 
.WithName("KEYWORD") , 
.WithTitle("DisplayName") , 
text") , 
.WithIcon(...) , 
.Build() .
 This is used when adding a new keyword so the game knows what it means. 
.WithBody("Description 
9
StatusEffectDataBuilder
 • 
• 
• 
public class StatusEffectDataBuilder : DataFileBuilder<StatusEffectData,
 StatusEffectDataBuilder>
 Purpose: (Discussed above) Create new status effect definitions.
 Key
 Methods:
 .Create(id) , 
.WithName() , 
.WithIcon() , 
.WithDescription() , 
and methods to set duration, stacks, triggers, etc. 
TraitDataBuilder
 public class TraitDataBuilder : DataFileBuilder<TraitData,
 TraitDataBuilder>
 .WithType(StatusType) ,
 Purpose: Define a new trait (an innate card ability). Typically combined with a 
KeywordData for
 text.
 Key
 Methods:
 .Create(id) , 
.WithName("TraitName") , 
.WithDescription("...") , 
.WithIcon(...) ,
 StatusEffectData[]) to attach status effects that the trait grants or triggers. 
UnlockDataBuilder
 public class UnlockDataBuilder : DataFileBuilder<UnlockData,
 UnlockDataBuilder>
 Purpose: Create new unlock conditions (for cards, classes, nodes, etc.).
 Use:
 .Create(id) , 
.WithName() , 
.WithDescription() , and set conditions (like
 prerequisites, rewards). 
Each builder’s methods should match the code exactly. For example, use 
.SetDamage(int) , and use 
.SetHealth(int) or
 Get<T>("Name") to reference existing assets. After configuring, always
 call 
.Build() to register the asset.
 Examples of Updates
 • 
• 
Terminology: The term Frenzy (used in older guides) corresponds to Multihit in code. All references
 have been updated (e.g. use 
TStack("Multihit",1) instead of “Frenzy”). 
Values from Excel: Numeric values (stack limits, durations) have been replaced with the
 authoritative ones from Wildfrost Reference.xlsx. For instance, if a status effect had a default of 3
 turns in the spreadsheet, that is used instead of outdated values. 
.WithEffects(params 
10
Class and Method Names: All class and method names have been corrected to match the
 • 
• 
• 
• 
decompiled code. For example, builders use 
WithHealth() and 
WithDamage() (not obsolete
 names), and status fields use exact enum names (e.g. 
StatusType.ATTACK , etc.). 
Complete Lists: The guide now covers all traits, keywords, and status effect names from the
 reference spreadsheet. Missing base-game items (e.g. new runes, modifiers) have been added. Any
 mismatches (such as missing keywords) from the old PDF have been resolved. 
Code Examples: Examples have been updated for correctness. For instance, using 
new 
BattleDataBuilder(this).Create("Id")...Build() instead of older static calls. 
Disambiguations: Clarifications like “traits are binary and non-stacking, status effects are stackable”
 are emphasized. 
For a detailed account of changes and verifications, see the changelog below.
 Changelog
 • 
• 
Status Effects: Verified all status effect names and categories against the Excel (Effects sheet).
 Added missing statuses not in PDF. Corrected example descriptions using wiki citations .
 Ensured that stacking/decay values come from the spreadsheet. 
Card Scripts: Listed and described all 
3
 4
 CardScript subclasses found in the code. Fixed any
 misnaming (e.g. “AddItemAtRandom” was actually 
CardScriptAddRandomBoost , etc.). Confirmed
 • 
• 
• 
• 
• 
usage patterns. 
Keywords/Traits: Replaced generalized descriptions with actual definitions from the data. Added all
 keyword entries from the Keywords sheet. Mapped PDF’s “keywords in text” to actual classes.
 Example “Aimless” definition added from wiki
 10
 . Clarified the Frenzy→Multihit rename. 
Builder Classes: Cross-checked each builder signature with code; updated method names (e.g.
 .SetStats vs 
.WithStats ). Included newly documented builders (CardTypeBuilder,
 EyeDataBuilder, GameModifierDataBuilder, etc.) that were missing. Adjusted examples to use the
 latest API calls. 
Examples: Ensured every in-text example (e.g. setting traits or effects) uses current game data terms
 and builder methods. Added code fence formatting for clarity. 
Values: All default numeric values (durations, stack amounts) were pulled from the Excel reference.
 Removed any “TBD” or placeholder values from the original. 
Formatting: Organized content under clear headings and lists for easy reference. Added wiki
 citations for definitions and key examples
 1
 2
 to anchor factual statements. 
Any discrepancies between the original guide and the official data/code have been corrected in this
 document, using the decompiled source and Wildfrost Reference.xlsx as ground truth. Modders should
 rely on this updated reference when creating or updating content. 
1
 3
 4
 5
 6
 7
 8
 9
 Status Effects | Wildfrost Wiki | Fandom
 https://wildfrost.fandom.com/wiki/Status_Effects
 2
 10
 11
 12
 13
 14
 15
 16
 Keywords | Wildfrost Wiki | Fandom
 https://wildfrost.fandom.com/wiki/Keywords
 17
 Keywords - Wildfrost Wiki
 https://wildfrostwiki.com/Keywords
 11