using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<TaskItem> Get()
        {
            return _taskRepository.GetAll();
        }
    }
}
