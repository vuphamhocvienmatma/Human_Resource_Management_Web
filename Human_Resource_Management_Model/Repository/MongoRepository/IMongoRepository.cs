using Human_Resource_Management_Libraly.BaseModel;
using Human_Resource_Management_Model.MongoClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Model.Repository.MongoRepository
{
    public interface IMongoRepository<TDocument> where TDocument : IMongoDocument
    {
        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);
     
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
      
        Task<TDocument> FindByIdAsync(string id);

        Task<MessageReport> InsertOneAsync(TDocument document);

        Task<MessageReport> InsertManyAsync(ICollection<TDocument> documents);

        Task<MessageReport> ReplaceOneAsync(TDocument document);

        Task<MessageReport> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task<MessageReport> DeleteByIdAsync(string id);

        Task<MessageReport> DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);

        Task<long> CountAsync(TDocument document);
    }
}
