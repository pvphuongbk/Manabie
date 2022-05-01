using Manabie.Togo.RedisRepository.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Manabie.Togo.Domain.Events.UserTask.Create
{
    public class CreatedUserTaskEventHandler : INotificationHandler<CreatedUserTaskEvent>
    {
        private IUserTaskRepositoryRedis _userTaskRepositoryRedis;
        public CreatedUserTaskEventHandler(IUserTaskRepositoryRedis userTaskRepositoryRedis)
        {
            _userTaskRepositoryRedis = userTaskRepositoryRedis;
        }

        public Task Handle(CreatedUserTaskEvent notification, CancellationToken cancellationToken)
        {
            // Save to redis
            _userTaskRepositoryRedis.Save(notification.UserTaskEntity);
            return Task.FromResult(true);
        }
    }
}
