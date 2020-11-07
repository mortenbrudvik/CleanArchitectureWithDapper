using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateAsync(TaskItem taskItem);
        Task<IEnumerable<TaskItem>> GetAll();
    }
}