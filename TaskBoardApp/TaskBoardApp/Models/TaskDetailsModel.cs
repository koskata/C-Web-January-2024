namespace TaskBoardApp.Models
{
    public class TaskDetailsModel : TaskViewModel
    {
        public string CreatedOn { get; set; } = null!;

        public string Board { get; set; } = null!;
    }
}
