using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task RegisterAsync(string email, string password, string username)
        {
            var user = await _userRepository.GetUserAsync(email);
            //If user does not exists
            if (user != null)
            {
                throw new ActioExcteption("email_in_user", $"Email: {email} is already in use.");
            }

            user = new User(email, username);
            user.SetPassword(password, _encrypter);

            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserAsync(email);
            //If user exists
            if (user == null)
            {
                throw new ActioExcteption("invalid_credentials", $"Invalid credentials.");
            }

            //If password does not match
            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new ActioExcteption("invalid_credentials", $"Invalid credentials.");
            }
        }
    }
}
