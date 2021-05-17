using System;
using System.Collections.Generic;

#nullable disable

namespace EchoBotDBbookingstatus.Models
{
    public partial class Bookingdetail
    {
        public string Bookingid { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public string Venue { get; set; }
        public string Status { get; set; }
    }
}
