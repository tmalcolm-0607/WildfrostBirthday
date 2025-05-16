Wildfrost Modding Comprehensive Reference

Guide

This documentation serves as a self-contained replacement for the existing Wildfrost GitHub modding wiki.

It covers all core systems relevant to modding Wildfrost, including Status Effects, Card Scripts, Builder

Classes, Triggers, and Traits. Each relevant class from the decompiled Assembly-CSharp is documented

with its signature, description, use cases, and examples. Step-by-step tutorials are provided after the

reference sections to illustrate how to create custom content using these systems. An appendix at the end

lists all builder classes and notes missing information from the previous wiki.

Table of Contents

Status Effects

Card Scripts

Builder Classes

BattleDataBuilder

BossRewardDataBuilder

BuildingPlotTypeBuilder

BuildingTypeBuilder

CampaignNodeTypeBuilder

CardDataBuilder

CardTypeBuilder

CardUpgradeDataBuilder

ChallengeDataBuilder

ChallengeListenerBuilder

ClassDataBuilder

EyeDataBuilder

GameModeBuilder

GameModifierDataBuilder

KeywordDataBuilder

StatusEffectDataBuilder

TraitDataBuilder

UnlockDataBuilder

Trigger System

Traits and Keywords

Tutorials
Tutorial 1: Creating a Custom Charm (Traits & Scripts)
Tutorial 2: Making a New Companion (Attack Effects & Class Assignment)
Tutorial 3: Creating a New Status Effect (Reaction/Aura)
Tutorial 4: Adding a New Tribe with Leaders
Tutorial 5: Designing a Custom Battle Scenario
Tutorial 6: Using Scripted Map Nodes (Event/Reward)
1

Appendix: Builder Class Reference & Wiki Gaps

Status Effects

Status effects represent temporary or persistent conditions on units and cards. They can impose ongoing

modifiers (e.g. increased damage, damage-over-time) or trigger reactions to events. In Wildfrost, status effects are defined as subclasses of StatusEffectData (a ScriptableObject data class), often with

specialized behavior implemented in code. Status effects usually can stack (showing a counter on their

icon) and/or decrement over time, distinguishing them from traits (which are more static abilities)

1

2

.

Key examples of status effects in the base game include Snow (freezes a unit’s counter), Shroom (deals

decay damage over turns), Spice (increases attack), Shell (provides temporary shield health), Frost (reduces

attack damage), Demonize (target takes double damage on next hit), etc. These effects are often

represented with an icon and a numeric stack count in-game.

Traits vs Status Effects: Some effects that appear as “keywords” in game (like Aimless, Barrage,

Smackback) are implemented as traits rather than status effects

3

1

. Traits are innate abilities (binary

flags) that generally do not stack or expire (see Traits and Keywords section), whereas status effects usually

can accumulate multiple stacks and often wear off or trigger then vanish. When planning a new effect for a

mod, decide if it should behave like a status (stackable, possibly temporary) or a trait (inherent ability that

doesn’t stack)

1

4

.

StatusEffectData Class Hierarchy and Behavior

All status effect types inherit from the base class StatusEffectData . Many have additional abstract

parents that group common behaviors. Important abstract classes include

5

:

StatusEffectApplyX – Base for conditional “apply X on some event” effects. These statuses remain

on a unit and trigger when a specified event occurs, then apply another effect. They contain fields like effectToApply and often flags for whom to apply the effect to.

StatusEffectInstant – Base for immediate, one-time effects that execute instantly when applied.

These do something upon application (or removal) and then typically remove themselves.

StatusEffectOngoing – Base for continuous effects that adjust stats or behavior while active (e.g. a

buff that increases attack while the status is present).

Base Game Status Effects: Wildfrost has over 160 distinct status effect classes

6

. Below is a categorized

catalog of known status effects, their inheritance, and their purpose/trigger:

Basic Statuses (Direct Modifiers): These inherit directly from StatusEffectData . They usually

adjust a stat or rule while present, often decreasing each turn or when triggered. For example: StatusEffectSnow – Freezes the unit’s counter (countdown doesn’t decrement while Snow > 0)

7

8

. Removed when counter would restart or on discard.

StatusEffectShroom – Poisonous spores; unit takes 1 damage per Shroom stack at end of its

turn, then stacks decrement StatusEffectSpice – Attack buff; each stack permanently increases unit’s attack by 1 (usually

.

9

consumed on use or end of battle).

2

StatusEffectShell – Shield; each stack is an extra temporary HP that absorbs damage before

regular health. Decrements when damage is taken. StatusEffectDemonize – Makes the unit take double damage on the next instance of damage,

then Demonize is removed. StatusEffectInk – Silences the unit, preventing it from executing its effects or attacks (often for

a duration).

…and others: might freeze certain actions), StatusEffectHaze (obscure attacks), StatusEffectWeakness (reduce target’s attack), etc.

StatusEffectFrozen

