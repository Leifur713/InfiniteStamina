using LabApi.Loader.Features.Plugins.Configuration;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;
using YamlDotNet.Serialization;

namespace InfiniteStamina
{
    public class Config : YamlConfig
    {
        [YamlMember(Alias = "is_enabled")]
        [DefaultValue(true)]
        public bool IsEnabled { get; set; } = true;

        [YamlMember(Alias = "allow_scps")]
        [DefaultValue(false)]
        public bool AllowScps { get; set; } = false;

        [Description("Stamina level to maintain. 1.0 = full, 0.5 = half, 0.0 = empty.")]
        [YamlMember(Alias = "stamina_value")]
        [DefaultValue(1.0f)]
        public float StaminaValue { get; set; } = 1.0f;

        [YamlMember(Alias = "exempt_roles")]
        public List<RoleTypeId> ExemptRoles { get; set; } = new List<RoleTypeId>
        {
            RoleTypeId.Spectator,
            RoleTypeId.Overwatch,
            RoleTypeId.None
        };
    }
}
