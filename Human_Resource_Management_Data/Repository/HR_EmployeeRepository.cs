using Human_Resource_Management_Model.HRM;
using Human_Resource_Management_Model.MongoClass;
using Human_Resource_Management_Model.Repository.MongoRepository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Data.Repository
{
    public interface IHR_EmployeeRepository : IMongoRepository<HR_Employee>
    {

    }
   
    public class HR_EmployeeRepository : MongoRepository<HR_Employee> ,IHR_EmployeeRepository
    {

    }
}
