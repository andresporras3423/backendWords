using backendWords.MappingClasses;
using System;
using System.Collections.Generic;

namespace backendWords.Models
{
    public partial class Techno
    {
        public Techno()
        {
            Words = new HashSet<Word>();
        }

        public string? TechnoName { get; set; }
        public bool? TechnoStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Id { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Word> Words { get; set; }

        public TechnoMap generateTechnoMap()
        {
            TechnoMap techno_ = new TechnoMap();
            techno_.techno_name = TechnoName;
            techno_.techno_status = TechnoStatus;
            techno_.id = Id;
            return techno_;
        }
    }
}
