using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfTasks
        {
            get
            {
                return Tasks.Count;
            }
        }

        public ICollection<Task> Tasks { get; set; }
            = new List<Task>();
    }
}
