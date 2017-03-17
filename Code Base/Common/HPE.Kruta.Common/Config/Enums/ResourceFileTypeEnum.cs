using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Common.Config.Enums
{
    [DataContract]
    public enum ResourceFileTypeEnum
    {
        [EnumMember]
        [Description("Unset")]
        Unset = 0,
        [EnumMember]
        [Description("XML")]
        XML = 1,
        [EnumMember]
        [Description("JSON")]
        JSON = 2,
        [EnumMember]
        [Description("Binary")]
        BINARY = 3,

    }
}
