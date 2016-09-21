namespace LunchOrder.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchProvider")]
    public partial class LunchProvider
    {
        [Key]
        public int ProviderId { get; set; }

        [StringLength(100)]
        public string Provider { get; set; }

        public int FK_OfficeId { get; set; }

        public virtual LunchOffice LunchOffice { get; set; }
    }
}
