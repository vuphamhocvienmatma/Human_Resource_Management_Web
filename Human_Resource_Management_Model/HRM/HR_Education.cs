﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Human_Resource_Management_Model.HRM
{
    [BsonIgnoreExtraElements]
    public class HR_Education
    {
        [BsonId]
        public string Id { get; set; }
    }
}
