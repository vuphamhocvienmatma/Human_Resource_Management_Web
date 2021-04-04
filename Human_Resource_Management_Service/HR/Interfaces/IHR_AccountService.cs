using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Human_Resource_Management_Libraly.BaseModel;
using Human_Resource_Management_Model.HRM;
using Microsoft.AspNetCore.Http;

namespace Human_Resource_Management_Service.HR.Interfaces
{
    public interface IHR_AccountService
    {
        Task<TableModel> GetTablePaging(string key, int pageNumber, int pageSize,
            string sort, HttpContext HttpContext);

        Task<IEnumerable<HR_Account>> GetAll();

        Task<MessageReport> Create(HR_Account model, HttpContext context);

        Task<MessageReport> Update(HR_Account model, HttpContext context);

        Task<MessageReport> DeleteOne(string id, HttpContext context);

        Task<MessageReport> DeleteMany(List<string> ids, HttpContext context);
    }
}