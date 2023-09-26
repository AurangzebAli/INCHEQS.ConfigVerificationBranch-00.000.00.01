//using INCHEQS.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using INCHEQS.DataAccessLayer;
using INCHEQS.Common;

namespace INCHEQS.ConfigVerificationBranch.ReleaseBranchLockedCheque
{
    public class ReleaseBranchLockedChequeDao : IReleaseBranchLockedChequeDao
    {
        private readonly ApplicationDbContext dbContext;
        public ReleaseBranchLockedChequeDao(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public DataTable ListLockedCheque(string bankCode)
        {
            DataTable ds = new DataTable();
            string stmt = "SELECT distinct inw.fldinwarditemid, inw.fldClearDate, inw.fldUIC, inw.fldChequeSerialNo, inw.fldAccountNumber, inw.fldAmount, usr.fldUserAbb FROM tblPendingInfo pinf LEFT JOIN view_items inw on pinf.fldInwardItemId=inw.fldInwardItemId LEFT JOIN tblUserMaster usr ON pinf.fldAssignedUserId = usr.fldUserId WHERE isnull(pinf.fldAssignedUserId,'') <> ''";

            //ds = dbContext.GetRecordsAsDataTable(stmt);
            ds = dbContext.GetRecordsAsDataTable(stmt);

            return ds;
        }

        public void DeleteProcessUsingCheckbox(string InwardItemId)
        {
            string[] aryText = InwardItemId.Split(',');
            //string stmt = "UPDATE tblInwardItemInfoStatus SET fldAssignedUserId = NULL WHERE fldAssignedUserId=@fldAssignedUserId";
            //dbContext.ExecuteNonQuery(stmt, new[] { new SqlParameter("@fldAssignedUserId", fldAssignedUserId) });

            if ((aryText.Length > 0))
            {
                string stmt = "UPDATE tblPendingInfo SET fldAssignedUserId = NULL WHERE fldInwardItemId in (" + DatabaseUtils.getParameterizedStatementFromArray(aryText) + ") ;";
                stmt = stmt + "Delete from tblBranchVerificationLock WHERE fldInwardItemId in (" + DatabaseUtils.getParameterizedStatementFromArray(aryText) + ") ;";
                dbContext.ExecuteNonQuery(stmt, DatabaseUtils.getSqlParametersFromArray(aryText).ToArray());

            }


        }
    }
}