using System;
using System.IO;
using System.Threading;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using PlayerRoles;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace InfiniteStamina
{
    public class InfiniteStaminaPlugin : Plugin
    {
        public override string Name        => "InfiniteStamina";
        public override string Description => "Grants all players (except SCPs) infinite stamina.";
        public override string Author      => "Artic / Leifur";
        public override Version Version    => new Version(1, 0, 0, 0);
        public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);

        private static readonly string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SCP Secret Laboratory", "LabAPI", "plugins", "InfiniteStamina", "config.yml"
        );

        private PluginCfg _cfg;
        private Thread _staminaThread;
        private bool _running;

        public override void Enable()
        {
            _cfg = LoadConfig();

            if (!_cfg.IsEnabled)
            {
                Logger.Info("InfiniteStamina is disabled in the config.yml.");
                return;
            }

            PlayerEvents.Spawned += OnPlayerSpawned;

            _running = true;
            _staminaThread = new Thread(StaminaLoop) { IsBackground = true };
            _staminaThread.Start();

            Logger.Info("InfiniteStamina activated!");
        }

        public override void Disable()
        {
            _running = false;
            PlayerEvents.Spawned -= OnPlayerSpawned;
            Logger.Info("InfiniteStamina disabled.");
        }

        private void OnPlayerSpawned(PlayerSpawnedEventArgs ev)
        {
            if (ev.Player == null) return;
            if (ev.Player.ReferenceHub.GetRoleId().GetTeam() == Team.SCPs) return;
            SetFullStamina(ev.Player);
        }

        private void StaminaLoop()
        {
            while (_running)
            {
                try
                {
                    foreach (var player in LabApi.Features.Wrappers.Player.List)
                    {
                        if (player == null || player.IsServer) continue;
                        if (player.ReferenceHub.GetRoleId().GetTeam() == Team.SCPs) continue;
                        SetFullStamina(player);
                    }
                }
                catch { }

                Thread.Sleep((int)(_cfg.RefreshInterval * 1000));
            }
        }

        private static void SetFullStamina(LabApi.Features.Wrappers.Player player)
        {
            try
            {
                player.ReferenceHub.playerStats
                    .GetModule<PlayerStatsSystem.StaminaStat>()
                    .CurValue = 1f;
            }
            catch { }
        }

        private static PluginCfg LoadConfig()
        {
            string dir = Path.GetDirectoryName(ConfigPath)!;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(ConfigPath))
            {
                SaveConfig(new PluginCfg());
                Logger.Info("[InfiniteStamina] config.yml erstellt.");
                return new PluginCfg();
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            return deserializer.Deserialize<PluginCfg>(File.ReadAllText(ConfigPath));
        }

        private static void SaveConfig(PluginCfg cfg)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            string header =
                "# ──────────────────────────────────────────\n" +
                "#   InfiniteStamina Plugin - configuration\n" +
                "# ──────────────────────────────────────────\n\n";

            File.WriteAllText(ConfigPath, header + serializer.Serialize(cfg));
        }
    }

    public class PluginCfg
    {
        public bool IsEnabled { get; set; } = true;
        public float RefreshInterval { get; set; } = 0.1f;
    }
}