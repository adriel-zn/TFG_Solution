using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG.WebApi.Models;

namespace TFG.WebApi.Interfaces
{
    public  interface IRepository
    {
        /* Authentication Interfaces */
        Tokens Authenticate(Users user);
    }
}
