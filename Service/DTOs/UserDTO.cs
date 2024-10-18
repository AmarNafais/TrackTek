using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class UpdateUserDTO:CreateUserDTO
    {
        public int Id { get; set; }
    }
}
