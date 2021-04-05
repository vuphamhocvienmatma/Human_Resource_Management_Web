using Human_Resource_Management_Service.HR.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class HRMController : Controller      
    {
        private IHR_AccountService _HR_AccountService;
        public HRMController(IHR_AccountService HR_AccountService)
        {
            _HR_AccountService = HR_AccountService;
        }

        public IActionResult Demo()
        {         
            return View();
        }

        public IActionResult Index()
        {
            var list = _HR_AccountService.GetAll().Result;
            return View(list);
        }
    }
}
