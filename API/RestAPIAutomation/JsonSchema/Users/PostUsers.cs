using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.RestAPIAutomation.JsonSchema.Users
{
    public class PostUsers
    {
        public string? name { get; set; }
        public string? job { get; set; }
        public string? id { get; set; }
        public DateTime? createdAt { get; set; }

        public DateTime? updatedAt { get; set;}
    }
}
