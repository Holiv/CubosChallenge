using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Transaction : BaseEntity
    {
        public double Value { get; set; }
        public string Description { get; set; }

        [ForeignKey("AccountId")]
        public Guid AccountId { get; set; }

    }
}