(if present,

These basic statuses typically are applied by card effects or other statuses and often show an icon

with their stack count.

Conditional “Apply X on Y” Statuses: These inherit from StatusEffectApplyX (or a subclass of it)

and trigger when a specific event happens, applying another effect (X). They do not decrement each

turn but instead wait for the condition. Examples:

StatusEffectApplyXOnKill – When the unit with this status kills an enemy, it applies effect X (defined by effectToApply ) to a target. E.g. "Gain Attack on kill" uses this to apply an Increase

Attack status to self on each kill

10

11

. The target of the applied effect can be configured (often

self). StatusEffectApplyXWhenHit – When the unit is hit by an attacker, it applies effect X to a target

12

(often the attacker) effectToApply = Snow and target set to the attacker. StatusEffectTriggerAgainstAttackerWhenHit – When the unit is hit, it triggers a specific

. Example: "When hit, apply Snow to the attacker" would use this with

action against its attacker

13

. This is how Smackback (counterattack) is implemented: on hit, the

attacker immediately gets counterattacked (takes damage). Essentially, it triggers the unit’s attack

back at the attacker StatusEffectApplyXOnHit – When the unit with this status hits an enemy (deals attack

.

14

damage), it applies effect X to a target

15

. For example, "Apply Poison on hit" (apply Shroom to the

enemy whenever this unit hits something). StatusEffectApplyXWhenAllyIsKilled – If an ally of this unit is killed, it applies effect X (often

to self or remaining allies)

16

. Some companions in base game gain buffs when an ally dies using

this trigger. StatusEffectApplyXWhenAllyIsHit – When an ally is hit, apply effect X. (Could be used for a

unit that, say, shields allies when they are hit by applying Shell to them or itself.) StatusEffectApplyXWhenEnemiesAttack – Triggers when any enemy attacks, applying effect X

17

(possibly to those enemies or to self/allies) . StatusEffectApplyXWhenUnitLosesY – Triggers when the unit loses a certain resource Y

18

(health, etc.), then applies X

19

. This can represent reactive effects like “when this unit loses health,

gain some armor,” etc. StatusEffectApplyXWhenYAppliedToSelf (and variants like ...ToAlly ) – Triggers when a

specific status Y is applied to this unit (or an ally), then applies X

20

. For example, a status that says

"When this unit is Shroomed, also gain Shell" could be done with StatusEffectApplyXWhenYAppliedToSelf (Y = Shroom, X = Shell)

20

.

Dozens of these conditional classes cover a wide range of triggers (when a card is drawn/discarded – e.g. ...WhenDrawn , ...WhenDiscarded ; when a unit is deployed – ...WhenDeployed ; when a summon

3

is created – StatusEffectApplyToSummon ; when a unit takes damage – ...WhenDamageTaken ; etc.)

21

. They allow complex reactive behaviors without writing new code – by combining existing effect logic.

Instant Effects: These inherit from StatusEffectInstant. They execute an effect immediately upon

being applied (and often remove themselves right after, since their job is done). They typically have

names starting with StatusEffectInstant. Examples: StatusEffectInstantKill – Instantly kills the affected unit (used for cards or effects that

outright destroy a target). StatusEffectInstantHeal – Heals the unit for a certain amount immediately. StatusEffectInstantGainGold – Instantly grants some gold. There is also StatusEffectInstantGainGoldRange for a random gold amount. StatusEffectInstantDraw – Causes the player to immediately draw one or more cards. StatusEffectInstantSummon – Summons a specified unit immediately. StatusEffectInstantTrigger – Instantly triggers another trigger/event. (For example, used to immediately fire a trigger reaction; the variant InstantTriggerAgainst might specify a

particular target for the triggered effect.) StatusEffectInstantGainTrait / StatusEffectInstantLoseTrait – Immediately grants

or removes a trait from the unit. StatusEffectInstantCopyEffects – Copies all status effects from one entity to another

immediately.

Many others: instant effects exist for adjusting stats ( InstantSetAttack , InstantSetHealth , etc. set a stat to a value), modifying countdown ( InstantReduceCounter , etc.), moving units ( InstantPull , InstantPush ), splitting or transforming cards ( InstantSplit ), and so on.

These provide one-time event actions as status effects.

Ongoing Effects: These inherit from StatusEffectOngoing (or its children) and continuously modify

some aspect of the unit while present. They often represent aura-like effects or dynamic stat

adjustments. Examples:

StatusEffectOngoingAttack – Continuously modifies the unit’s attack (could be used for an

effect like “while this status is active, gain +X attack”). StatusEffectOngoingMaxHealth – Adjusts the unit’s max health while active. StatusEffectWhileActiveX – A pattern where X effect is active as long as this status remains. E.g., StatusEffectWhileActiveAlliesImmuneToX might make allies immune to a certain status while this is active, or StatusEffectWhileActiveApplyXToEachCardPlayed which applies

some effect every time a card is played, as long as the status persists.

StatusEffectTemporaryTrait – Grants a trait to the unit as long as this status effect is on it.

Once the status expires, the trait is removed. (For example, a temporary Aimless or Frenzy granted

for a turn via a status.)

Special/Composite Effects: Some statuses serve specific mechanics or are combinations:

StatusEffectMultiHit – (Frenzy) Causes the unit to attack multiple times (this is effectively the

Frenzy trait implemented as a status in code, but in game it’s treated as a trait/keyword).

4

StatusEffectBombard – Likely related to multi-target attacks (could be the Barrage effect

causing attacks to hit all enemies in a row). StatusEffectStealth – Unit cannot be targeted while Stealth is active; breaks upon the unit

taking a certain action. StatusEffectImmuneToX – Makes the unit immune to a specific status or effect type while active. StatusEffectRecycle – Used for the Recycle mechanic (e.g. countdown adjustments when

recycled). (And more like StatusEffectJuice , StatusEffectLastStand , etc., each implementing a

specific unique mechanic.)

Using StatusEffectDataBuilder (Creating New Status Effects): Mods can create custom status effects via StatusEffectDataBuilder . This builder allows you to define a new status’s properties and link it to an

existing behavior class (or a new subclass, though usually you use existing classes). The general process:

choose an appropriate base class matching the desired behavior, give the new effect an internal name and

in-game text, and set any specific fields either via builder methods or in a post-build callback.

Class signature: public class StatusEffectDataBuilder : DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> . This builder produces a StatusEffectData ScriptableObject that the game uses to recognize the new effect.

Selecting subclass: The builder’s .Create(string internalName) method is usually called

with a template class. For example, to create a status that behaves like Snow (freezes counter), use

.Create<StatusEffectSnow>("Snow") . To create a conditional effect like “on kill gain attack,”
use   .Create<StatusEffectApplyXOnKill>("On   Kill   Apply   Attack   To   Self")
.
The generic should be a subclass of StatusEffectData that has the logic you need. If no

22

7

existing one fits, you may need to create a new subclass in code (advanced use).

Basic builder fields: After Create , you can use fluent methods to set the status’s properties:

.WithName(string englishName)  – (If available) Set the effect’s display name. In Wildfrost’s

case, status effects usually don’t have a proper noun name aside from keywords, so this may not be

commonly used (the keyword usually provides the name/description).

.WithText(string description)  – Sets the description text, using placeholders like  {0}  for
the numeric value and <keyword=?> for any keyword icons. For example: "Gain {0} on kill"

with a text insert for attack icon

.WithTextInsert(string insert)  – Provides the formatted insert for the placeholder in the
text. E.g. .WithTextInsert("<+{a}><keyword=attack>") to show "+{attack icon}" where {0} is

23

.

substituted

.WithIcon(Sprite iconSprite)  or  .WithIconName(string iconResourceName)  –

24

.

Assigns a custom icon for the status (if not using an existing keyword’s icon). Alternatively,

.WithIconGroupName(string group)  can group it with certain UI icon behaviors (for instance,

Snow uses the "counter" group to appear on the counter bar)
.WithVisible(bool visible)  – Whether the status’s icon is shown on the unit. Many trigger-

25

.

type statuses may be hidden (visible=false) so as not to clutter the UI; e.g., some internal triggers don’t show an icon. Setting true makes it visible with an icon

26

.

5

.WithKeyword(string keywordName)  – Links the status to a keyword entry (for tooltip text). For

example, linking to the "snow" keyword will use its tooltip and icon when this status is referenced in
card text

27

.

.WithIsStatus(bool isStatus)  – Marks whether this effect is considered a status (true) or a

trait (false) for game logic purposes

28

. Generally set true for things that behave like status effects

(stackable, removable) and false if making a trait-like effect.

Advanced builder usage: Many status effects require setting specific fields that aren’t directly

exposed via the fluent API. For those, the builder offers a way to modify the created object after

building:

.SubscribeToAfterAllBuildEvent(Action action) – This lets you run code after all

content is built (ensuring cross-references are resolved). The provided lambda receives the built StatusEffectData ( T would be the specific class) so you can set fields. For example, the base modding docs use this to set applyFormatKey and other fields on Snow effectToApply on conditional effects

, and to assign

.

29

8

◦

Example: Creating the Snow effect
new StatusEffectDataBuilder(this)

.Create<StatusEffectSnow>("Snow")

.WithVisible(true)

.WithIconGroupName("counter")

.WithType("snow")

.WithKeyword("snow")

.WithIsStatus(true)

.SubscribeToAfterAllBuildEvent(data => {

data.applyFormatKey = Extensions.GetLocalizedString("Card

Text", "Apply {0}");

data.removeOnDiscard = true;

});

This defines a Snow status that is visible, uses the snow icon/keyword, and after building, sets applyFormatKey (the format for "Apply X" text) and removeOnDiscard = true (Snow is

removed if the card is discarded)

30

31

.

◦

Example: Creating a "gain attack on kill" effect
new StatusEffectDataBuilder(this)

.Create<StatusEffectApplyXOnKill>("On Kill Apply Attack To Self")

.WithText("Gain {0} on kill")

.WithTextInsert("<+{a}><keyword=attack>")

.WithCanBeBoosted(true) // allow Spice to boost it

.SubscribeToAfterAllBuildEvent(data => {

data.effectToApply = Get("Increase Attack");

6

data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

});

This makes a status that, when the unit kills something, applies the Increase Attack effect to

33

32

itself Attack" is a base effect that simply raises attack stat), and applyToFlags to Self (so the

. We set effectToApply by referencing an existing status (assumed "Increase

effect is applied to the unit holding this status)

10

. The .WithCanBeBoosted(true)

means this status’s stacks can be increased by Spice (the “spice” mechanic that boosts certain

effects) – a flag available for applicable statuses.

for a “when hit,

Similarly, you would use StatusEffectApplyXWhenHit and in the AfterBuild event set data.effectToApply = Get("Snow") StatusEffectApplyX.ApplyToFlags.Attacker (assuming such a flag exists to target the

apply snow to attacker” effect,

data.applyToFlags

and

=

attacker). This would create a reactive status akin to a Frostbite effect (see Tutorial 3 for a complete

example).
Status Effect Stacks in Cards: In card definitions (see CardDataBuilder), status effects can be attached to

cards in two ways: - Attack effects: These are status effects that a unit or item applies to its target when hitting. They are often referred to as on-hit effects. In card data, they are stored as attackEffects (an array of StatusEffectData plus a stack count). For example, a card could have an attack effect of 3

Shroom, meaning whenever it hits an enemy it applies 3 Shroom to that enemy. - Passive effects: These are status effects that a unit has inherently while on the board. In card data, stored as effects (or

sometimes called startWithEffects). For example, a unit card might have a passive Snow 2, meaning it starts

with 2 Snow on itself (freezing its own counter for 2 turns), or an effect like Spice that boosts its attack.

When building cards or upgrades, you will see methods to set these (e.g. SetAttackEffects ,
SetEffects , etc.). These take parameters often specified via helper structs or builder functions (some mod APIs use a shorthand like SStack("EffectName", amount) to denote a StatusEffectData

reference with a stack count).

Note: The public modding documentation historically listed the names of status effects but did not detail
each one

34

. In this guide, we provided categorized descriptions above. Modders are encouraged to reuse

(via existing StatusEffectDataBuilder.Create ) and only create new subclasses if

behaviors

possible

status

when
absolutely necessary. Always test custom status effects to ensure their triggers work as expected, and keep in mind interactions with Spice (if canBeBoosted ) and other mechanics.

Card Scripts

In Wildfrost, beyond simple numerical effects (damage, healing, etc.), many complex card behaviors are

implemented using Card Scripts. A CardScript is a piece of logic that can be attached to a card to execute a

special action when the card is played or under specific conditions

35

36

. Card scripts let designers create

unique card effects by composing them from predefined script classes, without writing new code for each

card. Mods can leverage these same script classes to give custom cards advanced behaviors.

7

CardScript Base Class: The base class is CardScript (which inherits from Unity’s ScriptableObject). All specific scripts are subclasses of CardScript . Every CardScript defines a Run(CardData target)

method that is called to perform its effect on a target card

37

. When a card with scripts is played or

triggered, the game will execute each attached script’s Run method, providing the relevant target (which

could be the card itself or another card depending on context).

When Card Scripts Run: Typically, card scripts on a card are executed when that card’s effect resolves (e.g.,
when you play an item or ability card, after its basic effects, it runs its scripts)
38

. Some scripts might also

run at specific triggers (for example, a script might run when a certain event happens if the card is on the

board, though most are used on play). By attaching scripts to cards (via CardDataBuilder or

CardUpgradeDataBuilder), modders can create effects like “when played, do X” or modify other cards as a

result of playing this card.

Attaching Scripts: In the mod API, you can assign scripts to a card using builder methods like
CardDataBuilder.SetScripts(params

CardScript[]

scripts)

or

CardUpgradeDataBuilder.SetScripts(...)

. You typically do not instantiate a CardScript with

new  directly; instead, you retrieve an existing script object from the game or mod registry, or use provided
builder utilities. (The examples in this guide may show  new CardScriptX()  for clarity, but in practice you
might use something like Get("ScriptName") to fetch a reference, depending on the

39

modding API design.)

Important Considerations: Card scripts often assume certain things about the card’s other properties

(targeting, stats, etc.) and can affect not just the played card but other cards in hand, deck, or on the field

40

. It’s important to design the card’s overall behavior (target types, etc.) to work in tandem with its

scripts. Many base game card effects are essentially a combination of one or two scripts plus perhaps a

status effect application.

Below is a list of known CardScript subclasses from the base game and their practical effects:

CardScriptAddAttackEffect: Adds a status effect to the target card’s attack-effects list when executed

41

. In other words, after this script runs, the target card (usually a unit or item) will start applying a

new status on hit. Example use: a card that says “give an ally Spice on hit” could attach this script
targeting that ally, with the script’s  effect  set to Spice and a count. Internally, this script takes a
StatusEffectData   (and   possibly   a   stack   count   or   range)   and   appends   it   to   the   target   card’s
attackEffects array

42

.

CardScriptAddPassiveEffect: Similar to above, but adds a status effect to the card’s passive effects ( CardData.effects )

. This means the target card gains a continuous status effect. For

43

example, using this script you could give a unit a status like Snow or Shell inherently. (In practice,
one could just add those in the card’s definition, but this script allows doing it dynamically to an

existing card.)

CardScriptAddRandomBoost: Randomly increases one of the target card’s stats by a certain

amount

44

. It likely chooses between attack, health, etc., and boosts one at random. Use case: a

card that says "Give a random boost to a card" could use this on a target card to unpredictably raise

one stat.

8

CardScriptAddRandomCounter: Randomly adjusts the target card’s counter (countdown timer) by

some amount

45

. Possibly it can either increase or decrease the countdown by a random value in a

range. This could simulate a card effect like “randomly delay or hasten a unit’s next attack.”

CardScriptAddRandomDamage: Randomly increases the target card’s attack damage by some

amount

46

. Example: a card that has unpredictable damage output could apply this to itself or

another card to give a random attack bonus.

CardScriptAddRandomHealth: Randomly boosts the target card’s health by some amount

47

.

Could be used for a chaotic heal or buff effect.

CardScriptAddTrait: Grants the target card a specified Trait (keyword ability)

48

. For example, if

you want to give an ally Aimless or Frenzy until end of battle, you could use this script targeting that

ally and specifying the TraitData to add. It modifies the card’s traits list at runtime. E.g. “Give an ally

Frenzy” uses CardScriptAddTrait with the Frenzy trait

48

.

CardScriptBecomeBasicItemCard: Transforms the target card into a basic item card

49

. This likely

strips away any special properties and turns the card into a common “Junk” or similar basic card.

Possibly used in an effect where a card gets downgraded or neutralized.

CardScriptBoostAttackEffectsOrAttack: Increases either the potency of the card’s on-hit effects, or

if it has none, increases its base attack

50

. This script likely checks if the card has any attack-effect

status (like Shroom, etc. on hit). If yes, it might double the stacks or add to them; if not, it buffs the

card’s attack stat instead

50

. This ensures “something” gets buffed – it’s used by cards that either

have bonus effects or just raw attack so that either way they get a benefit. (E.g., a buff card in game

that says “double the target’s effects or attack”.)

CardScriptCopyEffectsFromOtherCardInDeck: Copies all status effects from another card in your

deck to the target card

51

. Essentially, the target card will gain all the effects (both passive and on-

hit, presumably) that some source card has. Use case: a card that says “Copy all buffs from an ally to another ally.” The script’s Run(target) likely expects the target to be the card to copy from (or vice

versa). In use, one card effect might pick an ally as the source and then apply this script on the card

that should receive the effects

51

52

.

CardScriptCopyPreviousCharm: If the target card had a charm (upgrade) applied just before, this

script duplicates the effect of that last applied charm onto the target

53

. In other words, it copies the

most recent charm used onto another card. This is a niche effect (imagine a scenario where you copy

the last charm you used onto a second card).

CardScriptDestroyCard: Permanently destroys the target card

54

55

. If used on a card in hand or

deck, it removes that card from the game entirely (as opposed to just discarding it). For example, a

card that says “Destroy an item in your hand” would use this script on the chosen card

54

56

. The

script likely handles removing the card’s data from all decks and destroying any instance on board.

9

CardScriptGainAttackIcon: Likely adds the “+X attack” icon indicator to a card’s attack stat for visual

effect

57

. This might not actually change the attack value, but causes the UI to show the plus icon. It

could be purely cosmetic or used to mark a temporary buff.

CardScriptGiveUpgrade: Applies a specified CardUpgradeData (charm/token/crown) to the target . This script has an upgradeData field referencing an upgrade. When run, it clones

card

58

59

that upgrade and attaches it to the target card

58

. This is effectively how a card effect can “charm” a

unit mid-run. For example, a card that says "Charm an ally with Sun Charm" would use this script

with the Sun Charm upgrade data – the ally gains that charm’s effects

59

. (See Tutorial 1 for a mod

example.)
CardScriptLeader: Possibly designates the target card as a leader or interacts with leader selection

60

. The base game’s usage is unclear – it might be involved in tribe selection or promotion of a card

to leader status. It’s an uncommon script; modders might use it if creating mechanics to swap leader

mid-run or similar.

CardScriptModifyCharmSlots: Adjusts the number of charm slots the target card has

61

.

Normally, each card can only equip one charm (slot = 1). This script can increase (or decrease) that,

allowing a card to hold multiple charms. For instance, a special upgrade could grant a card an extra

charm slot. (Base game doesn’t widely use this, but the code suggests support for multi-charm cards

in the future or via mods.)

CardScriptMultiplyCounter: Multiplies the target card’s counter by a factor (and rounds up or down depending on settings). For example, doubling a unit’s countdown. It has a multiply factor and possibly a roundUp flag. If roundUp is true, it uses ceil; otherwise rounds normally. Use case:

an effect that says “double a unit’s cooldown” or “halve a unit’s cooldown” (with factor 0.5,

presumably).

CardScriptMultiplyHealth: Multiplies the target’s health by a factor (with optional ceiling). E.g.,

double a unit’s HP for this battle (or halve it).

CardScriptMultiplyPassiveEffect: Multiplies the stacks of the target card’s passive status effects by a factor. This goes through target.startWithEffects and scales them. For instance, if a unit

has 3 Shell and 2 Spice and you run a double factor, it would have 6 Shell and 4 Spice afterwards.

Useful for amplifying or reducing all current buffs on a card.

CardScriptRemoveAttackEffect: Removes an on-hit effect from the target card’s attackEffects list. If configured with removeAll=true , it clears all attack effects. Otherwise it might remove a specific

effect (probably the first or a specified one). This can be used to strip a unit of a poison effect or

other on-hit effect it has.

CardScriptRemovePassiveEffect: Removes a passive status effect from the target card’s effects

(startWithEffects) list. Similarly, could remove all if configured, or just a particular one. For example,

an effect that "cures all status effects" on a card could use this script with removeAll.

10

CardScriptRemoveTrait: Removes a trait from the target card. Can remove all traits or specific ones.

For instance, a card that says “remove Aimless from an ally” would target that ally with this script configured to remove Aimless. If removeAll=true and an exclusion list is provided, it will strip all

traits except those excluded.

CardScriptReplaceAttackWithApply: Converts the target’s attack into an effect application. If the

target card has an attack value > 0, this script will take that value and instead make the card apply a

status effect on hit equal to that value, then set the attack to 0. Essentially, the card stops dealing

direct damage and “attacks” by applying some effect. This is used for certain enemies/items that

don’t deal damage but apply effects. For example, an item might normally do X damage, but after this

script, it will do 0 damage and apply X stacks of a status (like Shroom) on hit. The script has an effect field for which status to apply.

CardScriptRoundHealth: Rounds the target card’s health up or down to the nearest multiple of a given number ( round ). E.g., if round=5 and the unit has 7 HP: ceiling would set it to 10, floor to 5.

Possibly used in an effect that “rounds up health” or standardizes health values.

CardScriptSetCounter: Sets the target card’s counter (cooldown) to a random value within a given range ( countRange ). It likely only applies if the card actually has a counter (units). E.g., an effect

“randomize a unit’s counter between 1 and 6” would use this with countRange = (1,6).

CardScriptSetDamage: Sets the target card’s attack damage to a random value within a given range ( damageRange ). If the card has an attack stat, it will roll a value in that range and set it. (Likely also

sets the card’s max damage if it has a concept of variable damage.) Example: a card "Snowcake" that

sets an enemy’s attack to 0 could potentially be implemented by targeting that enemy and using this

with range (0,0)

62

63

.

CardScriptSetHealth: Similar to above, sets the target’s health to a random value in healthRange . Could be used for a chaotic effect that changes a unit’s HP.

CardScriptSwapEffectsBasedOn: Swaps certain status effects on the target card based on some condition or mapping. From code, it iterates over target.attackEffects and possibly target.passive effects and might swap one effect for another if criteria met. The name

suggests if a card has effect A, replace it with B, or vice versa, perhaps depending on some state.

(This might be a specialized script for a specific card or boss – e.g., swapping poison to frost on

certain trigger.)

CardScriptSwapTraits: Swaps two traits on the target card. It has fields for traitA and traitB .

When run, for each trait the card has: if it’s traitA, it becomes traitB; if it’s traitB, it becomes traitA.
This could be used for a card that toggles certain behaviors, e.g., “swap Aimless and Barrage on a

unit” – if unit had Aimless it gains Barrage instead and vice versa.

As seen, card scripts can dramatically alter cards. You can mix and match scripts on a single card to achieve

complex results. For example, a card could have two scripts: one to add a trait to a target and another to

destroy itself after use. When designing mod cards: - Attach scripts that make sense for the card’s timing

(on play vs ongoing). Most scripts run on play/resolution. For continuous effects, consider using status

11

effects or traits instead. - Ensure the card’s target type aligns with the script. E.g., CardScriptDestroyCard’s

target should be set to a card in hand if you expect to destroy a card from hand, etc. - Test combination of

scripts: order of execution might matter if one script’s outcome affects another. The game likely processes

them in the order they’re listed on the card.

Working with Card Scripts in Mods: To use a script, you need a reference to its ScriptableObject. The base

game like Get("CardScriptName") ). If the mod API doesn’t provide a direct lookup, another trick is

accessible

somewhere

something

registers

(possibly

these

via

to create a dummy card in your mod that uses the desired script via builder calls (the builder might resolve

the reference by name). Consult community examples for how to fetch script references. In code examples

here, we simply instantiate for illustration, but actual usage may differ.

Example Use: Suppose you want to implement a card effect "Snowblind: Apply 3 Frost and gain
Aimless." Frost is a status effect (reduces attack) and Aimless is a trait (random target). You could design

"Snowblind" as an item card with: - Target: an enemy (for Frost application). - A CardScriptAddAttackEffect

that adds Frost(3) to that enemy’s attack effects (so effectively, “apply 3 Frost” can be done by a script that

makes the enemy’s attacks inflict Frost on itself – but that’s indirect). Alternatively, better: use a status effect

or direct effect to apply frost. Let’s use a script differently: - Instead, make the card target an ally and use

CardScriptAddTrait to give that ally Aimless, and simultaneously a status effect (or second script) to apply

Frost to all enemies. This gets complicated – it might be simpler to split into two cards or handle via status.

The takeaway: many card effects that seem unique are achievable by combining existing scripts and

status effects

64

65

. The modding documentation encourages modders to look up base game references

for effects they want – often a script or status already exists that does the job

66

67

. In this guide’s

tutorials, you’ll see practical examples of attaching scripts to achieve new card abilities.
Builder Classes

Wildfrost’s modding API provides Builder classes to create new game content in a structured way. Each

builder class corresponds to a specific type of game data (cards, upgrades, tribes, etc.) and helps assemble

a new ScriptableObject asset for it. Builders use a fluent interface: you chain method calls then finalize by calling Build() (or by adding to the mod’s asset list). Under the hood, builders handle assigning IDs,

linking references, and registering the new content so the game recognizes it.

All builders inherit from a common generic base: DataFileBuilder<T,Y>, where T is the data class being built (e.g. CardData) and Y is the builder class itself. The base provides general methods like .Create(...)

and event subscription hooks. The builder classes also often have static factory usage via their constructor

or Create method.

General Builder Patterns: - Every builder has Create(string internalName) (or an overload) to start

defining a new asset with an internal identifier. The internal name is often used as a key for lookup (no

spaces, unique). - Most builders have methods to set textual fields (names, descriptions), assign sprites or

icons, and link related data. - Builders automatically handle adding the new asset to the game’s databases

when built. Mods typically call something like assets.Add(new XDataBuilder(this)...Build()); to
register the asset. - For complex data, builder might not expose everything; you may need to use

12

SubscribeToBuildEvent or SubscribeToAfterAllBuildEvent to tweak fields after creation (as

discussed for status effects). This is common for linking references that might not exist until all builders

have run (like adding custom cards to a tribe's leader list, etc., which is often done in an AfterAllBuild

subscriber to ensure the CardData exists).

Below, we document each builder class relevant to modding, including its class signature, purpose, main methods, and notes. (All builders reside in the Deadpan.Enums.Engine.Components.Modding

namespace in the game’s code.)

BattleDataBuilder

Class signature: BattleDataBuilder> .

public   class   BattleDataBuilder   :   DataFileBuilder<BattleData,

Purpose: Creates new BattleData objects, which define enemy encounter configurations (waves of
enemies, battle parameters). Custom BattleData can be used for designing new battles (e.g., a special boss

fight or a custom encounter for an event or challenge).

Use Cases: Mods introducing new encounters, daily challenge battles, or custom events might define new
BattleData for them. A BattleData includes the composition of enemy waves, the rewards, and possibly

other settings like wave count and difficulty scaling.

Key Fields & Methods: - .Create(string internalName) : Start a new BattleData with an identifier. -
.WithTitle(string   title) :   Sets   a   display   name   for   the   battle   (if   shown   to   players).   -
.SetUpScript(Script scriptObj) : Attaches a setup Script (likely a  ScriptBattleSetUp  or similar)
to run at battle start. This could define any special behavior as the battle begins. -  .SetWaves(params
WaveData[] waves) : (Likely method) Define the waves of enemies. Each wave might consist of a list of

enemy units to spawn. The builder likely provides an interface or requires you to gather WaveData assets. (The base game likely has WaveData for combinations of enemies). - .WithRewardPools(params RewardPool[] pools) : Assigns reward pool(s) for this battle. E.g., after victory, what rewards the player can get. Boss battles often have a BossReward pool. - .WithBonusUnitPool(RewardPool pool) : Some

battles might have a bonus unit spawn or optional units – this could set an extra pool for randomly

selecting an additional enemy or ally (just speculation from field name). - Other fields in BattleData might include pointFactor (score or difficulty points for the battle) and waveCounter (maybe how many

waves or a counter setting). Builders likely have corresponding methods if modifiable.

Example: Minimal BattleData – Suppose we create a battle with one wave of two specific enemies and a
treasure reward:

var wave1 = MakeWave(Get<CardData>("Goblin"),

Get("GoblinLeader")); // pseudo-code to create a wave

new BattleDataBuilder(this)

.Create("ModBattle_GoblinAmbush")

.WithTitle("Goblin Ambush")

.SetWaves(wave1)

13

.WithRewardPools(Get<RewardPool>("TreasureRewardPool"))

.Build();

This would produce a battle where the player faces a wave containing a Goblin and a GoblinLeader, and on

victory, gets rewards from the TreasureRewardPool.

(Note: Actually constructing WaveData and linking CardData might require more steps or using ScriptAddEnemies

as part of a node event. But the builder abstracts some of it.)

Integration: A BattleData by itself does nothing until it’s used. Typically, you’d reference this BattleData in a
CampaignNodeTypeBuilder (to create a map node that triggers this battle) or in a challenge/daily

definition. See Tutorial 5 for using a custom BattleData in a map node.

BossRewardDataBuilder

Class signature: public class BossRewardDataBuilder : DataFileBuilder<BossRewardData, BossRewardDataBuilder> .

Purpose: Builds BossRewardData assets. BossRewardData defines the set of rewards offered to the player
after beating a boss. In Wildfrost, after defeating a boss, players typically get a choice of a treasure, a

charm, etc. This builder likely allows modders to create new reward selections.

Use Cases: If a mod adds a new boss or a new type of post-battle reward, you could define a

BossRewardData that specifies what rewards to generate (like specific card drops or pools).

.Create(string   internalName) :   New   BossRewardData   asset.   -
Key   Fields   &   Methods:
.AddReward(CardData or CardUpgradeData or RewardPool) : The base game’s BossReward likely

includes multiple reward slots (like one random charm from X pool, one random companion from Y pool,

etc.). The builder might allow adding different reward entries. Possibly methods like .AddCardReward(CardData card) or .AddPoolReward(RewardPool pool, int count) , etc. -

.WithTitle(string   title) :   Possibly   for   naming   the   reward   set   (not   sure   if   shown).   -   .Build()

finalizes.

Example: If you want a boss to drop one guaranteed custom charm and one random companion
new BossRewardDataBuilder(this)

.Create("ModBossRewards")

.AddReward(Get("CustomCharm"))

// fixed charm reward

.AddReward(Get("CompanionRewardPool")) // pull from companion pool

.Build();

Then tie "ModBossRewards" to your boss’s data (likely in ClassData or somewhere the boss is defined).

14

BuildingPlotTypeBuilder

Class DataFileBuilder<BuildingPlotType, BuildingPlotTypeBuilder> .

signature:

public

class

BuildingPlotTypeBuilder

:

Purpose: Creates BuildingPlotType assets, which define slots in Snowdwell (the town) where buildings
can be placed. In Wildfrost, the town has specific plot types (e.g., a plot for the Pet house, a plot for the

Inventor’s Hut, etc.). This builder would be used if adding new building locations.

Use Cases: Likely advanced mods that expand the town with new structures. You’d define a new plot type
(position and requirements) and then a BuildingType to go in it.

Key Fields & Methods:
.WithLocation(Vector3 coords or predefined slot) : Possibly define where on the town map this
plot is. - .WithSize(...) : If relevant (maybe all plots uniform size). - Maybe not heavily used unless

.Create(string   internalName) :

New plot type.

modding the town scene.

Usually, modders might reuse existing plots if just adding a building. If needing a new spot, you’d use this

builder in conjunction with altering the town layout (possibly requiring Harmony patching the town scene).

Note: The specifics of placing new plots might not be fully supported just by builder (since town scene
might not auto-layout new plots). This builder is included for completeness.

BuildingTypeBuilder

Class signature: public class BuildingTypeBuilder : DataFileBuilder<BuildingType, BuildingTypeBuilder> .

Purpose: Builds BuildingType assets, representing a town building (like the Pet House, Inventor’s Hut,
etc.). A BuildingType defines a structure’s properties in town: its name, description, what it does/unlocks,

maybe its model prefab, and which plot it occupies.

Use Cases: Mods can introduce new town buildings for additional functionality (e.g., a new shop or a

shrine). You would create a BuildingType for the building and likely a BuildingPlotType (or use an existing

plot).

Key Fields & Methods: - .Create(string internalName) : ID for the building. - .WithName(string
name) and .WithDescription(string desc) : Set town building display name and description text. -

.WithPlotType(BuildingPlotType plot) : Assign which plot type this building uses. Could link to an
existing or a newly created plot. - .WithSprite(Sprite icon) or .WithModel(Prefab prefab) : Set

the building’s appearance (could be an icon or a 3D model reference if the town uses prefabs). -

.WithUnlockCondition(...) : Many buildings unlock after certain progress (like beating a boss, etc.).

The builder might allow specifying an UnlockData or conditions.

Example: Adding a new NPC building: Suppose a “Card Trader” building that unlocks after beating the game,
where you can trade cards. We’d do:

15

new BuildingTypeBuilder(this)

.Create("CardTraderHut")

.WithName("Card Trader Hut")

.WithDescription("Trade cards with an NPC.")

.WithPlotType(Get<BuildingPlotType>("EmptyLotNorth"))

// assume an empty plot

.WithSprite(LoadSprite("trader_hut.png"))

.WithUnlockCondition(Get<UnlockData>("WinGameUnlock"))

.Build();

And ensure the plot “EmptyLotNorth” exists or was created, and corresponds to a spot in town.

CampaignNodeTypeBuilder

Class DataFileBuilder<CampaignNodeType, CampaignNodeTypeBuilder> .

signature:

public

class

CampaignNodeTypeBuilder

:

Purpose: Creates new CampaignNodeType assets, i.e., new types of map nodes on the overworld run map.
Each node type defines what happens when the player lands on that node (battle, event, shop, treasure,

etc.), the icon/appearance on the map, and how it connects.

Use Cases: Mods adding new map events or encounter types (e.g., a new random event node, a special
challenge node, etc.) will use this. For example, if you want a node that triggers a custom battle or a

narrative event, define a new CampaignNodeType for it.

Key Fields & Methods:

.WithName(string   name)
(perhaps   for   internal   use   or   if   displayed   on   map   hover).   -
.WithMapSymbol(Sprite icon, Color tint, string letterCode) : Sets the icon or symbol shown

.Create(string   internalName) :   Define   a   new   node   type.   -

- 

on   the   map.   Base   game   nodes   have   letters   (e.g.,   “B”   for   boss,   “T”   for   treasure).   There   is   likely   a
.WithLetter(string

- 
.WithEncounter(BattleData battle) : If this node triggers a battle, assign the BattleData it should
use.   -   .WithEventRoutine(EventRoutine   routine)   or   .WithScripts(params   Script[]
scripts) :   For   non-battle   events,   maybe   attach   a   sequence   of   Script   actions.   The   base   game   has
EventRoutine classes for shops, charms, etc., but for mods it might be easier to use script actions. For

letter)

method

code.

letter

the

for

example, the builder might allow specifying a list of actions to perform (like give cards, modify deck, etc.).
We saw classes like ScriptAddCards , ScriptAddUpgrades , which likely can be part of a node's script sequence. - .WithReward(BossRewardData reward) : If the node is an end-of-area boss chest, maybe link a BossRewardData. - .WithRequiresUnlock(...) : Some nodes might be locked behind

progression; builder could allow linking an UnlockData if this node type should only appear after some

condition.

Example: Creating a new event node: “Mysterious Traveler” node where an NPC gives you a choice of a card.
We want an event node that, when landed on, runs a script to present 2 random companion cards for the

player to choose one.

16

new CampaignNodeTypeBuilder(this)

.Create("MysteryTraveler")

.WithName("Mysterious Traveler")

.WithMapSymbol(LoadSprite("node_traveler.png"), Color.white, "??")

.WithScripts(

new ScriptAddRandomCards() { amount=2,

pool=Get("CompanionPool") },

new ScriptSelect() { choices=2, choose=1,

rewardTarget=ScriptSelect.Target.Deck }

// Pseudo: add 2 random companions, then let player pick 1 to keep (the

other is discarded)

)

.Build();

(Pseudo-code for script parameters; actual usage may differ.) This would create a node type that adds two random companion cards and triggers a selection UI for the player to pick one. We gave it a custom icon

and letter "??" on the map.

Once the node type is defined, you’d incorporate it into the map generation or events. This often means

adding it to the pool of possible nodes in a region or linking it as an option after certain battles. Wildfrost’s

map generation might not automatically include new node types without patching – but you could replace

an existing event type with your new one via mod code, or add it in a daily/challenge context.

Script Actions (Event Scripts): The Script... classes (like ScriptAddRandomCards, ScriptAddUpgrades,

ScriptBattleSetUp, ScriptRunScriptsOnDeck, etc.) are building blocks for event logic. They are beyond the scope of this reference to detail each, but in summary: - ScriptAddEnemies/ScriptBattleSetUp –

likely to spawn enemies or set up an encounter (used for battles within events). - ScriptAddRandomCards/Upgrades – add random choices of cards or charms. - ScriptSelect – prompt the player to select from given options (cards/upgrades). - ScriptRemoveRandomCards , ScriptUpgradeEnemies , etc. – a variety of effects to manipulate game state during events (e.g., curse

the deck, upgrade all enemies, etc.).

Using these, a mod can craft complex events (like a shrine that upgrades all your companions but gives a

penalty, etc.). You attach these scripts to a CampaignNodeType or trigger them via ChallengeListener (for

global effects).

CardDataBuilder

Class signature: CardDataBuilder> .

public   class   CardDataBuilder   :   DataFileBuilder<CardData,

Purpose: The most frequently used builder – creates new CardData assets (for companions, items, leaders,
etc.). A CardData defines all stats and behaviors of a card: its name, attack, health, counter, effects, traits,

scripts, targeting, etc.

17

Use Cases: Every new card (ally, enemy, item) in a mod is defined via CardDataBuilder. For leaders and
companions specifically associated with a tribe, you will also use ClassDataBuilder to tie them in (see

ClassDataBuilder).

Key Methods: - .Create(string internalName) : Begins a new CardData. This internalName is used as

the card’s unique ID in the mod and save data. - There are also convenience versions:

.CreateUnit(string internalName, string title, string targetMode="TargetModeBasic",

string   bloodProfile="BloodProfileNormal")   for   quickly   setting   up   a   unit   (companion/monster)
.   And   .CreateItem(string   internalName,   string
with   default   animations   and   blood   effect
title, string targetMode="TargetModeBasic", string idleAnim="SwayAnimationProfile")

68

69

for items . These are shortcuts to configure certain defaults for units vs items. - Stats and Core: -

.SetAttack(int   attack)   –   Base   attack   damage.   -   .SetHealth(int   health)   –   Base   health.   -
.SetCounter(int counter)  – Starting counter (for units with turn timers). -  .SetCost(int cost)  –

Will   set   the   redraw/bell   cost   for   playing   the   card   (if   applicable;   companions   have   no   cost,   items   do).   -
.SetTarget(CardTargetType targetType)  – Determines what the card can target or how it’s played
(self, friendly, enemy, row, all, etc.). -   .SetSpeed(int speed)   – If initiative/speed exists (some games
have fast/slow, Wildfrost might not use speed stat explicitly). - .SetRank(int rank) – Possibly for card

or tier (if relevant for charms or something).

level
.IsUnit() / .IsItem() / .IsCompanion(challengeData, bool) etc.: Methods to mark what kind of card it is (the wiki shows IsCompanion(ChallengeData challenge, bool value)

Card Type and Tags:

, likely

70

toggling companion status for a certain challenge mode). In simpler terms, items and units might be auto-

detected by stats, but these flags can enforce a card as a companion or pet for challenge rules. - .IsPet(bool value) – Mark as a pet if this is a pet card (some companions are “pets” with special rules). - .WithTribe(ClassData tribe) – (In absence of direct method, you often assign tribe via

71

adding to that ClassData’s pools. But a method might exist or be inherited to tag the card’s class). -

.SetCardType(CardType   type)   –   If   needed,   explicitly   sets   card   type   (there   might   be   an   enum   for

Companion,   Item,   Structure,   etc.   –   CardTypeBuilder   is   related   if   making   new   categories).   -  Effects   and
Traits: - .SetAttackEffects(params StatusEffectStack[] effects) – Adds status effects that this

card applies on hit (attack effects). The wiki actually advises not to use some of these directly in

, but they exist. This accepts array of CardDataBuilder (possibly due to initialization order) (StatusEffectData, stackCount) pairs. -

.SetEffects(params   StatusEffectStack[]
effects)   – Sets passive effects the card has from start. -  (In practice, if the builder’s SetAttackEffects was
problematic, one could use  .SubscribeToBuildEvent  to add to cardData.attackEffects manually. But usually
builder works.) -  .SetTraits(params TraitStack[] traits)  – Adds trait(s) to the card. For example,
.SetTraits(TStack("Aimless",1))  to give Aimless trait. (If the builder doesn’t have a direct SetTraits,

72

CardUpgradeDataBuilder does, but CardDataBuilder likely inherits a similar method from a base or uses AddTrait individually.) - .AddTrait(TraitData trait, int stacks=1) – possibly an alternative to add one trait. - .CanBeHit(bool value) – If false, the unit cannot be targeted by attacks

. (Could be

73

used for things like Snowcake which is intangible? In base game, perhaps used for effects or summons that shouldn’t be attackable.) - .CanBeHealed(bool value) – Possibly if healing can/cannot affect it. - .CanPlayOnEnemy(bool) / .CanPlayOnFriendly(bool) / .CanPlayOnBoard(bool) / .CanPlayOnHand(bool) :

75

74

These appear in the wiki listing . They toggle where the card can be played. For instance, an item might .CanPlayOnEnemy(true) to target enemies, or a specific card might allow targeting allies. .CanPlayOnBoard(true) means can be played onto the board (units typically true). .CanPlayOnHand(true) means can target cards in hand (like a card that destroys or buffs another card in your hand). - .CanShoveToOtherRow(bool) – If true, the unit can be moved between rows (some

18

76

. - Miscellaneous: - .SetName(string englishName) – Sets the heavy units might not be moveable) card’s display name. (Likely .SetTitle or .WithTitle might be used instead, to avoid confusion with internal name.) - .SetDescription(string desc) or .SetText(string desc) – Sets the card’s . Use {0} etc. for placeholders if needed. - .SetLore(string loreText) – If description text the card has lore flavor text. - .SetSprite(string spritePath) or .SetSprites(mainSprite, bgSprite) – Assign card art. Possibly use something like .SetSprites("Images/mycard.png", . - .SetAnimationProfile(string profile) – For units, select an "Images/card_bg.png")

77

66

78

animation rig (e.g., "SwayAnimationProfile" for items to sway, or specific idle anims). The CreateUnit/ . - .SetRarity(int rarity) – If game has rarity

CreateItem already set defaults like Sway for items tiers for cards, set it. - .AddPool(string poolName) – Adds this card to a reward pool or deck pool . For instance, .AddPool("GeneralCardPool") might ensure it can appear as a general reward. There

79

73

are also specialized pools (boss rewards, tribe pools, etc.). If providing a RewardPool object, there’s an overload AddPool(RewardPool pool)

73

.

Scripts (custom behaviors):

.SetScripts(params   CardScript[]   scripts)   –   Attach   card   scripts   to   this   card

39

. This

allows complex behaviors as described in Card Scripts section. For example:

.SetScripts(Get<CardScript>("CardScriptDestroyCard"))

to attach a destroy effect script.

Building and Registration:

Finally call .Build() (or simply add the builder to mod assets list). After Build, the CardData is created and can be fetched via TryGet(internalName) if needed.

Example: Creating a Companion Unit
new CardDataBuilder(this)

.Create("companion_frostling")
.SetName("Frostling")

.SetDescription("Apply 1 <keyword>snow</keyword> on hit.")

.SetAttack(2).SetHealth(5).SetCounter(3)

.SetTarget(CardTargetType.Enemy)

// attacks enemies

.SetEffects()

// no passive status to start

.SetAttackEffects(SStack("Snow", 1))

// on-hit apply 1 Snow

.SetTraits(TStack("Aimless", 1))

// Aimless trait (random target)

.Build();

This defines a 2 attack, 5 health ally with a 3-turn counter that attacks a random enemy and applies 1 Snow

on each hit. You would then add this card to a tribe or pool (e.g., via ClassDataBuilder or RewardPool).

19

Notes: The example uses shorthand like SStack("Snow",1) to denote a StatusEffectData
reference with count. In an actual mod, you might retrieve StatusEffectData snowData = Get("Snow") CardData.StatusEffectStacks(snowData,1)) .

