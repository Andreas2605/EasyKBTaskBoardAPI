using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class TaskForCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public ICollection<AccountDto> Members { get; set; }
            = new List<AccountDto>();
    }
}
