using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class CreateUserRejectedEvent : IRejectedEvent
    {
        public string Reason { get; }

        public string Code { get; }

        public string Email { get; }

        private CreateUserRejectedEvent()
        {
            
        }

        public CreateUserRejectedEvent(string reason, string code, string email)
        {
            Reason = reason;
            Code = code;
            Email = email;
        }
    }
}
