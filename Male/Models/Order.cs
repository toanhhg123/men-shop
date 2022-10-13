using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Male.Models
{
    public class Order
    {
        [Key]
        public string id { set; get; } = Guid.NewGuid().ToString();
        public Account account { set; get; } = default!;
        public Product product { set; get; } = default!;

        public int Quantity { set; get; }

        public bool isConfirm { set; get; }

        public string? desc;
    }
}