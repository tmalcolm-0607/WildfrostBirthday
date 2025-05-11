# Stats

| Name                   | Corresponding Stat       | Key (Sub-stat)   | Text                        | Type      |   Par |   Priority |   PriorityAddOverPar |   PrioritySubUnderPar |
|:-----------------------|:-------------------------|:-----------------|:----------------------------|:----------|------:|-----------:|---------------------:|----------------------:|
| StatApplyBlock         | statusesApplied          | block            | Block Applied: {0}          | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatApplyBom           | statusesApplied          | vim              | Bom Applied: {0}            | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatApplyDemonize      | statusesApplied          | demonize         | Demonize Applied: {0}       | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatApplyFrost         | statusesApplied          | frost            | Frost Applied: {0}          | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatApplyHaze          | statusesApplied          | haze             | Haze Applied: {0}           | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatApplyInk           | statusesApplied          | ink              | Ink Applied: {0}            | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatApplyOverload      | statusesApplied          | overload         | Overburn Applied: {0}       | Normal    |    10 |          0 |                 0.1  |                   0   |
| StatApplyScrap         | statusesApplied          | scrap            | Scrap Added: {0}            | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatApplyShell         | statusesApplied          | shell            | Shell Applied: {0}          | Normal    |    10 |          0 |                 0.1  |                   0   |
| StatApplyShroom        | statusesApplied          | shroom           | Shroom Applied: {0}         | Normal    |    10 |          0 |                 0.1  |                   0   |
| StatApplySnow          | statusesApplied          | snow             | Snow Applied: {0}           | Normal    |    10 |          0 |                 0.1  |                   0   |
| StatApplySpice         | statusesApplied          | spice            | Spice Applied: {0}          | Normal    |    10 |          0 |                 0.1  |                   0   |
| StatApplyTeeth         | statusesApplied          | teeth            | Teeth Applied: {0}          | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatBattlesWon         | battlesWon               | nan              | Battles Won: {0}            | Count     |     1 |          0 |                 0    |                   0   |
| StatBestCardRename     | bestRename               | nan              | Best Rename: {0}            | RandomKey |     1 |          1 |                 1    |                   0   |
| StatBestCombo          | highestKillCombo         | nan              | Highest Kill Combo: {0}     | Best      |     2 |          0 |                 1    |                   0   |
| StatBestDamage         | highestDamageDealt       | nan              | Highest Damage Hit: {0}     | BestAny   |    10 |          0 |                 0.5  |                   0.1 |
| StatBestFrenzy         | highestStatusEffect      | frenzy           | Highest Frenzy Reached: {0} | Best      |     2 |          0 |                 1    |                   0   |
| StatBestSpice          | highestStatusEffect      | spice            | Highest Spice Reached: {0}  | Best      |     8 |          0 |                 0.5  |                   0.1 |
| StatBoughtDiscounts    | discountsBought          | nan              | Discounts Bought: {0}       | Normal    |     2 |          0 |                 1    |                   0   |
| StatCardsMunched       | cardsMunched             | nan              | Cards Munched: {0}          | Count     |     2 |          0 |                 1    |                   0   |
| StatCardsSummoned      | cardsSummoned            | nan              | Total Summons: {0}          | Count     |     8 |          0 |                 0.1  |                   0   |
| StatCharmsGained       | charmsGained             | nan              | Charms Gained: {0}          | Count     |     2 |          0 |                 1    |                   0   |
| StatCompanionsRecalled | friendliesRecalled       | nan              | Companions Recalled: {0}    | Count     |     5 |          0 |                 0.5  |                   0   |
| StatCrownsGained       | crownsGained             | nan              | Crowns Gained: {0}          | Count     |     1 |          0 |                 1    |                   0   |
| StatDamageBlocked      | damageBlocked            | nan              | Damage Blocked: {0}         | Count     |    10 |          0 |                 0.1  |                   0.1 |
| StatDamageDealt        | damageDealt              | nan              | Damage Dealt: {0}           | Count     |    10 |          0 |                 0.05 |                   0   |
| StatDamageFriendly     | friendlyDamageDealt      | nan              | Friendly Damage: {0}        | Count     |    10 |          0 |                 0.1  |                   0.1 |
| StatDamageTaken        | damageTaken              | nan              | Damage Taken: {0}           | Count     |     1 |          0 |                 0    |                   0   |
| StatGoblingEscaped     | enemiesEscaped           | Gobling          | Goblings Escaped: {0}       | Normal    |     1 |          0 |                 1    |                   0   |
| StatGoblingHits        | cardsHit                 | Gobling          | Gobling Hits: {0}           | Normal    |     1 |          0 |                 0.1  |                   0   |
| StatGoldFromCombos     | goldGained               | Combo            | Blings From Combos: {0}     | Normal    |    10 |          0 |                 0.1  |                   0   |
| StatGoldFromGoblings   | goldGained               | Gobling          | Blings From Goblings: {0}   | Normal    |    10 |          0 |                 0.1  |                   0   |
| StatGoldGained         | goldGained               | nan              | Blings Gained: {0}          | Count     |     1 |          0 |                 0    |                   0   |
| StatGoldSpent          | goldSpent                | nan              | Blings Spent: {0}           | Normal    |     1 |          0 |                 0    |                   0   |
| StatHealthHealed       | amountHealedFrom         | nan              | Amount Healed: {0}          | Count     |     1 |          0 |                 0.05 |                   0   |
| StatInjuries           | friendliesInjured        | nan              | Injuries: {0}               | Count     |     1 |          0 |                 1    |                   0   |
| StatKillBosses         | bossesKilled             | nan              | Bosses Defeated: {0}        | Count     |     1 |          0 |                 0    |                   0   |
| StatKillEnemies        | enemiesKilled            | nan              | Enemies Killed: {0}         | Count     |     1 |          0 |                 0    |                   0   |
| StatKillFriendlies     | friendliesDied           | nan              | Friendly Kills: {0}         | Count     |     1 |          0 |                 0    |                   0   |
| StatKillsWithOverload  | enemiesKilledDamageType  | overload         | Overburn Kills: {0}         | Normal    |     5 |          0 |                 0.5  |                   0   |
| StatKillsWithShroom    | enemiesKilledDamageType  | shroom           | Shroom Kills: {0}           | Normal    |     5 |          0 |                 0.5  |                   0   |
| StatKillsWithSmackback | enemiesKilledTriggerType | smackback        | Smackback Kills: {0}        | Normal    |     5 |          0 |                 0.5  |                   0   |
| StatKillsWithTeeth     | enemiesKilledDamageType  | spikes           | Teeth Kills: {0}            | Normal    |     5 |          0 |                 0.5  |                   0   |
| StatNakedGnomesKilled  | enemiesKilled            | NakedGnome       | Naked Gnomes Killed: {0}    | Normal    |     1 |          1 |                 1    |                   0   |
| StatNakedGnomesSpared  | enemiesEscaped           | NakedGnome       | Naked Gnomes Spared: {0}    | Normal    |     1 |         50 |                 1    |                   0   |
| StatRedrawBellHits     | redrawBellHits           | nan              | Redraw Bell Rings: {0}      | Normal    |     1 |          0 |                 0    |                   0   |
| StatRenames            | renames                  | nan              | Cards Renamed: {0}          | Count     |     1 |          0 |                 0    |                   0   |
| StatSmackback          | totalTriggers            | smackback        | Total Smackbacks: {0}       | Normal    |     5 |          0 |                 0.25 |                   0   |
| StatTime               | nan                      | nan              | Time Played: {0}            | Time      |     1 |          0 |                 0    |                   0   |
| StatTurns              | turnsTaken               | nan              | Turns Taken: {0}            | Normal    |     1 |          0 |                 0    |                   0   |