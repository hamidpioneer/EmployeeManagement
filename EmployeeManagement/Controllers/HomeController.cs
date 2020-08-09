using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public string Index()
        {
            return _employeeRepository.GetEmployee(2).Name;
        }
        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(2);

            ViewData["PageTitle"] = "Details of Employee";

            ViewData["EmployeeId"] = model.Id;
            ViewData["EmployeeName"] = model.Name;
            ViewData["EmployeeEmail"] = model.Email;
            ViewData["EmployeeDepartment"] = model.Department;

            return View(model);
        }
    }
}