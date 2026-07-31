using System;
using HarmonyLib;

namespace PesticideRework;

[HarmonyPatch]
public static class PesticideReworkPatches
{
    [HarmonyPatch(typeof(BounceShotgun), "OnUpgradesEnabled")]
    [HarmonyPostfix]
    public static void OnUpgradesEnabledPostfix(BounceShotgun __instance)
    {
        if (!ConfigManager.EnablePesticideRework.Value || !(__instance.ShotgunData.flamethrowerRange > 0f)) return;

        var hasTurboPesticide = false;
        foreach (var activeUpgrade in __instance.ActiveUpgrades)
            if (activeUpgrade.IsTurbocharged
                && (activeUpgrade.Upgrade.Name.Contains("Pesticide") ||
                    activeUpgrade.Upgrade.Name.Contains("Flamethrower")))
            {
                hasTurboPesticide = true;
                break;
            }

        if (hasTurboPesticide)
            __instance.ShotgunData.flamethrowerRange *= ConfigManager.TurbochargeRangeMultiplier.Value;

        __instance.OnBeforeDamage = (MutableDamageCallback)Delegate.Combine(
            __instance.OnBeforeDamage,
            new MutableDamageCallback(OnBeforeFlamethrowerDamage));
    }

    [HarmonyPatch(typeof(BounceShotgun), "OnUpgradesDisabled")]
    [HarmonyPostfix]
    public static void OnUpgradesDisabledPostfix(BounceShotgun __instance)
    {
        if (ConfigManager.EnablePesticideRework.Value && __instance.ShotgunData.flamethrowerRange > 0f)
            __instance.OnBeforeDamage = (MutableDamageCallback)Delegate.Remove(
                __instance.OnBeforeDamage,
                new MutableDamageCallback(OnBeforeFlamethrowerDamage));
    }

    private static void OnBeforeFlamethrowerDamage(ref DamageCallbackData data)
    {
        if ((data.damageData.damageFlags & (DamageFlags.DamageOverTime | DamageFlags.AOE))
            != (DamageFlags.DamageOverTime | DamageFlags.AOE)
            || !(data.source is BounceShotgun bounceShotgun))
            return;

        var hasTurboPesticide = false;
        foreach (var activeUpgrade in bounceShotgun.ActiveUpgrades)
            if (activeUpgrade.IsTurbocharged
                && (activeUpgrade.Upgrade.Name.Contains("Pesticide") ||
                    activeUpgrade.Upgrade.Name.Contains("Flamethrower")))
            {
                hasTurboPesticide = true;
                break;
            }

        if (hasTurboPesticide)
        {
            var missingHealthRatio = 1f - data.target.Health / data.target.MaxHealth;
            data.damageData.damage *= 1f + missingHealthRatio * ConfigManager.MissingHealthDamageMultiplier.Value;
        }
    }
}