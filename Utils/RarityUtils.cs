using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RarityLib.Utils
{
    public class RarityUtils
    {
        internal static Dictionary<int, Rarity> rarities = new Dictionary<int, Rarity>();
        public static int AddRarity(string name, float relitiveRarity, Color color, Color colorOff)
        {
            int i = rarities.Count;
            if (rarities.Values.Any(r => r.name == name))
                throw new ArgumentException($"Rarity with name {name} already exists");
            rarities.Add(i, new Rarity(name, relitiveRarity, color, colorOff));
            if (Main.deckCustomization && i > 2) DeckCustomizationCompat.RegesterRarity(rarities[i], i);
            return i;
        }

        public static CardInfo.Rarity GetRarity(string rarityName)
        {
            Rarity rarity = rarities.Values.ToList().Find(r => r.name == rarityName);
            if(rarity == null) return (CardInfo.Rarity)0;
            return (CardInfo.Rarity)rarities.Keys.ToList().Find(i => rarities[i] == rarity);
        }
    }

    public class Rarity
    {
        public string name;
        public float relitiveRarity;
        public Color color;
        public Color colorOff;

        public Rarity(string name, float relitiveRarity, Color color, Color colorOff)
        {
            this.name = name;
            this.relitiveRarity = relitiveRarity;
            this.color = color;
            this.colorOff = colorOff;
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
