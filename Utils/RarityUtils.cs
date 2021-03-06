using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RarityLib.Utils
{
    public class RarityUtils
    {
        internal static Dictionary<int, Rarity> rarities = new Dictionary<int, Rarity>();
        internal static Dictionary<CardInfo, float> CardRarities = new Dictionary<CardInfo, float>();
        public static IReadOnlyDictionary<int, Rarity> Rarities { get { return rarities; } }
        public static int AddRarity(string name, float relativeRarity, Color color, Color colorOff)
        {
            int i = rarities.Count;
            if (rarities.Values.Any(r => r.name == name))
            {
                UnityEngine.Debug.LogWarning($"Rarity with name {name} already exists");
                return rarities.Keys.Where(i => rarities[i].name == name).First();
            }
            rarities.Add(i, new Rarity(name, relativeRarity, color, colorOff, (CardInfo.Rarity)i));
            return i;
        }

        public static CardInfo.Rarity GetRarity(string rarityName)
        {
            Rarity rarity = rarities.Values.ToList().Find(r => r.name == rarityName);
            if(rarity == null) return (CardInfo.Rarity)0;
            return (CardInfo.Rarity)rarities.Keys.ToList().Find(i => rarities[i] == rarity);
        }

        public static Rarity GetRarityData(CardInfo.Rarity rarity)
        {
            return rarities[(int)rarity];
        }

        public static float GetCardRarityModifier(CardInfo card)
        {
            if (!CardRarities.ContainsKey(card))
            {
                CardRarities[card] = 1;
            }
            return CardRarities[card];
        }
        public static void SetCardRarityModifier(CardInfo card, float modifier)
        {
                CardRarities[card] = modifier;
        }

        internal static IEnumerator Reset()
        {
            CardRarities.Clear();
            yield break;
        }

    }

    public class Rarity
    {
        public string name;
        public float relativeRarity;
        public float calculatedRarity;
        public Color color;
        public Color colorOff;
        public CardInfo.Rarity value;

        internal Rarity(string name, float relativeRarity, Color color, Color colorOff, CardInfo.Rarity value)
        {
            this.name = name;
            this.relativeRarity = relativeRarity;
            this.calculatedRarity = relativeRarity;
            this.color = color;
            this.colorOff = colorOff;
            this.value = value;
        }
        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(Rarity))
            {
                return ((Rarity)obj).name == this.name;
            }
            return false;
        }
    }
}
