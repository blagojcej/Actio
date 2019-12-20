using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using Moq;
using Xunit;

namespace Actio.Services.Activities.Test.Unit.Services
{
    public class ActivityServiceTest
    {
        [Fact]
        public async Task activity_service_add_async_should_succeed()
        {
            //Arrange
            var category = "test";
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(x => x.GetAsync(category))
                .ReturnsAsync(new Category(category));
            var activityService = new ActivityService(activityRepositoryMock.Object, categoryRepositoryMock.Object);

            //Act
            var id = Guid.NewGuid();
            await activityService.AddAsync(id, Guid.NewGuid(), category, "activity", "description", DateTime.UtcNow);

            //Assert
            categoryRepositoryMock.Verify(x => x.GetAsync(category), Times.Once);
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}
