using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Male.Models
{
    public class PaymentMethod
    {
        [Key]
        public string id { set; get; } = Guid.NewGuid().ToString();

        public string? name { set; get; }
        public string? note { set; get; }
        public DateTime? createdAt { set; get; } = DateTime.Now;
        public DateTime? updateAt { set; get; } = DateTime.Now;
        public bool? isDelete { set; get; } = false;
        public bool? isActive { set; get; } = true;

    }
}