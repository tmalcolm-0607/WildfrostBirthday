// MadFamily Tribe Mod - Wildfrost
using Dead;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;
using static CardData;
using WildfrostBirthday.Helpers;


namespace WildfrostBirthday
{
    public class WildFamilyMod : WildfrostMod
    {
        public WildFamilyMod(string modDirectory) : base(modDirectory) => Instance = this;
        public static WildFamilyMod? Instance;

        public override string GUID => "madfamilymod.wildfrost.madhouse";
        public override string[] Depends => new string[] { };
        public override string Title => "MadHouse Family Tribe";
        public override string Description => "Mod made by the MadHouse Family for us to play with our family in game. Made for our Mother who is the greatest of all time";

        private bool preLoaded = false;
        public List<object> assets = new List<object>();        public override void Load()
        {
            if (!preLoaded)
            {
                // Use the automated registration system
                this.RegisterAllComponents();
                
                preLoaded = true;
            }

            base.Load();

            Events.OnEntityCreated += (entity) => this.FixImage(entity);
            //GameMode gameMode = this.TryGet<GameMode>("GameModeNormal"); //GameModeNormal is the standard game mode. 
            //gameMode.classes = gameMode.classes.Append(this.TryGet<ClassData>("MadFamily")).ToArray();
        }
        public override void Unload()
        {
            base.Unload();
            GameMode gameMode = this.TryGet<GameMode>("GameModeNormal");
            gameMode.classes = this.RemoveNulls(gameMode.classes); //Without this, a non-restarted game would crash on tribe selection            

            this.UnloadFromClasses();
            
        } 
    }
}



