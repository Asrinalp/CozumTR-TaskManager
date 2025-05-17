namespace TaskManager.DTOs
{
    public class TaskItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Period { get; set; } = string.Empty; // I will set Periods like Daily, Weekly, Monthly
    }
}
