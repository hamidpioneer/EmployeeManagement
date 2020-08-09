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
                new Employee() { Id=1, Name="AAAA", Email="aaa@mail.com", Department="DepartAaa"},
                new Employee() { Id=2, Name="BBBB", Email="bbb@mail.com", Department="DepartBbb"},
                new Employee() { Id=3, Name="CCCC", Email="ccc@mail.com", Department="DepartCcc"},
                new Employee() { Id=4, Name="DDDD", Email="ddd@mail.com", Department="DepartDdd"}
            };
        }
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(emp => emp.Id == id);
        }
    }
}
