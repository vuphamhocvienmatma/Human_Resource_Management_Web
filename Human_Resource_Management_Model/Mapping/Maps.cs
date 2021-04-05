using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Human_Resource_Management_Model.HRM;

namespace Human_Resource_Management_Model.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<HR_Account, HR_AccountViewModel>().ReverseMap();
        }
    }
}
