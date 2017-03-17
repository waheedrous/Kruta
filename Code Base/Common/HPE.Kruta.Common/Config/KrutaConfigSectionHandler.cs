using System.Configuration;
using System.Xml;

namespace HPE.Kruta.Common.Config
{
    using WorkspaceXml = KrutaConfigXml.Workspace;

    public sealed class KrutaConfigSectionHandler : IConfigurationSectionHandler
    {     

        #region Provider
        private static KrutaConfig _instance;
        public static KrutaConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (KrutaConfig)ConfigurationManager.GetSection(KrutaConfigXml.ConfigSectionName);
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }
        
        #endregion

        public KrutaConfigSectionHandler()
        {
            
        }

        public KrutaConfigSectionHandler(KrutaConfig configInstance)
        {
            Instance = configInstance;
        }

        /// <summary>
        /// This method parses the configuration file and returns an object that represents
        /// the configuration structure
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns>KrutaConfig</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            // Exit if there are no configuration settings.
            var eConfiguration = new KrutaConfig();
            if (section != null && section.ChildNodes.Count > 0) //ChildNodes is never null
            {
                var parser = new KrutaConfigParser();
                foreach (XmlNode configNode in section.ChildNodes)
                    switch (configNode.Name)
                    {
                        case WorkspaceXml.Node:
                            eConfiguration.WorkspaceConfig = parser.WorkspaceFactory(configNode);
                            break;
                            //if more nodes are added to config root, add case(es) here
                        default:
                            //do nothing;
                            break;
                    }
            }
            return eConfiguration;
        }

        
    }
    
}
