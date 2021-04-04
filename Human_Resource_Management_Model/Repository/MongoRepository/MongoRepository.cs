using Human_Resource_Management_Libraly.BaseModel;
using Human_Resource_Management_Model.MongoClass;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Model.Repository.MongoRepository
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
    where TDocument : IMongoDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository()
        {
            IMongoDatabase database = new MongoClient("mongodb://adminHR:anhlavu123@localhost:27017/?serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&authSource=HumanResources&authMechanism=SCRAM-SHA-256&3t.uriVersion=3&3t.connection.name=adminHR&3t.alwaysShowAuthDB=true&3t.alwaysShowDBFromUserRole=true").GetDatabase("HumanResources");
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return  _collection.AsQueryable();
        }

        public virtual  IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return  _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }
    
        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual async Task<TDocument> FindByIdAsync(string id)
        {
            return await Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual async Task<MessageReport> InsertOneAsync(TDocument document)
        {
            MessageReport result = new MessageReport(false, "Chưa được thực thi");

            try
            {
                await _collection.InsertOneAsync(document);
                result = new MessageReport(true, "Thêm mới thành công");
            }
            catch (Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", 
                    ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
                
            }
            return result;
        }

        public virtual async Task<MessageReport> InsertManyAsync(ICollection<TDocument> documents)
        {
            MessageReport result = new MessageReport(false, "Chưa được thực thi");
            try
            {
                await _collection.InsertManyAsync(documents);
                result = new MessageReport(true, "Thêm mới thành công");
            }
            catch (Exception ex)
            {

                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}",
                   ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            return result;
        }

        public virtual async Task<MessageReport> ReplaceOneAsync(TDocument document)
        {
            MessageReport result = new MessageReport(false, "Chưa được thực thi");
            try
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
                await _collection.FindOneAndReplaceAsync(filter, document);
                result = new MessageReport(true, "Thay bản ghi thành công");
            }
            catch (Exception ex)
            {

                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}",
                   ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            return result;
        }

        public async Task<MessageReport> DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            MessageReport result = new MessageReport(false, "Chưa được thực thi");
            try
            {
                await _collection.FindOneAndDeleteAsync(filterExpression);
                result = new MessageReport(true, "Xóa bản ghi thành công");
            }
            catch (Exception ex)
            {

                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}",
                  ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            return result;
        }

        public async Task<MessageReport> DeleteByIdAsync(string id)
        {
            MessageReport result = new MessageReport(false, "Chưa được thực thi");
            try
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                await _collection.FindOneAndDeleteAsync(filter);
                result = new MessageReport(true, "Xóa bản ghi thành công");
            }
            catch (Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}",
                  ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            return result;
        }

        public async Task<MessageReport> DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            MessageReport result = new MessageReport(false, "Chưa được thực thi");
            try
            {
                 await _collection.DeleteManyAsync(filterExpression);
                result = new MessageReport(true, "Xóa các bản ghi thành công");
            }
            catch (Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}",
                  ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }
            return result;         
        }

       
    }
}
