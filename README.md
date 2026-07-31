# Pesticide Rework

A BepInEx mod for Mycopunk that enhances the Pesticide flamethrower upgrade with turbocharged range and missing-health
damage scaling.

## Features

When a **turbocharged** upgrade whose name contains `Pesticide` or `Flamethrower` is equipped:

- **Turbocharged range** — Flamethrower range is multiplied (default **2×**).
- **Missing-health scaling** — Flamethrower DoT + AOE damage scales with how much health the target is missing (default
  up to **+125%** at 0 HP remaining).

## Getting Started

### Dependencies

- Mycopunk (base game)
- [BepInEx Pack for Mycopunk](https://thunderstore.io/c/mycopunk/p/BepInEx/BepInExPack_Mycopunk/) — 5.4.2403 or
  compatible
- HarmonyLib (bundled with BepInEx)

### Building

```bash
dotnet build --configuration Release
```

Output DLL: `bin/Release/netstandard2.1/PesticideRework.dll`

### Installing

**Via Thunderstore (recommended)**

1. Install with the Thunderstore Mod Manager / r2modman.

**Manual installation**

1. Install BepInEx for Mycopunk.
2. Copy `PesticideRework.dll` into `<Mycopunk Directory>/BepInEx/plugins/`.

## Configuration

Config file: `<Mycopunk Directory>/BepInEx/config/sparroh.pesticiderework.cfg`

Edits on disk are hot-reloaded (debounced). Damage multiplier applies on the next hit; range multiplier applies the next
time upgrades are enabled.

| Setting                          | Section | Default | Description                                                                                                 |
|----------------------------------|---------|---------|-------------------------------------------------------------------------------------------------------------|
| Enable Rework                    | General | `true`  | Enhances Pesticide flamethrower with turbocharged range boost and damage scaling with enemy missing health. |
| Turbocharge Range Multiplier     | General | `2`     | Multiplies flamethrower range when a turbocharged Pesticide/Flamethrower upgrade is equipped.               |
| Missing Health Damage Multiplier | General | `1.25`  | Extra damage multiplier at full missing health (0 HP remaining). Scales linearly with missing health ratio. |

## Authors

- Sparroh

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
