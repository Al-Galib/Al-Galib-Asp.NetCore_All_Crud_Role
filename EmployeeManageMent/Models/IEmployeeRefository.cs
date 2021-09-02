using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManageMent.Models
{
    public interface IEmployeeRefository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees();
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        Employee Delete(int id);
        string EmployeeVM(int id);
        
    }
}
