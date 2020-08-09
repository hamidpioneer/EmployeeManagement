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

            ViewBag.PageTitle = "Details of Employee";

            ViewBag.Employee = model;

            ViewBag.EmployeeName = model.Name;

            return View(model);
        }
    }
}