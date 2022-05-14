using BepInEx;
using HarmonyLib;
using RarityLib.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RarityLib
{
    // These are the mods required for our Mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.deckcustomization", BepInDependency.DependencyFlags.SoftDependency)]
    // Declares our Mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our Mod Is associated with
    [BepInProcess("Rounds.exe")]
    public class Main : BaseUnityPlugin
    {
        private const string ModId = "root.rarity.lib";
        private const string ModName = "Rarity Extention Library";
        public const string Version = "1.0.0";
        internal static bool deckCustomization;
        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            var plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            deckCustomization = plugins.Exists(plugin => plugin.Info.Metadata.GUID == "pykess.rounds.plugins.deckcustomization");
            RarityUtils.AddRarity("Common", 1, new Color(0.0978f, 0.1088f, 0.1321f), new Color(0.0978f, 0.1088f, 0.1321f));
            RarityUtils.AddRarity("Unommon", 0.4f, new Color(0.1745f, 0.6782f, 1f), new Color(0.1934f, 0.3915f, 0.5189f));
            RarityUtils.AddRarity("Rare", 0.1f, new Color(1f, 0.1765f, 0.7567f), new Color(0.5283f, 0.1969f, 0.4321f));
            RarityUtils.AddRarity("Legendary", 0.025f, new Color(1, 1, 0), new Color(0.7f, 0.7f, 0));
        }

        void Start()
        {
            //UnboundLib.Cards.CustomCard.BuildCard<testcard>();
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
