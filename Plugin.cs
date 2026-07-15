using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
[MycoMod(null, ModFlags.IsSandbox)]
public class SparrohPlugin : BaseUnityPlugin
{
    public const string PluginGUID = "sparroh.pesticiderework";
    public const string PluginName = "PesticideRework";
    public const string PluginVersion = "1.0.1";

    internal static new ManualLogSource Logger;
    internal static ConfigEntry<bool> enablePesticideRework;

    private Harmony harmony;

    private void Awake()
    {
        Logger = base.Logger;

        enablePesticideRework = Config.Bind(
            "General",
            "Enable Pesticide Rework",
            true,
            "Enhances Pesticide flamethrower with turbocharged range boost and damage scaling with enemy missing health.");

        harmony = new Harmony(PluginGUID);
        harmony.PatchAll(typeof(PesticideReworkPatches));

        Logger.LogInfo($"{PluginName} v{PluginVersion} loaded successfully.");
    }

    private void OnDestroy()
    {
        harmony?.UnpatchSelf();
    }
}
