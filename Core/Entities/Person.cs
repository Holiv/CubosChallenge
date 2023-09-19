using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Person : BaseEntity
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(14)]
        public string Document { get; set; }
        public string Password { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
