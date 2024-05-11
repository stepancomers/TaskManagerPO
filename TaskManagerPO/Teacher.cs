using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerPO
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string TeacherUserName { get; set; }
        public string TeacherPassword { get; set; }
        public string TeacherFullName { get; set; }
        public string LessonName { get; set; }
        public byte[] ImageData { get; set; }
    }
}
