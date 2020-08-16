using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id=1, Name="AAAA", Email="aaa@mail.com", Department=Dept.HR},
                new Employee() { Id=2, Name="BBBB", Email="bbb@mail.com", Department=Dept.Admin},
                new Employee() { Id=3, Name="CCCC", Email="ccc@mail.com", Department=Dept.IT},
                //new Employee() { Id=4, Name="DDDD", Email="ddd@mail.com", Department=Dept.Payroll}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(emp => emp.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee =_employeeList.FirstOrDefault(emp => emp.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(emp => emp.Id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee newEmployee = _employeeList.FirstOrDefault(emp => emp.Id == employeeChanges.Id);
            if(newEmployee != null)
            {
                newEmployee.Name = employeeChanges.Name;
                newEmployee.Department = employeeChanges.Department;
                newEmployee.Email = employeeChanges.Email;
            }
            return newEmployee;
        }
    }
}
