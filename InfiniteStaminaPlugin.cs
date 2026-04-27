using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Plugins;
using PlayerRoles;
using PlayerStatsSystem;

namespace InfiniteStamina
{
    public class InfiniteStamina : Plugin<Config>
    {
        public override string Name => "InfiniteStamina";
        public override string Author => "Leifur";
        public override string Description => "Maintains a configurable stamina level for players.";
        public override System.Version Version => new System.Version(1, 3, 0);
        public override System.Version RequiredApiVersion => LabApiProperties.CurrentVersion;

        public override void Enable()
        {
            if (!Config.IsEnabled)
            {
                Logger.Info("InfiniteStamina is disabled in config, skipping.");
                return;
            }

            ClampConfig();
            StaticUnityMethods.OnUpdate += OnUpdate;
            Logger.Info($"InfiniteStamina v{Version} loaded. StaminaValue={Config.StaminaValue:P0}");
        }

        public override void Disable()
        {
            StaticUnityMethods.OnUpdate -= OnUpdate;
            Logger.Info("InfiniteStamina unloaded.");
        }

        private void ClampConfig()
        {
            if (Config.StaminaValue < 0f || Config.StaminaValue > 1f)
            {
                Logger.Warn($"stamina_value {Config.StaminaValue} is out of range [0.0-1.0], clamping to 1.0.");
                Config.StaminaValue = 1f;
            }
        }

        private bool IsExempt(Player player)
        {
            if (player == null) return true;
            if (!Config.AllowScps && player.IsSCP) return true;
            if (Config.ExemptRoles.Contains(player.Role)) return true;
            return false;
        }

        private void OnUpdate()
        {
            foreach (Player player in Player.List)
            {
                if (player.IsHost) continue;
                if (IsExempt(player)) continue;

                var stamina = player.ReferenceHub?.playerStats?.GetModule<StaminaStat>();
                if (stamina == null) continue;

                stamina.CurValue = stamina.MaxValue * Config.StaminaValue;
            }
        }
    }
}
