using BepInEx;
using HarmonyLib;
using RarityLib.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnboundLib.GameModes;
using UnityEngine;

namespace RarityLib
{
    // These are the mods required for our Mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our Mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our Mod Is associated with
    [BepInProcess("Rounds.exe")]
    public class Main : BaseUnityPlugin
    {
        private const string ModId = "root.rarity.lib";
        private const string ModName = "Rarity Extention Library";
        public const string Version = "1.2.6";
        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            RarityUtils.Started = true;
            RarityUtils.AddRarity("Legendary", 0.025f, new Color(1, 1, 0), new Color(0.7f, 0.7f, 0));
        }

        void Start() {
            RarityUtils.FinalizeRaritys();
            //UnboundLib.Cards.CustomCard.BuildCard<testcard>();
            GameModeManager.AddHook(GameModeHooks.HookGameStart, gm => RarityUtils.Reset(), GameModeHooks.Priority.First);
            RarityAdder[] array = (RarityAdder[])Resources.FindObjectsOfTypeAll(typeof(RarityAdder));
            foreach(RarityAdder rarityAdder in array) {
                bool enabled = rarityAdder.enabled;
                if(enabled) {
                    rarityAdder.SetUp();
                }
            }
        }
    }

    public class testcard : UnboundLib.Cards.CustomCard
    {
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return;
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override string GetDescription()
        {
            return"";
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Legendary");
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[0];
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }

        protected override string GetTitle()
        {
            return "RARITY TEST CARD";
        }
    }
}
