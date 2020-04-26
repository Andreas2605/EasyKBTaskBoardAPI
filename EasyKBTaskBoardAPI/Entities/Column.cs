using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Entities
{
    public class Column
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }
        public int BoardId { get; set; }

        public ICollection<Task> Tasks { get; set; } 
            = new List<Task>();
    }
}
