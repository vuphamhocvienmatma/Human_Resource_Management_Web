using AutoMapper;
using Human_Resource_Management_Model.HRM;
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
        private readonly IMapper _mapper;
        public HRMController(IHR_AccountService HR_AccountService, IMapper mapper)
        {
            _HR_AccountService = HR_AccountService;
            _mapper = mapper;
        }

        public IActionResult Demo()
        {         
            return View();
        }

        public IActionResult Index()
        {          
            var list = _HR_AccountService.GetAll().Result;
            var mappedList = _mapper.Map<IEnumerable<HR_Account>, IEnumerable<HR_AccountViewModel>>(list.ToList());

            return Json(mappedList);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(HR_Account model)
        {
           var re = _HR_AccountService.Create(model,this.HttpContext);
           return View();
        }
    }
}
