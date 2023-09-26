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

namespace INCHEQS.ConfigVerificationBranch.BranchActivation {
    public class BranchActivationDao : IBranchActivationDao {

        private readonly ApplicationDbContext dbContext;
        public BranchActivationDao(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public BranchActivationModel GetCutOffTime(string clearDate) {
            BranchActivationModel branchActivationModel = new BranchActivationModel();
            string stmt = "Select fldKLCutOffTime, fldKLTimeFrom,fldUpdateTimestamp,fldActivation from tblCutOffTime where datediff(d,fldKLCutOffTime,@fldKLCutOffTime) = 0";
            DataTable dt = dbContext.GetRecordsAsDataTable(stmt, new[] {
                //new SqlParameter("@fldBranchDate",DateUtils.GetCurrentDatetime()) DateUtils.formatDateToSql(clearDate)
                new SqlParameter("@fldKLCutOffTime",DateUtils.GetCurrentDatetime())
            });

            if (dt.Rows.Count > 0) {

                DataRow row = dt.Rows[0];
                DateTime cutOffTime = Convert.ToDateTime(row["fldKLCutOffTime"]);
                DateTime startCutOffTime = Convert.ToDateTime(row["fldKLTimeFrom"]);
                //DateTime cutOffStartTimeDay2 = Convert.ToDateTime(row["fldBranchDay2CutoffStartTime"]);
                //DateTime cutOffEndTimeDay2 = Convert.ToDateTime(row["fldBranchDay2CutoffEndTime"]);

                //DateTime BranchDay2Date = Convert.ToDateTime(row["fldBranchDay2Date"]);

                branchActivationModel.fldActivation = row["fldActivation"].ToString();
               // branchActivationModel.fldBranchDay2CutOffTime = StringUtils.Mid(BranchDate.ToString("dd-MM-yyyy"),0,10);

                branchActivationModel.startCutOffTimeHour = StringUtils.Mid(startCutOffTime.ToString("HH:mm"), 0, 2);
                branchActivationModel.startCutOffTimeMin = StringUtils.Mid(startCutOffTime.ToString("HH:mm"), 3, 2);

                branchActivationModel.fldKLCutOffTime = Convert.ToDateTime(row["fldKLCutOffTime"].ToString());
                branchActivationModel.cutOffTimeHour = StringUtils.Mid(cutOffTime.ToString("HH:mm"), 0, 2);

                //branchActivationModel.cutOffStartTimeHourDay2 = StringUtils.Mid(cutOffStartTimeDay2.ToString("HH:mm"), 0, 2);
                //branchActivationModel.cutOffStartTimeMinDay2 = StringUtils.Mid(cutOffStartTimeDay2.ToString("HH:mm"), 3, 2);

                //branchActivationModel.cutOffEndTimeHourDay2 = StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 0, 2);
                branchActivationModel.cutOffTimeMin = StringUtils.Mid(cutOffTime.ToString("HH:mm"), 3, 2);


                //branchActivationModel.cutOffEndDateTimeDay2 = StringUtils.Mid(BranchDay2Date.ToString("dd-MM-yyyy"), 0, 10) + " " + StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 0, 2) + ":" + StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 3, 2);
                //branchActivationModel.fldBranchDay2CutOffTime = clearDate;
            }
            return branchActivationModel;
        }


        public List<BranchActivationModel>  GetCutOffTimes(string clearDate)
        {
            
            BranchActivationModel branchActivationModel = new BranchActivationModel();
            List<BranchActivationModel> lstbranchActivationModel = new List<BranchActivationModel>();


            string stmt = "Select fldKLCutOffTime, fldKLTimeFrom,fldUpdateTimestamp,fldActivation from tblCutOffTime where datediff(d,fldKLCutOffTime,@fldKLCutOffTime) = 0";
            DataTable dt = dbContext.GetRecordsAsDataTable(stmt, new[] {
                //new SqlParameter("@fldBranchDate",DateUtils.GetCurrentDatetime()) DateUtils.formatDateToSql(clearDate)
                new SqlParameter("@fldKLCutOffTime",DateUtils.formatDateToSql(clearDate))
            });

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    DateTime cutOffTime = Convert.ToDateTime(row["fldKLCutOffTime"]);
                    DateTime startCutOffTime = Convert.ToDateTime(row["fldKLTimeFrom"]);
                    branchActivationModel = new BranchActivationModel();
                    //DateTime cutOffStartTimeDay2 = Convert.ToDateTime(row["fldBranchDay2CutoffStartTime"]);
                    //DateTime cutOffEndTimeDay2 = Convert.ToDateTime(row["fldBranchDay2CutoffEndTime"]);

                    //DateTime BranchDay2Date = Convert.ToDateTime(row["fldBranchDay2Date"]);

                    branchActivationModel.fldActivation = row["fldActivation"].ToString();
                    // branchActivationModel.fldBranchDay2CutOffTime = StringUtils.Mid(BranchDate.ToString("dd-MM-yyyy"),0,10);

                    branchActivationModel.startCutOffTimeHour = StringUtils.Mid(startCutOffTime.ToString("HH:mm"), 0, 2);
                    branchActivationModel.startCutOffTimeMin = StringUtils.Mid(startCutOffTime.ToString("HH:mm"), 3, 2);

                    branchActivationModel.fldKLCutOffTime = Convert.ToDateTime(row["fldKLCutOffTime"].ToString());
                    branchActivationModel.cutOffTimeHour = StringUtils.Mid(cutOffTime.ToString("HH:mm"), 0, 2);

                    //branchActivationModel.cutOffStartTimeHourDay2 = StringUtils.Mid(cutOffStartTimeDay2.ToString("HH:mm"), 0, 2);
                    //branchActivationModel.cutOffStartTimeMinDay2 = StringUtils.Mid(cutOffStartTimeDay2.ToString("HH:mm"), 3, 2);

                    //branchActivationModel.cutOffEndTimeHourDay2 = StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 0, 2);
                    branchActivationModel.cutOffTimeMin = StringUtils.Mid(cutOffTime.ToString("HH:mm"), 3, 2);


                    //branchActivationModel.cutOffEndDateTimeDay2 = StringUtils.Mid(BranchDay2Date.ToString("dd-MM-yyyy"), 0, 10) + " " + StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 0, 2) + ":" + StringUtils.Mid(cutOffEndTimeDay2.ToString("HH:mm"), 3, 2);
                    //branchActivationModel.fldBranchDay2CutOffTime = clearDate;
                    lstbranchActivationModel.Add(branchActivationModel);

                }
            }
            return lstbranchActivationModel;
        }

