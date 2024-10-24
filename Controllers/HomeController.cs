using demo_part2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace demo_part2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
            public IActionResult Index()
            {
                return View();
            }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult ViewClaims()
        {
            get_claims collect = new get_claims(); // Make sure this returns a view_claims model
            return View(collect); // Make sure collect is properly populated
        }


        // Other actions...

        [HttpPost]
        public IActionResult ClaimSub(IFormFile file, Claim insert) // Updated to Claim
        {
            // Assigning
            string moduleName = insert.UserEmail;
            string hourWork = insert.HoursWorked;
            string hourRate = insert.HourRate;
            string description = insert.Description;
            // File info
            string filename = "no file";
            if (file != null && file.Length > 0)
            {
                // Get the file name 
                filename = Path.GetFileName(file.FileName);

                // Define the folder path (pdf folder) 
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf");

                // Ensure the pdf folder exists 
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Define the full path where the file will be saved 
                string filePath = Path.Combine(folderPath, filename);

                // Save the file to the specified path 
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            string message = insert.InsertClaim(moduleName, hourWork, hourRate, description, filename); // Make sure method names also follow PascalCase

            if (message == "done")
            {
                Console.WriteLine(message);
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                Console.WriteLine(message);
                return RedirectToAction("Dashboard", "Home");
            }
        }

        // Other actions...
    }
}
