using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADDyourAD.Models
{
    public partial class Advertisement
    {
        public Advertisement()
        {
            Comment = new HashSet<Comment>();
        }

        public int IdAdvertisement { get; set; }
        [Required]
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int IdUser { get; set; }
        public int IdCategory { get; set; }
        public byte[] Image { get; set; }
       

        public Category IdCategoryNavigation { get; set; }
        public User IdUserNavigation { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}
