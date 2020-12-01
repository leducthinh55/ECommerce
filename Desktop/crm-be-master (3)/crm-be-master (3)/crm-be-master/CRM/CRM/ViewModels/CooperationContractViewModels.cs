using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class CooperationContractVM
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public Guid ParnerId { get; set; }
        public DateTime DateSinged { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }

    public class CooperationContractVMDetail {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public Guid ParnerId { get; set; }
        public DateTime DateSinged { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }


    public class CooperationContractCM
    {
        public String Code { get; set; }
        public Guid ParnerId { get; set; }
        public DateTime DateSinged { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }


    public class CoContractTelServiceVM
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public int Percentage { get; set; }
        public string AppendixLink { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Note { get; set; }
        public bool IsClosed { get; set; }
        public Guid CooperationContractId { get; set; }
    }

    public class CoContractTelServiceDetailVM : CoContractTelServiceVM
    {

    }

    public class CoContractTelServiceUM
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public int Percentage { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Note { get; set; }
    }

    public class CoContractTelServiceCM
    {
        public Guid ServiceId { get; set; }
        public int Percentage { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Guid CooperationContractId { get; set; }
        public string Note { get; set; }
    }


}
