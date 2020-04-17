using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class BoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
            = new List<Task>();
    }
}
