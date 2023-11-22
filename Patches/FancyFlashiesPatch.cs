using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace FancyFlashies.Patches
{
    [HarmonyPatch("Update")]
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class FancyFlashiesPatch
    {
        static Color[] colorOrder = {
            new Color(1f, 0, 0, 1f),
            new Color(1f, 1f, 0, 1f),
            new Color(0, 1f, 1f, 1f),
            new Color(0, 1f, 0f, 1f),
            new Color(0, 1f, 1f, 1f),
            new Color(0, 0f, 1f, 1f),
            new Color(1f, 0f, 1f, 1f)
        };
        static int targetColorIndex = 0;

        [HarmonyPostfix]
        static void FlashlightColourPatch(ref float ___sprintMeter)
        {
            FlashlightItem[] flashlights = UnityEngine.Object.FindObjectsByType<FlashlightItem>(FindObjectsSortMode.None);
            PlayerControllerB[] players = UnityEngine.Object.FindObjectsByType<PlayerControllerB>(FindObjectsSortMode.None);

            if (Time.frameCount % 3 == 0)
            {
                foreach (FlashlightItem fl in flashlights)
                {
                    fl.flashlightBulb.color = Color.Lerp(fl.flashlightBulb.color, colorOrder[targetColorIndex], 0.25f * Time.deltaTime);
                    fl.flashlightBulbGlow.color = Color.Lerp(fl.flashlightBulbGlow.color, colorOrder[targetColorIndex], 0.25f * Time.deltaTime);
                }

                foreach (PlayerControllerB player in players)
                {
                    player.helmetLight.color = Color.Lerp(player.helmetLight.color, colorOrder[targetColorIndex], Time.deltaTime);
                }
            }
            if (Time.frameCount % 300 == 0)
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
}
