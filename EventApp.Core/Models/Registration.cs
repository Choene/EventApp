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
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UniqueReferenceNumber { get; set; }
    }

    public class RegistrationResult
    {
        public string UniqueReferenceNumber { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
