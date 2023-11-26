using BepInEx;
using FancyFlashies.Patches;
using HarmonyLib;

namespace FancyFlashies
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class FancyFlashiesBase : BaseUnityPlugin
    {
        private const string modGUID = "Asylud.FancyFlashies";
        private const string modName = "Fancy Flashies";
        private const string modVersion = "1.0.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static FancyFlashiesBase Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            harmony.PatchAll(typeof(FancyFlashiesBase));
            harmony.PatchAll(typeof(FancyFlashlightPatch));
            harmony.PatchAll(typeof(FancyPlayerControllerBPatch));
        }
    }
}
