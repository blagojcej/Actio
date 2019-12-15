using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string categoryName, string name, string description,
            DateTime createdAt)
        {
            var activityCategory = _categoryRepository.GetAsync(categoryName);

            //if category doesn't exists
            if (activityCategory == null)
            {
                throw new ActioExcteption("category_not_found", $"Category: '{categoryName}' was not found");
            }

            var activity = new Activity(id, name, categoryName, description, userId, createdAt);

            await _activityRepository.AddAsync(activity);
        }
    }
}
