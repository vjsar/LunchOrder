namespace LunchOrder.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchLocation")]
    public partial class LunchLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LunchLocation()
        {
            LunchProfiles = new HashSet<LunchProfile>();
        }

        [Key]
        public int LocationId { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public int FK_OfficeId { get; set; }

        public virtual LunchOffice LunchOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LunchProfile> LunchProfiles { get; set; }
    }
}
