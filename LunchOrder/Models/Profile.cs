using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchOrder.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int FK_OfficeId { get; set; }

        public int FK_ProviderId { get; set; }

        public int FK_LocationId { get; set; }

        public string Fav1 { get; set; }

        public string Fav2 { get; set; }

        public string Fav3 { get; set; }

        public LunchLocations LunchLocation { get; set; }

        public  LunchOffice LunchOffice { get; set; }

        public List<Models.LunchToOrder> LunchToOrder { get; set; }

        public LunchProviders LunchProvider { get; set; }
    }
}