namespace HPE.Kruta.Common.Config
{
    /// <summary>
    /// Xml mirror for eSerivces.Config block
    /// </summary>
    public static class KrutaConfigXml
    {
        

        //<HPE.Kruta.Common.Config>
        public const string ConfigSectionName = "HPE.Kruta.Common.Config";
        //  <WorkspaceConfig>
        public static class Workspace
        {
            public const string Node = "WorkspaceConfig";
            //    <Machines>
            public static class Machines
            {
                public const string MachinesNode = "MachineConfigs";
                //      <MachineConfig Key="SD040" DefaultEnvironment="QA"/>
                public static class Machine
                {
                    public const string Element = "MachineConfig";
                    public const string Key = "Key";
                    public const string DefaultEnvironmentAttribute = "DefaultEnvironment";
                }
            }
            //    <Environments>
            public static class Environments
            {
                public const string EnvironmentsNode = "EnvironmentConfigs";
                //      <EnvironmentConfig Key="DEV">
                public static class Environment
                {
                    public const string Node = "EnvironmentConfig";
                    public const string Key = "Key";
                    public static class FlagsXml
                    {
                        public const string Node = Workspace.FlagsXml.Node;

                        //<FlagConfigs>
                        //  <FlagConfig Key="SomeUniqueString" Active="True"  />
                        //  <FlagConfig Key="SomeOtherUniqueString" Active="False"  />
                        //</FlagConfigs>
                        public static class Flag
                        {
                            public const string Element = Workspace.FlagsXml.Flag.Element;
                            public const string GroupAttribute = Workspace.FlagsXml.Flag.GroupAttribute;
                            public const string KeyAttribute = Workspace.FlagsXml.Flag.KeyAttribute;
                            public const string ActiveAttribute = Workspace.FlagsXml.Flag.ActiveAttribute;
                            public const string DescriptionAttribute = Workspace.FlagsXml.Flag.DescriptionAttribute;
                        }
                    }
                    public static class KeyValuesXml
                    {
                        public const string Node = Workspace.KeyValuesXml.Node;

                        //<KeyValueConfigs>
                        //  <KeyValueConfig Key="SomeUniqueString" Value="AbcDefGhi"  />
                        //  <KeyValueConfig Key="SomeOtherUniqueString" Value="someperson@somewhere.com"  />
                        //</KeyValueConfigs>
                        public static class KeyValue
                        {
                            public const string Element = Workspace.KeyValuesXml.KeyValue.Element;
                            public const string GroupAttribute = Workspace.FlagsXml.Flag.GroupAttribute;
                            public const string KeyAttribute = Workspace.KeyValuesXml.KeyValue.KeyAttribute;
                            public const string ValueAttribute = Workspace.KeyValuesXml.KeyValue.ValueAttribute;
                            public const string DescriptionAttribute = Workspace.KeyValuesXml.KeyValue.DescriptionAttribute;
                        }
                    }
                }
            }

            public static class FlagsXml
            {
                public const string Node = "FlagConfigs";

                //<FlagConfigs>
                //  <FlagConfig Key="SomeUniqueString" Active="True"  />
                //  <FlagConfig Key="SomeOtherUniqueString" Active="False"  />
                //</FlagConfigs>
                public static class Flag
                {
                    public const string Element = "FlagConfig";
                    public const string GroupAttribute = "Group";
                    public const string KeyAttribute = "UniqueKey";
                    public const string ActiveAttribute = "Active";
                    public const string DescriptionAttribute = "Description";
                }
            }
            public static class KeyValuesXml
            {
                public const string Node = "KeyValueConfigs";

                //<KeyValueConfigs>
                //  <KeyValueConfig Key="SomeUniqueString" Value="AbcDefGhi"  />
                //  <KeyValueConfig Key="SomeOtherUniqueString" Value="someperson@somewhere.com"  />
                //</KeyValueConfigs>
                public static class KeyValue
                {
                    public const string Element = "KeyValueConfig";
                    public const string GroupAttribute = "Group";
                    public const string KeyAttribute = "UniqueKey";
                    public const string ValueAttribute = "Value";
                    public const string DescriptionAttribute = "Description";
                }
            }
        }
    }
}
