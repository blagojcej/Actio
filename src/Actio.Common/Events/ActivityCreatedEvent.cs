using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Events
{
    public class ActivityCreatedEvent : IAuthenticatedEvent
    {
        public Guid UserId { get; }

        public Guid Id { get; }

        public string Category { get; }

        public string ActivityName { get; }

        public string Description { get; }

        public DateTime CreatedAt { get; }

        protected ActivityCreatedEvent()
        {
            
        }

        public ActivityCreatedEvent(Guid id, Guid userId, string category, string activityName, string description, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Category = category;
            ActivityName = activityName;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
