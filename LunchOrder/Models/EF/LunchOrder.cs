namespace LunchOrder.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchOrder")]
    public partial class LunchOrder
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Lunch { get; set; }

        public DateTime? DateOrdered { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime LunchDay { get; set; }

        public int FK_ProfileId { get; set; }

        public virtual LunchProfile LunchProfile { get; set; }
    }
}
