namespace LunchOrder.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchOffice")]
    public partial class LunchOffice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LunchOffice()
        {
            LunchLocations = new HashSet<LunchLocation>();
            LunchProviders = new HashSet<LunchProvider>();
            LunchProfiles = new HashSet<LunchProfile>();
        }

        [Required]
        [StringLength(100)]
        public string Office { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Key]
        public int OfficeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LunchLocation> LunchLocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LunchProvider> LunchProviders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LunchProfile> LunchProfiles { get; set; }
    }
}
