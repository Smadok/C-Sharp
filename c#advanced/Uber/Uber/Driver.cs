using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber
{
    public class Driver
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("car")]
        public Car Car { get; set; }

        [BsonElement("location")]
        public Location Location { get; set; }

        [BsonElement("isAvailable")]
        public bool IsAvailable { get; set; }

        [BsonElement("rating")]
        public double Rating { get; set; }
    }
}
