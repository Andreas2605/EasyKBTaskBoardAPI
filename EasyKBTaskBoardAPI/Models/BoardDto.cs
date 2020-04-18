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

        public ICollection<TaskDto> Tasks { get; set; }
            = new List<TaskDto>();

        public ICollection<AccountDto> Members { get; set; }
            = new List<AccountDto>();
    }
}
