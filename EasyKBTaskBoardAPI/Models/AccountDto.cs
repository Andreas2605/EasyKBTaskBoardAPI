﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<BoardDto> Boards { get; set; }
            = new List<BoardDto>();
    }
}
