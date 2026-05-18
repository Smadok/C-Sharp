using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber
{
    [BsonIgnoreExtraElements]
    class Ride
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        [BsonElement("driverId")]
        public ObjectId DriverId { get; set; }

        [BsonElement("pickupLocation")]
        public BsonDocument PickupLocation { get; set; }

        [BsonElement("dropoffLocation")]
        public BsonDocument DropoffLocation { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("requestedAt")]
        public DateTime RequestedAt { get; set; }

        [BsonElement("completedAt")]
        public DateTime CompletedAt { get; set; }
    }
}
