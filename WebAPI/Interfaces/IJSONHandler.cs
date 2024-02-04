using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;

namespace WebAPI.Interfaces
{
    public interface IJSONHandler
    {
        Task<bool> Save(InputDTO input);
    }
}
