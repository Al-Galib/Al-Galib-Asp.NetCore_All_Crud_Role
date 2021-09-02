using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManageMent.Models
{
    public static  class ModelBulderExtensions
    {public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData
               (
                     new Employee
                     {
                         Id = 1,
                         Name = "Asad",
                         Department = Dept.Account,
                         Email = "asad@gmail.com"
                     },
                      new Employee
                      {
                          Id = 2,
                          Name = "Galib",
                          Department = Dept.HR,
                          Email = "galib@gmail.com"
                      },
                       new Employee
                       {
                           Id = 3,
                           Name = "Kamrul",
                           Department = Dept.SocialMeadia,
                           Email = "kamrul@gmail.com"
                       }
               );
        }
    }
}