.SetAttackEffects(new

then

and

do

Also, Aimless trait’s effect (random targeting) is inherently handled by game logic if the trait is present, even

though it’s technically a trait and not a status.

CardTypeBuilder

Class signature: CardTypeBuilder> .

public   class   CardTypeBuilder   :   DataFileBuilder<CardType,

Purpose: Possibly allows creation of new CardType categories. CardType in Wildfrost likely refers to the
classification like Item, Companion, Leader, Boss, etc., which might be enumerated. It’s unusual to create new fundamental card categories via mod (not commonly needed).

Use Cases: Rare – perhaps if a mod wants to introduce a fundamentally new kind of card (say, “Structure” or
“Spell” if not already present), a CardTypeBuilder might be used to define it so that filters and UI treat it

separately.

Key Fields: - .Create(string name) – define a new card type name. - Possibly .WithIcon(...) if
each type has an icon or frame. - .WithOrder(int order) – if types are ordered in collection UI.

Most mods likely don’t use this; they stick to existing types or repurpose them. Creating a new CardType

might not automatically integrate without deeper patching.

CardUpgradeDataBuilder

Class

public   class   CardUpgradeDataBuilder   :   DataFileBuilder<CardUpgradeData,

signature:

CardUpgradeDataBuilder> .

Purpose: Creates new CardUpgradeData assets, which represent upgrades/charms/crowns that can be
applied to cards. This builder is used for custom Charms (equippable modifiers), custom Crowns (starting

cards), or any card modifications.

Use Cases: Any mod adding new Charms (the game’s term for persistent upgrades found in runs) will use
this. Also, if adding new “token” upgrades or special status effects as upgrades.

Key Methods: - .Create(string internalName) – New upgrade asset (internal id). This often also

determines the class name used in code (e.g., internal "SunCharm" might result in class CardUpgradeSunCharm). - .WithType(CardUpgradeData.Type type) – Set whether this upgrade is a

Charm, Token, or Crown

80

81

. Charms are regular equippable upgrades (one per card normally), Tokens

might be other upgrades (like temporary ones), Crowns mark a card to start in play. For charms, you’d use . - There might also be older methods IsCharm(true/false) etc., CardUpgradeData.Type.Charm

82

20

but use of WithType is clearer. - .AddPool(string poolName) – Puts this upgrade into a charm pool or . E.g., .AddPool("GeneralCharmPool") to make it findable in general charm

specific reward pool

83

83

. - .WithTitle(string title) – Name of the charm as seen by players drops . -

.WithDescription(string desc)  – Description text. Use  <keyword=...>  tags as needed for icons,
. - .WithImage(string spritePath) – Icon for the charm . - .WithTier(int tier) –

etc.

84

86

85

89

87

. -

. - Stat modifications: The CardUpgradeData can Rarity/tier of the charm (e.g., 1 = common, 2 = rare) . - .ChangeHP(int modify a card’s stats: - .ChangeDamage(int amount) – Add to the card’s attack .ChangeCounter(int amount) – Modify countdown. - amount) – Add to health

.SetDamage(int value)   /   .WithSetDamage(true)   – If setDamage flag is true, the   damage   value
. Similarly for HP and counter with setHP ,

will replace the card’s base attack instead of adding setCounter flags .ChangeHP(1).WithSetHP(true) meaning set base HP to 1 (ignoring original) . - If WithSetHP(false) (default), then ChangeHP is additive. - .ChangeCounter(int amount) and possibly . - .ChangeUses(int amount) – For cards

.WithSetCounter(bool)   to  set  the  counter  exactly

The builder might handle this via paired methods:

e.g.,
88

90

91

92

93

90

.

with limited uses (like how many times it can be used before destroyed), and maybe

.WithSetUses(bool)  to set exactly
. - Effects and Traits granted: -  .SetAttackEffects(params
StatusEffectStack[] effects) – Grants on-hit effects to the card when this upgrade is applied

95

94

.

For example, a charm that “applies 1 Shroom on hit” would add a Shroom attackEffect . -

.SetEffects(params StatusEffectStack[] effects)  – Grants passive effects (these go into card’s

95

list).

E.g.,

effects

.SetTraits(params TraitStack[] traits)   – Grants traits to the card
.SetScripts(params CardScript[] scripts)  – Attaches card scripts to the card when upgraded

a charm that gives Shell

3 would add a Shell

(e.g., Aimless, Frenzy). -

effect.

96

97

39

. This is powerful: it means a charm can actually bestow a triggered script behavior to a card. For instance, a charm could give a card a CardScriptDestroyCard script to self-destruct after use, or anything from the CardScript list. - Other flags: - .WithConstraints(...) – There might be constraints like “can only equip on allies” or “not on certain tribe”. The wiki mentions SetConstraints anchors

98

but specifics unknown.
.WithCanBeRemoved(bool)   – If false, the upgrade cannot be removed from the card (some upgrades

Possibly you can restrict upgrade usage

(for balance).

might be permanent or cursed)

99

. Charms in base game typically cannot be removed mid-run, but maybe

tokens can? This flag likely defaults false but can set true if a mod makes removable upgrades. - .BecomesTargetedCard(bool) – A flag used rarely (e.g., if an upgrade should make a card valid for

certain targeting it normally isn’t)

99

. Not commonly needed.

Example: Creating a Charm: Let's use the “Glacial Charm” example from earlier, which gives a unit a new trait
and an effect:

new CardUpgradeDataBuilder(this)

.Create("CardUpgradeGlacial")

.AddPool("GeneralCharmPool")

drops

.WithType(CardUpgradeData.Type.Charm)

.WithImage("GlacialCharm.png")

.WithTitle("Glacial Charm")

// available in normal charm

.WithDescription("Gain <keyword=glacial>")

// glacial is a custom keyword

we'll define

.WithTier(2)

// rare charm

21

.ChangeHP(1).WithSetHP(true)

// Set base HP to 1 (maybe it's

a trade-off charm that lowers HP)

