using System.ComponentModel.DataAnnotations;

namespace TaskManagerPO
{
    public class TaskDescription
    {
        [Key]
        public int TaskDescriptionID { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
