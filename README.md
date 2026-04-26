# InfiniteStamina

A [LabAPI](https://github.com/northwood-studios/LabAPI) plugin for **SCP: Secret Laboratory** that maintains a configurable stamina level for players — no more running out of breath.

## Features

- **Configurable stamina value** — set any level from `0.0` (empty) to `1.0` (full), e.g. `0.5` keeps players at exactly half stamina
- **SCP toggle** — SCPs are excluded by default, can be enabled via config
- **Exempt roles** — define a list of roles that are unaffected (Spectator, Overwatch, None by default)
- **Enable/disable** without removing the plugin — just set `is_enabled: false`
- **Input validation** — invalid stamina values are automatically clamped and logged on startup

## Installation

1. Download the latest `InfiniteStamina.dll` from [Releases](../../releases)
2. Place it in your LabAPI plugins folder:
   ```
   %AppData%\SCP Secret Laboratory\LabAPI\plugins\
   ```
3. Restart your server — a `config.yml` will be generated automatically

## Configuration

Located at: `LabAPI/configs/<port>/InfiniteStamina/config.yml`

```yaml
# Enable or disable the plugin entirely.
is_enabled: true

# If true, SCPs are also affected.
allow_scps: false

# Stamina level to maintain. 1.0 = full, 0.5 = half, 0.0 = empty.
stamina_value: 1.0

# Roles that are NOT affected by this plugin.
exempt_roles:
  - Spectator
  - Overwatch
  - None
```

## Requirements

- SCP: Secret Laboratory Dedicated Server
- LabAPI (bundled with the server)

## Author

**Leifur**
