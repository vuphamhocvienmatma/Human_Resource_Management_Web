using AutoMapper;
using Human_Resource_Management_Libraly.Cryptography;
using Human_Resource_Management_Libraly.Helper;
using Human_Resource_Management_Model.HRM;
using Human_Resource_Management_Service.HR.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Human_Resource_Management_Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class HRMController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private IHR_AccountService _HR_AccountService;
        private readonly IMapper _mapper;
        public HRMController(IHR_AccountService HR_AccountService, IMapper mapper, IWebHostEnvironment environment)
        {
            _HR_AccountService = HR_AccountService;
            _mapper = mapper;
            hostingEnvironment = environment;
        }

        public IActionResult Demo()
        {         
            return View();
        }

        public IActionResult Index()
        {          
            var list = _HR_AccountService.GetAll().Result;
            var mappedList = _mapper.Map<IEnumerable<HR_Account>, IEnumerable<HR_AccountViewModel>>(list.ToList());
            
            return View(mappedList);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(HR_AccountEditModel model)
        {
           model.Id = Guid.NewGuid();
           model.Name = model.LastName + model.FirstName;
           model.PasswordSalt = Guid.NewGuid().ToString();
           model.Password = HashHelper.HashPassword(model.Password);

            if(model.AvatarImage != null)
            {
                var uniqueFileName = ImageHelper.GetUniqueFileName(model.AvatarImage.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                ImageHelper.CheckExsitsDirectory(uploads);
                var filePath = Path.Combine(uploads, uniqueFileName);
               
               
                model.AvatarImage.CopyTo(new FileStream(filePath, FileMode.Create));
                model.AvatarImagePath = filePath;
            }
            else
                model.AvatarImagePath = "~/Avatar/DefaultAvatar.jpg";
            var mappedModel = _mapper.Map<HR_AccountEditModel, HR_Account>(model);
           var re = _HR_AccountService.Create(mappedModel, this.HttpContext);
           return View();
        }

       
    }
}
