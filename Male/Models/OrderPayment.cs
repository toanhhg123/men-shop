using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Male.Models
{
    public class OrderPayment
    {
        [Key]
        public string id { set; get; } = Guid.NewGuid().ToString();

        public PaymentMethod? paymentMethod
        {
            set; get;
        }

        public Order? order { set; get; }

        public double total { set; get; }
        public string? note { set; get; }
        public bool isActive { set; get; } = true;

    }
}