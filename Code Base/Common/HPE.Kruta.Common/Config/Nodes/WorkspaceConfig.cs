using System;
using System.Collections.Generic;
using HPE.Kruta.Common.Config.Enums;

namespace HPE.Kruta.Common.Config
{
    public class WorkspaceConfig
    {
        public MachineConfig MachineConfig { get; set; }
        public Dictionary<string, FlagConfig> Flags { get; set; }
        public Dictionary<string, KeyValueConfig> KeyValues { get; set; }
        

        public IDictionary<EnvironmentsEnum,EnvironmentConfig> Environments { get; set; }
        
        /// <summary>
        /// Returns the EnvironmentConfig for the current Machine's DefaultEnvironment
        /// </summary>
        public EnvironmentConfig CurrentEnvironmentConfig
        {
            get
            {
                if(this.MachineConfig != null && 
                   Environments.ContainsKey(this.MachineConfig.DefaultEnvironment))
                {
                    return Environments[this.MachineConfig.DefaultEnvironment];    
                }

                return null;                                
            }
        }

        public WorkspaceConfig()
        {
            Flags = new Dictionary<string, FlagConfig>(StringComparer.OrdinalIgnoreCase);
            KeyValues = new Dictionary<string, KeyValueConfig>(StringComparer.OrdinalIgnoreCase);
            Environments = new Dictionary<EnvironmentsEnum, EnvironmentConfig>();
        }
    }
}
