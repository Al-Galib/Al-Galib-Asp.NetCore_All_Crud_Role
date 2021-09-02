using EmployeeManageMent.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManageMent.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Name Cannot exced 20 char")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public Dept Department { get; set; }

        public IFormFile Picture { get; set; }
        public int SumProperty { get; set; }
    }
}
