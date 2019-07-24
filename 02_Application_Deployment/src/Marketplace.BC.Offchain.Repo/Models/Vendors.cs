using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Marketplace.BC.Offchain.Repo.Models
{
    public class Vendors : IEntityModel<ObjectId>
    {
        public ObjectId Id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }
}
