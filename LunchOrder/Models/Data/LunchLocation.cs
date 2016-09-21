namespace LunchOrder.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchLocation")]
    public partial class LunchLocation
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        public int FK_OfficeId { get; set; }

        public virtual LunchOffice LunchOffice { get; set; }
    }
}
