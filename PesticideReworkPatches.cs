using System;
using HarmonyLib;

[HarmonyPatch]
public static class PesticideReworkPatches
{
    public static float TurbochargeRangeMultiplier = 2f;
    public static float MissingHealthDamageMultiplier = 1.25f;

    [HarmonyPatch(typeof(BounceShotgun), "OnUpgradesEnabled")]
    [HarmonyPostfix]
    public static void OnUpgradesEnabledPostfix(BounceShotgun __instance)
    {
        if (!SparrohPlugin.enablePesticideRework.Value || !(__instance.ShotgunData.flamethrowerRange > 0f))
        {
            return;
        }

        bool hasTurboPesticide = false;
        foreach (UpgradeInstance activeUpgrade in __instance.ActiveUpgrades)
        {
            if (activeUpgrade.IsTurbocharged
                && (activeUpgrade.Upgrade.Name.Contains("Pesticide") || activeUpgrade.Upgrade.Name.Contains("Flamethrower")))
            {
                hasTurboPesticide = true;
                break;
            }
        }

        if (hasTurboPesticide)
        {
            __instance.ShotgunData.flamethrowerRange *= TurbochargeRangeMultiplier;
        }

        __instance.OnBeforeDamage = (MutableDamageCallback)Delegate.Combine(
            __instance.OnBeforeDamage,
            new MutableDamageCallback(OnBeforeFlamethrowerDamage));
    }

    [HarmonyPatch(typeof(BounceShotgun), "OnUpgradesDisabled")]
    [HarmonyPostfix]
    public static void OnUpgradesDisabledPostfix(BounceShotgun __instance)
    {
        if (SparrohPlugin.enablePesticideRework.Value && __instance.ShotgunData.flamethrowerRange > 0f)
        {
            __instance.OnBeforeDamage = (MutableDamageCallback)Delegate.Remove(
                __instance.OnBeforeDamage,
                new MutableDamageCallback(OnBeforeFlamethrowerDamage));
        }
    }

    private static void OnBeforeFlamethrowerDamage(ref DamageCallbackData data)
    {
        if ((data.damageData.damageFlags & (DamageFlags.DamageOverTime | DamageFlags.AOE))
            != (DamageFlags.DamageOverTime | DamageFlags.AOE)
            || !(data.source is BounceShotgun bounceShotgun))
        {
            return;
        }

        bool hasTurboPesticide = false;
        foreach (UpgradeInstance activeUpgrade in bounceShotgun.ActiveUpgrades)
        {
            if (activeUpgrade.IsTurbocharged
                && (activeUpgrade.Upgrade.Name.Contains("Pesticide") || activeUpgrade.Upgrade.Name.Contains("Flamethrower")))
            {
                hasTurboPesticide = true;
                break;
            }
        }

        if (hasTurboPesticide)
        {
            float missingHealthRatio = 1f - data.target.Health / data.target.MaxHealth;
            data.damageData.damage *= 1f + missingHealthRatio * MissingHealthDamageMultiplier;
        }
    }
}
