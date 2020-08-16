using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAllEmployees();
        public Employee GetEmployee(int id);
        public Employee Add(Employee employee);
        public Employee Delete(int id);
        public Employee Update(Employee employeeChanges);
    }
}
