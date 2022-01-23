using System;
using System.Collections.Generic;

namespace ADDyourAD.Models
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public string Text { get; set; }
        public int IdAdvertisement { get; set; }
        public int IdUser { get; set; }
        public DateTime? Date { get; set; }

        public Advertisement IdAdvertisementNavigation { get; set; }
        public User IdUserNavigation { get; set; }
    }
}
