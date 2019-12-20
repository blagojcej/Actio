using System;
using System.Collections.Generic;
using System.Text;
using Actio.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Actio.API.Test.Unit.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void home_controller_get_should_return_string_content()
        {
            //Arange
            var controller = new HomeController();

            //Act
            var result = controller.Get();

            //Assert
            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            contentResult.Content.ShouldAllBeEquivalentTo("Hello from Actio API!");
        }
    }
}
