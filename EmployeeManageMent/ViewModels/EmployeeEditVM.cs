
using EmployeeManageMent.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManageMent.ViewModels
{
    public class EmployeeEditVM: EmployeeVM
    {
        //public int Id { get; set; }
        public String FilePath { get; set; }


    }
}
