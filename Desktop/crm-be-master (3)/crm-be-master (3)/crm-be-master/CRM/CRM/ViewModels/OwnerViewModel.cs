using System;
using System.ComponentModel;

namespace CRM.ViewModels
{
    public class OwnerVM
    {
        public Guid Id { get; set; }
        [DisplayName("Tên chủ sở hữu")]
        public string Name { get; set; }
        [DisplayName("Mã số doanh nghiệp")]
        public string CompanyCode { get; set; }
        [DisplayName("Nơi cấp")]
        public string IssuingCompanyPlace { get; set; }
        [DisplayName("Ngày cấp")]
        public string IssuingCompanyDate { get; set; }
        [DisplayName("Địa chỉ trụ sở chính")]
        public string AddressMainTown { get; set; }

        [DisplayName("Người đại diện pháp luật")]
        public string LegalRepresentativePeople { get; set; }
        [DisplayName("Chức danh")]
        public string Position { get; set; }
        [DisplayName("Giới tính")]
        public int Gender { get; set; }
        [DisplayName("Ngày sinh")]
        public string Birthday { get; set; }
        [DisplayName("Quốc tịch")]
        public string Country { get; set; }
    }
    public class OwnerCM
    {
        [DisplayName("Owner_Tên chủ sở hữu")]
        public string Name { get; set; }
        [DisplayName("Owner_Chức danh")]
        public string Position { get; set; }
    }

    public class OwnerCML
    {
        [DisplayName("Tên chủ sở hữu")]
        public string Name { get; set; }
        [DisplayName("Mã số doanh nghiệp")]
        public string CompanyCode { get; set; }
        [DisplayName("Nơi cấp")]
        public string IssuingCompanyPlace { get; set; }
        [DisplayName("Ngày cấp")]
        public string IssuingCompanyDate { get; set; }
        [DisplayName("Địa chỉ trụ sở chính")]
        public string AddressMainTown { get; set; }

        [DisplayName("Người đại diện pháp luật")]
        public string LegalRepresentativePeople { get; set; }
        [DisplayName("Chức danh")]
        public string Position { get; set; }
        [DisplayName("Giới tính")]
        public int Gender { get; set; }
        [DisplayName("Ngày sinh")]
        public string Birthday { get; set; }
        [DisplayName("Quốc tịch")]
        public string Country { get; set; }
    }

    public class OwnerUM : OwnerVM
    {
    }
}
