using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Human_Resource_Management_Model.MongoClass
{
    public interface IMongoDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }      
    }
}