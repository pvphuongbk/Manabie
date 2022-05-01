using AutoMapper;
using Manabie.Togo.Core.Base;
using Manabie.Togo.Core.Bus;
using Manabie.Togo.Data.Dto;
using Manabie.Togo.Data.Entity;
using Manabie.Togo.Domain.Commands.UserTask.Create;
using Manabie.Togo.RedisRepository.Interface;
using Manabie.Togo.Service.Implememt.UserTask;
using System;
using System.Threading.Tasks;

namespace Manabie.Togo.Service.Interface.UserTask
{
	public class UserTaskService : IUserTaskService
	{
		private IUserTaskRepositoryRedis _userTaskRepositoryRedis;
		private readonly IMediatorHandler _bus;
		private readonly IMapper _mapper;
		public UserTaskService(IUserTaskRepositoryRedis userTaskRepositoryRedis, IMediatorHandler bus, IMapper mapper)
		{
			_userTaskRepositoryRedis = userTaskRepositoryRedis;
			_bus = bus;
			_mapper = mapper;
		}

		public async Task<CreateUserTaskResponse> Create(UserTaskDto item)
		{
			var userTaskEntity = _mapper.Map<UserTaskDto, UserTaskEntity>(item);
			var createCommand = new CreatedUserTaskCommand { UserTaskEntity = userTaskEntity };
			var result = await _bus.SendCommand<CreatedUserTaskCommand, CreateUserTaskResponse>(createCommand);
			return result;
		}

		public async Task<ResponseBase> GetAllTaskByDay(Guid userId, DateTime taskDate)
		{
			var dataAsync = Task.Run(delegate ()
			{
				ResponseBase response = new ResponseBase();
				response.Data = _userTaskRepositoryRedis.GetAllByDay(userId, taskDate);
				return response;
			});
			var result = await dataAsync;
			return result;
		}
	}
}
