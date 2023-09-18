using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Account : BaseEntity
    {
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();

        [ForeignKey("PersonId")]
        public Person? Person { get; set; }
        public Guid PersonId { get; set; }

    }
}
