using System;
using System.ComponentModel;

namespace CRM.ViewModels
{
    public class DeputyVM
    {
        public Guid Id { get; set; }
        [DisplayName("Tên")]
        public string Name { get; set; }
        [DisplayName("Chức danh")]
        public string Position { get; set; }
        [DisplayName("Giới tính")]
        public string Gender { get; set; }
        [DisplayName("Ngày sinh")]
        public DateTime? Birthday { get; set; }
        [DisplayName("Quốc tịch")]
        public string Country { get; set; }
        [DisplayName("SĐT")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
    public class DeputyUM : DeputyVM
    {

    }

    public class DeputyCML
    {
        [DisplayName("Tên")]
        public string Name { get; set; }
        [DisplayName("Chức danh")]
        public string Position { get; set; }
        [DisplayName("Giới tính")]
        public string Gender { get; set; }
        [DisplayName("Ngày sinh")]
        public DateTime? Birthday { get; set; }
        [DisplayName("Quốc tịch")]
        public string Country { get; set; }
        [DisplayName("SĐT")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
