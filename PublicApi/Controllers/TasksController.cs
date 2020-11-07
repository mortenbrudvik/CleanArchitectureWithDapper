using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PublicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TasksController(ILogger<TasksController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> Get()
        {
            return await _taskRepository.GetAll();
        }

        [HttpPost]
        public async Task<TaskItem> PostTask(TaskItem task)
        {
            return await _taskRepository.CreateAsync(task);
        }
    }
}
