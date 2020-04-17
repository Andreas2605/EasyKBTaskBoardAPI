using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Entities
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }
        public int BoardId { get; set; }

        public ICollection<Account> Members { get; set; }
            = new List<Account>();
    }
}
