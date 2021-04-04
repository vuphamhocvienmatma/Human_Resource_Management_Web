using Human_Resource_Management_Libraly.Cryptography;
using Human_Resource_Management_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web.Controllers
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
           
                int iterations = 100000; // The number of times to encrypt the password - change this
                int saltByteSize = 64; // the salt size - change this
                int hashByteSize = 512; // the final hash - change this

                string password = "password"; // That's really secure! :)

                byte[] saltBytes = PBKDF2Provider.CreateSalt(saltByteSize);
                string saltString = Convert.ToBase64String(saltBytes);

                string pwdHash = PBKDF2Provider.PBKDF2_Keccak_GetHash(password, saltString, iterations, hashByteSize);
                
                bool isValid = PBKDF2Provider.ValidatePassword(password, saltBytes, iterations,
                    hashByteSize, Convert.FromBase64String(pwdHash));
         
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
