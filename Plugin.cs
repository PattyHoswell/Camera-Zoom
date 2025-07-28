using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Patty_CameraZoom_MOD
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string TARGET_SCENE = "Main";
        internal static ManualLogSource LogSource { get; private set; }

        internal static Harmony PluginHarmony { get; private set; }

        public static ConfigEntry<float> CameraZoom;
        void Awake()
        {
            LogSource = Logger;
            try
            {
                PluginHarmony = Harmony.CreateAndPatchAll(typeof(PatchList), PluginInfo.GUID);
            }
            catch (HarmonyException ex)
            {
                LogSource.LogError((ex.InnerException ?? ex).Message);
            }
            CameraZoom = Config.Bind("Camera", "Zoom", 0f, new ConfigDescription("Change the camera zoom in battle"));
            CameraZoom.SettingChanged += CameraZoom_SettingChanged;
        }

        private void CameraZoom_SettingChanged(object sender, System.EventArgs e)
        {
            var currentScene = SceneManager.GetActiveScene();
            if (currentScene.IsValid() && currentScene.name.ToUpperInvariant().Contains(TARGET_SCENE.ToUpperInvariant()))
            {
                ChangeZoom();
            }
        }

        public static void ChangeZoom()
        {
            var mainCamera = Camera.main;
            var mainCameraTransform = mainCamera.transform;
            mainCameraTransform.localPosition = new Vector3(mainCameraTransform.localPosition.x, mainCameraTransform.localPosition.y, CameraZoom.Value);
        }
    }
}
