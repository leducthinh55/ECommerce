using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class Personnel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [DisplayName("Tổng cộng nhân viên")]
        public int TotalEmployee { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalEmployeeOutSide { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung")]
        public int TotalEmployeeInSide { get; set; }

        //[Giới tính]
        [DisplayName("Tổng nhân viên nam")]
        public int TotalMaleEmployee { get; set; }
        [DisplayName("Tổng nhân viên nam Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalMaleEmployeeOutside { get; set; }
        [DisplayName("Tổng nhân viên nam Nội khu Công viên phần mềm Quang Trung")]
        public int TotalMaleEmployeeInside { get; set; }
        
        [DisplayName("Tổng nhân viên nữ")]
        public int TotalFeMaleEmployee { get; set; }
        [DisplayName("Tổng nhân viên nữ Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalFeMaleEmployeeOutside { get; set; }
        [DisplayName("Tổng nhân viên nữ Nội khu Công viên phần mềm Quang Trung")]
        public int TotalFeMaleEmployeeInside { get; set; }

        //[Official]
        [DisplayName("Tổng nhân viên chính thức")]
        public int TotalOfficialEmployee { get; set; }
        [DisplayName("Tổng nhân viên  chính thức Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalOfficialEmployeeOutside { get; set; }
        [DisplayName("Tổng nhân viên  chính thức Nội khu Công viên phần mềm Quang Trung")]
        public int TotalOfficialEmployeeInside { get; set; }

        //[part time]
        [DisplayName("Tổng nhân viên PartTime")]
        public int TotalPartTimeEmployee { get; set; }
        [DisplayName("Tổng nhân viên PartTime Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalPartTimeEmployeeOutside { get; set; }
        [DisplayName("Tổng nhân viên PartTime Nội khu Công viên phần mềm Quang Trung")]
        public int TotalPartTimeEmployeeInside { get; set; }

        //[phần mềm]
        [DisplayName("Tổng nhân viên chuyên viên phẩn mềm")]
        public int TotalSoftwareEmployee { get; set; }
        [DisplayName("Tổng nhân viên chuyên viên phẩn mềm Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalSoftwareEmployeeOutside { get; set; }
        [DisplayName("Tổng nhân viên chuyên viên phẩn mềm Nội khu Công viên phần mềm Quang Trung")]
        public int TotalSoftwareEmployeeInside { get; set; }

        //[khác]
        [DisplayName("Tổng nhân viên khác")]
        public int TotalOtherEmployee { get; set; }
        [DisplayName("Tổng nhân viên khác Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalOtherEmployeeOutside { get; set; }
        [DisplayName("Tổng nhân viên khác Nội khu Công viên phần mềm Quang Trung")]
        public int TotalOtherEmployeeInside { get; set; }


        [DisplayName("Tổng nhân viên trình độ [quốc tế]")]
        public int TotalInternationalEmployee { get; set; }
        [DisplayName("Tổng nhân viên trình độ [trong nước]")]
        public int TotalDomesticEmployee { get; set; }

        //[trong nước]
        [DisplayName("[trong nước] Tổng nhân viên trình độ trong nước bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutside { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideProfessor { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Phó Giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideAssociateProfessor { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Tiến Sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideDoctor { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Tiến sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideMaster { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ đại học bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideUniversity { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Cao đẳng bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideCollege { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ trung cấp bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideIntermediate { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ khác bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeOutsideOther { get; set; }



        [DisplayName("[trong nước] Tổng nhân viên trình độ trong nước nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInside { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideProfessor { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Phó Giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideAssociateProfessor { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Tiến Sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideDoctor { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Tiến sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideMaster { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ đại học nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideUniversity { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ Cao đẳng nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideCollege { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ trung cấp nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideIntermediate { get; set; }
        [DisplayName("[trong nước] Tổng nhân viên trình độ khác nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticEmployeeInsideOther { get; set; }

        //[quốc tế]

        [DisplayName("[quốc tế] Tổng nhân viên trình độ trong nước bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutside { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Phó Giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideAssociateProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Tiến Sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideDoctor { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Tiến sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideMaster { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ đại học bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideUniversity { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Cao đẳng bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideCollege { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ trung cấp bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideIntermediate { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ khác bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeOutsideOrther { get; set; }



        [DisplayName("[quốc tế] Tổng nhân viên trình độ trong nước nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInside { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Phó Giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideAssociateProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Tiến Sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideDoctor { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Tiến sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideMaster { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ đại học nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideUniversity { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ Cao đẳng nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideCollege { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ trung cấp nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideIntermediate { get; set; }
        [DisplayName("[quốc tế] Tổng nhân viên trình độ khác nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalEmployeeInsideOrther { get; set; }


        //-------------------- GIANG VIEN

        [DisplayName("Tổng cộng giảng viên")]
        public int TotalLecturers { get; set; }
        [DisplayName("Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalLecturersOutside { get; set; }
        [DisplayName("Nội khu Công viên phần mềm Quang Trung")]
        public int TotalLecturersInside { get; set; }

        //[Giới tính]
        [DisplayName("Tổng giảng viên nam")]
        public int TotalMaleLecturers { get; set; }
        [DisplayName("Tổng giảng viên nam Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalMaleLecturersOutside { get; set; }
        [DisplayName("Tổng giảng viên nam Nội khu Công viên phần mềm Quang Trung")]
        public int TotalMaleLecturersInside { get; set; }

        [DisplayName("Tổng giảng viên nữ")]
        public int TotalFemaleLecturers { get; set; }
        [DisplayName("Tổng giảng viên nữ Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalFemaleLecturersOutside { get; set; }
        [DisplayName("Tổng giảng viên nữ Nội khu Công viên phần mềm Quang Trung")]
        public int TotalFemaleLecturersInside { get; set; }

        //[Official]
        [DisplayName("Tổng giảng viên chính thức")]
        public int TotalOfficialLecturers { get; set; }
        [DisplayName("Tổng giảng viên  chính thức Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalOfficialLecturersOutside { get; set; }
        [DisplayName("Tổng giảng viên  chính thức Nội khu Công viên phần mềm Quang Trung")]
        public int TotalOfficialLecturersInside { get; set; }

        //[part time]
        [DisplayName("Tổng giảng viên PartTime")]
        public int TotalPartTimeLecturers { get; set; }
        [DisplayName("Tổng giảng viên PartTime Bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalPartTimeLecturersOutside { get; set; }
        [DisplayName("Tổng giảng viên PartTime Nội khu Công viên phần mềm Quang Trung")]
        public int TotalPartTimeLecturersInside { get; set; }

        [DisplayName("Tổng giảng viên trình độ [quốc tế]")]
        public int TotalInternationalLecturers { get; set; }
        [DisplayName("Tổng giảng viên trình độ [trong nước]")]
        public int TotalDomesticLecturers { get; set; }

        //[trong nước]
        [DisplayName("[trong nước] Tổng giảng viên trình độ trong nước bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutside { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideProfessor { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Phó Giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideAssociateProfessor { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Tiến Sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideDoctor { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Tiến sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideMaster { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ đại học bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideUniversity { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Cao đẳng bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideCollege { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ trung cấp bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideIntermediate { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ khác bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersOutsideOrther { get; set; }



        [DisplayName("[trong nước] Tổng giảng viên trình độ trong nước nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInside { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideProfessor { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Phó Giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideAssociateProfessor { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Tiến Sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideDoctor { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Tiến sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideMaster { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ đại học nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideUniversity { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ Cao đẳng nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideCollege { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ trung cấp nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideIntermediate { get; set; }
        [DisplayName("[trong nước] Tổng giảng viên trình độ khác nội khu Công viên phần mềm Quang Trung")]
        public int TotalDomesticLecturersInsideOrther { get; set; }

        //[quốc tế]

        [DisplayName("[quốc tế] Tổng giảng viên trình độ trong nước bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutside { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Phó Giáo sư bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideAssociateProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Tiến Sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideDoctor { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Tiến sĩ bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideMaster { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ đại học bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideUniversity { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Cao đẳng bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideCollege { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ trung cấp bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideIntermediate { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ khác bên ngoài Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersOutsideOrther { get; set; }



        [DisplayName("[quốc tế] Tổng giảng viên trình độ trong nước nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInside { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Phó Giáo sư nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideAssociateProfessor { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Tiến Sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideDoctor { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Tiến sĩ nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideMaster { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ đại học nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideUniversity { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ Cao đẳng nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideCollege { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ trung cấp nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideIntermediate { get; set; }
        [DisplayName("[quốc tế] Tổng giảng viên trình độ khác nội khu Công viên phần mềm Quang Trung")]
        public int TotalInternationalLecturersInsideOrther { get; set; }

        //--------------- SINH VIÊN Alumnus
        [DisplayName("Tổng sinh viên")]
        public int TotalAlumnus { get; set; }
        [DisplayName("Tổng sinh viên bên ngoài")]
        public int TotalAlumnusOutside { get; set; }
        [DisplayName("Tổng sinh viên nội khu")]
        public int TotalAlumnusInside { get; set; }

        [DisplayName("Tổng sinh viên công nghệ")]
        public int TotalSoftwareAlumnus { get; set; }
        [DisplayName("Tổng sinh viên công nghệ bên ngoài")]
        public int TotalSoftwareAlumnusOutside { get; set; }
        [DisplayName("Tổng sinh viên công nghệ nội khu")]
        public int TotalSoftwareAlumnusInside { get; set; }

        [DisplayName("Tổng sinh viên khác")]
        public int TotalOtherAlumnus { get; set; }
        [DisplayName("Tổng sinh viên khác bên ngoài")]
        public int TotalOtherAlumnusOutside { get; set; }
        [DisplayName("Tổng sinh viên khác nội khu")]
        public int TotalOtherAlumnusInside { get; set; }

        //--------------- HỌC VIÊN
        [DisplayName("Tổng học viên")]
        public int TotalStudent { get; set; }
        [DisplayName("Tổng học viên bên ngoài")]
        public int TotalStudentOutside { get; set; }
        [DisplayName("Tổng học viên nội khu")]
        public int TotalStudentInside { get; set; }

        //1-1
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}
