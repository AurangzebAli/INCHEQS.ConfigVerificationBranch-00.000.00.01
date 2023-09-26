using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INCHEQS.ConfigVerificationBranch.BranchActivation
{
    public class BranchActivationModel
    {

        public DateTime fldKLCutOffTime { get; set; }
        // public string cutOffTimeHour { get; set; }
        //  public string cutOffTimeMin { get; set; }
        public string fldKLActivation { get; set; }
        public string fldBPCKLActivation { get; set; }


        public string fldActivation { get; set; }

        public string cutOffStartTimeHourDay1 { get; set; }
        public string cutOffStartTimeMinDay1 { get; set; }
        public string cutOffEndTimeHourDay1 { get; set; }
        public string cutOffEndTimeMinDay1 { get; set; }
        public string fldBranchDay1CutOffTime { get; set; }

        public string cutOffStartTimeHourDay2 { get; set; }
        public string cutOffStartTimeMinDay2 { get; set; }
        public string cutOffEndTimeHourDay2 { get; set; }
        public string cutOffEndTimeMinDay2 { get; set; }
        public string fldBranchDay2CutOffTime { get; set; }

        public string fldCenterActivation { get; set; }

        public string cutOffEndDateTimeDay2 { get; set; }
    }
}