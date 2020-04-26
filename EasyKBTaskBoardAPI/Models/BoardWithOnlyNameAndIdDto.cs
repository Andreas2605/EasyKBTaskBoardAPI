using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class BoardWithOnlyNameAndIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
