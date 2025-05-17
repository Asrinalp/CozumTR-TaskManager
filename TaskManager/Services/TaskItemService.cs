using TaskManager.DTOs;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _repository;

        public TaskItemService(ITaskItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(t => new TaskItemDto
            {
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Period = t.Period
            });
        }

        public async Task<TaskItemDto?> GetByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskItemDto
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Period = task.Period
            };
        }

        public async Task<TaskItemDto> AddAsync(TaskItemDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Period = taskDto.Period
            };

            var created = await _repository.AddAsync(task);

            return new TaskItemDto
            {
                Title = created.Title,
                Description = created.Description,
                DueDate = created.DueDate,
                Period = created.Period
            };
        }

        public async Task<bool> UpdateAsync(int id, TaskItemDto taskDto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Title = taskDto.Title;
            existing.Description = taskDto.Description;
            existing.DueDate = taskDto.DueDate;
            existing.Period = taskDto.Period;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(existing);
            return true;
        }
    }
}
