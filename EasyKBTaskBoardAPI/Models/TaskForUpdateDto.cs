﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class TaskForUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public ICollection<AccountDto> Members { get; set; }
            = new List<AccountDto>();
    }
}
