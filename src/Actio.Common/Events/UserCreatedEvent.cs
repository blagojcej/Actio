using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    /// <summary>
    /// We don't have password property because it's not a good idea to send password as an Event for the ServiceBus
    /// And any other services other that service responsible for creating user (CreatingUserCommand) is interested about password
    /// </summary>
    public class UserCreatedEvent : IEvent
    {
        public string Email { get; }

        public string Username { get; }

        /// <summary>
        /// Empty constructor for serializing this event
        /// </summary>
        protected UserCreatedEvent()
        {
            
        }

        public UserCreatedEvent(string email, string username)
        {
            Email = email;
            Username = username;
        }
    }
}
