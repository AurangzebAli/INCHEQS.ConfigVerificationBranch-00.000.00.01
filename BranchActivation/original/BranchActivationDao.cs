//using INCHEQS.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using INCHEQS.DataAccessLayer;
using INCHEQS.Common;
using INCHEQS.ConfigVerficationBranch.Resource;

namespace INCHEQS.ConfigVerificationBranch.BranchActivation
{
    public class BranchActivationDao : IBranchActivationDao
    {

        private readonly ApplicationDbContext dbContext;
        public BranchActivationDao(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public BranchActivationModel GetCutOffTime(string clearDate)
        {
            BranchActivationModel branchActivationModel = new BranchActivationModel();

            string stmt = "Select fldBranchDay1Date,fldBranchDay2Date,fldBranchDay1CutoffStartTime,fldBranchDay2CutoffStartTime,fldBranchDay1CutoffEndTime,fldBranchDay2CutoffEndTime, fldUpdateTimestamp,fldActivation from tblCutOffTime";
            DataTable dt = dbContext.GetRecordsAsDataTable(stmt, new[] {
                new SqlParameter("@fldBranchDay1Date",DateUtils.GetCurrentDatetime())
            });

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                DateTime cutOffStartTimeDay1 = Convert.ToDateTime(row["fldBranchDay1CutoffStartTime"]);
                DateTime cutOffEndTimeDay1 = Convert.ToDateTime(row["fldBranchDay1CutoffEndTime"]);
                DateTime cutOffStartTimeDay2 = Convert.ToDateTime(row["fldBranchDay2CutoffStartTime"]);
                DateTime cutOffEndTimeDay2 = Convert.ToDateTime(row["fldBranchDay2CutoffEndTime"]);

                DateTime BranchDay2Date = Convert.ToDateTime(row["fldBranchDay2Date"]);
                DateTime BranchDay1Date = Convert.ToDateTime(row["fldBranchDay1Date"]);

                branchActivationModel.fldBranchDay1CutOffTime = StringUtils.Mid(BranchDay1Date.ToString("dd-MM-yyyy"), 0, 10);
                branchActivationModel.fldBranchDay2CutOffTime = StringUtils.Mid(BranchDay2Date.ToString("dd-MM-yyyy"), 0, 10);

                branchActivationModel.cutOffStartTimeHourDay1 = StringUtils.Mid(cutOffStartTimeDay1.ToString("HH:mm"), 0, 2);
                branchActivationModel.cutOffStartTimeMinDay1 = StringUtils.Mid(cutOffStartTimeDay1.ToString("HH:mm"), 3, 2);

                branchActivationModel.cutOffEndTimeHourDay1 = StringUtils.Mid(cutOffEndTimeDay1.ToString("HH:mm"), 0, 2);
                branchActivationModel.cutOffEndTimeMinDay1 = StringUtils.Mid(cutOffEndTimeDay1.ToString("HH:mm"), 3, 2);

                branchActivationModel.cutOffStartTimeHourDay2 = StringUtils.Mid(cutOffStartTimeDay2.ToString("HH:mm"), 0, 2);
                branchActivationModel.cutOffStartTimeMinDay2 = StringUtils.Mid(cutOffStartTimeDay2.ToString("HH:mm"), 3, 2);

                branchActivationModel.cutOffEndTimeHourDay2 = StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 0, 2);
                branchActivationModel.cutOffEndTimeMinDay2 = StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 3, 2);

                branchActivationModel.fldActivation = row["fldActivation"].ToString().Trim();

