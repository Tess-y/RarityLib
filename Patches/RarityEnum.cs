using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassesManagerReborn.Patchs
{
    [Serializable]
    [HarmonyPatch(typeof(Enum), "GetValues")]
    internal class RarityEnumValues
    {
        private static void Postfix(Type enumType, ref Array __result)
        {
            if(enumType == typeof(CardInfo.Rarity))
            {
                __result = RarityLib.Utils.RarityUtils.rarities.Values.Select(v => v.value).ToArray();
            }
        }
    }   
    [Serializable]
    [HarmonyPatch(typeof(Enum), "CompareTo")]
    internal class RarityEnumCompair
    {
        private static bool Prefix(Enum __instance, object target, ref int __result)
        {
            if(__instance is CardInfo.Rarity rarity1 && target is CardInfo.Rarity rarity2)
            {
                float r1 = RarityLib.Utils.RarityUtils.GetRarityData(rarity1).relativeRarity;
                float r2 = RarityLib.Utils.RarityUtils.GetRarityData(rarity2).relativeRarity;
                if(r1 == r2) {
                    __result = 0;
                }else if(r1 > r2) {
                    __result = -1;
                }else { 
                    __result = 1; 
                }
                return false;
            }
            return true;
        }
    }
    [Serializable]
    [HarmonyPatch(typeof(Enum), "GetNames")]
    internal class RarityEnumNames
    {
        private static void Postfix(Type enumType, ref string[] __result)
        {
            if(enumType == typeof(CardInfo.Rarity))
            {
                __result = RarityLib.Utils.RarityUtils.rarities.Values.Select(r => r.name).ToArray();
            }
        }
    }
    [Serializable]
    [HarmonyPatch(typeof(Enum), "ToString", new Type[] { })]
    internal class RarityEnumToString
    {
        private static void Postfix(Enum __instance, ref string __result)
        {
            if(__instance.GetType() == typeof(CardInfo.Rarity))
            {
                try
                {
                    __result = Enum.GetNames(typeof(CardInfo.Rarity))[(int)(CardInfo.Rarity)__instance];
                }
                catch { }
            }
        }
    }
}
