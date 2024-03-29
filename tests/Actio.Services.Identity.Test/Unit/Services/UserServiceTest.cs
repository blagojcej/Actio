﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Actio.Services.Identity.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Actio.Services.Identity.Test.Unit.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "secret";
            var name = "test";
            var salt = "salt";
            var hash = "hash";
            var token = "token";

            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandlerMock=new Mock<IJWTHandler>();
            encrypterMock.Setup(x => x.GetSalt()).Returns(salt);
            encrypterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);
            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken()
            {
                Token = token
            });

            var user = new User(email, password);
            user.SetPassword(password, encrypterMock.Object);
            userRepositoryMock.Setup(x => x.GetUserAsync(email)).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, jwtHandlerMock.Object);

            var jwt = await userService.LoginAsync(email, password);
            userRepositoryMock.Verify(x=> x.GetUserAsync(email), Times.Once);
            jwtHandlerMock.Verify(x=>x.Create(It.IsAny<Guid>()), Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.ShouldBeEquivalentTo(token);
        }
    }
}
