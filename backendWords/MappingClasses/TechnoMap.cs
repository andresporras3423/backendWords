using backendWords.Models;

namespace backendWords.MappingClasses
{
    public class TechnoMap
    {
        public int? id { get; set; }
        public string? techno_name { get; set; }
        public bool? techno_status { get; set; }

        public Techno generateTechno()
        {
            Techno techno_ = new Techno();
            techno_.TechnoName = techno_name;
            techno_.TechnoStatus = techno_status;
            techno_.Id = Convert.ToInt32(id);
            return techno_;
        }
    }
}
