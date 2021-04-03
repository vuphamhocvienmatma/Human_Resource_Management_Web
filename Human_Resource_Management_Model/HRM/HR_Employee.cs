
using Human_Resource_Management_Model.MongoClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Model.HRM
{
    [BsonCollection("HR_Employee")]
    public class HR_Employee : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
