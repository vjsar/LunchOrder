namespace LunchOrder.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LunchProfile")]
    public partial class LunchProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LunchProfile()
        {
            LunchOrders = new HashSet<LunchOrder>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public int FK_OfficeId { get; set; }

        public int FK_ProviderId { get; set; }

        public int FK_LocationId { get; set; }

        [StringLength(200)]
        public string Fav1 { get; set; }

        [StringLength(200)]
        public string Fav2 { get; set; }

        [StringLength(200)]
        public string Fav3 { get; set; }

        public virtual LunchLocation LunchLocation { get; set; }

        public virtual LunchOffice LunchOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LunchOrder> LunchOrders { get; set; }

        public virtual LunchProvider LunchProvider { get; set; }
    }
}
