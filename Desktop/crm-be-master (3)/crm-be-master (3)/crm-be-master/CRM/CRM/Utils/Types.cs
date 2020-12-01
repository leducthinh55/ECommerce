using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Utils
{
    public enum GVType
    {
        selectCustomer
    }

    public enum WorkflowType
    {
        nomral,doankhach,
        selectCustomer
    }
    

    public enum ContractStatus
    {
        PAYOFF = -1,
        CURRENT = 0,
    }

    public static class ErrorMess {
        public static string CREATE_CONTRACT = "Hệ thống không tạo được hợp đồng";
        public static string PAYOFF_CONTRACT = "Hệ thống không thể thanh lý hợp đồng";
        public static string INVALID_GLOBAL_VARIABLE = "Invalid Global Variable";
    }

    /// <summary>
    /// Type of ContractTelecom
    /// </summary>
    public enum TypeInvestment
    {
        KHONG,
        TAPTRUNG,
        DONVI
    }


}
