using MongoDB.Bson;
using System;

namespace Human_Resource_Management_Model.MongoClass
{
    public abstract class MongoDocument : IMongoDocument
    {
        public Guid Id { get; set; }

       
    }
}