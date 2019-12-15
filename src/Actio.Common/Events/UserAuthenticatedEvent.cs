using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    /// <summary>
    /// Other subscribers will know this Email is authenticated
    /// </summary>
    public class UserAuthenticatedEvent : IEvent
    {
        public string Email { get; set; }

        protected UserAuthenticatedEvent()
        {
            
        }

        public UserAuthenticatedEvent(string email)
        {
            Email = email;
        }
    }
}
