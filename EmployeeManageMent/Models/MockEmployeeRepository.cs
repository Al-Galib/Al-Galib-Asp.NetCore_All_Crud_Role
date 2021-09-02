using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManageMent.Models
{
    public class MockEmployeeRepository : IEmployeeRefository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1,Name="Asad",Email="ag@gmail.com",Department=Dept.Account},
                 new Employee(){Id=2,Name="Ikbal",Email="ag@gmail.com",Department=Dept.HR},
                  new Employee(){Id=3,Name="Galib",Email="ag@gmail.com",Department=Dept.It}
            };
        }
        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(s => s.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public string EmployeeVM(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeList;
        }

        public Employee Update(Employee employeechange)
        {
            Employee employee = _employeeList.FirstOrDefault(s => s.Id == employeechange.Id);
            if (employee != null)
            {
                employee.Name = employeechange.Name;
                employee.Email = employeechange.Email;
                employee.Department = employeechange.Department;
            }
            return employee;
        }
    }
}
