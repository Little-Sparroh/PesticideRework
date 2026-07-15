# Pesticide Rework

A BepInEx mod for Mycopunk that enhances the Pesticide flamethrower upgrade.

## Features

- **Turbocharged Range**: When a turbocharged Pesticide/Flamethrower upgrade is equipped, flamethrower range is doubled.
- **Missing Health Scaling**: Flamethrower DoT+AOE damage scales with how much health the target is missing (up to +125% at 0 HP remaining).

## Getting Started

### Dependencies

* Mycopunk (base game)
* [BepInEx](https://github.com/BepInEx/BepInEx) - Version 5.4.2403 or compatible
* .NET Framework 4.8
* [HarmonyLib](https://github.com/pardeike/Harmony) (included via NuGet)

### Building/Compiling

```bash
dotnet build --configuration Release
```

### Installing

**Via Thunderstore (Recommended)**:
1. Download and install via Thunderstore Mod Manager

**Manual Installation**:
1. Place the built `PesticideRework.dll` in your `<Mycopunk Directory>/BepInEx/plugins/` folder

## Configuration

Access mod settings at `<Mycopunk Directory>/BepInEx/config/sparroh.pesticiderework.cfg`:

| Setting | Default | Description |
|---------|---------|-------------|
| Enable Pesticide Rework | `true` | Enables turbocharged range and missing-health damage scaling. |

## Authors

- Sparroh

## License

This project is licensed under the MIT License - see the LICENSE file for details
