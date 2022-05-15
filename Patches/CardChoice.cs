﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RarityLib.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(CardChoice), "GetRanomCard")]
    internal class CardChoicePatchGetRanomCard
    {
        [HarmonyPriority(Priority.First)]
        private static bool Prefix(CardChoice __instance, ref GameObject __result)
        {
            float num = 0f;
            for (int i = 0; i < __instance.cards.Length; i++)
            {
                num += RarityLib.Utils.RarityUtils.rarities[(int)__instance.cards[i].rarity].relativeRarity;
            }
            float num2 = UnityEngine.Random.Range(0f, num);

			for (int j = 0; j < __instance.cards.Length; j++)
            {
                num2 -= RarityLib.Utils.RarityUtils.rarities[(int)__instance.cards[j].rarity].relativeRarity;
                if (num2 <= 0f)
				{
                    __result = __instance.cards[j].gameObject;
					break;
				}
			}
            return false;
        }
    }
}
