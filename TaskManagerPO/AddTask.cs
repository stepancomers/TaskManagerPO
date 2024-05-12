using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace TaskManagerPO
{
    internal class AddTask
    {
        string _name;
        string _lessonName;
        string _group;
        string _descriprionTask;
        int _taskID;
        public AddTask(string name, string lessonName, string group, string descriprionTask)
        {
            _name = name;
            _lessonName = lessonName;
            _group = group;
            _descriprionTask = descriprionTask;
        }

        public void AddTaskInDB()
        {
            using (var dbContext = new MyDBContext())
            {
                dbContext.Database.CreateIfNotExists();
                var tasks = dbContext.Tasks;
                bool recuringName = false;
                foreach (var task in tasks)
                {
                    if (task.Name == _name)
                        recuringName = true;
                }
                if (!recuringName)
                {
                    var newTask = new Task { Name = _name, DateAddTask = DateTime.Now, LessonName = _lessonName, Group = _group, IsReady = false };
                    dbContext.Tasks.Add(newTask);
                    dbContext.SaveChanges();
                    _taskID = newTask.TaskID;
                }
                else
                    MessageBox.Show("Одиннаковые имена у заданий не могут сущствовать");
            }
        }

        public void AddDescriprionTaskInDB()
        {
            using (var dbContext = new MyDBContext())
            {
                dbContext.Database.CreateIfNotExists();
                var taskDescriptions = dbContext.TaskDescriptions;
                var newTaskDescription = new TaskDescription { TaskId = _taskID, Name = _name, Description = _descriprionTask };
                dbContext.TaskDescriptions.Add(newTaskDescription);
                dbContext.SaveChanges();
                MessageBox.Show("successfully task add");
            }
        }
    }
}
