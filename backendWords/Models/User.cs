using backendWords.MappingClasses;
using System;
using System.Collections.Generic;

namespace backendWords.Models
{
    public partial class User
    {
        public User()
        {
            Technos = new HashSet<Techno>();
            Tests = new HashSet<Test>();
            Words = new HashSet<Word>();
        }

        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordDigest { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? RememberToken { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Techno> Technos { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Word> Words { get; set; }
    }
}