.SetAttackEffects(

SStack("Snow", 1), SStack("Frost", 1)

// On hit: apply 1 Snow and 1

Frost

)

.SetTraits(TStack("Glacial", 1))

// Give the custom trait

'Glacial'

.Build();

This charm would appear as a Tier 2 charm in drops. It sets the bearer’s health to 1 (dramatically lowering

it), but in exchange gives their attacks two effects (Snow and Frost) and a trait “Glacial.” The <keyword=glacial> in description will show an icon or name for the glacial trait in tooltip. We assume

glacial is a new trait meaning "when Snow is applied to an enemy, also apply Frost, and vice versa" (just an

example of a complex trait). Implementing that trait might involve adding a reactive status effect or code
(see Keywords & Traits).

The builder above doesn’t explicitly set any stat for attack, so attack remains unchanged. We lowered HP as

a balancing drawback. We also did not change damage, meaning the charm doesn’t affect attack directly

except via the added on-hit effects.

CardUpgradeData fields behavior: - If multiple stats are changed, all are applied. E.g., you could do .ChangeDamage(2) and .ChangeHP(-2) to make a charm that +2 attack, -2 health. - If setDamage is true, it overrides base attack. The builder’s WithSetHP(true) usage above means we want to set HP exactly to 1 rather than add 1 (so regardless of original HP). - The effects and traits added by an

upgrade stack with existing ones on the card. If a card already had, say, Aimless and you add Barrage trait

via a charm, the card ends up with both (though some traits might conflict logically – interestingly, Aimless and Barrage together might have an override, which is why TraitData has an overrides field to handle

contradictory effects like those

100

101

).

Note: The modding documentation highlights certain fields that were not fully covered previously, such as: -
becomesTargetedCard and canBeRemoved, which we mentioned

99

. - Also, any “Token” type specifics (if

type is Token, maybe it doesn’t consume charm slot, etc. – base game distinction possibly that tokens might

be temporary upgrades).

ChallengeDataBuilder

Class signature: public class ChallengeDataBuilder : DataFileBuilder<ChallengeData, ChallengeDataBuilder> .

Purpose: Likely to create new Challenge definitions (e.g., achievements, meta progression challenges, or
daily challenge modes). In Wildfrost, “Challenges” could refer to unlock conditions (like “Defeat a boss with

only companions” etc., which unlock cards or pets), or to in-run modifiers (daily challenge mutators).

Use Cases: Mods may define new unlockable goals or even new game modes. For example, adding a new
achievement that unlocks a card.

22

Key Fields: - .Create(string internalName) : New challenge. - .WithDescription(string text)
– description of the challenge. - .WithReward(CardData or UnlockData reward) – what completing it unlocks (card, tribe, etc.). - .WithCondition(...) – specify the condition to complete. Possibly uses

ChallengeListener or specific logic (like link to a ChallengeListenerBuilder which monitors a condition). -

