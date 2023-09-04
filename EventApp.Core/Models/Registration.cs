using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Core.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
