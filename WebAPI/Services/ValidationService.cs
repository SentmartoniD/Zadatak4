using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ValidationService : IValidation
    {
        public async Task<bool> Validate(InputDTO input)
        {
            if (String.IsNullOrEmpty(input.FirstName) || String.IsNullOrEmpty(input.LastName) || String.IsNullOrEmpty(input.Telephone))
                return false;
            else
                return true;
        }
    }
}