        public BranchActivationModel GetChequeActivation(string clearDate) {
            BranchActivationModel branchActivationModel = new BranchActivationModel();
            DataTable dt = new DataTable();
            string stmt = "Select * from tblChequeActivation where datediff(d,fldClearDate,@fldClearDate) = 0";
            dt = dbContext.GetRecordsAsDataTable(stmt, new[] { new SqlParameter("@fldClearDate", DateUtils.formatDateToSql(clearDate)) });

            if (dt.Rows.Count > 0) {
                DataRow row = dt.Rows[0];
                branchActivationModel.fldKLActivation = row["fldKLActivation"].ToString().Trim();
                branchActivationModel.fldBPCKLActivation = row["fldBPCKLActivation"].ToString().Trim();
                branchActivationModel.fldRVWKLActivation = row["fldRVWKLActivation"].ToString().Trim();
            }
            return branchActivationModel;
        }

        public DataTable GetChequesActivation(string clearDate)
        {
            DataTable dt = new DataTable();
            string stmt = "Select * from tblChequeActivation where datediff(d,fldClearDate,@fldClearDate) = 0";
            dt = dbContext.GetRecordsAsDataTable(stmt, new[] { new SqlParameter("@fldClearDate", DateUtils.formatDateToSql(clearDate)) });

            return dt;
        }

        public void DeleteCheckActivation() {
            string stmt = " DELETE FROM tblCutOffTime";
            dbContext.ExecuteNonQuery(stmt);

            string stmt2 = " DELETE FROM tblChequeActivation";
            dbContext.ExecuteNonQuery(stmt2);
        }

