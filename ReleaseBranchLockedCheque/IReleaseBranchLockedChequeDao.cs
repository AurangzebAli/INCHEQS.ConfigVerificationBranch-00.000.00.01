using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INCHEQS.ConfigVerificationBranch.ReleaseBranchLockedCheque
{
    public interface IReleaseBranchLockedChequeDao
    {
        DataTable ListLockedCheque(string bankCode);
        void DeleteProcessUsingCheckbox(string dataProcess);
    }
}