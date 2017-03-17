using HPE.Kruta.Common.Config.Enums;

namespace HPE.Kruta.Common.Config
{
    public class MachineConfig
    {
        public string Name { get; set; }
        public EnvironmentsEnum DefaultEnvironment { get; set; }

        public MachineConfig()
        {
            Name = System.Environment.MachineName;
            DefaultEnvironment = EnvironmentsEnum.DEV;
        }
    }
}
