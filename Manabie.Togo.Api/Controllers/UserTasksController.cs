using Manabie.Togo.Core.Base;
using Manabie.Togo.Data.Dto;
using Manabie.Togo.Domain.Commands.UserTask.Create;
using Manabie.Togo.Service.Implememt.UserTask;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Manabie.Togo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTasksController : ControllerBase
    {
        private IUserTaskService _userTaskService;
        public UserTasksController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [HttpPost]
        public async Task<ResponseBase> GetByDay([FromBody] GetUserTaskDto getUserTaskDto)
        {
            var users = await _userTaskService.GetAllTaskByDay(getUserTaskDto.UserId, getUserTaskDto.TaskDate);

            return users;
        }

        [HttpPost]
        public async Task<CreateUserTaskResponse> InsertUserTask([FromBody] UserTaskDto userTaskDto)
        {
            var users = await _userTaskService.Create(userTaskDto);

            return users;
        }
    }
}
