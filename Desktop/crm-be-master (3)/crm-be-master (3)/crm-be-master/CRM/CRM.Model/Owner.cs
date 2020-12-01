using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
    /// <summary>
    ///  Nhập theo GP ĐKKD
    /// </summary>
    public class Owner
    {
        public const int DIFFERENT = 1;
        public const int THESAME = 0;
        public Owner()
        {
            Gender = 0;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public DateTime? Birthday { get; set; }
        [DisplayName("Quốc tịch")]
        public string Country { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public int Compare(object obj)
        {

            var owner1 = (Owner)this;
            var owner2 = (Owner)obj;
            int rs = THESAME;
            if (owner1.Name != owner2.Name) rs = DIFFERENT;
            if (owner1.CompanyCode != owner2.CompanyCode) rs = DIFFERENT;
            if (owner1.IssuingCompanyPlace != owner2.IssuingCompanyPlace) rs = DIFFERENT;
            if (owner1.IssuingCompanyDate != owner2.IssuingCompanyDate) rs = DIFFERENT;
            if (owner1.AddressMainTown != owner2.AddressMainTown) rs = DIFFERENT;
            if (owner1.LegalRepresentativePeople != owner2.LegalRepresentativePeople) rs = DIFFERENT;
            if (owner1.Position != owner2.Position) rs = DIFFERENT;
            if (owner1.Gender != owner2.Gender) rs = DIFFERENT;
            if (owner1.Birthday != owner2.Birthday) rs = DIFFERENT;
            if (owner1.Country != owner2.Country) rs = DIFFERENT;
            return rs;
        }
    }
}
