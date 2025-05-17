using TaskManager.DTOs;

namespace TaskManager.Services
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItemDto>> GetAllAsync();
        Task<TaskItemDto?> GetByIdAsync(int id);
        Task<TaskItemDto> AddAsync(TaskItemDto taskDto);
        Task<bool> UpdateAsync(int id, TaskItemDto taskDto);
        Task<bool> DeleteAsync(int id);
    }
}
