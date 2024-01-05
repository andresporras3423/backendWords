using backendWords.MappingClasses;
using System;
using System.Collections.Generic;

namespace backendWords.Models
{
    public partial class Test
    {
        public int? Correct { get; set; }
        public int? Total { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Id { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }

        public TestMap generateTestMap()
        {
            TestMap testMap_ = new TestMap();
            testMap_.correct = Correct;
            testMap_.total = Total;
            testMap_.id = Id;
            testMap_.user_id = UserId;
            return testMap_;
        }
    }
}
