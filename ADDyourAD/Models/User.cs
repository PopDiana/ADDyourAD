using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADDyourAD.Models
{
    public partial class User
    {
        public User()
        {
            Advertisement = new HashSet<Advertisement>();
            Comment = new HashSet<Comment>();
        }


        public int IdUser { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Advertisement> Advertisement { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}
