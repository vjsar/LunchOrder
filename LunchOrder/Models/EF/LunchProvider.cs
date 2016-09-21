namespace LunchOrder.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchProvider")]
    public partial class LunchProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LunchProvider()
        {
            LunchProfiles = new HashSet<LunchProfile>();
        }

        [Key]
        public int ProviderId { get; set; }

        [StringLength(100)]
        public string Provider { get; set; }

        public int FK_OfficeId { get; set; }

        public virtual LunchOffice LunchOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LunchProfile> LunchProfiles { get; set; }
    }
}
