using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachToEach.Domain.Enum
{
    public enum StatusCode
    {
        //User
        UserNotFound = 0,
        UserNotCreated = 1,
        UserNotRemoved = 2,
        UserNotUpdated = 3,

        OK = 200,
        InternalServerError = 500
    }
}
