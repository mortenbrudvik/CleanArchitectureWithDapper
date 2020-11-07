using System.Collections.Generic;

namespace Infrastructure
{
    public interface ITaskRepository
    {
        void Create(TaskItem taskItem);
        IEnumerable<TaskItem> GetAll();
    }
}