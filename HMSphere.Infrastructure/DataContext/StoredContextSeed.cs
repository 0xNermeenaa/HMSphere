using HMSphere.Domain.Entities;
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
        public static async Task SeedAsync(HmsContext context)
        {
            //if (!context.Doctors.Any())
            //{
            //    var doctorsData = File.ReadAllText(".json");
            //    var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorsData);
            //    foreach (var item in doctors)
            //    {
            //        context.Doctors.Add(item);
            //    }
            //    await context.SaveChangesAsync();
            //}



        }
        //    public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        //    {
        //        if (!userManager.Users.Any())
        //        {
        //            //create new users
        //            var user = new ApplicationUser
        //            {
        //                FirstName = "",
        //                LastName = "",
        //                Email = "",
        //                UserName = "",
        //                Address =""




        //            };
        //            await userManager.CreateAsync(user, "Em19@63");


        //    }

        //}
    }
}
