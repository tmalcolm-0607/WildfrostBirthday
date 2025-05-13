using UnityEngine;

namespace WildfrostBirthday.Tribes
{
    public static class Tribe_MadFamily
    {
        public static void Register(WildFamilyMod mod)
        {
            mod.assets.Add(
                mod.TribeCopy("Basic", "MadFamily")
                    .WithFlag("Images/tribe_madfamily.png")
                    .WithSelectSfxEvent(FMODUnity.RuntimeManager.PathToEventReference("event:/sfx/card/draw_multi"))
                    .SubscribeToAfterAllBuildEvent((data) =>
                    {
                        GameObject gameObject = data.characterPrefab.gameObject.InstantiateKeepName();
                        UnityEngine.Object.DontDestroyOnLoad(gameObject);
                        gameObject.name = "Player (MadFamily)";
                        data.characterPrefab = gameObject.GetComponent<Character>();
                        data.id = "MadFamily";
                        data.leaders = mod.DataList<CardData>("leader-alison", "leader-tony", "leader-cassie", "leader-caleb", "leader-kaylee");
                        Inventory inventory = ScriptableObject.CreateInstance<Inventory>();
                        inventory.deck.list = mod.DataList<CardData>("SnowGlobe", "Sword", "Gearhammer", "Sword", "Gearhammer", "SunlightDrum", "Junkhead", "companion-lulu", "companion-poppy").ToList();
                        inventory.upgrades.Add(mod.TryGet<CardUpgradeData>("charm-book_charm"));
                        data.startingInventory = inventory;
                        // Additional reward pool logic can be modularized here if needed
                    })
            );
        }
    }
}
