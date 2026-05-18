using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber
{
    public class Car
    {
        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("plateNumber")]
        public string PlateNumber { get; set; }

        [BsonElement("color")]
        public string Color { get; set; }
    }
}
