using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomService.Domain.Models.Requests
{
    public class AuthorizationRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
