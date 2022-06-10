using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movieAPI.Models
{
    public class movieEntitiy
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public string DirectorName { get; set; }
        public int DurationTime { get; set; }
    }
}