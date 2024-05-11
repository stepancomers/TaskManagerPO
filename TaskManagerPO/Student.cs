using System.ComponentModel.DataAnnotations;

namespace TaskManagerPO
{
    public class Student
    {
        [Key]
        public int StidentId { get; set; }
        public string StudentUserName { get; set; }
        public string StudentPassword { get; set; }
        public string StudentFullName { get; set; }
        public string StudentGroup { get; set; }
        public byte[] ImageData { get; set; }
    }
}
