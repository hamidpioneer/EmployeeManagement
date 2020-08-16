using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class EmployeeModelBuilderExtensions
    {
        public static void SeedEmployee(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "Aa", Email = "aa@mail.com", Department = Dept.Admin },
                    new Employee { Id = 2, Name = "Bb", Email = "bb@mail.com", Department = Dept.HR },
                    new Employee { Id = 3, Name = "Cc", Email = "cc@mail.com", Department = Dept.IT }
                );
        }
    }
}
