using backendWords.Models;

namespace backendWords.MappingClasses
{
    public class WordMap
    {
        public int? id { get; set; }
        public string? word { get; set; }
        public string? techno_id { get; set; }
        public string? translation { get; set; }

        public Word generateWord()
        {
            Word word_ = new Word();
            word_.Word1 = word;
            word_.Translation = translation;
            word_.TechnoId = Convert.ToInt32(techno_id);
            word_.Id = Convert.ToInt32(id);
            return word_;
        }
    }
}
