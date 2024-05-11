using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerPO
{
    public class Task
    {
        [Key]
        public int TaskID { get; set; }
        public string Name {  get; set; }
        public DateTime DateAddTask { get; set; }
        public string LessonName { get; set; }
        public string Group {  get; set; }
        public bool IsReady { get; set; }
    }
}