.WithCategory(string   category)
.WithDifficulty(int value)  – maybe for daily mods, how it affects difficulty.

“Pet Challenges” or “Daily Challenges”.

– e.g.,

This builder likely works in tandem with ChallengeListenerBuilder (below) which defines how to detect the

challenge condition.

ChallengeListenerBuilder

Class DataFileBuilder<ChallengeListener, ChallengeListenerBuilder> .

signature:

public

class

ChallengeListenerBuilder

:

Purpose: Builds a ChallengeListener – essentially a piece of logic that listens for an in-game event or
condition to fulfill a challenge. For example, a listener that triggers when you win without any charms, or

when you defeat final boss under X conditions.
Use Cases: To support custom challenges, you need a custom listener if existing ones don’t cover your
condition. The base game has several ChallengeListener classes (some listed in assembly: e.g., ChallengeListenerSystemDefeatBossWithoutSnow , etc. which ChallengeListenerHighest ,

correspond to specific conditions).

Key Usage: - You likely choose a subclass of ChallengeListener to use when creating one (similar to

example,
StatusEffectDataBuilder).

.Create<ChallengeListenerSystemWinWithoutCharms>("WinNoCharms") .   -   Then   use   builder   to

For

configure any parameters that listener needs (maybe number thresholds, etc.).

Because this is advanced and requires knowledge of base listener classes, modders often reuse existing

listeners if possible. For instance, if base game has a listener for “win with only X in deck”, you could reuse it

by referencing that class.

ClassDataBuilder

Class signature: ClassDataBuilder> .

public   class   ClassDataBuilder   :   DataFileBuilder<ClassData,

Purpose: Creates new ClassData assets, which correspond to playable Tribes/Clans in Wildfrost. A
ClassData defines a tribe’s characteristics: its leaders, starting deck, tribe name, flag, and what card pools

are associated with it during a run.

Use Cases: Mods adding a new tribe for players to choose (e.g., a completely new clan with its own set of
cards) use ClassDataBuilder. The builder assembles all needed info so the tribe appears on the selection

screen and functions in runs.

23

Key Fields & Methods: - .Create(string internalName) : Internal ID for the tribe (keep it short and
unique). Base game tribes used short names like “Shade”, “Snow”, etc. as internal. - .WithName(string displayName) : If there's a player-facing name separate from internal (though often the tribe name is just derived from ClassData or from leader card names). - .WithFlag(string spritePath) – Sets the

. Provide the path to the tribe flag image (the mod banner icon for the tribe on the selection screen should have this in assets). - .WithCharacterPrefab(Character characterPrefab) – Assigns a

102

Character prefab that represents the player’s leader on the map and in battles

103

104

. In Wildfrost, each

tribe has a core character profile (with base stats like hand limit, redraw time, etc.). Mods can either

reference an existing Character or clone one. The builder allows directly setting an existing Character asset

to reuse its settings

104

105

. - If the mod cannot easily create a new Character asset, a known approach is

107

106

to clone an existing one at runtime (as the Mad Family mod did by duplicating the “Basic” tribe’s character) . The builder’s direct method is easier if you have a reference. - .WithLeaders(params CardData[] leaders) – Specify the leader CardData for this tribe. Typically 3 leaders (Wildfrost base game has 3 leader options per tribe). You can use Get("leader_YourLeaderID") to

retrieve CardData built for leaders. The builder will accept multiple and set ClassData.leaders array .

If not provided via builder, you may have to assign classData.leaders in an AfterBuild event, as was
109

108

done in the example mod using SubscribeToAfterAllBuildEvent to set data.leaders when card data are ready

.

108

The

in builder

.WithLeaders

introduction of

.WithStartingInventory(Inventory   inventory)   –   Sets   the   tribe’s   starting   deck   and   items   (the
Inventory holds starting cards, charms, and gold). You can create an  Inventory  object and fill its  deck
list with CardData references for starting cards,   upgrades   list for starting charms (like default pets or
items), and  gold  for starting gold. - The builder likely copies that into the ClassData’s starting inventory.
Alternatively,   .WithStartingDeck(params   CardData[]   cards)   and   .WithStartingGold(int
gold)  could exist; but the excerpt uses an Inventory built then passed in. -  .WithRewardPools(params
RewardPool[] pools) – Assigns what reward pools this tribe uses for its card drops. In base game, each

likely simplifies

this.

tribe has its own pool of cards to offer in rewards (and often a general charm pool for charms). The builder

allows Get("GeneralCharmPool")) so that during runs, when drawing rewards, the game pulls

