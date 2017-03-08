using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Common.Enum
{
    public enum QueueStatusEnum
    {
        New = 1,
        InProgress = 2,
        Completed = 3
    }

    public enum RolesEnum
    {
        Administrator,
        Login,
        CanAssign
    }
}