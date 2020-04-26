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
        public AccountDto Account { get; set; }

        public ICollection<ColumnDto> Columns { get; set; }
            = new List<ColumnDto>();
    }
}
