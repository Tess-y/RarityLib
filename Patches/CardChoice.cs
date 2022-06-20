using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RarityLib.Utils;

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
                num += RarityUtils.rarities[(int)__instance.cards[i].rarity].calculatedRarity * RarityUtils.GetCardRarityModifier(__instance.cards[i]);
            }
            float num2 = UnityEngine.Random.Range(0f, num);

			for (int j = 0; j < __instance.cards.Length; j++)
            {
                num2 -= RarityUtils.rarities[(int)__instance.cards[j].rarity].calculatedRarity * RarityUtils.GetCardRarityModifier(__instance.cards[j]);
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
