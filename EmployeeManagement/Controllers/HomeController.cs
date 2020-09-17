using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EmployeeManagement.Controllers
{

    //[Route("Home")]
    [Route("[controller]/[action]")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger logger;

        public HomeController(
            IEmployeeRepository employeeRepository,
            IWebHostEnvironment webHostEnvironment,
            ILogger<HomeController> logger
            )
        {
            _employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }



        //[Route("Home")]
        [Route("")]
        [Route("/")]
        [Route("~/Home")]
        [AllowAnonymous]
        public ViewResult Index()
        {
            return View(_employeeRepository.GetAllEmployees());
        }








        [Route("{id?}")]
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {

            //throw new Exception("Error in Details Page");
            logger.LogInformation("{{LogInformation}}");
            logger.LogWarning("{{LogWarning}}");
            logger.LogCritical("{{LogCritical}}");
            logger.LogDebug("{{LogDebug}}");
            logger.LogError("{{LogError}}");
            logger.LogTrace("{{LogTrace}}");

            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
            };
            return View(homeDetailsViewModel);
 
        }










        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);

            HomeEditViewModel homeEditViewModel = new HomeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(homeEditViewModel);
        }

        [HttpPost]
        public IActionResult Create(HomeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                uniqueFileName = ProcessUploadFile(model);
                #region Multiple Photo Uploads
                //if (model.Photos != null && model.Photos.Count > 0)
                //{
                //    foreach (IFormFile photo in model.Photos)
                //    {
                //        if(photo.ContentType == "image/jpeg"
                //            || photo.ContentType == "image/png"
                //            || photo.ContentType == "image/gif")
                //        {
                //            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //            photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //        }
                //    }
                //}
                #endregion
       
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", "Home", new { id = newEmployee.Id });

            }
            return View();
        }




        [HttpPost]
        public IActionResult Edit(HomeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    employee.PhotoPath = ProcessUploadFile(model);
                }

                _employeeRepository.Update(employee);
                return RedirectToAction("Details", "Home", new { id = employee.Id });
            }

            return View();
        }

        private string ProcessUploadFile(HomeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                if (model.Photo.ContentType == "image/jpeg"
                        || model.Photo.ContentType == "image/png"
                        || model.Photo.ContentType == "image/gif")
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
    }
}