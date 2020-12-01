using System;
namespace CRM.ViewModels
{
    public class ContractTelecomVM
    {
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public Guid Id { get; set; }
        public String ContractNo { get; set; }
        public DateTime? DateSigned { get; set; }
        public int Status { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public Guid CustomerId { get; set; }

        // Người đại diện
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
       

        public string Note { get; set; }
        public string LocationBuild { get; set; }

    }
    
}
