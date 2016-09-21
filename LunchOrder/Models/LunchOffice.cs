using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace LunchOrder.Models
{
    public class LunchOffice
    {
        public int OfficeId { get; set; }
        public string Office { get; set; }
        public string Country { get; set; }
        public List<LunchLocations> LunchLocations { get; set; }
        public List<LunchProviders> LunchProviders { get; set; }
        
    }

    public class LunchLocations
    {
        public int OfficeId { get; set; }
        public string Location { get; set; }
        public int LocationId { get; set; }
    }

    public class LunchProviders
    {
        public int OfficeId { get; set; }
        public string Provider { get; set; }
        public int ProviderId { get; set; }
    }
}