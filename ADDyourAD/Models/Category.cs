using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADDyourAD.Models
{
    public partial class Category
    {
        public Category()
        {
            Advertisement = new HashSet<Advertisement>();
        }

        public int IdCategory { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public ICollection<Advertisement> Advertisement { get; set; }
    }
}
