using System;
using System.Collections.Generic;
using System.Xml;
using HPE.Kruta.Common.Enums;
using HPE.Kruta.Common.Utils;
using HPE.Kruta.Common.Config.Enums;

namespace HPE.Kruta.Common.Config
{
    using MachinesXml = KrutaConfigXml.Workspace.Machines;
    using FlagsXml = KrutaConfigXml.Workspace.FlagsXml;
    using KeyValuesXml = KrutaConfigXml.Workspace.KeyValuesXml;
    using ResourceFilesXml = KrutaConfigXml.Workspace.ResourceFiles;
    

    using EnvironmentsXml = KrutaConfigXml.Workspace.Environments;
    using EnvironmentFlagsXml = KrutaConfigXml.Workspace.Environments.Environment.FlagsXml;
    using EnvironmentKeyValuesXml = KrutaConfigXml.Workspace.Environments.Environment.KeyValuesXml;
    
    using DataConnectionsXml = KrutaConfigXml.Workspace.Environments.Environment.DataConnections;
    
    public class KrutaConfigParser
    {
        /// <summary>
        /// Entry point for KrutaConfigSectionHandler.Create()
        /// 
        /// This method processes the WorkspaceConfig node of the HPE.Kruta.Common.Config, and returns an object that represents the configuration information.
        /// </summary>
        /// <param name="workspaceNode"></param>
        /// <returns>WorkspaceConfig</returns>
        internal WorkspaceConfig WorkspaceFactory(XmlNode workspaceNode)
        {
            var result = new WorkspaceConfig();

            // Exit if there are no configuration settings.
            if (workspaceNode != null && workspaceNode.ChildNodes.Count > 0)
            {
                result.MachineConfig =
                    MachineFactoryDefaultEnvironment(workspaceNode.SelectSingleNode(MachinesXml.MachinesNode));

                result.Flags =
                    FlagsFactory(workspaceNode.SelectSingleNode(FlagsXml.Node));

                result.KeyValues =
                    KeyValuesFactory(workspaceNode.SelectSingleNode(KeyValuesXml.Node));

                // Attempt to match a machine node with the current machine, if there is no machine, a default has been provided by InitializeWorkspace()
                result.Environments = EnvironmentsFactory(workspaceNode.SelectSingleNode(EnvironmentsXml.EnvironmentsNode));

                //must occur LAST
                MergeGlobalAndEnvironmentConfig(result);
                
            }
            return result;

        }

        private void MergeGlobalAndEnvironmentConfig(WorkspaceConfig workspaceConfig)
        {
            MergeGlobalAndEnvironmentFlags(workspaceConfig);
            MergeGlobalAndEnvironmentKeyValues(workspaceConfig);
        }

        private void MergeGlobalAndEnvironmentFlags(WorkspaceConfig workspaceConfig)
        {
            if(
                (workspaceConfig.Flags!=null && workspaceConfig.Flags.Count>0)
                && (workspaceConfig.Environments != null && workspaceConfig.Environments.Count > 0)
            )
            {
                //for each environment
                foreach (KeyValuePair<Enums.EnvironmentsEnum, EnvironmentConfig> environment in workspaceConfig.Environments)
                {
                    //if the environment has no flags, then it inherits ALL global flags
                    if(environment.Value.Flags == null || environment.Value.Flags.Count == 0)
                    {
                        environment.Value.Flags = workspaceConfig.Flags;
                        continue;
                    }
                    //if the environment has flags, then it inherits all global flags it doesn't override
                    foreach (var kvp in workspaceConfig.Flags)
                    {
                        if(!environment.Value.Flags.ContainsKey(kvp.Key))
                            environment.Value.Flags.Add(kvp.Key,kvp.Value);
                    }
                }
            }
        }

