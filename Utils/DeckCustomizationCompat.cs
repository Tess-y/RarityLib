using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RarityLib.Utils
{
    internal class DeckCustomizationCompat
    {
		public static void RegesterRarity(Rarity rarity, int i)
        {
			Type deckCust = GetType("DeckCustomization");
			((Dictionary<CardInfo.Rarity, float>)deckCust.GetField("RarityRarities", BindingFlags.Static|BindingFlags.NonPublic).GetValue(null))[(CardInfo.Rarity)i] = rarity.relativeRarity;

		}
		internal static Type GetType(string typeName)
		{
			string assemblyName = "DeckCustomization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			Assembly assembly = null;
			foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (a.FullName == assemblyName)
				{
					assembly = a;
					break;
				}
			}
			if (assembly == null) return null;
			Type[] types = assembly.GetTypes();
			foreach (Type t in types)
			{
				if (t.Name == typeName) return t;
			}
			return null;
		}
	}
}
