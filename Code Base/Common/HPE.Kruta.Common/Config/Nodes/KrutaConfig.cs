namespace HPE.Kruta.Common.Config
{
    public class KrutaConfig
    {   
        public WorkspaceConfig WorkspaceConfig { get; set; }  
      
        /// <summary>
        /// identical to WorkspaceConfig.CurrentEnvironmentConfig
        /// </summary>
        public EnvironmentConfig CurrentEnvironmentConfig
        {
            get
            {
                return this.WorkspaceConfig.CurrentEnvironmentConfig ?? new EnvironmentConfig();
            }
        }

        public KrutaConfig()
        {
            WorkspaceConfig = new WorkspaceConfig(); ;
        }
    }
}
