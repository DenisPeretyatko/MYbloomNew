using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomService.Domain.Models.Responses
{
    public class AuthorizationResponse
    {
        public string Id;
        public AuthorizationType Type;
        public string Mail;
    }

    public enum AuthorizationType
    {
        Technician = 1,
        Manager = 2
    }
}
