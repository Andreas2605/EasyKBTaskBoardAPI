﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class BoardForCreationDto
    {
        [Required]
        public string Name { get; set; }

        public ICollection<AccountDto> Members { get; set; }
            = new List<AccountDto>();
    }
}
