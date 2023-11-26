using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace FancyFlashies.Patches
{
    public static class ColorManager
    {
        public static Color[] colorOrder = {
            new Color(1f, 0, 0, 1f),
            new Color(1f, 1f, 0, 1f),
            new Color(0, 1f, 1f, 1f),
            new Color(0, 1f, 0f, 1f),
            new Color(0, 1f, 1f, 1f),
            new Color(0, 0f, 1f, 1f),
            new Color(1f, 0f, 1f, 1f)
        };

        public static int targetColorIndex = 0;

        public static void ColorTick(int frame)
        {
            if (frame % 300 == 0)
            {
                if (targetColorIndex == colorOrder.Length - 1)
                {
                    targetColorIndex = 0;
                }
                else
                {
                    targetColorIndex++;
                }
            }
        }
    }

    [HarmonyPatch(typeof(FlashlightItem))]
    public class FancyFlashlightPatch { 
    
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void FlashlightColourPatch(FlashlightItem __instance)
        {
            if (Time.frameCount % 3 == 0)
            {
                __instance.flashlightBulb.color = Color.Lerp(__instance.flashlightBulb.color, ColorManager.colorOrder[ColorManager.targetColorIndex], 0.75f * Time.fixedDeltaTime);
                __instance.flashlightBulbGlow.color = Color.Lerp(__instance.flashlightBulbGlow.color, ColorManager.colorOrder[ColorManager.targetColorIndex], 0.75f * Time.fixedDeltaTime);                
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControllerB))]
    public class FancyPlayerControllerBPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void HelmlightColourPatch(PlayerControllerB __instance)
        {
            if (Time.frameCount % 3 == 0)
            {
                __instance.helmetLight.color = Color.Lerp(__instance.helmetLight.color, ColorManager.colorOrder[ColorManager.targetColorIndex], 0.75f * Time.deltaTime);
            }

            ColorManager.ColorTick(Time.frameCount);
        }
    }
}