                branchActivationModel.cutOffEndDateTimeDay2 = StringUtils.Mid(BranchDay2Date.ToString("dd-MM-yyyy"), 0, 10) + " " + StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 0, 2) + ":" + StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 3, 2);
            }
            else
            {
                branchActivationModel.fldBranchDay1CutOffTime = DateTime.Now.ToString("dd-MM-yyyy");
                branchActivationModel.fldBranchDay2CutOffTime = clearDate;
            }
            return branchActivationModel;
        }

        public BranchActivationModel GetChequeActivation(string clearDate)
        {
            BranchActivationModel branchActivationModel = new BranchActivationModel();
            DataTable dt = new DataTable();
            string stmt = "Select * from tblChequeActivation where datediff(d,fldClearDate,@fldClearDate) = 0";
            dt = dbContext.GetRecordsAsDataTable(stmt, new[] { new SqlParameter("@fldClearDate", DateUtils.formatDateToSql(clearDate)) });

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                branchActivationModel.fldCenterActivation = row["fldCenterActivation"].ToString().Trim();
                //branchActivationModel.fldBPCKLActivation = row["fldBPCKLActivation"].ToString().Trim();
            }
            return branchActivationModel;
        }

        public void DeleteCheckActivation()
        {
            string stmt = " DELETE FROM tblCutOffTime";
            dbContext.ExecuteNonQuery(stmt);

            string stmt2 = " DELETE FROM tblChequeActivation";
            dbContext.ExecuteNonQuery(stmt2);
        }

        public void InsertCutOffTime(string activation, DateTime cutoffDay1StartTime, string currentUser)
        {

            Dictionary<string, dynamic> sqlChequeActivation = new Dictionary<string, dynamic>();
            sqlChequeActivation.Add("fldActivation", activation);
            //sqlChequeActivation.Add("fldBranchDay1Date", cutOffDay1Date);
            //sqlChequeActivation.Add("fldBranchDay2Date", cutOffDay2Date);
            sqlChequeActivation.Add("fldBranchDay1CutoffStartTime", cutoffDay1StartTime);
            //sqlChequeActivation.Add("fldBranchDay2CutoffStartTime", cutoffDay2StartTime);
           // sqlChequeActivation.Add("fldBranchDay1CutoffEndTime", cutoffDay1EndTime);
            //sqlChequeActivation.Add("fldBranchDay2CutoffEndTime", cutoffDay2EndTime);
            sqlChequeActivation.Add("fldUpdateUserId", currentUser);
            sqlChequeActivation.Add("fldUpdateTimestamp", DateUtils.GetCurrentDatetime());
            sqlChequeActivation.Add("fldCreateUserId", currentUser);
            sqlChequeActivation.Add("fldCreateTimestamp", DateUtils.GetCurrentDatetime());
            //sqlChequeActivation.Add("fldBankCode", BankCode);

            dbContext.ConstructAndExecuteInsertCommand("tblCutOffTime", sqlChequeActivation);

        }
        public void InsertChequeActivation(FormCollection col, String currentUser, string BankCode)
        {

            Dictionary<string, dynamic> sqlChequeActivation = new Dictionary<string, dynamic>();

            sqlChequeActivation.Add("fldCenterActivation", col["cpcActivate"]);
            sqlChequeActivation.Add("fldUpdateUserID", currentUser);
            sqlChequeActivation.Add("fldUpdateTimeStamp", DateUtils.GetCurrentDatetime());
            sqlChequeActivation.Add("fldCreateUserId", currentUser);
            sqlChequeActivation.Add("fldCreateTimestamp", DateUtils.GetCurrentDatetime());
            sqlChequeActivation.Add("fldBankCode", BankCode);
            sqlChequeActivation.Add("fldClearDate", DateUtils.formatDateToSql(col["fldClearDate"]));

            dbContext.ConstructAndExecuteInsertCommand("tblChequeActivation", sqlChequeActivation);

        }

        public DataTable GetMessageList()
        {
            string stmt = "select * from tblMessageListing";
            return dbContext.GetRecordsAsDataTable(stmt);
        }

        public void InsertNewMessage(string textMessage, String currentUser, string BankCode, string SpickCode)
        {

            Dictionary<string, dynamic> sqlMessage = new Dictionary<string, dynamic>();

            sqlMessage.Add("fldMessage", textMessage);
            sqlMessage.Add("fldSpickCode", SpickCode);
            sqlMessage.Add("fldBranchCode", "");
            sqlMessage.Add("fldCreateUserID", currentUser);
            sqlMessage.Add("fldCreateTimeStamp", DateTime.Now);
            sqlMessage.Add("fldBankCode", BankCode);

            dbContext.ConstructAndExecuteInsertCommand("tblMessage", sqlMessage);
        }

        public string GetMessage()
        {
            DataTable ds = new DataTable();
            string result = "";

            string stmt = "Select top 1 fldMessage, fldCreateTimeStamp from tblMessage where datediff(d,fldCreateTimeStamp,getdate()) = 0 order by fldcreatetimestamp desc";

            ds = dbContext.GetRecordsAsDataTable(stmt);

            if (ds.Rows.Count > 0)
            {
                result = ds.Rows[0]["fldCreateTimeStamp"].ToString() + " : " + ds.Rows[0]["fldMessage"].ToString();
            }

            return result;
        }

        public List<string> Validate(FormCollection col)
        {
            List<string> err = new List<string>();

            if (col["txtMessage"].Equals("") && col["MessageOption"].Equals(""))
            {
                err.Add(Locale.PleaseKeyInMessage);
            }
            if (col["txtMessage"] != "" && col["MessageOption"] != "")
            {
                err.Add(Locale.PleaseSubmitOneMessage);
            }
            return err;
        }
    }
}