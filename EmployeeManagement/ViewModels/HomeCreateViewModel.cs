using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class HomeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name can nto exceed 50 characters!")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        //[DisplayName("Office Email")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }

        public List<IFormFile> Photos { get; set; }
    }
}
