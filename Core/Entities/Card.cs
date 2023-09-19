using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Card : BaseEntity
    {
        public string Type { get; set; }
        public long Number { get; set; }
        public int Cvv { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
        public Guid AccountId { get; set; }

    }
}
