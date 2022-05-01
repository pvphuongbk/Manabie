using Manabie.Togo.Data.Entity;
using Manabie.Togo.RedisRepository.BaseDatabase;
using Manabie.Togo.RedisRepository.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manabie.Togo.RedisRepository.Implememt
{
    public class UserTaskRepositoryRedis : BaseRepositoryRedis<UserTaskEntity>, IUserTaskRepositoryRedis
    {
        public UserTaskRepositoryRedis(IConnectionMultiplexer redisConnection, IDatabase database) : base(redisConnection, database, "UserTaskDto") { }

        public override async Task SaveAsync(UserTaskEntity item)
        {
            var key = MakeUserTaskKey(item.UserId, item.TaskDate);
            await SaveByKeyAsync(key, item);
        }

        public async Task<IEnumerable<UserTaskEntity>> GetAllByDay(Guid userId, DateTime taskDate)
        {
            var key = MakeUserTaskKey(userId, taskDate);
            return await GetAllBykeyAsync(key);
        }

        private string MakeUserTaskKey(Guid userId, DateTime taskDate)
        {
            return $"{userId}:{taskDate.ToString("dd-MM-yyyy")}";
        }
    }
}
