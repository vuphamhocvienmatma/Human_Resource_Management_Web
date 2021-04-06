using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Libraly.Helper
{
    public static class ImageHelper
    {
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        public static void CheckExsitsDirectory(string uploadPath)
        {
            if(!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        }
    }
}
