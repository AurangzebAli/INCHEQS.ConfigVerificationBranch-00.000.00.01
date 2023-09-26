using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INCHEQS.ConfigVerificationBranch.BranchActivation
{
    public interface IBranchActivationDao
    {
        BranchActivationModel GetCutOffTime(string clearDate);
        BranchActivationModel GetChequeActivation(string clearDate);
        void InsertCutOffTime(string activation, DateTime cutoffDay1StartTime, string currentUser);
        void InsertChequeActivation(FormCollection col, String currentUser, string BankCode);
        void DeleteCheckActivation();
        DataTable GetMessageList();
        void InsertNewMessage(string textMessage, String currentUser, string BankCode, string SpickCode);
        string GetMessage();
        List<string> Validate(FormCollection col);
    }
}