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
        // SAVE THE INPUT DATA AS A JSON OBJECT TO THE DISK
        Task<bool> Save(InputDTO input);

        // GET ALL THE JSON OBJECTS FROM THE JSONDb FILE
        Task<List<Input>> GetAll();

        // GET 1 JSON OBJECT BY ID
        Task<Input> GetById(int id);

        // DELETE A JSON OBJECT BY ID
        Task<bool> DeleteById(int id);
    }
}
