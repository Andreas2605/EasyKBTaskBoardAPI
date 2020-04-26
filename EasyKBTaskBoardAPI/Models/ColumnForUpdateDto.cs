using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Models
{
    public class ColumnForUpdateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
