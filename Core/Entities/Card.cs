﻿using System;
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
        public string Number { get; set; }
        public string Cvv { get; set; }


        //public Account? Account { get; set; }
        [ForeignKey("AccountId")]
        public Guid AccountId { get; set; }
        
        [ForeignKey("PersonId")]
        public Guid PersonId { get; set; }

    }
}
