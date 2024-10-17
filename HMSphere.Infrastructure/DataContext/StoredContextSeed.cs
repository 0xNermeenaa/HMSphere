using HMSphere.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HMSphere.Infrastructure.DataContext
{
    public class StoredContextSeed
    {
        //public static async Task SeedAsync(HmsContext context)
        //{
        //    if (!context.Departments.Any())
        //    {
        //        var departmentsData = File.ReadAllText("../HMSphere.Infrastructure/SeedData/Departments.json");
        //        var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);
        //        foreach (var item in departments)
        //        {
        //            context.Departments.Add(item);
        //        }
        //        await context.SaveChangesAsync();
        //    }



      
        //public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, HmsContext context)
        //{
        //    if (!userManager.Users.Any())
        //    {
        //        //create new users
        //        var user = new ApplicationUser
        //        {
        //            FirstName = "Mohamed",
        //            LastName = "Ali",
        //            Email = "mohdali30060@gmail.com",
        //            UserName = "",
        //            Address = ""
        //        };
        //        await userManager.CreateAsync(user, "Asd123@");
        //        await userManager.AddToRoleAsync(user, "Doctor");

        //        var doctor = new Doctor
        //        {
        //            Id=user.Id,
        //            Specialization = "General surgery",
        //            DepartmentId = 2,
        //        };
        //        await context.Doctors.AddAsync(doctor);
        //        await context.SaveChangesAsync();
        //    }

        //}
    }
}
