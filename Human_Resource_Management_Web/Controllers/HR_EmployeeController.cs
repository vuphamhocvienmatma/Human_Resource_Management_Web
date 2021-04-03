using Human_Resource_Management_Data.Repository;
using Human_Resource_Management_Model.HRM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web.Controllers
{
    public class HR_EmployeeController : Controller
    {
        private IHR_EmployeeRepository _HR_EmployeeRepository;
        public HR_EmployeeController(IHR_EmployeeRepository HR_EmployeeRepository)
        {
            _HR_EmployeeRepository = HR_EmployeeRepository;
        }

        public IActionResult Index()
        {       
            var list = _HR_EmployeeRepository.AsQueryable();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HR_Employee model)
        {
            _HR_EmployeeRepository.InsertOneAsync(model);
            return View();
        }
    }
}
