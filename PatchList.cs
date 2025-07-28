using HarmonyLib;
using ShinyShoe.Loading;

namespace Patty_CameraZoom_MOD
{
    internal class PatchList
    {
        [HarmonyPostfix, HarmonyPatch(typeof(LoadScreen), "StartLoadingScreen")]
        public static void StartLoadingScreen(LoadScreen __instance, ref ScreenManager.ScreenActiveCallback ___screenActiveCallback)
        {
            if (__instance.name == ScreenName.Game)
            {
                ___screenActiveCallback += (IScreen _) =>
                {
                    Plugin.ChangeZoom();
                };
            }
        }
    }
}
