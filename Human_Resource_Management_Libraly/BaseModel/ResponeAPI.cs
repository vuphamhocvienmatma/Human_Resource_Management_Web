using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Libraly.BaseModel
{
    public class ResponeAPI<T>
    {
        public int Status { get; set; }
        public string Desc { get; set; }
        public T Data { get; set; }
    }
}
