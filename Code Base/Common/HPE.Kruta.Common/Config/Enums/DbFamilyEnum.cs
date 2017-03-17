using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Common.Config.Enums
{
    public enum DbFamilyEnum
    {
        [Description("Mixed (Testing Purposes)")]
        Mixed = -1,
        [Description("Unset")]
        Unset = 0,
        [Description("Fake")]
        Fake = 1,
        [Description("Microsoft SQL Server")]
        SqlServer = 2,
        [Description("ORACLE")]
        ORACLE = 3,
        [Description("ODBC")]
        ODBC = 4,
        [Description("OLEDB")]
        OLEDB = 5,

    }
}
