using Human_Resource_Management_Data.MongoRepository;
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
        private readonly IMongoRepository<HR_Employee> _HR_EmployeeRepository;
        public HR_EmployeeController(IMongoRepository<HR_Employee> HR_EmployeeRepository)
        {
            _HR_EmployeeRepository = HR_EmployeeRepository;
        }
        public IActionResult Index()
        {
            var list = _HR_EmployeeRepository.FilterBy(x => x.Id != null);
            return View(list);
        }
    }
}
