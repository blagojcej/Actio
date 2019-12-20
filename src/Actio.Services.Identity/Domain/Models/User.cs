using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Services;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Username { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
            
        }

        public User(string email, string username)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ActioExcteption("empty_user_email", $"User email can not be empty.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ActioExcteption("empty_user_name", $"User name can not be empty.");
            }

            Id =Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Username = username;
            CreatedAt=DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioExcteption("empty_password", $"Password can not be empty.");
            }

            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.Equals(encrypter.GetHash(password, Salt));
    }
}