        private void MergeGlobalAndEnvironmentKeyValues(WorkspaceConfig workspaceConfig)
        {
            if (
                (workspaceConfig.KeyValues != null && workspaceConfig.KeyValues.Count > 0)
                && (workspaceConfig.Environments != null && workspaceConfig.Environments.Count > 0)
            )
            {
                //for each environment
                foreach (KeyValuePair<EnvironmentsEnum, EnvironmentConfig> environment in workspaceConfig.Environments)
                {
                    //if the environment has no KeyValues, then it inherits ALL global KeyValues
                    if (environment.Value.KeyValues == null || environment.Value.KeyValues.Count == 0)
                    {
                        environment.Value.KeyValues = workspaceConfig.KeyValues;
                        continue;
                    }
                    //if the environment has KeyValues, then it inherits all global KeyValues it doesn't override
                    foreach (var kvp in workspaceConfig.KeyValues)
                    {
                        if (!environment.Value.KeyValues.ContainsKey(kvp.Key))
                            environment.Value.KeyValues.Add(kvp.Key, kvp.Value);
                    }
                }
            }
        }

        internal MachineConfig MachineFactoryDefaultEnvironment(XmlNode machinesNode)
        {
            var result = new MachineConfig();

            if (machinesNode != null)
            {
                var machineNodeList = machinesNode.SelectNodes(MachinesXml.Machine.Element);
                if (machineNodeList != null)
                    foreach (XmlNode machineNode in machineNodeList)
                    {
                        if (
                               machineNode != null
                            && machineNode.Attributes[MachinesXml.Machine.Key] != null
                            && machineNode.Attributes[MachinesXml.Machine.Key].Value.ToLower() == Environment.MachineName.ToLower()
                            && machineNode.Attributes[MachinesXml.Machine.DefaultEnvironmentAttribute] != null
                        )
                        {
                            result = MachineFactory(machineNode);
                            break;
                        }
                    }
            }
            return result;
        }

        internal MachineConfig MachineFactory(XmlNode machineNode)
        {
            //defaults to current machine and DEVELOPMENT
            // this allows us to not need to define Nodes for individual developers machines
            var result = new MachineConfig
                             {
                DefaultEnvironment = EnvironmentsEnum.DEV //<- DO NOT CHANGE TO UNSET
            };

            if (machineNode != null)
            {
                result = new MachineConfig
                {
                    Name = machineNode.Attributes[MachinesXml.Machine.Key].Value,
                    DefaultEnvironment = EnumUtils.Parse<EnvironmentsEnum>(
                           machineNode.Attributes[MachinesXml.Machine.DefaultEnvironmentAttribute].Value
                       )
                };

            }
            return result;
        }

