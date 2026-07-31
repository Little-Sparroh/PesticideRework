using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace PesticideRework;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
[MycoMod(null, ModFlags.IsSandbox)]
public class PesticideReworkPlugin : BaseUnityPlugin
{
    public const string PluginGUID = "sparroh.pesticiderework";
    public const string PluginName = "PesticideRework";
    public const string PluginVersion = "1.0.1";

    internal new static ManualLogSource Logger;

    private Harmony harmony;

    private void Awake()
    {
        Logger = base.Logger;

        ConfigManager.Initialize(Config, Logger);

        harmony = new Harmony(PluginGUID);
        harmony.PatchAll(typeof(PesticideReworkPatches));

        Logger.LogInfo($"{PluginName} v{PluginVersion} loaded successfully.");
    }

    private void Update()
    {
        ConfigManager.Tick();
    }

    private void OnDestroy()
    {
        ConfigManager.Dispose();
        harmony?.UnpatchSelf();
    }
}