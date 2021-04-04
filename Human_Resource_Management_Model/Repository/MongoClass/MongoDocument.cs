using MongoDB.Bson;
using System;

namespace Human_Resource_Management_Model.MongoClass
{
    public abstract class MongoDocument : IMongoDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}