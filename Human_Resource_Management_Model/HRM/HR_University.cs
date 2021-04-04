using MongoDB.Bson.Serialization.Attributes;

namespace Human_Resource_Management_Model.HRM
{
    [BsonIgnoreExtraElements]
    public class HR_University
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}