        internal Dictionary<EnvironmentsEnum, EnvironmentConfig> EnvironmentsFactory(
            XmlNode environmentsNode
            )
        {
            var result = new Dictionary<EnvironmentsEnum, EnvironmentConfig>();

            // Find any environments that are configured.  Defaults have already been provided by the initialize workspace.
            if (environmentsNode != null)
            {
                var environmentNodeList = environmentsNode.SelectNodes(EnvironmentsXml.Environment.Node);
                if (environmentNodeList != null)
                {
                    foreach (XmlNode environmentNode in environmentNodeList)
                    {
                        EnvironmentConfig environmentConfig = EnvironmentFactory(environmentNode);

                        if (result.ContainsKey(environmentConfig.Key))
                            result[environmentConfig.Key] = environmentConfig;
                        else
                            result.Add(environmentConfig.Key, environmentConfig);

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Entry point for EnvironmentConfig nodes and children nodes
        /// </summary>
        /// <param name="environmentNode"></param>
        /// <returns></returns>
        internal EnvironmentConfig EnvironmentFactory(XmlNode environmentNode)
        {

            var result = new EnvironmentConfig();
            if (environmentNode != null)
            {
                string environmentKey;
                if(
                    environmentNode.Attributes[EnvironmentsXml.Environment.Key].TryGetInnerText(
                        out environmentKey)
                    )
                {

                    result.Key =
                        EnumUtils.Parse<EnvironmentsEnum>(environmentKey,EnvironmentsEnum.DEV.ToString());

                    result.Flags =
                        FlagsFactory(environmentNode.SelectSingleNode(EnvironmentFlagsXml.Node));

                    result.KeyValues =
                        KeyValuesFactory(environmentNode.SelectSingleNode(EnvironmentKeyValuesXml.Node));
                }
            }

            return result;
        }

        internal Dictionary<string, FlagConfig> FlagsFactory(XmlNode flagNodes)
        {
            var result = new Dictionary<string, FlagConfig>(StringComparer.OrdinalIgnoreCase);

            if (flagNodes != null)
            {
                var flagNodeList = flagNodes.SelectNodes(
                    EnvironmentFlagsXml.Flag.Element
                    );

                if (flagNodeList != null)
                    foreach (XmlNode flag in flagNodeList)
                    {
                        if (
                            flag.Attributes[EnvironmentFlagsXml.Flag.KeyAttribute] != null
                            && flag.Attributes[EnvironmentFlagsXml.Flag.ActiveAttribute] != null
                            )
                        {
                            var kvp = FlagFactory(flag);
                            result.Add(kvp.Key, kvp.Value);
                        }
                    }
            }


            return result;
        }

        internal Dictionary<string, KeyValueConfig> KeyValuesFactory(XmlNode keyValueNodes)
        {
            var result = new Dictionary<string, KeyValueConfig>(StringComparer.OrdinalIgnoreCase);

            if (keyValueNodes != null)
            {
                var nodeList = keyValueNodes.SelectNodes(
                    EnvironmentKeyValuesXml.KeyValue.Element
                    );

                if (nodeList != null)
                    foreach (XmlNode node in nodeList)
                    {
                        if (
                            node.Attributes[EnvironmentKeyValuesXml.KeyValue.KeyAttribute] != null
                            && node.Attributes[EnvironmentKeyValuesXml.KeyValue.ValueAttribute] != null
                            )
                        {
                            var kvp = KeyValueFactory(node);
                            result.Add(kvp.Key, kvp.Value);
                        }
                    }
            }


            return result;
        }

        internal KeyValuePair<string, FlagConfig> FlagFactory(XmlNode flagNode)
        {
            KeyValuePair<string, FlagConfig>? result = null;

            string key = null;
            var value = new FlagConfig();

            if (flagNode != null)
            {
                if (
                    flagNode.Attributes[EnvironmentFlagsXml.Flag.KeyAttribute].TryGetInnerText(out key)
                )
                {
                    value.UniqueKey = key;
                    string group;
                    value.Group = (
                        flagNode.Attributes[EnvironmentFlagsXml.Flag.GroupAttribute].TryGetInnerText(
                            out group)
                        )
                        ? group
                        : string.Empty;
                    string active;
                    if (
                        flagNode.Attributes[EnvironmentFlagsXml.Flag.ActiveAttribute].TryGetInnerText(out active)
                        )
                    {
                        bool bActive;
                        bool.TryParse(active, out bActive);
                        value.Active = bActive;
                    }
                    string description;
                    value.Description = (
                        flagNode.Attributes[EnvironmentFlagsXml.Flag.DescriptionAttribute].TryGetInnerText(
                            out description)
                        )
                        ? description
                        : string.Empty;

                }
                if(key != null)
                    result = new KeyValuePair<string, FlagConfig>(key, value);
            }
            if (!result.HasValue)
                result = new KeyValuePair<string, FlagConfig>(key, value);

            return result.GetValueOrDefault();
        }

        internal KeyValuePair<string, KeyValueConfig> KeyValueFactory(XmlNode keyValueNode)
        {
            KeyValuePair<string, KeyValueConfig>? result = null;

            string key = null;
            var value = new KeyValueConfig();

            if (keyValueNode != null)
            {
                if (
                    keyValueNode.Attributes[EnvironmentKeyValuesXml.KeyValue.KeyAttribute].TryGetInnerText(out key)
                )
                {
                    string tValue = null;
                    value.UniqueKey = key;
                    if (
                        keyValueNode.Attributes[EnvironmentKeyValuesXml.KeyValue.ValueAttribute].TryGetInnerText(out tValue)
                        )
                    {
                        value.Value = tValue;
                    }
                    string group;
                    value.Group = (
                        keyValueNode.Attributes[EnvironmentFlagsXml.Flag.GroupAttribute].TryGetInnerText(
                            out group)
                        )
                        ? group
                        : string.Empty;
                    string description;
                    value.Description = (
                        keyValueNode.Attributes[EnvironmentKeyValuesXml.KeyValue.DescriptionAttribute].TryGetInnerText(
                            out description)
                        )
                        ? description
                        : string.Empty;
                }
                if (key != null)
                    result = new KeyValuePair<string, KeyValueConfig>(key, value);
            }
            if (!result.HasValue)
                result = new KeyValuePair<string, KeyValueConfig>(key, value);

            return result.GetValueOrDefault();
        }

    }
}
