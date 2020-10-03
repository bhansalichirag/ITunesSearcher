using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ClickInfo
    {

        [Key]
        public int Id { get; set; }

        public string TrackName { get; set; }

        public string TrackURL { get; set; }

        public int count { get; set; }
    }
}
