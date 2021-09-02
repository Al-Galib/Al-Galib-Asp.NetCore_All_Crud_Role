using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManageMent.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string  City { get; set; }
    }
}
