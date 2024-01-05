using backendWords.Models;

namespace backendWords.MappingClasses
{
    public class TestMap
    {
        public int? id { get; set; }
        public int? correct { get; set; }
        public int? total { get; set; }
        public int? user_id { get; set; }

        public Test generateTest()
        {
            Test test_ = new Test();
            test_.Id = id;
            test_.Correct = correct;
            test_.Total = total;
            test_.UserId = user_id == null ? -1 : (int)user_id;
            return test_;
        }
    }
}
