namespace LunchOrder.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lunch")]
    public partial class Lunch
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [Column("Lunch")]
        [StringLength(500)]
        public string Lunch1 { get; set; }

        [StringLength(255)]
        public string Exclusion { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(1)]
        public string Platter { get; set; }

        public DateTime? DateOrdered { get; set; }

        [StringLength(50)]
        public string Provider { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime LunchDay { get; set; }
    }
}
