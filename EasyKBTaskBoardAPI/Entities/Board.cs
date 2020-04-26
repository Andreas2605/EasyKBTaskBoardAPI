using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Entities
{
    public class Board
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public int AccountId { get; set; }

        public ICollection<Column> Columns { get; set; }
            = new List<Column>();
    }
}
