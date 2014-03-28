namespace LitProRead.BusinessObjects
{
    using System;
    using System.Collections.Generic;

    public class StudentAssessmentGoalsBO
    {
        public Nullable<int> ID { get; set; }
        public Nullable<System.DateTime> AssessDate{ get; set; }
        public Nullable<System.DateTime> AssessNextReview{ get; set; }

        public string AssessRoal{ get; set; }
        public string AssessProgress { get; set; }
        public string AssessProof{ get; set; }
        public string AssessSkill{ get; set; }
        public string AssessFollowUp{ get; set; }

        public Nullable<System.DateTime> DateCreated{ get; set; }
        public Nullable<System.DateTime> DateModified{ get; set; }
        public string LastModifiedBy{ get; set; }
    }
}
