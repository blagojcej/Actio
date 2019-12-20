using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Actio.API.Controllers;
using Actio.API.Repositories;
using Actio.Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using Xunit;

namespace Actio.API.Test.Unit.Controllers
{
    public class ActivitiesControllerTest
    {
        [Fact]
        public async void activities_controller_post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var activityRepositoryMock=new Mock<IActivityRepository>();
            var controller = new ActivitiesController(busClientMock.Object, activityRepositoryMock.Object);
            var userId = Guid.NewGuid();
            controller.ControllerContext=new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User=new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name, userId.ToString()) }, "test"))
                }
            };

            var command = new CreateActivityCommand()
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await controller.Post(command);
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            contentResult.Location.ShouldBeEquivalentTo($"activities/{command.Id}");
        }
    }
}
