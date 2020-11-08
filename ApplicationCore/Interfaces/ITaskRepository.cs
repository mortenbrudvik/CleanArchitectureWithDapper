using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateAsync(TaskItem taskItem);
        Task<IEnumerable<TaskItem>> GetAll();
    }
}