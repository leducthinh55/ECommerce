using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    public class ContactVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Note { get; set; }
        public string Functional { get; set; }
        public string Nation { get; set; }
        public int? Gender { get; set; }
    }

    public class ContactCM
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Note { get; set; }
        public string Functional { get; set; }
        public string Nation { get; set; }
        public int? Gender { get; set; }
    }

    public class ContactUM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Note { get; set; }
        public string Functional { get; set; }
        public string Nation { get; set; }
        public int? Gender { get; set; }
    }

    public class CallCenterContactVM : ContactVM
    {
        public CustomerDetailVM Customer { get; set; }
    }
    
    /// <summary>
    ///   This class with be convert data in QT.11 Ky hop dong, some Data can be null is convert to String
    /// </summary>
    public class ContractQT11
    {
        public Guid Id { get; set; }
        public string ContractNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public string Square { get; set; }
        public string UnitPrice { get; set; }
        public string UnitServicePrice { get; set; }
        public DateTime? StartDateRent { get; set; }
        public string StartDateService { get; set; }
        public DateTime? UpPriceDate { get; set; }
        public string LevelUpUnitPrice { get; set; }
        public string LevelUpUnitServicePrice { get; set; }

        //2
        public string Floor_2 { get; set; }
        public string Room_2 { get; set; }
        public string Square_2 { get; set; }
        public string UnitPrice_2 { get; set; }
        public string UnitServicePrice_2 { get; set; }
        public string StartDateRent_2 { get; set; }
        public string StartDateService_2 { get; set; }
        public string UpPriceDate_2 { get; set; }
        public string LevelUpUnitPrice_2 { get; set; }
        public string LevelUpUnitServicePrice_2 { get; set; }
        //3
        public string Floor_3 { get; set; }
        public string Room_3 { get; set; }
        public string Square_3 { get; set; }
        public string UnitPrice_3 { get; set; }
        public string UnitServicePrice_3 { get; set; }
        public string StartDateRent_3 { get; set; }
        public string StartDateService_3 { get; set; }
        public string UpPriceDate_3 { get; set; }
        public string LevelUpUnitPrice_3 { get; set; }
        public string LevelUpUnitServicePrice_3 { get; set; }
        //4
        public string Floor_4 { get; set; }
        public string Room_4 { get; set; }
        public string Square_4 { get; set; }
        public string UnitPrice_4 { get; set; }
        public string UnitServicePrice_4 { get; set; }
        public string StartDateRent_4 { get; set; }
        public string StartDateService_4 { get; set; }
        public string UpPriceDate_4 { get; set; }
        public string LevelUpUnitPrice_4 { get; set; }
        public string LevelUpUnitServicePrice_4 { get; set; }

    }

    public class ContractQT11Reduce
    {
        public Guid Id { get; set; }
        public string ContractNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Building { get; set; }

        public string Floor { get; set; }
        public string Room { get; set; }

        public string Floor_2 { get; set; }
        public string Room_2 { get; set; }

        public string Floor_3 { get; set; }
        public string Room_3 { get; set; }

        public string Floor_4 { get; set; }
        public string Room_4 { get; set; }
    }
}
