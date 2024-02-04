using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IJSONHandler
    {
        Task<bool> Save(InputDTO input);

        Task<List<Input>> GetAll();

        Task<Input> GetById(int id);

        Task<bool> DeleteById(int id);
    }
}
