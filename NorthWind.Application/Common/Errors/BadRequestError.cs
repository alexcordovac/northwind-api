using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Application.Common.Errors
{
    public class BadRequestError : Error
    {
        public BadRequestError(string message) : base(message)
        {
            
        }
    }
}
