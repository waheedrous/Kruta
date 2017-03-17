using System;
using System.Collections.Generic;
using HPE.Kruta.Common.Config.Enums;

namespace HPE.Kruta.Common.Config
{
    public class EnvironmentConfig
    {
        public EnvironmentsEnum Key { get; set; }
        public Dictionary<string, FlagConfig> Flags { get; set; }
        public Dictionary<string, KeyValueConfig> KeyValues { get; set; }
        
        public EnvironmentConfig()
        {
            Key = EnvironmentsEnum.DEV; //NOTE: Dev is appropriate; do not change to UNSET
            Flags = new Dictionary<string, FlagConfig>(StringComparer.OrdinalIgnoreCase);
            KeyValues = new Dictionary<string, KeyValueConfig>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Case insensitve getter by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FlagConfig GetFlagConfig(string key)
        {
            if (Flags.ContainsKey(key))
                return Flags[key];
            return null;
        }

        public KeyValueConfig GetKeyValueConfig(string key)
        {
            if (KeyValues.ContainsKey(key))
                return KeyValues[key];
            return null;
        }
    }
}
