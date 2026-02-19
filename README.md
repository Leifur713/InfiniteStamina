# InfiniteStamina
A plugin for **SCP: Secret Laboratory** using **LabAPI** that gives all human players infinite stamina. SCP players are excluded and keep their default behavior.

---

## Features
- ✅ Infinite stamina for all human players (MTF, Chaos, Scientists, D-Class, etc.)
- ❌ SCPs are **not** affected
- ⚙️ Configurable refresh interval via `config.yml`
- 🔌 Easy to enable/disable via config

---

## Requirements
- [SCP: Secret Laboratory](https://store.steampowered.com/app/700330/SCP_Secret_Laboratory/)
- [LabAPI](https://github.com/northwood-studios/LabAPI)
- [YamlDotNet](https://github.com/aaubry/YamlDotNet)

---

## Installation
1. Download the latest release `.dll` from the [Releases](../../releases) page.
2. Place the `.dll` file into your server's plugin folder:
   ```
   AppData/Roaming/SCP Secret Laboratory/LabAPI/plugins/
   ```
3. Start your server once to generate the `config.yml`.
4. Configure the plugin to your liking (see below).

---

## Configuration
The config file is automatically created at:
```
AppData/Roaming/SCP Secret Laboratory/LabAPI/plugins/InfiniteStamina/config.yml
```

| Option             | Type    | Default | Description                                      |
|--------------------|---------|---------|--------------------------------------------------|
| `is_enabled`       | bool    | `true`  | Enable or disable the plugin                     |
| `refresh_interval` | float   | `0.1`   | How often (in seconds) stamina is refreshed      |

**Example `config.yml`:**
```yaml
is_enabled: true
refresh_interval: 0.1
```

---

## How It Works
- When a player **spawns**, their stamina is immediately set to full — unless they are an SCP.
- A background thread continuously resets stamina to full for all human players at the configured interval.
- SCPs are detected via their team (`Team.SCPs`) and are automatically skipped.

---

## Building from Source
1. Clone this repository:
   ```bash
   git clone https://github.com/YourUsername/InfiniteStamina.git
   ```
2. Open the project in Visual Studio or Rider.
3. Make sure your references to `LabAPI` and `YamlDotNet` are correct.
4. Build the project — the `.dll` will be in the `bin/` folder.

---

## License
This project is licensed under the [MIT License](LICENSE).

---

## Author
Made by **DeinName**  
Feel free to open an issue or pull request if you find a bug or want to contribute!