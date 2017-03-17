using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Common.Enum
{
    /// <summary>
    /// This enum keys the available environments for the eServices platform
    /// </summary>
    [DataContract]
    public enum EnvironmentsEnum
    {
        [EnumMember]
        [Description("EnvironmentConfig Unset")]
        Unset = 0,
        [EnumMember]
        [Description("Development")]
        DEV = 1,
        [EnumMember]
        [Description("Quality Assurance")]
        QA = 2,
        [EnumMember]
        [Description("User Acceptance Testing")]
        UAT = 3,
        [EnumMember]
        [Description("AAMLive Production")]
        PROD = 4,
        [EnumMember]
        [Description("Staging")]
        STAGING = 5,
        [EnumMember]
        [Description("Integration")]
        INTEGRATION = 6,
        [EnumMember]
        [Description("Develepment Integration")]
        DEVINT = 8,
        [EnumMember]
        [Description("QA Integration")]
        QAINT = 9,
        [EnumMember]
        [Description("TIAA-CREF AT")]
        AT = 10,
        [EnumMember]
        [Description("TIAA-CREF IT1")]
        IT1 = 11,
        [EnumMember]
        [Description("Staging Integration")]
        STGINT = 12,
        [EnumMember]
        [Description("TIAA-CREF ST2")]
        ST2 = 13,
        [EnumMember]
        [Description("TIAA-CREF ST4")]
        ST4 = 14,
        [EnumMember]
        [Description("TIAA-CREF PRODFIX")]
        PRODFIX = 15,
        [EnumMember]
        [Description("TIAA-CREF Production")]
        TIAAPROD = 16
    }
}
