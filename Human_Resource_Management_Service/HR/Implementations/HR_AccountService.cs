using Human_Resource_Management_Data.Repository;
using Human_Resource_Management_Libraly.BaseModel;
using Human_Resource_Management_Model.HRM;
using Human_Resource_Management_Service.HR.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Human_Resource_Management_Service.HR.Implementations
{
    public class HR_AccountService : IHR_AccountService
    {
        private readonly IHR_AccountRepository _HR_AccountRepository;
        public HR_AccountService(IHR_AccountRepository HR_AccountRepository)
        {
            _HR_AccountRepository = HR_AccountRepository;
        }

        public async Task<MessageReport> Create(HR_Account model, HttpContext context)
        {
            return await _HR_AccountRepository.InsertOneAsync(model);
        }

        public async Task<MessageReport> DeleteMany(List<string> ids, HttpContext context)
        {
            return await _HR_AccountRepository.DeleteManyAsync(l => ids.Contains(l.Id.ToString()));         
        }

        public async Task<MessageReport> DeleteOne(string id, HttpContext context)
        {
            return await _HR_AccountRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<HR_Account>> GetAll()
        {
            var list = _HR_AccountRepository.AsQueryable();

            return await Task.FromResult(list);
        }

        public async Task<TableModel> GetTablePaging(string key, int pageNumber, int pageSize, string sort, HttpContext HttpContext)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MessageReport> Update(HR_Account model, HttpContext context)
        {
            return await _HR_AccountRepository.ReplaceOneAsync(model);
        }
    }
}