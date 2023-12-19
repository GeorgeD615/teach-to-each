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

        //Account
        AccountAlreadyExists = 4,


        //Homework
        HomeworkNotCreated = 5,
        HomeworkNotUpdated = 6,

        //Rating
        RatingNotCreated = 7,

        OK = 200,
        InternalServerError = 500
    }
}
