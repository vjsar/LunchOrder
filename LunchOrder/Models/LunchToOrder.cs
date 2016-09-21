using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchOrder.Models
{
    public class LunchToOrder
    {
        public int Id { get; set; }

        public string LunchOrder { get; set; }

        public DateTime? DateOrdered { get; set; }

        public DateTime LunchDay { get; set; }

        public int FK_ProfileId { get; set; }
       
    }
}