        public void InsertCutOffTime(string activation, string cutOffTimeFrom, string cutOffTime, string currentUser,string clearingType) {

            Dictionary<string, dynamic> sqlChequeActivation = new Dictionary<string, dynamic>();
            sqlChequeActivation.Add("fldActivation", activation);
            //sqlChequeActivation.Add("fldBranchDay2Date", cutOffDay2Date);
            sqlChequeActivation.Add("fldKLTimeFrom", cutOffTimeFrom);
            //sqlChequeActivation.Add("fldBranchDay2CutoffStartTime", cutoffDay2StartTime);
            sqlChequeActivation.Add("fldKLCutOffTime", cutOffTime);
            //sqlChequeActivation.Add("fldBranchDay2CutoffEndTime", cutoffDay2EndTime);
            sqlChequeActivation.Add("fldUpdateUserId", currentUser);
            sqlChequeActivation.Add("fldUpdateTimestamp", DateUtils.GetCurrentDatetime());
            sqlChequeActivation.Add("fldCreateUserId", currentUser);
            sqlChequeActivation.Add("fldCreateTimestamp", DateUtils.GetCurrentDatetime());
            sqlChequeActivation.Add("fldClearingType", clearingType);

            dbContext.ConstructAndExecuteInsertCommand("tblCutOffTime", sqlChequeActivation);

        }
        public void InsertChequeActivation(FormCollection col, String currentUser, string BankCode) {

            Dictionary<string, dynamic> sqlChequeActivation = new Dictionary<string, dynamic>();

            sqlChequeActivation.Add("fldKLActivation", col["ccuActivate"]);
            sqlChequeActivation.Add("fldBPCKLActivation", col["branchVerify"]);
            sqlChequeActivation.Add("fldUpdateUserID", currentUser);
            sqlChequeActivation.Add("fldUpdateTimeStamp", DateUtils.GetCurrentDatetime());
            sqlChequeActivation.Add("fldCreateUserId", currentUser);
            sqlChequeActivation.Add("fldCreateTimestamp", DateUtils.GetCurrentDatetime());
            sqlChequeActivation.Add("fldBankCode", BankCode);
            sqlChequeActivation.Add("fldClearDate", DateUtils.formatDateToSql(col["fldClearDate"]));
            sqlChequeActivation.Add("fldRVWKLActivation", col["reviewMineActivation"]);

            dbContext.ConstructAndExecuteInsertCommand("tblChequeActivation", sqlChequeActivation);

        }

        public DataTable GetMessageList() {
            string stmt = "select * from View_Message";
            return dbContext.GetRecordsAsDataTable(stmt);
        }

        public void InsertNewMessage(string textMessage, String currentUser,string BankCode,string SpickCode) {

            Dictionary<string, dynamic> sqlMessage = new Dictionary<string, dynamic>();

            sqlMessage.Add("fldMessage", textMessage);
            sqlMessage.Add("fldSpickCode", SpickCode);
            sqlMessage.Add("fldBranchCode", "");
            sqlMessage.Add("fldCreateUserID", currentUser);
            sqlMessage.Add("fldCreateTimeStamp", DateTime.Now);
            sqlMessage.Add("fldBankCode", BankCode);

            dbContext.ConstructAndExecuteInsertCommand("tblMessage", sqlMessage);
        }

        public string GetMessage() {
            DataTable ds = new DataTable();
            string result = "";

            string stmt = "Select top 1 fldMessage, fldCreateTimeStamp from tblMessage where datediff(d,fldCreateTimeStamp,getdate()) = 0 order by fldcreatetimestamp desc";

            ds = dbContext.GetRecordsAsDataTable(stmt);

            if (ds.Rows.Count > 0) {
                result = ds.Rows[0]["fldCreateTimeStamp"].ToString() + " : " + ds.Rows[0]["fldMessage"].ToString();
            }

            return result;
        }

        public List<string> Validate(FormCollection col) {
            List<string> err = new List<string>();

            if (col["txtMessage"].Equals("") && col["MessageOption"].Equals("")) {
                err.Add(Locale.PleaseKeyInMessage);

            }
            if (col["txtMessage"]!="" && col["MessageOption"]!="") {
                err.Add(Locale.PleaseSubmitOneMessage);
            }
           
            return err;
        }
    }
}