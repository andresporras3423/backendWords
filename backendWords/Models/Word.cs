using backendWords.MappingClasses;
using System;
using System.Collections.Generic;

namespace backendWords.Models
{
    public partial class Word
    {
        public string? Word1 { get; set; }
        public string? Translation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TechnoId { get; set; }

        public virtual Techno? Techno { get; set; }
        public virtual User? User { get; set; }

        public WordMap generateWordMap()
        {
            WordMap word_ = new WordMap();
            word_.word = Word1;
            word_.translation = Translation;
            word_.techno_id = TechnoId.ToString();
            word_.id = Id;
            return word_;
        }
    }
}