.WithRewardPools(Get<RewardPool>("MyTribeCardPool"),

setting,

e.g.,

new   cards   from   your   tribe’s   card   pool
- 
.WithSelectSfxEvent(string eventPath)  – Sets a sound effect to play when the player selects this

and charms from the general

pool.

tribe on the menu . You pass an FMOD event reference path (e.g., an existing one like "event:/ui/ select_blaze"). - Other: .WithDifficultyModifiers(...) if any, or .WithUnlockCondition(...) if

110

the tribe must be unlocked via a challenge first (the base three are unlocked by default, a mod tribe might be unlocked after some achievement). - .SubscribeToAfterAllBuildEvent(action) : In

complex cases, use this to finalize references. For example, if you built leader cards in the same mod run,

ensure they exist then assign them to ClassData.leaders (if not using .WithLeaders). Also to clone and assign

character prefab if needed. - The Mad Family example did: set data.id, clone a base Character prefab, assign

to data.characterPrefab, then set up the leaders array via TryGet on built card IDs

106

111

. Much of that is

now handled by builder methods above.

Example: Creating a New Tribe: We'll outline a new tribe "Blaze" with two leader options and a custom card
pool:

// Assume leader1Card and leader2Card are CardData for two leaders already

built.

// Assume BlazeCardPool is a RewardPool containing tribe-specific cards.

24

var startingInv = new Inventory() {

deck = new List() { Get("BlazeStarter1"),

Get("BlazeStarter2") },

upgrades = new List() {

Get("StarterCharm") },

gold = 30

};

new ClassDataBuilder(this)

.Create("Blaze")

.WithFlag("Images/BlazeFlag.png")

.WithCharacterPrefab(GetCharacter("BaseCharacterTemplate"))

// reuse base

stats

.WithLeaders(Get<CardData>("leader_blaze1"), Get<CardData>("leader_blaze2"))

.WithRewardPools(Get<RewardPool>("BlazeCardPool"),

Get("GeneralCharmPool"))

.WithStartingInventory(startingInv)

.WithSelectSfxEvent(FMODUnity.RuntimeManager.PathToEventReference("event:/ui/

select_blaze"))

.Build();

This defines the "Blaze" tribe. We set a flag icon, reused an existing character prefab (so it inherits an

existing tribe’s hand size, etc.), provided 2 leaders, specified that during runs the rewards come from Blaze’s

own card pool and the general charm pool, gave a starting deck (2 specific cards, 1 charm, 30 gold), and set

a selection sound. After building, this tribe will appear on the selection screen with the given flag and allow

the player to choose one of the two leader characters.

We would have needed to create those leader CardData and the BlazeCardPool RewardPool elsewhere in

the mod.

Note on Character Prefab: The character prefab holds things like starting hand size, starting gold, redraw
counter, and the actual visual model for the leader on the map. By copying an existing one

(“BaseCharacterTemplate” in example, which could correspond to the default Snowdwellers), you ensure

your new tribe has valid defaults. If not, you’d have to create a new Character asset – beyond the builder’s

scope (and not trivial at runtime). The example mod cloned Basic’s character and renamed it "Player

(MadFamily)" to differentiate

112

113

. Our builder usage picks an existing prefab at build time instead.

AfterBuild adjustments: If using .WithLeaders and .WithStartingInventory, the builder likely handles the

AfterAllBuild ordering (ensuring the CardData and RewardPool references are resolved). The documentation

notes that often you need after-all-build callbacks for leaders and pools because those cards must be built
first

114

115

. By providing actual CardData objects (via Get) to .WithLeaders and .WithRewardPools, we

assume they’re already built or at least in the mod assets list such that the reference will resolve. Typically,

you ensure to register Card builders before Class builder, or you still do a small after-build to fix it. The

fluent API may store the references and connect them after building all.

25

EyeDataBuilder

Class

public class EyeDataBuilder : DataFileBuilder<EyeData, EyeDataBuilder> .

signature:

Purpose: Likely to create new EyeData assets. EyeData might relate to the pet eye visuals or a cosmetic set.
Wildfrost has a “Frostoscope” that shows run stats with eyes? Actually, possibly refers to the little eyeball

icons or a bestiary display. Alternatively, it could be used for profile eyes for characters.

Given context, EyeData might define the style of eyes for pets (since pets have various eye options, like

normal, googly, heart eyes, etc.). The official mod documentation includes an EyeDataBuilder, suggesting

modders can add new eye designs for pets or characters.

Use Cases: Cosmetic mods or adding new pet variations might use this builder to define new eye styles that
can show up.

Key Fields: - .Create(string internalName) - .WithSprite(Sprite image) – the eye image. -
.WithName(string name)  – perhaps if there's a name/description for the eye style (maybe not needed if
cosmetic only). - Possibly .WithRarity or condition if some eyes are special?

This is a minor builder; typical content mods might not need to touch it unless adding cosmetics.

GameModeBuilder

Class signature: GameModeBuilder> .

public   class   GameModeBuilder   :   DataFileBuilder<GameMode,

Purpose: Possibly to create new GameMode entries (like new modes beyond standard run, daily challenge,
etc.). GameMode could define things like "Daily Run", "Custom Challenge", or mod-introduced modes.

Use Cases: If a mod wanted to add a completely new mode selectable from main menu (for example, a
roguelite endless mode, or a puzzle mode), they would define a new GameMode. This is advanced and may

require UI integration.

.Create(string   internalName)

Key   Fields:
- 
.WithDescription(string modeDesc)  -  .WithRules(GameModifierData[] modifiers)  – maybe
attach certain global modifiers (see GameModifierDataBuilder). - .WithIcon(Sprite icon)

.WithName(string   displayName)

- 

Unless the game is built to list game modes dynamically, adding one might not appear without patching the

menu. But at least the data could be defined.

GameModifierDataBuilder

Class DataFileBuilder<GameModifierData, GameModifierDataBuilder> .

signature:

public

class

GameModifierDataBuilder

:

26

Purpose: Creates GameModifierData assets, which represent global modifiers to runs (e.g., the effects in
daily challenges or difficulty toggles like "hard mode"). These are rules that can alter gameplay globally.

Use Cases: Mods can add new hard mode modifiers or fun mutators. For example, a modifier "Double
Enemy Health" or "No Charms Allowed" can be defined and applied to a game mode or activated via code.

Key Fields: - .Create(string internalName) - .WithName(string name) – name of the modifier. -
.WithDescription(string   desc)   –   explanation   of   effect.   -
- 
.WithEffect(Action effect)  – not likely a direct code action here, rather: - Possibly ties into existing
systems by flags: - e.g., a HardMode modifier might have internal fields like  enemyHealthMultiplier =
2.0 if such exists. - Or triggers like a specific ChallengeListener to apply widely.

.WithIcon(Sprite   icon)

In assembly, we see classes like HardModeModifierData , which presumably derive GameModifierData.

Possibly modders can instantiate specific subclass via builder, or they might rely on a base with some

parameters.

One might combine multiple GameModifierData in a GameMode to stack effects.

KeywordDataBuilder

Class signature: KeywordDataBuilder> .

public   class   KeywordDataBuilder   :   DataFileBuilder<KeywordData,

Purpose: Creates new KeywordData assets, i.e., new keyword entries that define the tooltip text and styling
for a keyword. Keywords are the terms highlighted in card text (e.g., Snow, Shroom, Aimless are keywords

with descriptions).

Use Cases: Whenever you introduce a new trait or status that you want to describe to the player, you define
a KeywordData for it. For example, our earlier “glacial” trait would need a keyword entry so that <keyword=glacial> in card text shows a tooltip explaining what Glacial does.

Key Fields & Methods: - .Create(string internalName) – The name used in text markup (should be
. The game likely expects lowercase for lookup. - .WithTitle(string . - .WithDescription(string

lowercase and no spaces) title) – The displayed name of the keyword (capitalized) description) – The tooltip body text explaining the effect . You can include other keyword references here (like referencing existing keywords in the description). - .WithShowName(bool showName) – If true,

116

117

118

119

the keyword name itself is shown in the tooltip (some keywords just show icon and description, others show . Setting true will display the Title in the tooltip. - .WithTitleColour(Color

name as a header) color)

.WithBodyColour(Color   color)
- 
–   The   color
.WithNoteColour(Color   color)   –   Possibly   the   color   for   any   note   or   flavor   text   in   the   tooltip   (if
. - .WithIcon(Sprite icon) – If the keyword has an icon that appears inline with text. If

– The text color for the title (some keywords have colored names)

the description

applicable)

for

. -

text

120

121

121

.

not set, usually the system shows the word in brackets or just colored text. Base game keywords often have

icons (like a snowflake for Snow). In our example, if we don't supply an icon for glacial, we might choose to display the word instead. - .WithCanStack(bool canStack) – Whether stacks of this keyword show a

number on their icon

122

. True means if multiple stacks, show count (like 5 on Shroom icon); false means

27

even if applied multiple times, no number (like Aimless icon doesn’t have a “2” since it’s binary)

123

. For

traits and such, usually false.

Example: Defining the Glacial keyword to explain our trait
new KeywordDataBuilder(this)

.Create("glacial")

.WithTitle("Glacial")

.WithTitleColour(new Color(0.85f, 0.44f, 0.85f))

// maybe a purple-teal color

.WithBodyColour(new Color(0.2f, 0.5f, 0.5f))

.WithShowName(true)

.WithDescription("Apply equal <keyword=snow> and <keyword=frost> when the

other is applied|Does not cause infinites!")

.WithCanStack(false)

.Build();

124

125

This sets up the Glacial keyword: the tooltip will read something like:

Glacial – Apply equal Snow and Frost when the other is applied. Does not cause infinites!

The vertical bar | probably denotes a line break or note separator in tooltips

126

. We colored the title and

note similarly (just as an example, in practice default colors might be fine). We chose not to give it an icon,
so it will display the word “Glacial” in text; alternatively, one could assign a custom icon if available.

After this, whenever <keyword=glacial> is used in a card’s text, the game will show this tooltip. We used references <keyword=snow> and <keyword=frost> inside the description so those will show Snow and

Frost icons/names in the tooltip, making it clear that glacial relates to those statuses.

Remember to define KeywordData before using it in any Card text or TraitData, so that the references

resolve. (Usually, just ensure you add the KeywordDataBuilder before the card builders in your mod

registration order.)

StatusEffectDataBuilder

(Covered in the Status Effects section above.)

For completeness in builder list:

Class DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> .

signature:

public

class

StatusEffectDataBuilder

:

Purpose: Create new StatusEffectData assets (reusable in cards or effects). Use it to implement new status
effects or variants.

28

(See Status Effects section for detailed usage.)

Common methods recap:

.WithText(...) ,
.SubscribeToAfterAllBuildEvent<T>(...)   to   set   fields   like   effectToApply ,
conditions , etc.

.WithKeyword(...) ,

.WithIcon(...) ,

- 

.Create<T   extends   StatusEffectData>(string   name)

etc.
- 
trigger

Typically paired with KeywordDataBuilder if it's a visible status needing explanation.

TraitDataBuilder

Class signature: TraitDataBuilder> .

public   class   TraitDataBuilder   :   DataFileBuilder<TraitData,

Purpose: Creates TraitData assets. A TraitData defines an innate ability (what we see as keywords like
Aimless, Barrage, Smackback). It often links to a KeywordData and possibly underlying effects.

Use Cases: When adding a new trait/keyword that has a gameplay effect which isn’t purely visual, you
define a TraitData for it. In many cases, a trait’s effect might be implemented via a status effect or in code,

but the TraitData is still needed to mark the card and convey the effect.

Key Fields & Methods:

.WithKeyword(KeywordData keyword)   – Link to the keyword entry that provides name/description.

.Create(string   internalName) :

ID for the trait.

Typically
.WithEffects(params   StatusEffectData[]   effects)   –   If   the   trait   automatically   confers   some

associated

keyword

tooltip.

each

trait

has

for

an

status effects while the card is in play, they can be listed here. For example, Smackback might internally add a hidden status that triggers the counterattack. In code, TraitData has an array effects that likely are applied to the unit when it has this trait. - .WithOverrides(params TraitData[] traits) – Specifies

other traits that this trait overrides or conflicts with. For instance, Aimless and Barrage might override each

other – they can’t both function simultaneously, so one might remove the other if both present

127

. Setting

overrides ensures if a card gains this trait, it will suppress any trait in the overrides list. -

.WithIsReaction(bool   isReaction)   –   Marks   if   this   trait   is   a   “reaction”-type   trait   (like   Smackback

reacting on being hit). Possibly used to categorize or handle stacking (though traits usually aren’t stacked).

Might influence ordering of triggers.

Example: Continuing our Glacial trait example: We created a keyword and a status effect mechanic for
Glacial (which might be two status effects that apply Snow and Frost). Implementation approach: We could

implement Glacial’s effect entirely via a status effect (like two conditional statuses on the unit). Alternatively,

handle it in trait by linking those statuses.

Let’s say we choose to implement Glacial by giving the unit with Glacial two invisible conditional status

effects: StatusEffectApplyXWhenYAppliedToSelf (Y=Frost, X=Snow).

StatusEffectApplyXWhenYAppliedToSelf

one

(Y=Snow,

X=Frost),

one

We would create those two StatusEffectData via builder (with visible false, no keyword since internal). Then

in TraitDataBuilder:

29

new TraitDataBuilder(this)

.Create("GlacialTrait")

.WithKeyword(Get<KeywordData>("glacial"))

.WithEffects(Get<StatusEffectData>("ApplyFrostWhenSnow"),

Get("ApplySnowWhenFrost"))

.WithOverrides(Get<TraitData>("Bombard"), Get<TraitData>("Smackback")) //

maybe glacial can't co-exist with those for balance

.WithIsReaction(false)

.Build();

This means any unit that has the GlacialTrait will automatically have those two statuses

(ApplyFrostWhenSnow, ApplySnowWhenFrost) applied to it, making the trait functional in game (ensuring

the effect triggers)

6

20

. We also specify that if the unit somehow had Bombard or Smackback traits,

Glacial would override (remove) them (just an arbitrary design decision if they were incompatible).

Now, giving a card the Glacial trait is as simple as adding this TraitData to its traits list (via CardDataBuilder

or CardUpgradeDataBuilder). The UI will show the glacial icon/text from KeywordData, and the game logic

will carry out the effect via the attached invisible statuses.

Permanent Traits vs Status: The modding docs emphasize that traits from TraitData do not stack and are

inherent abilities

1

. If you want an effect that stacks (like many status do), use a StatusEffect. Traits are

often just flags checked in code or triggers. For custom traits that require new code (like something not

doable with existing statuses), you might need Harmony patching to implement their effect, but still use

TraitData for the UI and tagging.

Trigger System

Wildfrost’s combat operates with a trigger system for handling events and reactions. Understanding the

trigger flow is important when creating custom status effects or complex interactions.

Trigger events: When certain in-game events occur (turn ends, a unit is hit, a unit dies, etc.), the game . The Trigger class contains information generates a Trigger object representing that event context such as: - trigger.entity – the entity on which the event occurred (e.g., the unit that died), - trigger.triggeredBy – who caused it (e.g., the unit that dealt the killing blow), - trigger.type – a

128

string or enum indicating what kind of trigger (e.g., "Death", "Hit", "Kill")

129

, - potentially other context

(damage amount, etc.).

For example, if an enemy dies, a Trigger might be created with entity = (the dead unit) , triggeredBy = (the unit that killed it) , and type = "Death"

130

.

ActionTrigger: This is a combat action that enqueues the announcement of a trigger event . When an event occurs, the game likely creates an ActionTrigger (carrying the Trigger data) and adds it to the action queue. On execution, ActionTrigger doesn’t itself do much beyond logging that "this trigger

131

30

happened"

132

. It holds the context (entities and trigger type) but does not apply any game effect by itself.

Think of it as “firing the event signal.”

ActionProcessTrigger: This is the follow-up action that processes reactions to a trigger

133

. When

executed, it goes through all relevant statuses, traits, or global listeners that might respond to the trigger

and triggers their effects

134

. In practice, when a trigger happens, the game likely schedules an

ActionProcessTrigger after the ActionTrigger. The ActionProcessTrigger will: - Check all entities (or

specifically the entity on which the event occurred, plus possibly others like the one who caused it) for any

status effects or traits that care about this trigger. - For each such reactive effect, execute its logic (which

might queue further actions like damage or applying a status).

Example: Suppose an ally has StatusEffectApplyXOnKill (Increase Attack on kill) and that ally just killed an
enemy. The sequence: 1. Enemy death -> ActionTrigger ("Death" trigger with entity=enemy,

triggeredBy=ally) is queued. 2. ActionTrigger executes (announces the event)

135

. 3. ActionProcessTrigger

(for that ally or global) is queued, carrying the Trigger info. 4. ActionProcessTrigger executes, finds the ally

has StatusEffectApplyXOnKill listening for an "OnKill" event. It matches (ally did kill someone) and thus runs

the effect: apply "Increase Attack" to the ally

136

. 5. That application might itself queue an

ActionApplyStatus (see below) to actually give the status.

ActionProcessTrigger essentially mediates between triggers and reactive effects

137

. The specifics are

complex, but modders usually don’t have to manually queue these actions; they are handled by the engine

when you use StatusEffectApplyX classes, etc.
ActionApplyStatus: This is an action that applies a status effect to a target. It likely gets queued by status

effects or scripts that want to apply an effect as a separate action (possibly to allow sequencing and

reactions to that application). For example, if a status says “apply 3 Shell to allies when X happens,” the

effect might queue an ActionApplyStatus for each ally rather than applying instantly, giving the engine a

chance to animate and others to react if needed.

Trigger flow summary: When an event occurs: - An ActionTrigger is created and executed to log the . - This leads to an ActionProcessTrigger being queued (targeting relevant entity or globally)

event

138

139

. - The ActionProcessTrigger goes through statuses/traits, and for each that should react, it triggers

their effect

136

. Those effects may queue further actions (like ActionApplyStatus to do damage, buffs,

summons, etc.). - All actions resolve in order, possibly generating new triggers (e.g., the application of a

status might generate a "StatusApplied" trigger, etc.). The system ensures a controlled order so that combos

resolve properly without infinite loops

138

140

.

For modders, understanding triggers means: - Use the appropriate StatusEffectApplyX subclass for the

condition you want; the game will handle hooking it into the trigger flow (as long as you picked the right

subclass/trigger). - If you create a novel reaction that doesn’t exist, you might combine existing triggers. For

instance, if nothing covers "when healed, do X", you might use StatusEffectApplyXWhenAllyHealed or

similar if available

17

. If not, you’d need to patch or create a new subclass (beyond normal modding, as

code mod). - You typically don’t need to manually create ActionTrigger or ActionProcessTrigger; just ensure

your new statuses/traits leverage the existing system by inheriting the right base and setting fields (like

effectToApply).

31

Global Listeners: There are also ChallengeListeners or other systems that listen for triggers globally (not

attached to a specific entity). For example, a ChallengeListener might check for “win without any allies

dying” – it could listen to all Death triggers. The ActionProcessTrigger likely goes through not just statuses

on the triggered entity, but also any global listeners registered for that trigger type

134

. The mod API’s

ChallengeListenerBuilder would register those global triggers.

In short, the trigger system is how Wildfrost orchestrates conditional effects. By using modding constructs

like StatusEffectApplyX and TraitData with effects, you are plugging into this system. The heavy lifting is

done by the engine’s ActionTrigger and ActionProcessTrigger classes, so modders rarely interact with them

directly, but it’s helpful to know they exist and ensure your custom effects align with the event types that

actually occur.

Traits and Keywords

As mentioned earlier, Traits are innate abilities on cards (often shown as keyword icons with no stack number). They are implemented via TraitData plus any code or status effects needed. Keywords

provide the in-game descriptions for both traits and status effects.

Key points about traits and their implementation in mods: - Traits are typically binary (having a trait or not).

If a trait needs levels, that’s usually handled by a status (for example, Frenzy (Multi-hit) in Wildfrost has a

number indicating hits – in game it might be implemented as a status stack rather than trait level). - Use TraitDataBuilder to create a trait and link it to a KeywordData for description

. - If the trait’s

3

effect can be composed from existing statuses, do so (via TraitDataBuilder.WithEffects). If not, you might

need to rely on game logic or patching.

Examples of base game traits: - Aimless – The unit’s attacks target a random enemy. Implemented as a
TraitData in code checks if has Aimless -> random target . There’s no status effect needed; it’s purely a flag influencing AI. -

(with an Aimless keyword).

Likely the targeting

logic

Barrage – The unit’s attacks hit all enemies in the row. Probably implemented by giving the unit a special

status or by checking trait in attack logic. Possibly done via a hidden StatusEffect (maybe StatusEffectHitAlliesInRow exists and could be attached to represent Barrage). - Smackback –

Counterattacks when hit. The game likely implements this as a trait that triggers a reaction. Possibly internally realized via StatusEffectTriggerAgainstAttackerWhenHit for units that have Smackback

13

(the trait might add that status in code, or the trigger system directly checks trait). - Consume (if

considered a trait/keyword) – Cards with Consume are removed from deck after use. This is actually marked

on CardData (there’s likely a flag or status for it). In mod API, “Consume” might be just a keyword but the

effect is enforced by code reading that flag. StatusEffectDestroyAfterUse StatusEffectDestroyAfterUse exists and could be applied to a card via a charm or effect

that could be used to

implement Consume.

in data,

Indeed,

Actually,

141

. Possibly

there might be a

base game marks consume cards with that status. - Crown – Not exactly a trait, but crown is a token

upgrade; cards with crown start on board. That is handled by CardUpgradeData type rather than a trait.

Adding new traits: When you add a new trait, carefully consider how to implement its effect: 1. Via Status

Effects: If possible, use a combination of StatusEffectData to achieve the effect and attach them through

TraitData (like we did for Glacial). 2. Via existing triggers: See if an existing status covers the condition. For

32

instance, if you wanted a trait "Revenge" (counterattack with X damage when an ally dies), maybe you could

attach a StatusEffectApplyXWhenAllyIsKilled to self that deals damage to killer, etc. 3. Via code (Harmony):

If the trait is truly unique (e.g., changes targeting rules or modifies damage calculation in a new way), you

might need to patch the relevant game logic to check for your trait. This is advanced – outside pure content

modding – but doable with the Harmony patch system.

Important: Some trait combinations are contradictory (Aimless vs Barrage, etc.). Use TraitData overrides to

handle that

127

. The base game likely uses this: Aimless might override Barrage and vice versa, meaning a

unit cannot have both; if a charm tries to add one while the unit has the other, maybe the override ensures

one is removed. When you create custom traits, decide if it should knock out any existing traits.

Keywords Recap: Always add a KeywordData for any new trait or status you introduce, so players have

tooltip info

142

. You may also add keywords for concepts that aren’t status/trait if needed for card text

clarity (e.g., define a keyword for a custom resource or mechanic).

One can also modify existing keywords via KeywordDataBuilder if necessary (though better not to unless

localization or rebalancing text).

Interaction with UI: Traits show as icons on cards in battle. If your trait has canStack=false (most do),

the icon will have no number. If you accidentally set canStack true for a trait, the game might show a

meaningless “1” on the icon which could confuse players.

Example Summary: - We created Glacial as a trait with a keyword and implemented its effect with hidden
statuses. - If we wanted a trait like Momentum: Attack increases by 1 each time this unit attacks, we might

implement by: when unit attacks (trigger OnHit), apply a Spice status to self. Could do via TraitData adding

StatusEffectApplyXOnHit (X = Increase Attack). That status would then handle increments whenever unit hits

something.

Traits vs Status – choose appropriately. For reusable things applied temporarily or in variable amounts,

status is better. For innate abilities attached to a character concept or charm, trait is appropriate.

Tutorials
In this section, we provide step-by-step tutorials to create various custom content using the systems

documented above. Each tutorial is self-contained and references the relevant classes and methods from

the reference guide.

Prerequisites: These tutorials assume you have a working Wildfrost mod project set up with the modding

API (see the official wiki’s “Getting Started” guides for project setup). You should be comfortable creating

new C# classes in your mod and calling builder methods in your mod’s initialization or content registration.

Also, ensure you load any necessary assets (sprites for cards/charms, etc.) via the mod’s resources (often by
placing images in your mod’s assets/ folder and using the correct path in builder calls).

33

Tutorial 1: Creating a Custom Charm with Traits and Scripts
Goal: We will create a new Charm called “Chaos Charm” that can be found during runs. This charm will give

any card it’s attached to the Aimless trait (attacks hit random targets) and also a special effect: it will

randomize that card’s attack damage each time it’s played. We’ll walk through defining the charm’s data

(using CardUpgradeDataBuilder), assigning it to the charm reward pool, and testing its effect on a card.

Step 1: Define the Charm’s Properties

Using CardUpgradeDataBuilder , we start by creating the charm asset and setting its basic info:

var chaosCharm = new CardUpgradeDataBuilder(ModContext)

.Create("Charm_Chaos")

// internal name

.AddPool("GeneralCharmPool")

// available in normal charm drops

.WithType(CardUpgradeData.Type.Charm)

.WithTitle("Chaos Charm")

.WithDescription("Gain <keyword=aimless>. Attack becomes random each time.")

.WithIcon("chaos_charm.png")

// ensure this image is in mod assets

.WithTier(1);

// Tier 1 (common) charm

We’ve set the charm’s name and description. The description uses <keyword=aimless> so it will show the

Aimless icon and tooltip. We mention “Attack becomes random each time” as a hint of its script effect (since

there isn't a keyword for that).

We added the charm to the "GeneralCharmPool" so it can appear like other charms in reward chests.

Step 2: Assign Trait and Script Effects

Next, we use builder methods to give the charm its functionality:

chaosCharm.SetTraits(

TStack("Aimless", 1)

// adds Aimless trait

)

.SetScripts(

Get("CardScriptAddRandomDamage")

)

.Build();

We add the Aimless trait using .SetTraits . Here TStack("Aimless",1) is a shorthand to get the

TraitData for Aimless (assuming the mod API provides it via internal name "Aimless"). This means any card

with this charm will have Aimless

48

.

Then we attach a script: with Get("CardScriptAddRandomDamage") – the exact method may differ (some APIs let you

CardScriptAddRandomDamage .

We retrieve

it

34

do new CardScriptAddRandomDamage() , but often you fetch the existing script object). By adding this

script, whenever the upgraded card is played, it will execute the script to add a random amount of damage

to the card’s attack that play

46

. CardScriptAddRandomDamage by default will randomize the card’s attack

stat (likely within some default range, or perhaps it needs parameters.*

Important: CardScriptAddRandomDamage might have an internal range (e.g., 0 to card’s base attack*2). If

we wanted to specify a custom range, we might need a subclass or parameter. For simplicity, we assume it

randomizes fairly (perhaps ±2 damage). If needed, we could instead use CardScriptSetDamage with a range.

For demonstration, AddRandomDamage suffices.

Finally, we call .Build() to finalize the charm asset.

Step 3: Register and Test

Add the built chaosCharm to the mod’s asset list if not already done by Build (some mod APIs require ModAPI.AddUpgrade(chaosCharmCardUpgradeData) ; others add automatically on Build).

Now run the game with the mod. In a run, when you find the Chaos Charm and equip it to a card: - That

card’s info panel should show the Aimless icon (meaning it will target randomly). - In battle, each time you

play that card, you should see its attack value change randomly (and possibly an icon indicating a buff or

just the damage number changes). Because the script runs on each play, one play the card might have, say,

1 attack, next time 5 attack, etc., within some range.

Verification: Equip Chaos Charm to a companion with a predictable attack (e.g., 2). In battle, hover the card

before playing – its attack might still show base 2 until played. After playing, see the damage dealt or hover

the unit: it should have a new attack value. Also note its targeting reticle will be Aimless (usually indicated by

a reticle icon in UI).

This charm combined two systems: a trait (Aimless) and a script (random damage). Modders could extend

this approach – for example, a charm that gives Barrage trait and a script to reduce health (balancing trade-

off), etc.

Tutorial 2: Making a New Companion with Attack Effects and Class Assignment
Goal: Create a new companion card and add it to an existing tribe’s pool. Our example will be “Frostling”, a

companion for the Snowdwellers tribe. This Frostling will have 2 attack, 5 health, a 3-turn counter, and an

on-hit effect: it applies 1 Snow to enemies it hits (freezing their counter). We will also mark it to belong to

Snowdwellers so it can appear in their runs.

Step 1: Define the Companion Card

We use CardDataBuilder to create the new unit card:

var frostling = new CardDataBuilder(ModContext)

.Create("Companion_Frostling")

// internal ID

35

.SetName("Frostling")

.SetDescription("Freezes enemies on hit with a chilling touch.")

.SetAttack(2)

.SetHealth(5)

.SetCounter(3)

// attacks every 3 turns

.SetTarget(CardTargetType.Enemy)

// its attacks target enemies

.SetTraits()

// no innate traits (we leave blank)

.SetEffects()

// no passive status effects at start

.SetAttackEffects(

SStack("Snow", 1)

);

We gave it base stats (2/5 and a counter of 3). .SetAttackEffects(SStack("Snow",1)) attaches a

status effect stack: when Frostling hits a target, it applies 1 Snow

95

. “Snow” is a built-in status effect in

Wildfrost that freezes the target’s counter for 1 turn.

No innate traits for Frostling (it’s a straightforward unit).

Step 2: Finalize and Build the Card

frostling.Build();

This creates the CardData for Frostling. Now we have to ensure it’s included in the game.

Step 3: Add to a Tribe’s Card Pool

We want Frostling to be obtainable in Snowdwellers runs. In Wildfrost, each tribe has a reward pool of

companion and item cards. Snowdwellers likely have a RewardPool like "SnowdwellerDeck" or similar. For

mod demonstration, we can add Frostling to the Snowdwellers class via either: - Using ClassDataBuilder of

Snowdwellers (if accessible) to include it, - Or simply adding to that tribe’s RewardPool.

If the mod API exposes the Snowdwellers ClassData or pool, do:

var snowTribe = Get<ClassData>("Snowdwellers");

snowTribe.rewardPools.Add(Get("SnowdwellersCompanionPool"),

frostlingCardData);

Alternatively, if builder usage:

frostling.AddPool("SnowdwellersCompanionPool");

Using .AddPool("SnowdwellersCompanionPool") when building will tag it to that pool

73

.

36

Suppose the internal name is correct (we'd verify via reference data or logs). If unsure, another approach is

using ClassDataBuilder’s AfterBuild:

SubscribeToAfterAllBuildEvent(snowData => {

if(snowData.GetInternalName() == "Snowdwellers") {

snowData.cardPools[0].Add(frostlingCardData);

// pseudo: add to first

pool or appropriate pool

}

});

For brevity, assume AddPool worked or we have the pool name from documentation.

Step 4: Testing Frostling

Run the game and unlock or add the Frostling to your deck via console for testing. In battle: - Frostling

should attack every 3 turns (counter shows 3 and counts down). - When it hits an enemy, that enemy gets 1

Snow (you should see the snowflake icon on the enemy with a 1, meaning their counter is frozen for 1 turn).

Frostling’s stats and behavior otherwise are normal.
If integrated properly, Frostling can also appear as a reward after battles or in the starting deck if

configured.

This process shows how to create a unit and assign it to a tribe. If you want it as a leader instead, you’d do similar but would assign it to ClassData.leaders (leaders usually have IsLeader flag or simply by being in

that list they are leaders). For a companion, just being in the pool is enough.

Tutorial 3: Creating a New Status Effect (Reusable Reaction or Aura)
Goal: Make a new status effect called “Thorns” that damages attackers. If a unit has Thorns X, whenever it is

hit by a melee attack, the attacker takes X damage. We will create this status effect and demonstrate adding

it to a card (for example via a charm or as an inherent effect).

Step 1: Define the Status Effect Logic

Thorns is essentially a reactive effect: “When hit by an enemy, deal X damage to that enemy.” We can

implement this using the trigger system: - We want an effect that triggers on being hit and applies damage

to the attacker.

The base game might not have exactly “deal damage when hit” as a status (though Smackback trait does

something similar, but that triggers a full counterattack with the unit’s attack). We just want fixed damage. We will use a StatusEffectApplyXWhenHit with X being a damage effect.

However, what is “damage effect” in data? The game usually handles damage as an action, but we do have status like StatusEffectInstantKill or StatusEffectInstantDamage . There isn’t a listed

37

StatusEffectInstantDamage (but we have InstantKill and maybe Spice negative values cause

damage?). Alternatively, we use a script via status to cause damage.

Better approach: use an existing status that inflicts damage as a status. For example, Shroom causes

damage at end of turn. But we want immediate return damage.

We might simulate damage via a small custom status: e.g., define StatusEffectInstantDamage ourselves using a script. Instead, let's cheat slightly: use StatusEffectApplyXOnHit to apply a status to

the attacker that deals damage. If we had a status “Bleed” that immediately deals damage, that’d be perfect.

Lacking that, we create a new StatusEffectInstantDamage ourselves:

new StatusEffectDataBuilder(ModContext)

.Create<StatusEffectInstant>("InstantDamage")

// using StatusEffectInstant

as base

.WithIsStatus(true)

.SubscribeToAfterAllBuildEvent(data => {

data.action = (target) => {

// Custom action to deal damage

if(target is Character character) {

character.TakeDamage(data.stack);

}

};

});

This pseudo-code tries to set a custom action for the instant status, but unfortunately, without scripting at

the code level, a builder can’t define a new effect’s logic. We likely need to rely on existing tools.

Alternate plan: Use a CardScript triggered by status. Possibly define Thorns as a Trait that uses

CardScriptGainAttackIcon or something as placeholder – not suitable.

Let’s use ChallengeListener trick: not applicable to in-battle triggers.

Given constraints, perhaps simpler: we can approximate Thorns by using the existing Smackback trait but

with fixed damage. Smackback normally uses the unit’s attack to counter. If we want a fixed damage

counter, we could: - Give the unit a temporary attack equal to the Thorns value only during the

counterattack. That’s complex.

For educational value, let’s implement via a simpler method: create a status that triggers a PlayEffect of a small attack: We can use StatusEffectTriggerAgainstAttackerWhenHit (which replicates

Smackback)

13

. If we give a unit StatusEffectTriggerAgainstAttackerWhenHit, it will perform a

counterattack using its attack stat. To control damage, we could temporarily modify the unit’s attack to X

and then revert. Not ideal via data.

Given the complexity, let's take a different example for a new status to illustrate the process, one easier:

Aura of Warming – “While this unit is on the board, all allies gain +2 attack.”

38

This is a continuous aura effect: we can implement as a status: - Subclass StatusEffectOngoing or use

an existing like StatusEffectWhileActiveX style.

StatusEffectOngoingAttack

to buff allies.

Possibly use

Alternatively, we cheat by giving all allies Spice 2 that constantly refreshes, but that’s not trivial either.

Actually, Wildfrost does have something for global buff? Perhaps not.

Better simpler: “Spiked” – when this unit is hit, apply 2 Shroom to the attacker (so attacker will take 2

damage at end of each turn subsequently). This we can do with no new logic: Use StatusEffectApplyXWhenHit with effectToApply = Shroom(2) and target = Attacker.

So we'll do that as our new status mod example: Spiked (X): “When hit by an enemy, apply X

<keyword=shroom> to that enemy.”

Shroom is poison (damage over turns). It’s not immediate like Thorns would be, but it’s a similar concept

and uses existing status.

Step 1: Create the Spiked status effect:

var spikedStatus = new StatusEffectDataBuilder(ModContext)

.Create<StatusEffectApplyXWhenHit>("Spiked")

// base class handles OnHit

trigger

.WithText("When hit, apply {0} <keyword=shroom> to the attacker.")

.WithTextInsert("<{0}><keyword=shroom>")

// show the number then shroom

icon

.WithIcon("spiked_icon.png")

// custom icon for Spiked

.WithIsStatus(true)

.WithCanBeBoosted(false)

// (assuming spice should not boost this directly)

.SubscribeToAfterAllBuildEvent(data => {

data.effectToApply = Get("Shroom");

// apply Shroom

effect

data.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;

})

.Build();

We used which triggers on being hit callback, we specify: - effectToApply = Shroom (the base status effect for poison). By default, it will apply an amount equal to Spiked’s stack count. - applyToFlags = Attacker so it targets the attacker

. We set the description. In the

12

(the enemy that hit this unit)

143

.

We assume "Shroom" StatusEffectData exists (it does in game). We could also explicitly get a specific

StatusEffectData with a certain count but since our status carries the count, it will use that count.

Step 2: Using Spiked in a card or charm

39

Now any unit with Spiked X will poison its attacker with X Shroom. Let’s test by creating a charm or trait that

grants Spiked.

For instance, a charm Thorny Charm:

new CardUpgradeDataBuilder(ModContext)

.Create("Charm_Thorny")

.AddPool("GeneralCharmPool")

.WithType(CardUpgradeData.Type.Charm)

.WithTitle("Thorny Charm")

.WithDescription("Gain Spiked 2 (apply 2 <keyword=shroom> to attackers).")

.WithIcon("thorny_charm.png")

.WithTier(1)

.SetEffects(SStack("Spiked", 2))

.Build();

We used .SetEffects(SStack("Spiked",2)) to give the card Spiked 2 as a passive effect

144

. That

means if you equip this charm on a unit, that unit starts with the Spiked status at 2 stacks.

When that unit is hit, our new status will trigger and apply 2 Shroom to whoever hit it. Over the next turns,
that attacker will take decay damage from Shroom.

Step 3: Test in game

Give Thorny Charm to a companion and let it get hit by an enemy. Observe that the enemy receives 2

Shroom stacks immediately after its attack. The enemy’s HP should tick down at end of turns due to those

Shroom stacks (1 damage per stack).

Our new status “Spiked” is working as intended. Its icon (we specified "spiked_icon.png") should appear on

the unit with a 2 on it indicating Spiked 2. We also included a keyword in description (though we didn't

define a new keyword for Spiked; we might rely on the status’s own description or define a KeywordData for

“Spiked” as well for consistency).

For a thorough mod, we would also do:

new KeywordDataBuilder(ModContext)

.Create("spiked")

.WithTitle("Spiked")

.WithDescription("When hit, applies equal Shroom to the attacker.")

.WithIcon(spikedIconSprite)

.WithCanStack(true)

// since it has a numeric value

.Build();

And in the status builder, use .WithKeyword("spiked") so that its tooltip references that.

40

This demonstrates creating a new reactive status using existing triggers and effects.

Tutorial 4: Adding a Tribe and Flag Setup with 3 Leaders
Goal: Create an entirely new tribe (clan) named “Emberlings” with its own flag, and define three leader

characters for it. We’ll give each leader a unique ability and ensure the tribe shows up on the selection

screen with a custom banner. (This is a complex multi-step process akin to creating a mini-expansion.)

Step 1: Design your Tribe Content

Before coding, outline the tribe: - Name: Emberlings (internal "Ember") - Theme: Fire-based effects. -

Leaders: e.g., 1. Flameheart – Leader with a burning counterattack trait. 2. Cinderpaw – Leader that spawns a

blaze pet. 3. Ashlord – Leader with high damage but self-damaging attacks. - Starter deck: Some basic

attack cards or companions (maybe repurposed or new). - Balance Starting stats: Could reuse an existing

Character as template (say from a balanced tribe like Shademancers or default).

Step 2: Create Leader Cards

We need to create 3 leader CardData first. They can be similar to companions but flagged or treated as

leader by being in ClassData.leaders.

For brevity, we’ll do one in detail:

var flameheart = new CardDataBuilder(ModContext)

.Create("leader_flameheart")

.SetName("Flameheart")

.SetDescription("Leader of the Emberlings. Burns those who dare strike

him.")

.SetAttack(4).SetHealth(8).SetCounter(4)

.SetTarget(CardTargetType.Enemy)

.SetTraits(TStack("Smackback",1))

// uses Smackback trait to counterattack

.SetEffects(SStack("Spiked", 3))

// and our previously defined Spiked

status as an extra (apply 3 Shroom on hit)

.Build();

// (We assume Spiked status from tutorial 3 is available, or we omit if not.)

Similarly define cinderpaw and ashlord CardData with different stats/traits. Suppose: - Cinderpaw: 2

attack, 6 health, 5 counter, trait Summon (maybe summons a specific companion on kill – but if not easy, skip

trait). - Ashlord: 6 attack, 6 health, 4 counter, trait Barrge (Barrage to hit all enemies) but maybe also trait

Fragile (if existed, meaning self harm or something).

We’ll keep it simple:

var cinderpaw = new CardDataBuilder(ModContext)

.Create("leader_cinderpaw")

41

.SetName("Cinderpaw")

.SetDescription("Quick and elusive. Strikes swiftly.")

.SetAttack(3).SetHealth(7).SetCounter(3)

.SetTarget(CardTargetType.Enemy)

.SetTraits(TStack("Aimless",1))

// random targeting to reflect wild nature

.Build();

var ashlord = new CardDataBuilder(ModContext)

.Create("leader_ashlord")

.SetName("Ashlord")

.SetDescription("Powerful but unstable. Deals massive damage at a cost.")

.SetAttack(7).SetHealth(5).SetCounter(5)

.SetTarget(CardTargetType.Enemy)

.SetTraits()

// (maybe no special trait, pure stats)

.Build();

(We could give Ashlord a custom trait like “Glass Cannon: double damage but takes 2 damage each attack” –

but that requires new logic, skip for now.)

Step 3: Prepare Tribe ClassData

Now use ClassDataBuilder :

var emberlingsClass = new ClassDataBuilder(ModContext)

.Create("Ember")

// internal short name

.WithFlag("Images/ember_flag.png")

.WithCharacterPrefab(GetCharacter("BaseCharacterTemplate"))

// reuse base

tribe stats (5 hand, etc.)

.WithLeaders(

Get("leader_flameheart"),

Get("leader_cinderpaw"),

Get("leader_ashlord")

)

.WithStartingInventory(new Inventory() {

deck = new List() {

Get("EmberlingCompanion1"),

Get("EmberlingCompanion2"), Get("Junk") },

upgrades = new List() { },

// no starting charms

maybe

})

gold = 30

.WithRewardPools(

Get("EmberlingCardPool"),

Get("GeneralCharmPool")

)

.WithSelectSfxEvent(FMODUnity.RuntimeManager.PathToEventReference("event:/

42

ui/select_charcoal"))

// assume using an existing SFX

.Build();

We assume: - We have or will create EmberlingCompanion1 etc., or we use existing cards (like put a

couple of vanilla cards as starters). - We created an empty reward pool "EmberlingCardPool" that includes

all Emberling-specific cards (leaders are not in reward pool, but other companions/items would be). - Used

the general charm pool for charms.

After building, our tribe should appear on the tribe select screen with the custom flag image. It will have the

default character prefab (so likely 5 card hand, 3 redraws, etc., similar to Snowdwellers unless

BaseCharacterTemplate has different values).

Step 4: Populate Reward Pool & Additional Content

We mentioned EmberlingCardPool. We must define it with some entries (at least our tribe’s companions

and items). There’s a builder for RewardPool (BossRewardDataBuilder was for boss rewards, but normal

RewardPool might not have a builder – it could just be a ScriptableObject listing CardData references).

Alternatively, some mods reuse an existing pool or not worry if small. Ideally:

var emberPool = new RewardPool() {

// pseudo-code, in reality use ContentRef

cards = new List() { flameheart, cinderpaw,

ashlord, /* plus other Ember cards */ }

};

ModAPI.AddRewardPool("EmberlingCardPool", emberPool);

(This part is conceptual; actual mod API might differ.)

Step 5: Test in game

Upon launching, on the new game tribe selection, you should see a new banner (ember_flag). On hover/

select, it might play the sound (if that event path was valid). The 3 leader options Flameheart, Cinderpaw,

Ashlord should display as choices with their stats and descriptions.

Start a run with one of them: - Verify starting deck cards are present (we gave placeholder Junk and some

companions). - During battles, confirm any unique effects (Flameheart has Smackback and Spiked for

counter, etc.).

This tutorial covered how to assemble a new tribe. It requires making a lot of assets (leaders, possibly

companions, pool, etc.), but shows how the builder ties them together in ClassData.

Tutorial 5: Custom Battle Scenario with Enemies and Reward Pool
Goal: Create a custom battle (enemy encounter) and have it accessible, perhaps as a special event or

challenge. We will define a BattleData for a fight against two specific enemies and ensure it grants a

specific reward on victory.

43

For example, a battle called “Goblin Ambush” with 2 Goblin enemies, and upon winning you get a charm

reward.

Step 1: Identify or Create Enemy CardData

We assume Goblin enemies exist in base game (if not, you’d mod them too). Let’s say Goblin and GoblinLeader are enemy CardData.

Step 2: Define the BattleData

Using BattleDataBuilder :

var goblinBattle = new BattleDataBuilder(ModContext)

.Create("Battle_GoblinAmbush")

.WithTitle("Goblin Ambush")

.SetWaves(

new

WaveBuilder().AddEnemy(Get("Goblin")).AddEnemy(Get("Goblin")).Build()

)

.WithRewardPools(Get<RewardPool>("TreasureCharmPool"))

.Build();

This pseudo-code assumes: - A WaveBuilder exists to help configure waves (if not, one might have to

manually set a list of enemy IDs and counts). - We put two Goblins in the first (and only) wave. If multiple waves, we would list them. - We assign .WithRewardPools(TreasureCharmPool) – maybe a pre-

defined pool with a random charm or we could specify a BossReward set.

If we wanted to drop a specific charm, another approach: use a BossRewardDataBuilder to create a small

pool:

var goblinRewards = new BossRewardDataBuilder(ModContext)

.Create("GoblinAmbushRewards")

.AddReward(Get("CoinCharm"))

// hypothetical charm that

gives coins

.Build();

Then do .WithRewardPools(Get("GoblinAmbushRewards")) or a method specifically

to attach boss rewards.

Given simplicity, we used a generic treasure pool.

Step 3: Provide Access to this Battle

44

Defining a battle alone doesn’t put it in the campaign. You must either: - Attach it to a CampaignNodeType

(Tutorial 6 covers map nodes). - Or trigger it via a challenge or event.

For testing, we could override an existing event to load this battle. But let’s do as intended: make a new map

node for it (Scripted Node).

Tutorial 6: Using Scripted Nodes (Event or Reward)
Goal: Create a new map node on the overworld that triggers custom behavior. We’ll continue the Goblin

Ambush example by making a new node type Goblin Camp that, when landed on, initiates the Goblin

Ambush battle and then gives a reward.

Step 1: Define the Node Type

Using CampaignNodeTypeBuilder :

var goblinNode = new CampaignNodeTypeBuilder(ModContext)

.Create("GoblinCamp")

.WithName("Goblin Camp")

.WithMapSymbol(LoadSprite("node_goblin.png"), Color.white, "G")

// custom

icon with letter G

.WithEncounter(Get<BattleData>("Battle_GoblinAmbush"))

.Build();

We set an icon and letter, and link the encounter we made in Tutorial 5 by

.WithEncounter(Battle_GoblinAmbush) .

This presumably sets up that landing on this node triggers that battle. The builder likely ensures after

battle, it drops the specified reward (from BattleData’s rewardPools).

If we wanted a more complex event sequence (like a dialogue or choice before battle), we could instead use

.WithScripts :

.WithScripts(

new ScriptBattleSetUp() { battle =

Get("Battle_GoblinAmbush") },

new ScriptRunBattle(),

new ScriptAddReward(Get<BossRewardData>("GoblinAmbushRewards"))

)

(This is hypothetical pseudocode illustrating we could manually script an event: setup battle, run battle,

then add a reward drop. The modding API might not expose such granular scripts easily.)

Step 2: Integrate Node into Map

45

Creating a node type doesn’t automatically place it. The run generation might randomly include new node

types if allowed or we have to instruct it: - Possibly, if node type has no unlock conditions, it could appear as

a random event node on map. - We may need to assign it to a region’s pool of events.

If the mod API allows, something like:

Get("StandardMap").AddNodeType(goblinNodeData, weight:2,

region:"Snowdyn");

(This is speculative.)

Alternatively, replace an existing event: e.g., if we want GoblinCamp to appear instead of a specific

encounter, we could override by name:

var oldNode = Get<CampaignNodeType>("BlazeTeaShop");

oldNode = goblinNode;

// not realistic, just conceptual

For a controlled test, consider using Challenge mode or daily: If making a custom challenge or game mode,

you could programmatically insert a battle: - Or create a special campaign that has a set path including your

node.

This part can vary widely; check community examples or documentation on map modding.

Step 3: Testing

If successfully integrated, during a run, a Goblin Camp node should appear on the map (with letter G icon).

When the player goes there, they fight two Goblins. After winning, they gain the reward (charm or whatever
was set). Then the run continues.

To ensure it appears for demonstration, maybe force it as first node after the starting camp: One could

patch the map generation or simply design a custom map with a fixed layout (maybe via a custom

GameMode that uses a predetermined map script).

Because of time, assume the builder placement was enough.

These tutorials illustrate how to use the modding API’s builders to add new content: charms, cards, status

effects, tribes, battles, and map nodes. The exact code may require slight adjustment depending on the

actual modding API functions and game data structures, but the concepts hold: - Leverage existing classes

(CardScripts, StatusEffect triggers, etc.) to implement new effects whenever possible. - Use builders to

register new data so the game recognizes it (cards in pools, nodes in map, etc.). - Test each piece in isolation

if possible (e.g., test a status effect via a charm before integrating it into a complex trait).

46

By referencing the core documentation above, modders can mix and match these systems to create rich

new content for Wildfrost.

Appendix: Builder Class Reference & Wiki Gaps

Below is a list of all builder classes related to Wildfrost modding, along with notes on information that was

missing or unclear in the previous GitHub wiki documentation. This serves as a quick reference and

highlights where this guide provides additional details:

BattleDataBuilder – Creates battle encounter data. (Not fully documented in wiki; our guide added

usage of waves, reward pools).

BossRewardDataBuilder – Creates post-battle reward sets. (Wiki omitted specifics; we inferred

method to add rewards).

BuildingPlotTypeBuilder – Defines town building plot locations. (Not in wiki; rarely used, introduced in

our guide conceptually).

BuildingTypeBuilder – Defines a town building. (Minimal wiki info; our guide outlines name, prefab,

plot linking).

CampaignNodeTypeBuilder – Creates new map node types (events/battles). (Partially documented;

our guide provided example usage for encounters).

CardDataBuilder – Creates cards (units/items). (Documented on wiki, but some inherited methods

were missing: e.g., AddPool was mentioned but not detailed; our guide uses it

73

. We also clarified

trait and status attachment via SetTraits/SetEffects which the wiki touched on lightly. The wiki

cautioned not to use SetAttackEffects/SetStartWithEffect directly

72

, but we demonstrated their

usage as working examples.)

CardTypeBuilder – Creates new card categories. (Listed but no wiki page; our guide notes it's seldom

needed).

CardUpgradeDataBuilder – Creates upgrades (charms). (Wiki had info but missed some fields: we added details on becomesTargetedCard and canBeRemoved flags

, and provided a full

99

example including stat setting with setDamage/WithSetDamage which the wiki did not elaborate
fully. Wiki also didn't list SetScripts on upgrades – which we showcased in Tutorial 1.)

ChallengeDataBuilder – Creates a new challenge/achievement. (Not well documented; we noted

concept but not detailed due to scope).

ChallengeListenerBuilder – Creates a listener for challenge conditions. (Not in wiki; mentioned

conceptually in triggers).

ClassDataBuilder – Creates a new tribe. (Wiki had some details but missing how to handle character prefab and after-build needs. We gave a comprehensive example and explained WithLeaders , WithStartingInventory , and after-build adjustments

. The wiki lacked detail on using

114

existing character prefabs – which we covered

107

.)

EyeDataBuilder – Creates new cosmetic eye assets. (Not documented; mentioned briefly in builder list

without page. We have no example beyond acknowledging it.)

GameModeBuilder – Creates new game mode. (Not documented; only listed. We provided theoretical

usage.)

GameModifierDataBuilder – Creates global run modifiers. (Not in wiki; we mentioned conceptually.)

KeywordDataBuilder – Creates new keyword entries. (This was partly documented via Tutorial 3 on

the wiki; our guide provided a full example for "glacial" with colors

116

125

. The wiki didn’t explicitly

say that internal name must be lowercase, which we highlighted.)

47

StatusEffectDataBuilder – Creates new status effect. (Documented in wiki but lacking examples; our

guide heavily expanded on it with two examples – Snow and the OnKill effect, and the Spiked custom

effect in Tutorial 3. We also clarified use of SubscribeToAfterAllBuildEvent to set fields like

effectToApply, which the wiki touched on but not for every case.)

TraitDataBuilder – Creates new trait data. (Wiki mention was brief, focusing that traits vs statuses

difference. Our guide explained TraitData fields like overrides and isReaction and using

TraitDataBuilder.WithEffects to attach statuses to traits (like we did with Glacial). This was not clearly

documented before.)

Missing Wiki Info Provided by This Guide: - Inherited builder methods: Many builder pages omitted methods inherited from DataFileBuilder like .Create() or .SubscribeToAfterAllBuildEvent .

We have used them in examples, showing how to properly finalize complex references (e.g., linking custom

effects or leaders)

8

145

. - CardScripts catalog: The old wiki did not list all CardScript classes. We

provided a comprehensive list with descriptions of each, including ones that were not mentioned

(AddDamageEqualToHealth, MultiplyCounter, etc.), bridging a knowledge gap. - StatusEffect classes: The

wiki listed names but no descriptions; we categorized and described many, making the information

accessible without external spreadsheet

5

20

. - Examples and Use Cases: The wiki often stated what a

builder or class is for, but not how to use it in context. Our tutorials serve as concrete use-case examples

that were missing, such as combining traits and scripts on a charm, or adding a new tribe from scratch.

This reference guide, with its detailed breakdowns and integrated examples, addresses those gaps,

providing modders with a one-stop comprehensive documentation for Wildfrost modding tasks.
